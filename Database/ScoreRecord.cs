using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKoFinal.Database
{
    public class ScoreRecord
    {
        public string Player { get; }
        public double Score { get; }

        public ScoreRecord(string player, double score)
        {
            Player = player;
            Score = score;
        }
    }
}
