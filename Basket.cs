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

        private int total;

        public Basket()
        {
            this.StockKeepingUnits = new Dictionary<char, int>();
            this.AppliedPromotions = new List<Promotion>();
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

        public float CalculateCost (List<StockKeepingUnit> skus)
        {
            var prices = from sku in skus where this.StockKeepingUnits.ContainsKey(sku.StockKeepingUnitId) select this.StockKeepingUnits[sku.StockKeepingUnitId] * sku.UnitPrice ;
           
                return this.total;
        }

        public int NoOftimespromotionCanBeApplied(Promotion promotion)
        {
            List<int> numberOfTimesEachPromotionCanBeApplied = new List<int>();
            foreach( var pc in promotion.PromotionConditions)
            {
                numberOfTimesEachPromotionCanBeApplied.Add(NoOftimesPromotionConditionCanBeApplied(pc.Value));
            }
            return numberOfTimesEachPromotionCanBeApplied.Min(z => z);
        }

        public int NoOftimesPromotionConditionCanBeApplied(PromotionCondition promotionCondition)
        {
            int remainder = this.StockKeepingUnits[promotionCondition.SkuId] % promotionCondition.Quantity;
            return (this.StockKeepingUnits[promotionCondition.SkuId] - remainder) / promotionCondition.Quantity;
        }
    }
}
