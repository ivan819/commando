using System;
using System.Collections.Generic;
using System.Text;

namespace commando
{
    public class ScoreNamePair : IComparable
    {
        public string name;
        public int score;

        public ScoreNamePair(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public int CompareTo(object obj)
        {
            ScoreNamePair snp = (ScoreNamePair)obj;
            if (this.score == snp.score)
            {
                return 0;
            }
            else if (this.score > snp.score)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}