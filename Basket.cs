namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Basket
    {
        private int total;

        public Basket()
        {
            this.StockKeepingUnits = new Dictionary<char, int>();
            this.AppliedPromotions = new List<Promotion>();
        }

        public List<Promotion> AppliedPromotions { get; set; }

        public Dictionary<char, int> StockKeepingUnits { get; set; }

        public void AddItem(char stockKeepingUnit)
        {
            if (!this.StockKeepingUnits.ContainsKey(stockKeepingUnit))
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

        public void AddPromotion(Promotion promotion)
        {
            this.AppliedPromotions.Add(promotion);
        }

        public int CalculateCost(Dictionary<char, int> skus)
        {
            var prices = from id in skus.Keys
                          where this.StockKeepingUnits.ContainsKey(id)
                          let quantity = this.StockKeepingUnits[id]
                          let price = skus[id]
                          select new { id, quantity, price };

            var totals = from price in prices select price.quantity * price.price;

            return totals.Sum();
        }

        public int CalculateCostWithPromotions(Dictionary<char, int> skus)
        {
            int promotionCoveredTotalCost = 0;
            Dictionary<char, int> copyOfBasketItemCount = this.StockKeepingUnits.ToDictionary(p => p.Key, p => p.Value);
            foreach (Promotion p in this.AppliedPromotions)
            {
                int noOftimespromotionCanBeApplied = this.NoOftimespromotionCanBeApplied(p);
                foreach (var pc in p.PromotionConditions)
                {
                    copyOfBasketItemCount[pc.Key] = copyOfBasketItemCount[pc.Key] - (noOftimespromotionCanBeApplied * pc.Value.Quantity);
                    promotionCoveredTotalCost += pc.Value.SubstituteUnitPrice * noOftimespromotionCanBeApplied;
                }
            }

            Basket nonpromobasket = new Basket();
            nonpromobasket.StockKeepingUnits = copyOfBasketItemCount;
            nonpromobasket.CalculateCost(skus);
            return promotionCoveredTotalCost + nonpromobasket.CalculateCost(skus);
        }

        public int NoOftimespromotionCanBeApplied(Promotion promotion)
        {
            List<int> numberOfTimesEachPromotionCanBeApplied = new List<int>();
            foreach (var pc in promotion.PromotionConditions)
            {
                numberOfTimesEachPromotionCanBeApplied.Add(this.NoOftimesPromotionConditionCanBeApplied(pc.Value));
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
