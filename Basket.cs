namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Basket Class.
    /// </summary>
    public class Basket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Basket"/> class.
        /// </summary>
        public Basket()
        {
            this.StockKeepingUnits = new Dictionary<char, int>();
            this.AppliedPromotions = new List<Promotion>();
        }

        /// <summary>
        /// Gets or sets AppliedPromotions Property.
        /// </summary>
        public List<Promotion> AppliedPromotions { get; set; }

        /// <summary>
        /// Gets or sets StockKeepingUnits Property.
        /// </summary>
        public Dictionary<char, int> StockKeepingUnits { get; set; }

        /// <summary>
        /// Adds Item to basket.
        /// </summary>
        /// <param name="stockKeepingUnit">Char representing A StockKeepingUnitID.</param>
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

        /// <summary>
        /// Removes item from Basket.
        /// </summary>
        /// <param name="stockKeepingUnit">Char representing A StockKeepingUnitID.</param>
        public void RemoveItem(char stockKeepingUnit)
        {
            if (this.StockKeepingUnits[stockKeepingUnit] > 0)
            {
                this.StockKeepingUnits[stockKeepingUnit] = --this.StockKeepingUnits[stockKeepingUnit];
            }
        }

        /// <summary>
        /// Sets Quantity for sku.
        /// </summary>
        /// <param name="stockKeepingUnit">Char representing A StockKeepingUnitID.</param>
        /// <param name="quantity">int representing quantity.</param>
        public void SetQuantity(char stockKeepingUnit, int quantity)
        {
            this.StockKeepingUnits[stockKeepingUnit] = quantity;
        }

        /// <summary>
        /// Adds Promotion.
        /// </summary>
        /// <param name="promotion">Promotion to Add.</param>
        public void AddPromotion(Promotion promotion)
        {
            this.AppliedPromotions.Add(promotion);
        }

        /// <summary>
        /// Calculates Cost Without Promotions.
        /// </summary>
        /// <param name="skus">Dictionary containing Prices.</param>
        /// <returns>int.</returns>
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

        /// <summary>
        /// Calculates Cost With Promotions.
        /// </summary>
        /// <param name="skus">Dictionary containing Prices.</param>
        /// <returns>int.</returns>
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

        /// <summary>
        /// No of times promotion can be applied.
        /// </summary>
        /// <param name="promotion">Promotion to Apply.</param>
        /// <returns>int.</returns>
        public int NoOftimespromotionCanBeApplied(Promotion promotion)
        {
            List<int> numberOfTimesEachPromotionCanBeApplied = new List<int>();
            foreach (var pc in promotion.PromotionConditions)
            {
                numberOfTimesEachPromotionCanBeApplied.Add(this.NoOftimesPromotionConditionCanBeApplied(pc.Value));
            }

            return numberOfTimesEachPromotionCanBeApplied.Min(z => z);
        }

        /// <summary>
        /// No of times Promotion Condition Can Be Applied.
        /// </summary>
        /// <param name="promotionCondition">The promotion Condition that needs to be satisfied.</param>
        /// <returns>int.</returns>
        public int NoOftimesPromotionConditionCanBeApplied(PromotionCondition promotionCondition)
        {
            int remainder = this.StockKeepingUnits[promotionCondition.SkuId] % promotionCondition.Quantity;
            return (this.StockKeepingUnits[promotionCondition.SkuId] - remainder) / promotionCondition.Quantity;
        }
    }
}
