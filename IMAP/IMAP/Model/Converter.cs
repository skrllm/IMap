using System;
using System.Collections.Generic;
using System.Text;

namespace IMAP.Model
{
    class Converter
    {
        public static double FahrenheitToCelsius(double Temperature)
        {
            Temperature = (Temperature - 32) * 5 / 9;
            return Math.Round(Temperature,1);
        }

        public static double KelvinToCelsius(double Temperature)
        {
            Temperature = Temperature - 273.15;
            return Math.Round(Temperature, 1);
        }

        public static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}
