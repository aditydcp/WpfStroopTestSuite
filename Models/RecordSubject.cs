using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// Transformation data class used to transform
    /// SubjectData class used in the app 
    /// into the data structure to be saved as .csv file
    /// </summary>
    internal class RecordSubject
    {
        public string SubjectName { get; set; }
        public string SubjectId { get; set; }
        public string GroupId { get; set; }
        public DateTime CollectionStartTime { get; set; }
        public DateTime CollectionEndTime { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public RecordSubject()
        {
            SubjectName = string.Empty;
            SubjectId = string.Empty;
            GroupId = string.Empty;
        }

        /// <summary>
        /// Complete constructor using known
        /// <seealso cref="SubjectData"/> object.
        /// </summary>
        /// <param name="subjectData">A <see cref="SubjectData"/> object</param>
        public RecordSubject(SubjectData subjectData)
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
        }

        /// <summary>
        /// Complete constructor
        /// </summary>
        public RecordSubject(string subjectName, string subjectId, string groupId, DateTime collectionStartTime, DateTime collectionEndTime)
        {
            SubjectName = subjectName;
            SubjectId = subjectId;
            GroupId = groupId;
            CollectionStartTime = collectionStartTime;
            CollectionEndTime = collectionEndTime;
        }
    }
}
