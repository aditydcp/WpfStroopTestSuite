using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    public class SubjectData
    {
        private string? SubjectName { get; set; }
        private string? SubjectId { get; set; }
        private string? GroupId { get; set; }
        public DateTime CollectionStartTime { get; }
        public DateTime CollectionEndTime { get; set; }
        public List<BlockData> Blocks { get; }

        public SubjectData()
        {
            CollectionStartTime = DateTime.Now;
            Blocks = new List<BlockData>();
        }

        public void SaveBlockData(BlockData data)
        {
            Blocks.Add(data);
        }

        #region Getter & Setter
        public void SetSubjectCredentials(string name, string subjectId, string groupId)
        {
            SubjectName = name;
            SubjectId = subjectId;
            GroupId = groupId;
        }

        public void ClearSubjectCredentials()
        {
            SubjectName = null;
            SubjectId = null;
            GroupId = null;
        }

        /// <summary>
        /// Get <see cref="SubjectData.SubjectName"/> property of this object.
        /// </summary>
        /// <param name="mode">
        /// Return mode in case the property is null.
        /// <list type="bullet">
        /// <b>a</b> is the as-is mode. It will return an empty string.
        /// </list>
        /// <list type="bullet">
        /// <b>d</b> is the default mode. It will return "John Doe".
        /// </list>
        /// </param>
        /// <returns>Name of this subject</returns>
        public string GetSubjectName(string mode = "a")
        {
            if (SubjectName == null)
            {
                if (mode == "a") return string.Empty;
                else if (mode == "d") return "John Doe";
                else return string.Empty;
            }
            else return SubjectName;
        }

        /// <summary>
        /// Get <see cref="SubjectData.SubjectId"/> property of this object.
        /// </summary>
        /// <param name="mode">
        /// Return mode in case the property is null.
        /// <list type="bullet">
        /// <b>a</b> is the as-is mode. It will return an empty string.
        /// </list>
        /// <list type="bullet">
        /// <b>d</b> is the default mode. It will return "01".
        /// </list>
        /// </param>
        /// <returns>Subject ID of this subject</returns>
        public string GetSubjectId(string mode = "a")
        {
            if (SubjectId == null)
            {
                if (mode == "a") return string.Empty;
                else if (mode == "d") return "01";
                else return string.Empty;
            }
            else return SubjectId;
        }

        /// <summary>
        /// Get <see cref="SubjectData.GroupId"/> property of this object.
        /// </summary>
        /// <param name="mode">
        /// Return mode in case the property is null.
        /// <list type="bullet">
        /// <b>a</b> is the as-is mode. It will return an empty string.
        /// </list>
        /// <list type="bullet">
        /// <b>d</b> is the default mode. It will return "01".
        /// </list>
        /// </param>
        /// <returns>Group ID of this subject</returns>
        public string GetGroupId(string mode = "a")
        {
            if (GroupId == null)
            {
                if (mode == "a") return string.Empty;
                else if (mode == "d") return "01";
                else return string.Empty;
            }
            else return GroupId;
        }
        #endregion

        public string ToConsoleString(bool includeBlocks = true)
        {
            if (!includeBlocks)
            {
                return
                "Subject: " + SubjectName + "\n" +
                "SID: " + SubjectId + " | " +
                "GID: " + GroupId;
            }

            string blockText;
            if (Blocks.Count < 1)
            {
                blockText = "Block has not started yet";
            }
            else
            {
                blockText = "Block Data:\n" +
                    Blocks.Last().ToConsoleString();
            }
            return
                "Subject: " + SubjectName + "\n" +
                "SID: " + SubjectId + " | " +
                "GID: " + GroupId + "\n" +
                blockText;
        }
    }
}
