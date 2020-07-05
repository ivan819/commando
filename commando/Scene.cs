using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace commando
{
   
    public class Scene
    {
        // Moving objects list
        public List<Enemy> enemies;
        public List<Bullet> bullets;
        public List<PowerUp> powerups;
        public List<Explosion> explosions;
        public List<MovingObject> MarkedForDeletion;
        public Player player;

        // Score variables
        public long Score;
        public int Multiplier;
        public int level;

        // Timer for Powerup display message
        public Timer PowerUpMessageTimer;
        public bool showMessage;
        public string Message;

        // Timers for enemy and powerup spawns
        public Timer EnemySpawnTimer;
        public Timer PowerupSpawnTimer;
        public Timer Difficulity;



        public Random rand = new Random();

        public Scene()
        {
            this.enemies = new List<Enemy>();
            this.bullets = new List<Bullet>();
            this.powerups = new List<PowerUp>();
            this.explosions = new List<Explosion>();
            this.MarkedForDeletion = new List<MovingObject>();
            this.showMessage = false;
            this.Score = 0;
            this.Multiplier = 1;
            this.player = new Player();
            this.level = 1;

            this.PowerUpMessageTimer = new Timer();
            this.PowerUpMessageTimer.Interval = 3000;
            this.PowerUpMessageTimer.Tick += new EventHandler(this.PowerUpMessageTick);


            EnemySpawnTimer = new Timer();
            EnemySpawnTimer.Interval = 3000;
            EnemySpawnTimer.Tick += new System.EventHandler(this.ClockTickEnemySpawn);
            EnemySpawnTimer.Start();

            PowerupSpawnTimer = new Timer();
            PowerupSpawnTimer.Interval = 5000;
            PowerupSpawnTimer.Tick += new System.EventHandler(this.ClockTickPowerupSpawn);
            PowerupSpawnTimer.Start();

            Difficulity = new Timer();
            Difficulity.Interval = 60000;
            Difficulity.Tick += new System.EventHandler(this.DifficulityTick);
            Difficulity.Start();
        }

        // entry point on every tick
        public void DoAllTheWork(bool w, bool a, bool s, bool d, bool space, Graphics g)
        {
            DrawMoveShootAllObjects(g);
            DrawUI(g);
            CheckOutOfBounds(player);
            player.Move(w, a, s, d);
            player.Shoot(space, this);
            player.Draw(g);
        }

        // Drawing the hud
        public void DrawUI(Graphics g)
        {
            Brush HealthBoxBrush = new SolidBrush(Color.Green);
            Pen BoxOutlinePen = new Pen(Color.Black, 3);
            Brush BlackTextBrush = new SolidBrush(Color.Black);
            Brush MessageTextBrush = new SolidBrush(Color.Red);
            String message = "";
            Font font = new Font(FontFamily.GenericMonospace, 19, FontStyle.Bold);

            Rectangle rect;
            double x = (player.Health / (double)player.MaxHealth) * 10;

            // Health bar draw
            for (int i = 0; i < x; i++)
            {
                if (x <= 2) { HealthBoxBrush = new SolidBrush(Color.Red); }
                else if (x <= 4) { HealthBoxBrush = new SolidBrush(Color.Orange); }
                rect = new Rectangle(190 + 20 * i, 0, 20, 20);
                g.FillRectangle(HealthBoxBrush, rect);
            }

            // Black outline
            rect = new Rectangle(190, 0, 200, 20);
            g.DrawRectangle(BoxOutlinePen, rect);

            // Draw health and score
            message = "HP: " + player.Health + "/" + player.MaxHealth;
            g.DrawString(message, font, BlackTextBrush, 0, 0);
            message = "SCORE: " + this.Score;
            g.DrawString(message, font, BlackTextBrush, 400, 0);
            message = "LEVEL: " + this.level;
            g.DrawString(message, font, BlackTextBrush, 225, 23);

            // Draw stats
            font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
            message = "_____________________\nDAMAGE: " + player.Damage + "\nSPEED: " + player.SpeedX + "\nSHOOTRATE: " + player.ShootRate;
            g.DrawString(message, font, BlackTextBrush, 0, 10);

            // Draw powerup message if powerup is picked up
            if (this.showMessage)
            {
                g.DrawString(Message, font, MessageTextBrush, 0, 70);
            }

            // multiplier 
            message = "_____________________\nMULTIPLIER: ";
            g.DrawString(message, font, BlackTextBrush, 400, 10);
            message = "x" + this.Multiplier;
            font = new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold);
            g.DrawString(message, font, new SolidBrush(Color.Black), 500, 20);
        }

        private void DrawMoveShootAllObjects(Graphics g)
        {
            // Draw powerups and check collisions with player
            foreach (PowerUp powerup in powerups)
            {
                powerup.Draw(g);
                if (player.IsCollidingWith(powerup))
                {
                    PowerUp(powerup.powerType);
                    MarkForDeletion(powerup);
                }
            }

            // Draw explosions
            foreach (Explosion exp in explosions)
            {
                exp.Draw(g);
               
            }

            // Draw all enemies and check collisions with player
            foreach (Enemy enemy in enemies)
            {
                CheckOutOfBounds(enemy);
                enemy.Move();
                enemy.Shoot(this);
                enemy.Draw(g);
                if (player.IsCollidingWith(enemy))
                {
                    // Enemy dies instantly
                    this.explosions.Add(new Explosion(enemy.X,enemy.Y,this));
                    MarkForDeletion(enemy);
                    // if not invincible take dmg and reset score multiplier
                    if (!player.Invincible)
                    {
                        player.Health -= enemy.Damage;
                        this.Multiplier = 1;
                    }
                    // Player death
                    if (player.Health <= 0)
                    {
                        player.Kill();
                    }

                }
            }

            //Draw all bullets and check collisions with player and enemies
            foreach (Bullet bullet in bullets)
            {
                CheckOutOfBounds(bullet);
                bullet.Move();
                bullet.Draw(g);

                if (player.IsCollidingWith(bullet))
                {
                    // if not invincible take dmg and reset score multiplier
                    if (!player.Invincible)
                    {
                        player.Health -= bullet.Damage;
                        this.Multiplier = 1;
                    }
                    if (player.Health <= 0)
                    {
                        player.Kill();
                    }
                    MarkForDeletion(bullet);
                }

                // Every bullet with every enemy collision check
                foreach (Enemy enemy in enemies)
                {
                    if (bullet.IsCollidingWith(enemy))
                    {
                        // bullet dies instantly
                        MarkForDeletion(bullet);
                        enemy.Health -= bullet.Damage;
                        if (enemy.Health <= 0)
                        {
                            // if enemy is killed increase score
                            this.explosions.Add(new Explosion(enemy.X, enemy.Y, this));

                            MarkForDeletion(enemy);

                            this.Score += enemy.Score * this.Multiplier;
                            this.Multiplier++;
                        }
                    }
                }

            }
            DeleteMarkedObjects();
        }
        // Power up hits player
        public void PowerUp(String power)
        {
            this.showMessage = true;
            if (power.Equals("6"))
            {
                // limit some of the stats
                if(this.player.SpeedX >= 15)
                {
                    this.player.ShootRate -= 50;
                    this.player.SpeedX += 1;
                    this.player.SpeedY += 1;
                    this.player.BulletSpeed += 1;
                }
                this.player.Damage += 7;
                this.player.MaxHealth += 10;
                this.player.Health += 10;
               
                
                Message = "+STATS";
                this.player.ShootRateTimer.Stop();
                this.player.ShootRateTimer.Interval=this.player.ShootRate;
                this.player.ShootRateTimer.Start();


            }
            else if (power.Equals("4"))
            {
                this.player.Multishot = true;
                player.MultishotTimer.Start();
                Message = "MULTISHOT";
            }
            else if (power.Equals("5"))
            {
                this.player.Instakill = true;

                player.InstakillTimer.Start();
                Message = "INSTAKILL";
            }
            else if (power.Equals("2"))
            {
                this.player.Health += 100;
                if (this.player.Health > this.player.MaxHealth)
                {
                    this.player.Health = this.player.MaxHealth;
                }

                Message = "HEAL";
            }
            else if (power.Equals("1"))
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.SpeedY = 0;
                    enemy.ShootChance = 0;
                }
                Message = "FREEZE";
            }
            else if (power.Equals("3"))
            {
                this.player.Invincible = true;
                player.InvincibleTimer.Start();
                Message = "INVINCIBLE";
            }
            PowerUpMessageTimer.Start();
        }



        // OOB Check for moving objects
        public void CheckOutOfBounds(MovingObject mo)
        {
            if (mo is Player)
            {
                if (mo.Y < 0)
                {
                    mo.Y = 0;
                }
                if (mo.X < 0)
                {
                    mo.X = 0;
                }
                if (mo.Y + mo.Height > Utils.FORM_HEIGHT)
                {
                    mo.Y = Utils.FORM_HEIGHT - mo.Height;
                }
                if (mo.X + mo.Width > Utils.FORM_WIDTH)
                {
                    mo.X = Utils.FORM_WIDTH - mo.Width;
                }
            }
            else
            {
                if (mo.Y < -50 || mo.X < -10 || mo.Y + mo.Height > Utils.FORM_HEIGHT + 50 || mo.X + mo.Width > Utils.FORM_WIDTH + 10)
                {
                    MarkForDeletion(mo);
                }
            }
        }

        // Functions for deletion
        // marking becouse cannot delete when going thru for loop list
        public void MarkForDeletion(MovingObject mo)
        {
            MarkedForDeletion.Add(mo);
        }

        public void DeleteMarkedObjects()
        {
            foreach (MovingObject mo in MarkedForDeletion)
            {
                if (mo is Enemy)
                {
                    this.Remove((Enemy)mo);
                }
                else if (mo is Bullet)
                {
                    this.Remove((Bullet)mo);
                }
                else if (mo is PowerUp)
                {
                    this.Remove((PowerUp)mo);
                }
                else if (mo is Explosion)
                {
                    this.explosions.Remove((Explosion)mo);
                }

            }
            MarkedForDeletion.Clear();
        }


        // Message Tick
        public void PowerUpMessageTick(object sender, EventArgs e)
        {
            this.showMessage = false;
            PowerUpMessageTimer.Stop();
        }

        // Add/Remove MovingObjects
        public void Add(Enemy f)
        {
            enemies.Add(f);
        }
        public void Add(PowerUp f)
        {
            powerups.Add(f);
        }
        public void Add(Bullet f)
        {
            bullets.Add(f);
        }
        public void Remove(Enemy f)
        {
            enemies.Remove(f);
        }
        public void Remove(PowerUp f)
        {
            powerups.Remove(f);
        }
        public void Remove(Bullet f)
        {
            bullets.Remove(f);
        }

        void ClockTickEnemySpawn(object sender, EventArgs e)
        {
            EnemyType enemy;
            int x = rand.Next(0, 10);
            if (x == 0)
            {
                enemy = Utils.mine;
            }
            else if (x <= 4)
            {
                enemy = Utils.homing;
            }
            else
            {
                enemy = Utils.normal;
            }
            enemies.Add(new Enemy(enemy,level));
        }

        void ClockTickPowerupSpawn(object sender, EventArgs e)
        {
            if (this.powerups.Count <= 2)
            {
                this.Add(new PowerUp(rand.Next(1, 7).ToString()));
            }

        }
        void DifficulityTick(object sender, EventArgs e)
        {
            this.level++;
            if (EnemySpawnTimer.Interval >= 400)
            {
                EnemySpawnTimer.Stop();
                EnemySpawnTimer.Interval -= 300;
                EnemySpawnTimer.Start();
            }
            
        }

        
    }
}
