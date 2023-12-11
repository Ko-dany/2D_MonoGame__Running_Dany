using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DKoFinal.Database
{
    public class ScoreRecordManager
    {
        /* ================= Score Record Manager ================= */
        /* This class manages reading records from & writing records to txt file using File I/O */

        private string filePath;
        private List<ScoreRecord> scoreRecords;

        public ScoreRecordManager(string filePath)
        {
            this.filePath = filePath;
            scoreRecords = new List<ScoreRecord>();
        }

        public List<ScoreRecord> ReadScores()
        {
            List<ScoreRecord> scoreRecords = new List<ScoreRecord>();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string record = streamReader.ReadLine();
                        string[] fields = record.Split("|");

                        if (fields.Length == 2)
                        {
                            string player = fields[0];
                            double score;
                            if (double.TryParse(fields[1], out score))
                            {
                                

                                scoreRecords.Add(new ScoreRecord(player, score));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return scoreRecords;
        }

        public void WriteScores(List<ScoreRecord> scores)
        {
            try
            {
                List<ScoreRecord> topScores = scores.OrderBy(s => s.Score).Take(5).ToList();

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var score in topScores)
                    {
                        streamWriter.WriteLine($"{score.Player}|{score.Score}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
            }
        }

    }
}
