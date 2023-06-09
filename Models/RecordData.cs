using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    internal class RecordData
    {
        #region Subject properties
        public string SubjectName { get; set; }
        public string SubjectId { get; set; }
        public string GroupId { get; set; }
        public DateTime CollectionStartTime { get; set; }
        public DateTime CollectionEndTime { get; set; }
        #endregion

        #region Block properties
        public int Block { get; set; }
        public DateTime BlockStartTime { get; set; }
        public DateTime BlockEndTime { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public long BlockDuration { get; set; }
        public int BlockFinalScore { get; set; }
        public int BlockTrialCount { get; set; }
        public double BlockAccuracy { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public double BlockMeanReactionTimeOnCorrectTrials { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public double BlockMeanReactionTime { get; set; }
        #endregion

        #region Trial properties
        public DateTime TrialStartTime { get; set; }
        public DateTime TrialEndTime { get; set; }
        /// <summary>
        /// This property is in resolution of 100 nanoseconds.
        /// See also <seealso cref="DateTime.Ticks"/>.
        /// </summary>
        public long ReactionTime { get; set; }
        public string Question { get; set; }
        public string Solution { get; set; }
        public string UserAnswer { get; set; }
        public int Result { get; set; }
        public int StartScore { get; set; }
        public int EndScore { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public RecordData()
        {
            SubjectName = string.Empty;
            SubjectId = string.Empty;
            GroupId = string.Empty;
            Question = string.Empty;
            Solution = string.Empty;
            UserAnswer = string.Empty;
        }

        #region Complete Constructors
        /// <summary>
        /// Complete constructor using known
        /// <seealso cref="SubjectData"/>, <seealso cref="BlockData"/> and <seealso cref="TrialData"/>.
        /// Only use this constructor when all data belong to the same entity.
        /// </summary>
        /// <param name="subjectData"><see cref="SubjectData"/> object</param>
        /// <param name="blockData"><see cref="BlockData"/> object that corresponds to <paramref name="subjectData"/></param>
        /// <param name="trialData"><see cref="TrialData"/> object that corresponds to <paramref name="blockData"/></param>
        public RecordData(SubjectData subjectData, BlockData blockData, TrialData trialData)
        {
            //if (subjectData.SubjectName == null) SubjectName = string.Empty;
            //else SubjectName = subjectData.SubjectName;
            //if (subjectData.SubjectId == null) SubjectId = string.Empty;
            //else SubjectId = subjectData.SubjectId;
            //if (subjectData.GroupId == null) GroupId = string.Empty;
            //else GroupId = subjectData.GroupId;
            SubjectName = subjectData.GetSubjectName();
            SubjectId = subjectData.GetSubjectId();
            GroupId = subjectData.GetGroupId();
            CollectionStartTime = subjectData.CollectionStartTime;
            CollectionEndTime = subjectData.CollectionEndTime;

            Block = (int)blockData.Stage;
            BlockStartTime = blockData.StartTime;
            BlockEndTime = blockData.EndTime;
            BlockDuration = GetDurationInTicks(blockData.StartTime, blockData.EndTime);
            BlockFinalScore = blockData.Score;
            BlockTrialCount = blockData.TrialsCount;
            BlockAccuracy = blockData.Accuracy;
            BlockMeanReactionTimeOnCorrectTrials = blockData.MeanReactionTimeOnCorrectTrials;
            BlockMeanReactionTime = blockData.MeanReactionTime;

            TrialStartTime = trialData.StartTime;
            TrialEndTime = trialData.EndTime;
            ReactionTime = trialData.ReactionTime;
            Question = trialData.Trial.Type.ToString();
            Solution = trialData.Trial.Color.ToString();
            Result = (int)trialData.Result;
            UserAnswer = trialData.UserAnswer.ToString();
            StartScore = trialData.StartScore;
            EndScore = trialData.EndScore;
        }

        /// <summary>
        /// Complete constructor with all the required property
        /// </summary>
        public RecordData(
            string? subjectName,
            string? subjectId,
            string? groupId,
            DateTime collectionStartTime,
            DateTime collectionEndTime,

            int block,
            DateTime blockStartTime,
            DateTime blockEndTime,
            long blockDuration,
            int blockFinalScore,
            int blockTrialCount,
            double blockAccuracy,
            double blockMeanReactionTimeOnCorrectTrials,
            double blockMeanReactionTime,

            DateTime trialStartTime,
            DateTime trialEndTime,
            long reactionTime,
            string question,
            string solution,
            string userAnswer,
            int result,
            int startScore,
            int endScore)
        {
            if (subjectName == null) SubjectName = string.Empty;
            else SubjectName = subjectName;
            if (subjectId == null) SubjectId = string.Empty;
            else SubjectId = subjectId;
            if (groupId == null) GroupId = string.Empty;
            else GroupId = groupId;
            CollectionStartTime = collectionStartTime;
            CollectionEndTime = collectionEndTime;

            Block = block;
            BlockStartTime = blockStartTime;
            BlockEndTime = blockEndTime;
            BlockDuration = blockDuration;
            BlockFinalScore = blockFinalScore;
            BlockTrialCount = blockTrialCount;
            BlockAccuracy = blockAccuracy;
            BlockMeanReactionTimeOnCorrectTrials = blockMeanReactionTimeOnCorrectTrials;
            BlockMeanReactionTime = blockMeanReactionTime;
            TrialStartTime = trialStartTime;
            TrialEndTime = trialEndTime;
            ReactionTime = reactionTime;
            Question = question;
            Solution = solution;
            UserAnswer = userAnswer;
            Result = result;
            StartScore = startScore;
            EndScore = endScore;
        }
        #endregion
        #endregion

        private static long GetDurationInTicks(DateTime start, DateTime end) { return end.Ticks - start.Ticks; }
    }
}
