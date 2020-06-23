using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Scene
    {
        public List<Drawable> drwObj;
        public int NumObjects { get { return drwObj.Count; } }

        public Scene()
        {
            drwObj = new List<Drawable>();
        }
        public void AddObj(Drawable f)
        {
            drwObj.Add(f);
        }
        public void DrawAll(Graphics g)
        {
            foreach (Drawable frm in drwObj)
                frm.Draw(g);
        }
    }
}
