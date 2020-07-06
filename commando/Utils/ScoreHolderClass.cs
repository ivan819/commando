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
        private static List<ScoreNamePair> scores;
        private static String filePath;

        static ScoreHolderClass()
        {
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "highscores.txt");
            scores = new List<ScoreNamePair>();
            createTXT();
        }

        public static void add(string name, long score)
        {


            scores.Add(new ScoreNamePair(name, score));
            save();
        }

        public static List<ScoreNamePair> getScores()
        {
            load();
            scores.Sort();
            return scores;
        }

        private static void save()
        {
            TextWriter tw = new StreamWriter(filePath);

            foreach (ScoreNamePair item in scores)
            {
                tw.WriteLine(string.Format("{0}:{1}", item.name, item.score));
            }

            tw.Close();
            //System.IO.File.WriteAllLines(, this.scores);
        }

        private static void load()
        {
            scores = new List<ScoreNamePair>();
            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string s in lines)
            {
                if (!s.Equals(""))
                {
                    string[] parts = s.Split(':');
                    scores.Add(new ScoreNamePair(parts[0], Int32.Parse(parts[1])));
                }

            }
        }

        private static void createTXT()
        {
            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

        }

    }
}
