using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApp.Utilities
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class Validations
    {
        public static (bool isValid, string value) IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, null);

            string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return (regex.IsMatch(email), email);
        }

        public static (bool isValid, string value) IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, null);

            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$";
            Regex regex = new Regex(pattern);
            return (regex.IsMatch(password), password);
        }

        public static (bool isValid, string value) IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, null);

            string pattern = @"^[a-zA-Z\s-]+$";
            Regex regex = new Regex(pattern);
            return (regex.IsMatch(name) && name.Length <= 50, name);
        }

        public static (bool isValid, decimal value) IsValidAmount(string input)
        {
            bool isValid = decimal.TryParse(input, out decimal amount) && amount > 0;
            return (isValid, amount);
        }


        public static T GetValidInput<T>(string prompt, Func<string, (bool isValid, T value)> validationFunc, string errorMessage)
        {
            string input;
            (bool isValid, T value) result;

            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                result = validationFunc(input);
                if (!result.isValid)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!result.isValid);

            return result.value;
        }


        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
    }

}

