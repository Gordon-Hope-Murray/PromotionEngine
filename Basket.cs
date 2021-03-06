namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Basket Class.
    /// </summary>
    public class Basket : IBasket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Basket"/> class.
        /// </summary>
        public Basket()
        {
            this.StockKeepingUnits = new Dictionary<char, int>();
            this.StockKeepingUnitsNotcoveredByPromotion = new Dictionary<char, int>();
            this.AppliedPromotions = new PromotionList();
        }

        public int PromotionCoveredTotalCost { get; set; }

        /// <summary>
        /// Gets or sets AppliedPromotions Property.
        /// </summary>
        public PromotionList AppliedPromotions { get; set; }

        /// <summary>
        /// Gets or sets StockKeepingUnits Property.
        /// </summary>
        public Dictionary<char, int> StockKeepingUnits { get; set; }

        /// <summary>
        /// Gets or sets StockKeepingUnits Property.
        /// </summary>
        public Dictionary<char, int> StockKeepingUnitsNotcoveredByPromotion { get; set; }

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
        public void AddPromotion(PromotionBase promotion)
        {
            this.AppliedPromotions.Add(promotion);
        }

        /// <summary>
        /// Calculates Cost Without Promotions.
        /// </summary>
        /// <param name="priceList">Dictionary containing Prices.</param>
        /// <param name="quantities">Dictionary containing Quantities.</param>
        /// <returns>int.</returns>
        public int CalculateCost(Dictionary<char, int> priceList, Dictionary<char, int> quantities)
        {
            var prices = from id in priceList.Keys
                         where quantities.ContainsKey(id)
                         let quantity = quantities[id]
                         let price = priceList[id]
                         select new { id, quantity, price };

            var totals = from price in prices select price.quantity * price.price;

            return totals.Sum();
        }

        /// <summary>
        /// Applies Promotions.
        /// </summary>
        public void ApplyPromotions()
        {
            foreach (var promotion in this.AppliedPromotions)
            {
                if (promotion.IsApplied)
                {
                    promotion.ApplyPromotion(this);
                }
            }
        }

        /// <summary>
        /// Applies Promotions and returns total cost with promotions.
        /// </summary>
        /// <param name="priceList">Dictionary.<char, int>.</param>
        /// <returns>cost.</returns>
        public int CalculateCostWithPromotions(Dictionary<char, int> priceList)
        {
            this.PromotionCoveredTotalCost = 0;
            this.ApplyPromotions();

            return this.PromotionCoveredTotalCost + this.CalculateCost(priceList, this.StockKeepingUnitsNotcoveredByPromotion);
        }

        /// <summary>
        /// No of times promotion can be applied.
        /// </summary>
        /// <param name="promotion">Promotion to Apply.</param>
        /// <returns>int.</returns>
        public int NoOftimespromotionCanBeApplied(IPromotion promotion)
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
        public int NoOftimesPromotionConditionCanBeApplied(IPromotionCondition promotionCondition)
        {
            int remainder = this.StockKeepingUnits[promotionCondition.SkuId] % promotionCondition.Quantity;
            return (this.StockKeepingUnits[promotionCondition.SkuId] - remainder) / promotionCondition.Quantity;
        }
    }
}
