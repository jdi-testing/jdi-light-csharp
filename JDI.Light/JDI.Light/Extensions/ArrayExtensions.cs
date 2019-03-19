using System;

namespace JDI.Light.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Convert all array values into string values
        /// </summary>
        /// <param name="values">Array</param>
        /// <returns>Array of string values</returns>
        public static string[] ToStringExtension(Array values)
        {
            var arr = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                arr[i] = values.GetValue(i).ToString();
            }

            return arr;
        }
    }
}