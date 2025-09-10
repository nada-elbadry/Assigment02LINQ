using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LINQ02.Data
{
    internal class Product : IComparable<Product>
    {
        public int CompareTo(Product? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other == null) return 1;
            return this.UnitPrice.CompareTo(other.UnitPrice);
        }
        public long ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }


        public override string ToString()
            => $"ProductID:{ProductID},ProductName:{ProductName},Category{Category},UnitPrice:{UnitPrice},UnitsInStock:{UnitsInStock}";

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   ProductID == product.ProductID &&
                   ProductName == product.ProductName &&
                   Category == product.Category &&
                   UnitPrice == product.UnitPrice &&
                   UnitsInStock == product.UnitsInStock;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductID, ProductName, Category, UnitPrice, UnitsInStock);
        }

        public bool Equals(Product? product)
        {
            if (product is null) return false;
            return ProductID == product.ProductID &&
                               ProductName == product.ProductName &&
                               Category == product.Category &&
                               UnitPrice == product.UnitPrice &&
                               UnitsInStock == product.UnitsInStock;
        }
    }

    class Product02 : Product
    {
        public int SerialNumber { get; set; }
    }
}
