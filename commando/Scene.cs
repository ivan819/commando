using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Scene
    {
        public List<Enemy> enemies;
        public List<Bullet> bullets;

        Random rand = new Random();
        public Player player { get; set; }

        public Scene(Player player)
        {
            enemies = new List<Enemy>();
            bullets = new List<Bullet>();
            this.player = player;
        }
        public void Add(Enemy f)
        {
            enemies.Add(f);
        }
        public void Add(Bullet f)
        {
            bullets.Add(f);
        }
        public void Remove(Enemy f)
        {
            enemies.Remove(f);
        }
        public void Remove(Bullet f)
        {
            bullets.Remove(f);
        }
        public void DrawAll(Graphics g)
        {
            Utils.CheckOutOfBounds(player);
            player.Draw(g);

            foreach (Enemy enemy in enemies)
            {
                enemy.Move();
                enemy.Shoot(this, rand);
                enemy.Draw(g);
            }

            foreach (Bullet bullet in bullets)
            {
                if (player.IsCollidingWith(bullet))
                {
                    player.Health -= bullet.Damage;
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
                        if (enemy.Health <= 0) { Utils.MarkForDelition(enemy); }
                    }
                }
                bullet.Draw(g);
            }
            Utils.DeleteMarkedObjects(this);
        }
    }
}
