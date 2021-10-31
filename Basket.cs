using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Basket 
    {
        public List<Promotion> AppliedPromotions { get; set; }

        public Dictionary<char,int> StockKeepingUnits { get; }

        public Basket()
        {
            this.StockKeepingUnits = new Dictionary<char, int>();
        }

        public void AddItem(char stockKeepingUnit)
        {
            if(!this.StockKeepingUnits.ContainsKey(stockKeepingUnit))
            {
                this.StockKeepingUnits.Add(stockKeepingUnit, 1);
            }
            else
            {
                this.StockKeepingUnits[stockKeepingUnit] = ++this.StockKeepingUnits[stockKeepingUnit];
            }
            
        }

        public void RemoveItem(char stockKeepingUnit)
        {
            if (this.StockKeepingUnits[stockKeepingUnit] > 0)
            {
                this.StockKeepingUnits[stockKeepingUnit] = --this.StockKeepingUnits[stockKeepingUnit];
            }
        }

        public void SetQuantity(char stockKeepingUnit, int quantity)
        {
            this.StockKeepingUnits[stockKeepingUnit] = quantity;
        }

        public void AddPromotion (Promotion promotion)
        { 
            this.AppliedPromotions.Add(promotion); 
        }

        public float GetPrice ()
        {
            throw new Exception("Getprice is not implemented for Basket");
        }
    }
}
