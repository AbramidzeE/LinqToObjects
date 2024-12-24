using System;
using System.Collections.Generic;
using System.Linq;
using Linq.DataSources;

namespace Linq
{
    /// <summary>
    /// Considers use partition operations (methods 'Take', 'Skip', 'TakeWhile' and 'SkipWhile') in LINQ queries.
    /// Partitioning : <see cref="IEnumerable{TSource}"/> → <see cref="IEnumerable{TSource}"/>
    /// The operation of dividing an input sequence into two sections, without rearranging the elements,
    /// and then returning one of the sections.
    /// </summary>
    public static class PartitioningData
    {
        /// <summary>
        /// Gets only the first 3 elements of the source array.
        /// </summary>
        /// <returns>The first 3 elements of the source array</returns>
        public static IEnumerable<int> Take()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            return numbers.Take(3);
        }

        /// <summary>
        /// Gets the first 3 orders from customers in Washington with date of orders. 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<(string customerId, int orderId, DateTime orderDate)> CustomersTake()
        {
            List<Customer> customers = Customers.CustomerList;
            
            return customers.Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders.Select(o => (c.CustomerId, o.OrderId, o.OrderDate))).OrderBy(res => res.OrderDate).Take(3).ToList();
        }

        /// <summary>
        ///  Gets all but the first 4 elements of the array.
        /// </summary>
        /// <returns>All elements but the first 4 elements of the array.</returns>
        public static IEnumerable<int> Skip()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            numbers.Skip(4);
            return numbers.Skip(4);
        }

        /// <summary>
        /// Gets all but the first 2 orders from customers in Washington.
        /// </summary>
        /// <returns>All but the first 2 orders from customers in Washington.</returns>
        public static IEnumerable<(string customerId, int orderId, DateTime orderDate)> CustomersSkip()
        {
            List<Customer> customers = Customers.CustomerList;

            var result = customers
                .Where(c => c.City == "Washington")
                .SelectMany(c => c.Orders
                .Skip(2),
                (customer, order) => new
                {
                    customerId = customer.CustomerId,
                    orderId = order.OrderId,
                    orderDate = order.OrderDate
                });

            return result.Select(r => (r.customerId, r.orderId, r.orderDate));


        }

        /// <summary>
        ///  Gets elements starting from the beginning of the array until a number is hit that is not less than 6.
        /// </summary>
        /// <returns>Elements starting from the beginning of the array until a number is hit that is not less than 6.</returns>
        public static IEnumerable<int> TakeWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var result = numbers
                .TakeWhile((num) => num < 6);

            return result;
        }

        /// <summary>
        /// Gets elements starting from the beginning of the array until a number is hit that is less than its position in the array.
        /// </summary>
        /// <returns>Elements starting from the beginning of the array until a number is hit that is less than its position in the array.</returns>
        public static IEnumerable<int> IndexedTakeWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var result = numbers
                .TakeWhile((num, index) => num >= index);

            return result;
        }

        /// <summary>
        /// Gets the elements of the array starting from the first element divisible by 3.
        /// </summary>
        /// <returns>The elements of the array starting from the first element divisible by 3.</returns>
        public static IEnumerable<int> SkipWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var result = numbers
                 .SkipWhile((num) => num % 3 != 0);
            return result;
        }

        /// <summary>
        /// Gets the elements of the array starting from the first element less than its position.
        /// </summary>
        /// <returns>The elements of the array starting from the first element less than its position.</returns>
        public static IEnumerable<int> IndexedSkipWhile()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var result = numbers
                .SkipWhile((num, index) => num >= index);

            return result;
        }
    }
}