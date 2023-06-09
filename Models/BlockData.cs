using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    public class BlockData
    {
        public Stages Stage { get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public int TrialsCount { get; set; }
        public double Accuracy { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public double MeanReactionTimeOnCorrectTrials { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public double MeanReactionTime { get; set; }
        public List<TrialData> Trials { get; }
        public bool IsFinished { get; set; }

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        /// <param name="stage">The stage on which the block is held</param>
        public BlockData(Stages stage)
        {
            Stage = stage;
            Trials = new List<TrialData>();
            IsFinished = false;
        }

        /// <summary>
        /// Complete constructor.
        /// Property <see cref="BlockData.IsFinished"/> is automatically assigned to true.
        /// </summary>
        public BlockData(
            Stages stage,
            DateTime startTime,
            DateTime endTime,
            int score,
            int trialsAttempted,
            double accuracy,
            double meanReactionTimeOnCorrectTrials,
            double meanReactionTime,
            List<TrialData> trials)
        {
            Stage = stage;
            StartTime = startTime;
            EndTime = endTime;
            Score = score;
            TrialsCount = trialsAttempted;
            Accuracy = accuracy;
            MeanReactionTimeOnCorrectTrials = meanReactionTimeOnCorrectTrials;
            MeanReactionTime = meanReactionTime;
            Trials = trials;
            IsFinished = true;
        }
        #endregion

        public void SaveTrialData(TrialData data)
        {
            Trials.Add(data);
        }

        /// <summary>
        /// Quick calculate only calculate the TrialsCount and latest Score
        /// </summary>
        public void QuickCalculateBlockData()
        {
            // get score from the last trial
            Score = Trials.Last().EndScore;

            // get total trials attempted
            TrialsCount = Trials.Count;
        }

        public void CalculateBlockData()
        {
            // get score from the last trial
            Score = Trials.Last().EndScore;

            // get total trials attempted
            TrialsCount = Trials.Count;

            // calculate the other metrics
            int correctCounter = 0;
            long correctReactionTimeStore = 0;
            long allReactionTimeStore = 0;
            foreach (var trial in Trials)
            {
                if (trial.Result == Result.Correct)
                {
                    correctCounter++;
                    correctReactionTimeStore += trial.ReactionTime;
                }
                allReactionTimeStore += trial.ReactionTime;
            }

            // calculate accuracy
            Accuracy = (correctCounter * 1.00) / (TrialsCount * 1.00) * 100;

            // calculate mean reaction time
            MeanReactionTime = (allReactionTimeStore * 1.00) / (TrialsCount * 1.00);
            MeanReactionTimeOnCorrectTrials = (correctReactionTimeStore * 1.00) / (correctCounter * 1.00);

            // set to finished
            IsFinished = true;
        }

        public string ToConsoleString()
        {
            string trialText;
            if (Trials.Count < 1)
            {
                trialText = "No Trial Yet";
            }
            else
            {
                trialText = "Last Trial Data:\n" +
                Trials.Last().ToConsoleString();
            }
            return
                "Stage: " + (int)Stage + "\n" +
                "StartTime: " + StartTime + "\n" +
                "EndTime: " + EndTime + "\n" +
                "Score: " + Score + "\n" +
                "TrialsCount: " + TrialsCount + "\n" +
                "Accuracy: " + Accuracy + "\n" +
                "MRTCorrect: " + MeanReactionTimeOnCorrectTrials + "\n" +
                "MRT: " + MeanReactionTime + "\n" +
                "IsFinished: " + IsFinished + "\n" +
                trialText;

        }

        public bool IsUnpopulated()
        {
            bool isUnpopulated = false;
            if (Trials.Count < 1) { isUnpopulated = true; }
            return isUnpopulated;
        }
    }
}
