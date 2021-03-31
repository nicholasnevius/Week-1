using System.Runtime.InteropServices.ComTypes;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Numerics;
using System;
using System.Linq;
using System.Collections.Generic;


namespace FizzBuzzRunner
{
    class FizzBuzzer
    {
        /// <summary>
        /// Output:
        ///     Apply rules to the num input and will return a string with that result
        /// Inputs:
        ///     num   = the current number in out FizzBuzz loop
        ///     rules = A list of KeyValue<int, string> where the int is the multiple to run the rule on
        ///             and string is the string to ensure shows up whenever printing a multiple of the int parameter
        /// </summary>
        private static String RunRules(int num, List<KeyValuePair<int, string>> rules)
        {
            string generated = "";
            foreach (var rule in rules)
            {
                // The substring checking is important for preventing duplicates such as:
                //      with rules (3, "Fizz") (5, "Buzz") (15, "FizzBuzz")
                //      with num 15 would have printed "FizzBuzzFizzBuzz" without this check
                // This is needed incase we have a number inbetween 3 and 15 in which case
                // we cannot rely on a fallthrough from 3-5 to print "FizzBuzz" for the 15 case
                if (num % rule.Key == 0 && !generated.Contains(rule.Value))
                {
                    generated += rule.Value;
                }
            }
            return generated;
        }

        /// <summary>
        /// Output:
        ///     If num failed to match any rules then it will return a string of just num
        ///     otherwise it will just return rules_appplied without modifications
        /// Inputs:
        ///     string rules_applied = the string after we ran HandleZero and ApplyRules
        ///     num                  = the current nummber in our FizzBuzz loop
        /// </summary>
        private static string HandleRulesFailed(string rules_applied, int num)
        {
            if (rules_applied.Length == 0)
            {
                rules_applied += Convert.ToString(num);
            }
            return rules_applied;
        }

        /// <summary>
        /// Reason:
        ///     0 % ANYTHING is always 0 so it needs to be handled as a unique edgecase
        /// Result:
        ///     Will print out 0 if the num input is 0
        /// Output:
        ///     Will return true if the current num input is 0 and false otherwise
        /// Inputs:
        ///     num   = the current number in out FizzBuzz loop
        /// </summary>
        private static bool HandleZero(int num)
        {
            if (num == 0)
            {
                Console.WriteLine(Convert.ToString(num));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Result:
        ///     Will perform FizzBuzz on the current number while applying the rules to it
        /// Inputs:
        ///     num   = the current number in out FizzBuzz loop
        ///     rules = A list of KeyValue<int, string> where the int is the multiple to run the rule on
        ///             and string is the string to ensure shows up whenever printing a multiple of the int parameter
        /// </summary>
        private static void PrintNum(int num, List<KeyValuePair<int, string>> rules)
        {
            // zero must be handled differently
            // will print to the console so no need to keep running this func
            if (HandleZero(num))
            {
                return;
            }

            string rules_applied = RunRules(num, rules);
            string formatted = HandleRulesFailed(rules_applied, num);
            Console.WriteLine(formatted);
        }

        /// <summary>
        /// Result:
        ///     If it finds an error in the start + end or the rules parameters then it will throw an exception with an explanation
        /// Inputs:
        ///     start = number to start our FizzBuzz loop at
        ///     end   = number to end our FizzBuzz loop at
        ///     rules = A list of KeyValue<int, string> where the int is the multiple to run the rule on
        ///             and string is the string to ensure shows up whenever printing a multiple of the int parameter
        /// </summary>
        private static void HandleInputErrors(int start, int end, List<KeyValuePair<int, string>> rules)
        {
            if (end < start)
            {
                throw new ArgumentException("End must be equal to or larger than start");
            }
            // TODO: decide if I should throw an exception or just print all numbers
            if (rules == null)
            {
                throw new ArgumentException("Rules were null. If you wish to not apply rules please just pass an empty list");
            }
        }

        /// <summary>
        /// Result:
        ///     Will perform FizzBuzz from start to end while applying rules to each number
        /// Inputs:
        ///     start = number to start at (can be negative)
        ///     end   = numbet to end at (can be negative)
        ///     rules = A list of KeyValue<int, string> where the int is the multiple to run the rule on
        ///             and string is the string to ensure shows up whenever printing a multiple of the int parameter
        /// </summary>
        public static void RunFizzbuzz(int start, int end, List<KeyValuePair<int, string>> rules)
        {
            HandleInputErrors(start, end, rules);
            var sorted_rules = rules.OrderBy(rule => rule.Key).ToList();
            Enumerable.Range(start, end - start + 1).ToList().ForEach(i => PrintNum(i, sorted_rules));
        }
    }

    // class with Main method to act as a runner for FizzBuzzer
    class FizzBuzzRunner
    {
        static void Main(string[] args)
        {
            // rules are pairs of int (multiple) and string (thing to print)
            List<KeyValuePair<int, string>> rules = new List<KeyValuePair<int, string>>();
            rules.Add(new KeyValuePair<int, string>(3, "Fizz"));
            rules.Add(new KeyValuePair<int, string>(5, "Buzz"));
            rules.Add(new KeyValuePair<int, string>(15, "FizzBuzz"));

            FizzBuzzer.RunFizzbuzz(-400, 600, rules);
        }
    }
}