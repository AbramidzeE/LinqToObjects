using System;
using System.Collections.Generic;
using System.Linq;
using Linq.DataSources;
using Linq.EqualityComparers;

namespace Linq
{
    /// <summary>
    /// Considers how to group elements of sequences into buckets ( `group by` and `into` keywords, methods GroupBy, GroupJoin ) in LINQ queries.
    /// Grouping definition: <see cref="IEnumerable{TSource}"/> → <see cref="IEnumerable{IGrouping{TKey,TElement}}"/> 
    /// Grouping refers to the operation of putting data into groups so that the elements in each group share a common attribute.
    /// </summary>
    public static class GroupingData
    {
        /// <summary>
        /// Partitions a list of words by their first letter ans sorts by it.
        /// </summary>
        /// <returns>Sorted by key (first letter) sequence of words grouped by first letter.</returns>
        public static IEnumerable<IGrouping<char, string>> GroupByProperty()
        {
            string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            var groupedWords = words
                .GroupBy(word => word[0])
                .OrderBy(group => group.Key);

            return groupedWords;

        }

        /// <summary>
        /// Groups elements on the remainder of an integer when dividing it by 5.
        /// </summary>
        /// <returns>The sequence of pairs: the remainder of an integer when dividing it by 5 and the numbers with a given remainder.</returns>
        public static IEnumerable<(int remainder, IEnumerable<int> numbers)> Grouping()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var groupedByRemainder = numbers
                .GroupBy(n => n % 5)
                .Select(group => (remainder: group.Key, numbers: group.AsEnumerable()));

            return groupedByRemainder;


        }

        /// <summary>
        /// Partitions a list of products by category.
        /// </summary>
        /// <returns>The sequences of products grouped by categories in which the number of products is less than or equal to 7.</returns>
        public static IEnumerable<(string category, IEnumerable<string> productsName)> GroupByCategory()
        {
            List<Product> products = Products.ProductList;

            var groupedProducts = products
                   .GroupBy(p => p.Category)
                   .Where(g => g.Count() <= 7)
                   .Select(g => (category: g.Key, productsName: g
                   .Select(p => p.ProductName)));

            return groupedProducts;


        }

        /// <summary>
        /// Partitions a list of words by custom comparer <see cref="AnagramEqualityComparer"/>.
        /// </summary>
        /// <returns>The sequences of words grouped by anagram comparer.</returns>
        public static IEnumerable<IGrouping<string, string>> GroupByCustomComparer()
        {
            string[] anagrams =
                {"from   ", "  mane", " salt", " earn ", "name   ", "  last   ", " near ", " form  ", "mean"};


            var groupedAnagrams = anagrams
                 .GroupBy(word => new string(word.Where(c => !char.IsWhiteSpace(c)).ToArray()), new AnagramEqualityComparer())
                 .Select(group => new
                 {
                     Key = group.First().Trim(),
                     Words = group
                 })
                 .Select(group => group.Words);

            return groupedAnagrams;
        }

        /// <summary>
        /// Partitions a list of words by custom comparer <see cref="AnagramEqualityComparer"/>.
        /// </summary>
        /// <returns>The sequences of words in upper case grouped by anagram comparer.</returns>
        public static IEnumerable<IGrouping<string, string>> NestedGroupByCustom()
        {
            string[] anagrams =
                {"from   ", "  mane", " salt", " earn ", "name   ", "  last   ", " near ", " form  ", "mean"};

            return anagrams.GroupBy(word => word.Trim().ToLower(), word => word.ToUpper(), new AnagramEqualityComparer());

        }
    }
}