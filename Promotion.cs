namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Promotion.
    /// </summary>
    public class Promotion : IPromotion, IEquatable<Promotion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Promotion"/> class.
        /// </summary>
        public Promotion()
        {
            this.PromotionConditions = new Dictionary<char, PromotionCondition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Promotion"/> class.
        /// </summary>
        /// <param name="promotionConditions">Set of promotion conditions to initialise with.</param>
        public Promotion(List<PromotionCondition> promotionConditions)
        {
        }

        /// <summary>
        /// Process promotion delegate.
        /// </summary>
        /// <param name="basket">Basket To which Promotion Will be Applied.</param>
        public delegate void ProcessBookCallback(Basket basket);

        /// <summary>
        /// Gets or sets promotionID property.
        /// </summary>
        public int PromotionID { get; set; }

        /// <summary>
        /// Gets Or Sets SubstituteUnitPrice.
        /// </summary>
        public int SubstituteUnitPrice { get; set; }

        /// <summary>
        /// Gets or Sets PromotionConditions property.
        /// </summary>
        public Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether promotion is to be applied to basket or not.
        /// </summary>
        public bool IsApplied { get; set; }

        // Call a passed-in delegate on each promotion to process it:
        // public void ProcessPromotions(ProcessBookCallback processBook)
        // {
        //    foreach (KeyValuePair<char,PromotionCondition> b in this.PromotionConditions)
        //    {
        //        if (b.Value.)
        //            // Calling the delegate:
        //            processBook(b);
        //    }
        // }

        public void ApplyPromotion(Basket basket)
        {
            int noOfTimesPromotionCanBeApplied = this.NoOftimespromotionCanBeApplied(basket);
            foreach (var pc in this.PromotionConditions)
            {
                basket.StockKeepingUnitsNotcoveredByPromotion[pc.Value.SkuId] = basket.StockKeepingUnits[pc.Value.SkuId] - (noOfTimesPromotionCanBeApplied * pc.Value.Quantity);
            }

            basket.promotionCoveredTotalCost += this.SubstituteUnitPrice * noOfTimesPromotionCanBeApplied;
        }

        /// <summary>
        /// No of times promotion can be applied.
        /// </summary>
        /// <param name="basket">basket to Apply Promotion to.</param>
        /// <returns>int.</returns>
        public int NoOftimespromotionCanBeApplied(Basket basket)
        {
            List<int> numberOfTimesEachPromotionCanBeApplied = new List<int>();
            foreach (var pc in this.PromotionConditions)
            {
                numberOfTimesEachPromotionCanBeApplied.Add(pc.Value.NoOftimesPromotionConditionCanBeApplied(basket));
            }

            return numberOfTimesEachPromotionCanBeApplied.Min(z => z);
        }

        /// <summary>
        /// Adds Promotion condition.
        /// </summary>
        /// <param name="promotionCondition">PromotionCondition.</param>
        public void AddPromotionCondition(PromotionCondition promotionCondition)
        {
            this.PromotionConditions.Add(promotionCondition.SkuId, promotionCondition);
        }

        /// <summary>
        /// Implemts Equatable interface.
        /// </summary>
        /// <param name="other">promotion to compare with.</param>
        /// <returns>bool.</returns>
        public bool Equals(Promotion other)
        {
            if (this.PromotionID == other.PromotionID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
