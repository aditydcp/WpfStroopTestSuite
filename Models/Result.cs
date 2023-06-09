using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// Enum class for trial result.
    /// </summary>
    public enum Result
    {
        Correct = 1,
        Incorrect = 2,
        Timeout = 3
    }

    public static class ResultExtensions
    {
        /// <summary>
        /// My own ToString()
        /// </summary>
        /// <param name="result"></param>
        /// <returns>String</returns>
        public static string ToMyString(this Result result)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(result switch
            {
                Result.Correct => "Correct",
                Result.Incorrect => "Incorrect",
                Result.Timeout => "Timeout",
                _ => "Nothing"
            });
            stringBuilder.Append(" (" + (int)result + ")");
            return stringBuilder.ToString();
        }

        public static int ToMyInt(this Result result)
        {
            return result switch
            {
                Result.Correct => 1,
                Result.Incorrect => 2,
                Result.Timeout => 3,
                _ => -1
            };
        }
    }
}
