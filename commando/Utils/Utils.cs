﻿using System;
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
       

        public static EnemyType normal = new EnemyType(1, 1, 50, 50, 30, 120, "normal", 10,50);
        public static EnemyType homing = new EnemyType(0, 2, 50, 50, 50, 60, "homing", 15,100);
        public static EnemyType mine = new EnemyType(0, 0, 30, 30, 20, 0, "mine", 170,25);

        static Utils()
        {
            map = new Dictionary<string, Image>();
           // MarkedForDeletion = new List<MovingObject>();
            map.Add("player", commando.Properties.Resources.firing);
            map.Add("bullet", commando.Properties.Resources.bullet);
            map.Add("mine", commando.Properties.Resources.mine);
            map.Add("normal", commando.Properties.Resources.blue);
            map.Add("homing", commando.Properties.Resources.red);
            map.Add("explosion", commando.Properties.Resources.explosion);
            map.Add("player-left", commando.Properties.Resources.veering2);
            map.Add("player-right", commando.Properties.Resources.veering1);
            map.Add("background", commando.Properties.Resources.background);

            map.Add("1", commando.Properties.Resources.freeze);
            map.Add("2", commando.Properties.Resources.heal);
            map.Add("3", commando.Properties.Resources.invincibility);
            map.Add("4", commando.Properties.Resources.multishot);
            map.Add("5", commando.Properties.Resources.instakill);
            map.Add("6", commando.Properties.Resources.stats);

        }
        public static Image getImg(string type)
        {
            return map[type];
        }
       
    }

    // Helper class for different enemy types
    public class EnemyType
    {
        public int sx { get; set; }
        public int sy { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int hp { get; set; }
        public int schance { get; set; }
        public string image { get; set; }
        public int score { get; set; }
        public int dmg { get; set; }
        public EnemyType(int sx, int sy, int w, int h, int hp, int schance, string image, int dmg, int score)
        {
            this.sx = sx;
            this.sy = sy;
            this.w = w;
            this.h = h;
            this.hp = hp;
            this.schance = schance;
            this.image = image;
            this.dmg = dmg;
            this.score = score;
        }
    }
}
