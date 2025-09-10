using Demo_LINQ02.Data;

namespace Assigment02LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Aggregate Operators

            // 1. Get the total units in stock for each product category.
            var q1 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     select new { Category = g.Key, TotalStock = g.Sum(p => p.UnitsInStock) };
            Console.WriteLine("Total units in stock per category:");
            foreach (var item in q1)
                Console.WriteLine($"{item.Category}: {item.TotalStock}");
            Console.WriteLine(new string('-', 50));

            // 2. Get the cheapest price among each category's products
            var q2 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     select new { Category = g.Key, MinPrice = g.Min(p => p.UnitPrice) };
            Console.WriteLine("Cheapest price per category:");
            foreach (var item in q2)
                Console.WriteLine($"{item.Category}: {item.MinPrice}");
            Console.WriteLine(new string('-', 50));

            // 3. Get the products with the cheapest price in each category (Use Let)
            var q3 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     let minPrice = g.Min(p => p.UnitPrice)
                     from p in g
                     where p.UnitPrice == minPrice
                     select new { g.Key, p.ProductName, p.UnitPrice };
            Console.WriteLine("Products with cheapest price per category:");
            foreach (var item in q3)
                Console.WriteLine($"{item.Key}: {item.ProductName} - {item.UnitPrice}");
            Console.WriteLine(new string('-', 50));

            // 4. Most expensive price per category
            var q4 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     select new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) };
            Console.WriteLine("Most expensive price per category:");
            foreach (var item in q4)
                Console.WriteLine($"{item.Category}: {item.MaxPrice}");
            Console.WriteLine(new string('-', 50));

            // 5. Products with most expensive price per category
            var q5 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     let maxPrice = g.Max(p => p.UnitPrice)
                     from p in g
                     where p.UnitPrice == maxPrice
                     select new { g.Key, p.ProductName, p.UnitPrice };
            Console.WriteLine("Products with most expensive price per category:");
            foreach (var item in q5)
                Console.WriteLine($"{item.Key}: {item.ProductName} - {item.UnitPrice}");
            Console.WriteLine(new string('-', 50));

            // 6. Average price per category
            var q6 = from p in ListGenerator.ProductList
                     group p by p.Category into g
                     select new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) };
            Console.WriteLine("Average price per category:");
            foreach (var item in q6)
                Console.WriteLine($"{item.Category}: {item.AvgPrice}");
            Console.WriteLine(new string('-', 50));

            #endregion

            #region LINQ - Set Operators

            // 1. Unique category names
            var q7 = ListGenerator.ProductList.Select(p => p.Category).Distinct();
            Console.WriteLine("Unique category names:");
            foreach (var c in q7) Console.WriteLine(c);
            Console.WriteLine(new string('-', 50));

            // 2. Unique first letter from both product and customer names
            var q8 = ListGenerator.ProductList.Select(p => p.ProductName[0])
                        .Union(ListGenerator.CustomerList.Select(c => c.CustomerName[0]))
                        .Distinct();
            Console.WriteLine("Unique first letters from products and customers:");
            foreach (var ch in q8) Console.WriteLine(ch);
            Console.WriteLine(new string('-', 50));

            // 3. Common first letters
            var q9 = ListGenerator.ProductList.Select(p => p.ProductName[0])
                        .Intersect(ListGenerator.CustomerList.Select(c => c.CustomerName[0]));
            Console.WriteLine("Common first letters:");
            foreach (var ch in q9) Console.WriteLine(ch);
            Console.WriteLine(new string('-', 50));

            // 4. Product first letters not in customer first letters
            var q10 = ListGenerator.ProductList.Select(p => p.ProductName[0])
                         .Except(ListGenerator.CustomerList.Select(c => c.CustomerName[0]));
            Console.WriteLine("Product first letters not in customer first letters:");
            foreach (var ch in q10) Console.WriteLine(ch);
            Console.WriteLine(new string('-', 50));

            // 5. Last three characters from names
            var q11 = ListGenerator.ProductList.Select(p => p.ProductName.Length >= 3 ?
                                            p.ProductName.Substring(p.ProductName.Length - 3) : p.ProductName)
                     .Concat(ListGenerator.CustomerList.Select(c => c.CustomerName.Length >= 3 ?
                                            c.CustomerName.Substring(c.CustomerName.Length - 3) : c.CustomerName));
            Console.WriteLine("Last 3 characters of product and customer names:");
            foreach (var s in q11) Console.WriteLine(s);
            Console.WriteLine(new string('-', 50));

            #endregion

            #region LINQ - Partitioning Operators

            // 1. First 3 orders from Washington customers
            var q12 = ListGenerator.CustomerList
                        .Where(c => c.City == "Washington")
                        .SelectMany(c => c.Orders)
                        .Take(3);
            Console.WriteLine("First 3 orders from Washington:");
            foreach (var o in q12) Console.WriteLine($"{o.OrderID} - {o.OrderDate}");
            Console.WriteLine(new string('-', 50));

            // 2. All but first 2 orders from Washington
            var q13 = ListGenerator.CustomerList
                        .Where(c => c.City == "Washington")
                        .SelectMany(c => c.Orders)
                        .Skip(2);
            Console.WriteLine("All but first 2 orders from Washington:");
            foreach (var o in q13) Console.WriteLine($"{o.OrderID} - {o.OrderDate}");
            Console.WriteLine(new string('-', 50));

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 3. Return elements until a number < position
            var q14 = numbers.TakeWhile((n, i) => n >= i);
            Console.WriteLine("Elements until number < position:");
            foreach (var n in q14) Console.WriteLine(n);
            Console.WriteLine(new string('-', 50));

            // 4. Elements starting from first divisible by 3
            var q15 = numbers.SkipWhile(n => n % 3 != 0);
            Console.WriteLine("Elements starting from first divisible by 3:");
            foreach (var n in q15) Console.WriteLine(n);
            Console.WriteLine(new string('-', 50));

            // 5. Elements starting from first less than position
            var q16 = numbers.SkipWhile((n, i) => n >= i);
            Console.WriteLine("Elements starting from first < position:");
            foreach (var n in q16) Console.WriteLine(n);
            Console.WriteLine(new string('-', 50));

            #endregion

            #region LINQ - Quantifiers

            // 1. Any word contains 'ei'
            string[] dict = System.IO.File.ReadAllLines("dictionary_english.txt");
            var q17 = dict.Any(w => w.Contains("ei"));
            Console.WriteLine($"Any word contains 'ei': {q17}");
            Console.WriteLine(new string('-', 50));

            // 2. Categories with at least one out of stock product
            var q18 = from p in ListGenerator.ProductList
                      group p by p.Category into g
                      where g.Any(p => p.UnitsInStock == 0)
                      select g;
            Console.WriteLine("Categories with at least one out of stock product:");
            foreach (var g in q18)
            {
                Console.WriteLine($"Category: {g.Key}");
                foreach (var p in g) Console.WriteLine($"   {p.ProductName}");
            }
            Console.WriteLine(new string('-', 50));

            // 3. Categories with all products in stock
            var q19 = from p in ListGenerator.ProductList
                      group p by p.Category into g
                      where g.All(p => p.UnitsInStock > 0)
                      select g;
            Console.WriteLine("Categories with all products in stock:");
            foreach (var g in q19)
            {
                Console.WriteLine($"Category: {g.Key}");
                foreach (var p in g) Console.WriteLine($"   {p.ProductName}");
            }
            Console.WriteLine(new string('-', 50));

            #endregion

            #region LINQ - Grouping Operators

            // 1. Group numbers by remainder when divided by 5
            List<int> numbersList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var q20 = from n in numbersList
                      group n by n % 5 into g
                      select g;
            Console.WriteLine("Group numbers by remainder % 5:");
            foreach (var g in q20)
            {
                Console.WriteLine($"Remainder {g.Key}: {string.Join(", ", g)}");
            }
            Console.WriteLine(new string('-', 50));

            // 2. Group words by first letter (dictionary)
            var q21 = from w in dict
                      group w by w[0] into g
                      select g;
            Console.WriteLine("Group words by first letter:");
            foreach (var g in q21.Take(5)) // just print few groups
            {
                Console.WriteLine($"{g.Key}: {string.Join(", ", g.Take(5))}...");
            }
            Console.WriteLine(new string('-', 50));

            // 3. Group words that consist of the same characters
            string[] arr = { "from", "salt", "earn", "last", "near", "form" };
            var q22 = from w in arr
                      group w by String.Concat(w.OrderBy(c => c)) into g
                      select g;
            Console.WriteLine("Group words with same characters:");
            foreach (var g in q22)
            {
                Console.WriteLine($"{g.Key}: {string.Join(", ", g)}");
            }
            Console.WriteLine(new string('-', 50));

            #endregion
        }
    }
}
