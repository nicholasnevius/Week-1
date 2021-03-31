using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System;

namespace palindrome
{
    enum PalindromeCalculationMethods
    {
        Reversing,
        Recursion,
        Efficient
    }

    class PalindromeCalculator
    {
        /// <summary>Will return the reverse of the inputted string
        /// <param>
        ///     input = the string you wish to reverse
        /// </param>
        /// <returns>
        ///     a string that is the reverse of the input
        /// </returns>
        /// </summary>
        private static string ReverseString(string input)
        {
            string reversed = "";
            for (int i = input.Length - 1; i >= 0; i--)
            {
                reversed += input[i];
            }
            return reversed;
        }

        /// <summary> Check if a string is a palindrome by comparing it to its reverse
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool IsPalindromeReverse(string input)
        {
            return input.Equals(ReverseString(input));
        }

        /// <summary>Check if a string is a palindrome by looping over half of it
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool IsPalindromeEfficient(string input)
        {
            for (int i = 0, j = input.Length - 1; i <= j; i++, j--)
            {
                if (input[i] != input[j])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>The recursive function for checking if a string is a palindrome.
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        ///             will shrink as we recurse
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool Recursing(string input)
        {
            if (input.Length < 2)
            {
                return true;
            }
            if (input[0] != input[input.Length - 1])
            {
                return false;
            }
            return Recursing(input.TrimEnd().TrimStart());
        }

        /// <summary>The entrypoint for checking if a string is a palindrome through recursion
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool IsPalindromeRecursive(string input)
        {
            return Recursing(input);
        }

        /// <summary>Handle any errors in user input
        /// <param>
        ///     input = the string you wish to check for errors
        /// </param>
        /// <exception>
        ///     Can throw an Exception if the input gotten from the user is somehow null
        /// </exception>
        /// </summary>
        private static void HandleUserInputError(string input)
        {
            if (input == null)
            {
                throw new Exception("How did you even manage to type in null?");
            }
        }

        /// <summary>Perform checks if string is trivially a palindrome
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool InitialChecks(string input)
        {
            return input.Length < 2;
        }

        /// <summary>The entrypoint for checking if a string is a palindrome through recursion
        /// <param>
        ///     method = the method it will use to calculate for checking
        /// </param>
        /// <param>
        ///     input = the string you wish to check if it is a palindrome
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        private static bool CheckCalculationMethod(PalindromeCalculationMethods method, string input)
        {
            switch (method)
            {
                case PalindromeCalculationMethods.Efficient:
                    return IsPalindromeEfficient(input);
                case PalindromeCalculationMethods.Recursion:
                    return IsPalindromeRecursive(input);
                case PalindromeCalculationMethods.Reversing:
                    return IsPalindromeReverse(input);
                default:
                    return false; ;
            }
        }

        /// <summary>Checks for a palindrome through the given method
        /// <param>
        ///     input = a string to check whether it is a palindrome or not
        /// </param>
        /// <param>
        ///     method = The method (Recursion, Reversing, or Efficient) to calculate
        /// </param>
        /// <returns>
        ///     True if input is a palindrome and false otherwise
        /// </returns>
        /// </summary>
        public static bool IsPalindrome(string input, PalindromeCalculationMethods method)
        {
            HandleUserInputError(input);
            if (InitialChecks(input) || CheckCalculationMethod(method, input))
            {
                return true;
            }
            return false;
        }

        /// <summary>Prints PrintTrue if input is a palindrome or PrintFalse if it is not
        /// <param>
        ///     input = the string to check whether it is a palindrome or not
        /// </param>
        /// <param>
        ///     PrintTrue = the string to print if input is a palindrome
        /// </param>
        /// <param>
        ///     PrintFalse = the string to print if input is not a palindrome
        /// </param>
        /// <param>
        ///     method = The calculation method (Recursive, Reversing, or Efficient) to use for calculation
        /// </param>
        /// </summary>
        public static void PrintIsPalindrome(string input, string PrintTrue, string PrintFalse, PalindromeCalculationMethods method)
        {
            if (IsPalindrome(input, method))
            {
                Console.WriteLine(PrintTrue);
            }
            else
            {
                Console.WriteLine(PrintFalse);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string user_input = Console.ReadLine();
            PalindromeCalculator.PrintIsPalindrome(user_input, "true", "false", PalindromeCalculationMethods.Efficient);
        }
    }
}

