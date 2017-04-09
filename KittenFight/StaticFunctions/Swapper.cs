using System.Collections.Generic;
using System.Linq;

namespace KittenFight.StaticFunctions
{
    public class Swapper
    {
        public static List<string> GetAllPermutations(string input)
        {
            var permutations = new List<string>();
            RecPermutation(string.Empty, input, permutations);

            return permutations.Distinct().ToList();
        }

        private static void RecPermutation(string soFar, string input, List<string> permutations)
        {
            if (string.IsNullOrEmpty(input))
            {
                permutations.Add(soFar);
                return;
            }
            else
            {
                for (var i = 0; i < input.Length; i++)
                {

                    var remaining = input.Substring(0, i) + input.Substring(i + 1);
                    RecPermutation(soFar + input[i], remaining, permutations);
                }
            }
        }
    }
}
