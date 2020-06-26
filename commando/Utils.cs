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
        public static Dictionary<string,Image> map;
        public static int BULLET_UPPER_BOUND = -10;
        public static int FORM_WIDTH = 600;
        public static int FORM_HEIGHT = 700;
        public static List<MovingObject> MarkedForDeletion;
        static Utils()
        {
            map = new Dictionary<string, Image>();
            MarkedForDeletion = new List<MovingObject>();
            map.Add("player",commando.Properties.Resources.ship);
            map.Add("bullet", commando.Properties.Resources.ship);
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
        }
        public static void CheckOutOfBounds(Bullet bullet)
        {
            if (bullet.Y < BULLET_UPPER_BOUND)
            {
                MarkForDelition(bullet);
            }
        }

        private static void MarkForDelition(MovingObject mo)
        {
            MarkedForDeletion.Add(mo);
        }

        public static void DeleteMarkedObjects(Scene scene)
        {
            foreach(MovingObject mo in MarkedForDeletion)
            {
                scene.RemoveObject(mo);
            }
            MarkedForDeletion.Clear();
        }


    }
}
