using System;
using System.Linq;

namespace BinaryStringValidator
{
    public class BinaryStringValidator
    {
        public bool IsStringValid(string input)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(input))
            {
                result = HasEqualZerosAndOnes(input);
            }

            if (result)
            {
                result = CheckPrefixes(input);
            }

            return result;
        }

        private bool HasEqualZerosAndOnes(string input)
        {
            bool result = false;
            int ones = input.Count(c => c == '1');
            int zeros = input.Count(c => c == '0');

            if (ones == zeros && (ones + zeros) == input.Length)
            {
                result = true;
            }
            return result;
        }

        private bool CheckPrefixes(string input)
        {
            for (int loop = 0; loop < input.Length; loop++)
            {

                string prefix = input.Substring(0, loop + 1);
                Console.WriteLine($"Checking prefix: {prefix}");
                if (IsBadPrefix(prefix))
                {
                    Console.WriteLine("Bad prefix.");
                    return false;
                }
                Console.WriteLine("Good prefix.");
            }
            return true;
        }

        private bool IsBadPrefix(string s)
        {
            int ones = s.Count(c => c == '1');
            int zeros = s.Count(c => c == '0');
            if (zeros > ones)
            {
                return true;
            }

            return false;
        }
    }

}
