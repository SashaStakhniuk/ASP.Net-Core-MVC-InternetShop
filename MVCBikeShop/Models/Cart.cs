using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Bike bike,int quantity)
        {
            CartLine line = lineCollection.Where(x => x.Bike.BikeId == bike.BikeId).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Bike = bike,
                    Quantity=quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Bike bike)
        {
            lineCollection.RemoveAll(x => x.Bike.BikeId == bike.BikeId);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x=> x.Bike.Price* x.Quantity);
        }
        public IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public Bike Bike { get; set; }
        public int Quantity { get; set; }

    }
}
