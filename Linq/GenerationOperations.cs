using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    /// <summary>
    /// Considers how to create sequences of integers (methods 'Empty', 'Range', and 'Repeat') in LINQ queries.
    /// Generation Methods: void → <see cref="IEnumerable{TResult}"/>
    /// Generation refers to creating a new sequence of values.
    /// </summary>
    public static class GenerationOperations
    {
        /// <summary>
        /// Creates an empty sequence of integral numbers.
        /// </summary>
        /// <returns>Empty sequence of integral numbers.</returns>
        public static IEnumerable<int> EmptySequence()
        {
            return Enumerable.Empty<int>();
        }

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range with additional information about their parity-oddness.
        /// </summary>
        /// <returns> The sequence of integral numbers in the specified range with additional information about their parity-oddness.
        /// </returns>
        public static IEnumerable<(int number, string oddEven)> RangeOfIntegers()
        {
            return Enumerable.Range(100, 20).Select(number =>
            {
                string odds;

                if (number % 2 == 0) 
                {
                    odds = "even";
                }
                else
                {
                    odds = "odd";
                }
                return(number, odds);
            });
               
        }

        /// <summary>
        /// Generates a sequence that contains one repeated value.
        /// </summary>
        /// <returns>The sequence that contains one repeated value. </returns>
        public static IEnumerable<int> RepeatNumber()
        {
            return Enumerable.Repeat(7, 10);
             
        }
    }
}
