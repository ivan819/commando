using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Scene
    {
        public List<Enemy> enemies;
        public List<Bullet> bullets;
        public List<PowerUp> powerups;
        public Timer Instakill;
        public Timer Invincible;
        public Timer Multishot;
        public Timer PowerUpMessage;
        public long Score;
        public int Multiplier;
        public bool showMessage;
        public string Message;

        public Random rand = new Random();
        public Player player { get; set; }

        public Scene(Player player)
        {
            this.enemies = new List<Enemy>();
            this.bullets = new List<Bullet>();
            this.powerups = new List<PowerUp>();
            this.showMessage = false;
            this.Score = 0;
            this.Multiplier = 1;
            this.player = player;
            this.Instakill = new Timer();
            this.Instakill.Interval = 10000;
            this.Instakill.Tick += new EventHandler(this.InstakillTick);
            this.Invincible = new Timer();
            this.Invincible.Interval = 10000;
            this.Invincible.Tick += new EventHandler(this.InvincibleTick);
            this.Multishot = new Timer();
            this.Multishot.Interval = 10000;
            this.Multishot.Tick += new EventHandler(this.MultishotTick);
            this.PowerUpMessage = new Timer();
            this.PowerUpMessage.Interval = 3000;
            this.PowerUpMessage.Tick += new EventHandler(this.PowerUpMessageTick);
        }
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

        public void PowerUp(String power)
        {
            this.showMessage = true;
            if (power.Equals("6"))
            {
                this.player.Damage += 5;
                this.player.ShootRate -= 50;
                this.player.SpeedX += 1;
                this.player.MaxHealth += 10;
                this.player.Health += 10;
                this.player.SpeedY += 1;
                Message = "+STATS";

            }
            else if (power.Equals("4"))
            {
                this.player.Multishot = true;
                Multishot.Start();
                Message = "MULTISHOT";
            }
            else if (power.Equals("5"))
            {
                this.player.Instakill = true;

                Instakill.Start();
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
                Invincible.Start();
                Message = "INVINCIBLE";
            }
            PowerUpMessage.Start();
        }

        public void DrawUI(Graphics g)
        {
            Brush BoxPen = new SolidBrush(Color.Green);
            Pen OutlinePen = new Pen(Color.Black, 3);
            Rectangle rect;
            double x = (player.Health / (double)player.MaxHealth) * 10;
            for (int i = 0; i < x; i++)
            {
                if (x <= 2) { BoxPen = new SolidBrush(Color.Red); }
                else if (x <= 4) { BoxPen = new SolidBrush(Color.Orange); }
                rect = new Rectangle(190 + 20 * i, 0, 20, 20);
                g.FillRectangle(BoxPen, rect);
            }
            rect = new Rectangle(190, 0, 200, 20);
            g.DrawRectangle(OutlinePen, rect);

            g.DrawString("HP: " + player.Health + "/" + player.MaxHealth, new Font(FontFamily.GenericMonospace, 19, FontStyle.Bold),
             new SolidBrush(Color.Black), 0, 0);
            g.DrawString("_____________________\nDAMAGE: " + player.Damage + "\nSPEED: " + player.SpeedX + "\nSHOOTRATE: " + player.ShootRate, new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
            new SolidBrush(Color.Black), 0, 10);
            if (this.showMessage)
            {
                g.DrawString(Message, new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
           new SolidBrush(Color.Red), 0, 70);
            }
            
            string padded = "SCORE: " + this.Score;
            g.DrawString(padded, new Font(FontFamily.GenericMonospace, 19, FontStyle.Bold), new SolidBrush(Color.Black), 400, 0);

            g.DrawString("_____________________\nMULTIPLIER: ", new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold),
            new SolidBrush(Color.Black), 400, 10);
            g.DrawString("x" + this.Multiplier, new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold),
            new SolidBrush(Color.Black), 500, 20);
        }
        public void DrawAll(Graphics g)
        {
            DrawUI(g);
            Utils.CheckOutOfBounds(player);
            player.Draw(g);


            foreach (PowerUp powerup in powerups)
            {
                if (player.IsCollidingWith(powerup))
                {
                    PowerUp(powerup.powerType);
                    Utils.MarkForDelition(powerup);
                }
                
              
                powerup.Draw(g);
            }

            foreach (Enemy enemy in enemies)
            {
                if (player.IsCollidingWith(enemy))
                {
                    if (!player.Invincible)
                    {
                        player.Health -= enemy.Damage;
                        this.Multiplier = 1;
                    }

                    if (player.Health <= 0)
                    {
                        player.Kill();
                    }
                    Utils.MarkForDelition(enemy);
                    // PowerUp("multishot");
                }
                enemy.Move();
                enemy.Shoot(this, rand);
                enemy.Draw(g);
            }

            foreach (Bullet bullet in bullets)
            {
                if (player.IsCollidingWith(bullet))
                {
                    if (!player.Invincible)
                    {
                        player.Health -= bullet.Damage;
                        this.Multiplier = 1;
                    }
                    if (player.Health <= 0)
                    {
                        player.Kill();
                    }
                    Utils.MarkForDelition(bullet);
                }

                bullet.Move();
                Utils.CheckOutOfBounds(bullet);
                foreach (Enemy enemy in enemies)
                {
                    if (bullet.IsCollidingWith(enemy))
                    {
                        Utils.MarkForDelition(bullet);
                        enemy.Health -= bullet.Damage;
                        if (enemy.Health <= 0)
                        {
                            Utils.MarkForDelition(enemy);
                            this.Score += enemy.Score * this.Multiplier;
                            this.Multiplier++;
                        }
                    }
                }
                bullet.Draw(g);
            }
            Utils.DeleteMarkedObjects(this);
        }

        public void InstakillTick(object sender, EventArgs e)
        {
            player.Instakill = false;
            Instakill.Stop();
        }
        public void InvincibleTick(object sender, EventArgs e)
        {
            player.Invincible = false;
            Invincible.Stop();
        }
        public void MultishotTick(object sender, EventArgs e)
        {
            player.Multishot = false;
            Multishot.Stop();
        }

        public void PowerUpMessageTick(object sender, EventArgs e)
        {
            this.showMessage = false;
            PowerUpMessage.Stop();
        }
    }
}
