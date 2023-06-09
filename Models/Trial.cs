using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// Class definition of a Trial.
    /// </summary>
    public class Trial
    {
        /// <summary>
        /// The actual color displayed.
        /// This property corresponds as the answer.
        /// See <see cref="Color"/>.
        /// </summary>
        public Color Color { get; }
        
        /// <summary>
        /// The type displayed.
        /// See <see cref="Type"/>.
        /// </summary>
        public Type Type { get; }

        private static readonly Color UnavailableColor = Color.None;

        public Result GetResult(string answer)
        {
            if (answer == Color.ToString()) return Result.Correct;
            else if (answer == Color.None.ToString()) return Result.Timeout;
            else return Result.Incorrect;
        }

        public Result GetResult(Color answer)
        {
            if (answer == Color) return Result.Correct;
            else if (answer == Color.None) return Result.Timeout;
            else return Result.Incorrect;
        }

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Trial() { }

        /// <summary>
        /// Complete constructor using string parameters
        /// </summary>
        /// <param name="color">The actual color. The answer.</param>
        /// <param name="type">The type. The question/challenge.</param>
        public Trial(string color, string type)
        {
            Color input = ColorExtensions.FromString(color);
            if (input == UnavailableColor) throw new ArgumentOutOfRangeException(nameof(color));
            Color = ColorExtensions.FromString(color);
            Type = TypeExtensions.FromString(type);
            
        }

        /// <summary>
        /// Complete constructor
        /// </summary>
        /// <param name="color">The actual color. The answer.</param>
        /// <param name="type">The type. The question/challenge.</param>
        public Trial(Color color, Type type)
        {
            if (color == UnavailableColor) throw new ArgumentOutOfRangeException(nameof(color));
            Color = color;
            Type = type;
        }
        #endregion

        public string ToConsoleString()
        {
            return
                "Type: " + Type.ToMyString() + "\n" +
                "Color (Answer): " + Color.ToMyString();
        }
    }
}
