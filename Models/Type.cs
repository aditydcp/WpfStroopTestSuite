using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStroopTestSuite.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum Type
    {
        Square,
        Red,
        Green,
        Blue,
        Yellow,
    }

    public static class TypeExtensions
    {
        public static string ToMyString(this Type type)
        {
            StringBuilder @string = new();
            @string.Append(type switch
            {
                Type.Square => "Square",
                Type.Red => "Red",
                Type.Green => "Green",
                Type.Blue => "Blue",
                Type.Yellow => "Yellow",
                _ => "Nothing"
            });
            @string.Append(" (" + (int)type + ")");
            return @string.ToString();
        }

        public static Type FromString(string str)
        {
            return str switch
            {
                "Square" => Type.Square,
                "square" => Type.Square,
                "Red" => Type.Red,
                "red" => Type.Red,
                "Green" => Type.Green,
                "green" => Type.Green,
                "Blue" => Type.Blue,
                "blue" => Type.Blue,
                "Yellow" => Type.Yellow,
                "yellow" => Type.Yellow,
                _ => throw new NotSupportedException()
            };
        }
    }
}
