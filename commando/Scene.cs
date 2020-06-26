using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Scene
    {
        public List<MovingObject> movingObjects;
        public Player player { get; set; }
        public int NumObjects { get { return movingObjects.Count; } }

        public Scene(Player player)
        {
            movingObjects = new List<MovingObject>();
            this.player = player;
        }
        public void AddObj(MovingObject f)
        {
            movingObjects.Add(f);
        }
        public void RemoveObject(MovingObject f)
        {
            movingObjects.Remove(f);
        }
        public void DrawAll(Graphics g)
        {
            Utils.CheckOutOfBounds(player); 
            player.Draw(g);
            foreach (MovingObject movingObj in movingObjects)
            {
                movingObj.Move();
                movingObj.Draw(g);
                if (movingObj is Bullet){
                    Utils.CheckOutOfBounds((Bullet) movingObj);
                }
               
            }
            
           
                
                
        }
    }
}
