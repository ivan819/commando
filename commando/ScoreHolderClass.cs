using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class ScoreHolderClass
    {
        private List<ScoreNamePair> scores;

        public ScoreHolderClass()
        {
            this.scores = new List<ScoreNamePair>();
            this.load();
        }

        public void add(string name, int score)
        {


            this.scores.Add(new ScoreNamePair(name, score));

            this.save();
        }

        public List<ScoreNamePair> getScores()
        {
            this.scores.Sort();
            return this.scores;
        }

        private void save()
        {
            TextWriter tw = new StreamWriter(@"C:\Users\Gregor Mandikj\Desktop\baza");

            foreach (ScoreNamePair item in this.scores)
            {
                tw.WriteLine(string.Format("{0}:{1}", item.name, item.score));
            }

            tw.Close();
            //System.IO.File.WriteAllLines(, this.scores);
        }

        private void load()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Gregor Mandikj\Desktop\baza");

            foreach (string s in lines)
            {
                if (!s.Equals(""))
                {
                    string[] parts = s.Split();
                    this.scores.Add(new ScoreNamePair(parts[1], Int32.Parse(parts[0])));
                }

            }
        }

        private void createTXT()
        {

        }

    }
}
