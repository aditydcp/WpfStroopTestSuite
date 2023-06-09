using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// Enum class representing
    /// the actual color and also the possible solutions
    /// for Trials
    /// </summary>
    public enum Color
    {
        None = 0, // this may only be used as User Answer, not as solution in a trial
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
    }

    public static class ColorExtensions
    {
        public static int GetAnswerOnlyIndex(this Color color)
        {
            return 0;
        }

        public static string ToMyString(this Color color)
        {
            StringBuilder @string = new();
            @string.Append(color switch
            {
                Color.Red => "Red",
                Color.Green => "Green",
                Color.Blue => "Blue",
                Color.Yellow => "Yellow",
                _ => "Nothing"
            });
            @string.Append(" (" + (int)color + ")");
            return @string.ToString();
        }

        public static Color FromString(string str)
        {
            return str switch
            {
                "Red" => Color.Red,
                "red" => Color.Red,
                "Green" => Color.Green,
                "green" => Color.Green,
                "Blue" => Color.Blue,
                "blue" => Color.Blue,
                "Yellow" => Color.Yellow,
                "yellow" => Color.Yellow,
                _ => throw new NotSupportedException()
            };
        }

        public static int ToMyInt(this Color color)
        {
            return color switch
            {
                Color.Red => 1,
                Color.Green => 2,
                Color.Blue => 3,
                Color.Yellow => 4,
                _ => -1
            };
        }
    }
}
