using System;

namespace Store.Model
{
    public class Food
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }

        public double PricePerKg { get; set; }

        public double QuantityInStockKg { get; set; }
    }
}
