using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Utils
    {
        public static Dictionary<string, Image> map;
        public static int BULLET_UPPER_BOUND = -10;
        public static int FORM_WIDTH = 600;
        public static int FORM_HEIGHT = 700;
        public static List<MovingObject> MarkedForDeletion;

        public static EnemyType normal = new EnemyType(0, 1, 30, 30, 50, 60, "player", 20);
        public static EnemyType mine = new EnemyType(0, 3, 20, 20, 0, 20, "player", 150);

        static Utils()
        {
            map = new Dictionary<string, Image>();
            MarkedForDeletion = new List<MovingObject>();
            map.Add("player", commando.Properties.Resources.ship_new);
            map.Add("bullet", commando.Properties.Resources.ship);
            map.Add("enemy", commando.Properties.Resources.ship);


        }
        public static Image getImg(string type)
        {
            return map[type];
        }
        public static void CheckOutOfBounds(Player player)
        {
            if (player.Y < 0)
            {
                player.Y = 0;
            }
            if (player.X < 0)
            {
                player.X = 0;
            }
            if (player.Y + player.Height > FORM_HEIGHT)
            {
                player.Y = FORM_HEIGHT - player.Height;
            }
            if (player.X + player.Width > FORM_WIDTH)
            {
                player.X = FORM_WIDTH - player.Width;
            }
        }
        public static void CheckOutOfBounds(Bullet bullet)
        {
            // TODO
            if (bullet.Y < BULLET_UPPER_BOUND)
            {
                MarkForDelition(bullet);
            }
        }

        public static void MarkForDelition(MovingObject mo)
        {
            MarkedForDeletion.Add(mo);
        }

        public static void DeleteMarkedObjects(Scene scene)
        {
            foreach (MovingObject mo in MarkedForDeletion)
            {
                if (mo is Enemy)
                {
                    scene.Remove((Enemy)mo);
                }
                else if (mo is Bullet)
                {
                    scene.Remove((Bullet)mo);
                }

            }
            MarkedForDeletion.Clear();
        }



    }

    public class EnemyType
    {
        public int sx { get; set; }
        public int sy { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int hp { get; set; }
        public int schance { get; set; }
        public string image { get; set; }

        public int dmg { get; set; }
        public EnemyType(int sx, int sy, int w, int h, int hp, int schance, string image, int dmg)
        {
            this.sx = sx;
            this.sy = sy;
            this.w = w;
            this.h = h;
            this.hp = hp;
            this.schance = schance;
            this.image = image;
            this.dmg = dmg;
        }
    }
}
