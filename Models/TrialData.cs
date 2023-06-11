using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// Data class containing information for individual trial.
    /// </summary>
    public class TrialData
    {
        public Result Result { get; set; }
        /// <summary>
        /// This property is calculated from <seealso cref="DateTime.Ticks"/> value difference
        /// between <see cref="TrialData.StartTime"/> and <see cref="TrialData.EndTime"/>.
        /// The value is in resolution of 100 nanosecond.
        /// </summary>
        public long ReactionTime { get; set; }
        public int StartScore { get; set; }
        public int EndScore { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Trial Trial { get; set; }
        public string UserAnswer { get; set; }

        #region Constructors
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public TrialData()
        {
            Trial = new();
            UserAnswer = string.Empty;
        }

        /// <summary>
        /// Partial constructor.
        /// Pass a score to assign as the <see cref="TrialData.StartScore"/>.
        /// This constructor will automatically assign <seealso cref="DateTime.Now"/> as the <see cref="TrialData.StartTime"/>.
        /// </summary>
        /// <param name="startingScore">The score when the trial starts.</param>
        public TrialData(int startingScore)
        {
            StartScore = startingScore;
            StartTime = DateTime.Now;
            Trial = new();
            UserAnswer = string.Empty;
        }

        /// <summary>
        /// Complete constructor without reactionTime parameter.
        /// This will automatically calculate the <see cref="TrialData.ReactionTime"/>.
        /// </summary>
        public TrialData(Result result, int startScore, int endScore, DateTime startTime, DateTime endTime, Trial trial, string answer)
        {
            Result = result;
            StartScore = startScore;
            EndScore = endScore;
            StartTime = startTime;
            EndTime = endTime;
            CalculateReactionTime(StartTime, EndTime);
            Trial = trial;
            UserAnswer = answer;
        }

        /// <summary>
        /// Complete constructor without reactionTime and endScore parameter.
        /// This will automatically calculate the <see cref="TrialData.ReactionTime"/> and <see cref="TrialData.EndScore"/>.
        /// </summary>
        public TrialData(Result result, int startScore, DateTime startTime, DateTime endTime, Trial trial, string answer)
        {
            Result = result;
            StartScore = startScore;
            StartTime = startTime;
            EndTime = endTime;
            EndScore = result switch
            {
                Result.Correct => startScore + 1,
                _ => startScore,
            };
            CalculateReactionTime(StartTime, EndTime);
            Trial = trial;
            UserAnswer = answer;
        }

        /// <summary>
        /// Complete constructor
        /// </summary>
        public TrialData(Result result, long reactionTime, int startScore, int endScore, DateTime startTime, DateTime endTime, Trial trial, string answer)
        {
            Result = result;
            ReactionTime = reactionTime;
            StartScore = startScore;
            EndScore = endScore;
            StartTime = startTime;
            EndTime = endTime;
            Trial = trial;
            UserAnswer = answer;
        }
        #endregion

        /// <summary>
        /// Set the values of <see cref="TrialData.Result"/>, <see cref="TrialData.EndScore"/>, <see cref="TrialData.EndTime"/>,
        /// <see cref="TrialData.Trial"/>, <see cref="TrialData.Solution"/> and <see cref="TrialData.UserAnswer"/>.
        /// This function do not tamper with values of <see cref="TrialData.StartScore"/> and <see cref="TrialData.StartTime"/>.
        /// Use this function when <seealso cref="TrialData.TrialData(int)"/> constructor is used for this object.
        /// </summary>
        /// <param name="result">The result</param>
        /// <param name="endScore">The final score after result</param>
        /// <param name="endTime">The DateTime when the answer is submitted</param>
        /// <param name="trial">The trial in <seealso cref="Models.Trial"/> type</param>
        /// <param name="answer">The answer submitted by user</param>
        public void EnterData(Result result, int endScore, DateTime endTime, Trial trial, string answer)
        {
            Result = result;
            EndTime = endTime;
            EndScore = endScore;
            CalculateReactionTime(StartTime, EndTime);
            Trial = trial;
            UserAnswer = answer;
        }

        /// <summary>
        /// Set the values of <see cref="TrialData.Result"/>, <see cref="TrialData.EndScore"/>, <see cref="TrialData.EndTime"/>,
        /// <see cref="TrialData.Trial"/>, <see cref="TrialData.Solution"/> and <see cref="TrialData.UserAnswer"/>.
        /// This function do not tamper with values of <see cref="TrialData.StartScore"/> and <see cref="TrialData.StartTime"/>.
        /// Use this function when <seealso cref="TrialData.TrialData(int)"/> constructor is used for this object.
        /// </summary>
        /// <param name="result">The result</param>
        /// <param name="endScore">The final score after result</param>
        /// <param name="endTime">The DateTime when the answer is submitted</param>
        /// <param name="trial">The trial in <seealso cref="Models.Trial"/> type</param>
        /// <param name="answer">The answer submitted by user</param>
        public void EnterData(Result result, DateTime endTime, Trial trial, string answer)
        {
            Result = result;
            EndTime = endTime;
            EndScore = result switch
            {
                Result.Correct => StartScore + 1,
                _ => StartScore,
            };
            CalculateReactionTime(StartTime, EndTime);
            Trial = trial;
            UserAnswer = answer;
        }

        /// <summary>
        /// Assign this object's <see cref="TrialData.ReactionTime"/>
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private void CalculateReactionTime(DateTime startTime, DateTime endTime)
        {
            long elapsedTicks = endTime.Ticks - startTime.Ticks;
            //ReactionTime = elapsedTicks / 10000.00;
            ReactionTime = elapsedTicks;
        }

        public string ToConsoleString()
        {
            return
                "Result: " + Result.ToMyString() + "\n" +
                "Reaction Time: " + (ReactionTime / 10000.00) + " ms\n" +
                "StartScore: " + StartScore + "\n" +
                "EndScore: " + EndScore + "\n" +
                "StartTime: " + StartTime + "\n" +
                "EndTime: " + EndTime + "\n" +
                "Trial:\n" + Trial.ToConsoleString() + "\n" +
                "User Inputted Answer: " + UserAnswer;
        }
    }
}
