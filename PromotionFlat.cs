namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Promotion.
    /// </summary>
    public class PromotionFlat : PromotionBase, IPromotion, IEquatable<PromotionFlat>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionFlat"/> class.
        /// </summary>
        public PromotionFlat()
        {
            this.PromotionConditions = new Dictionary<char, PromotionCondition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionFlat"/> class.
        /// </summary>
        /// <param name="promotionConditions">Set of promotion conditions to initialise with.</param>
        public PromotionFlat(List<PromotionCondition> promotionConditions)
        {
        }

        /// <summary>
        /// Process promotion delegate.
        /// </summary>
        /// <param name="basket">Basket To which Promotion Will be Applied.</param>
        public delegate void ProcessBookCallback(Basket basket);

        // Call a passed-in delegate on each promotion to process it:
        // public void ProcessPromotions(ProcessBookCallback processPromotion)
        // {
        //    foreach (KeyValuePair<char,PromotionCondition> b in this.PromotionConditions)
        //    {
        //        if (b.Value.)
        //            // Calling the delegate:
        //            processPromotion(b);
        //    }
        // }

        /// <summary>
        /// Apply Flat Promotion.
        /// </summary>
        /// <param name="basket">basket to Apply Flat Promotion.</param>
        public override void ApplyPromotion(Basket basket)
        {
            int noOfTimesPromotionCanBeApplied = this.NoOftimespromotionCanBeApplied(basket);
            foreach (var pc in this.PromotionConditions)
            {
                basket.StockKeepingUnitsNotcoveredByPromotion[pc.Value.SkuId] = basket.StockKeepingUnits[pc.Value.SkuId] - (noOfTimesPromotionCanBeApplied * pc.Value.Quantity);
            }

            basket.PromotionCoveredTotalCost += this.SubstituteUnitPrice * noOfTimesPromotionCanBeApplied;
        }

        /// <summary>
        /// Implemts Equatable interface.
        /// </summary>
        /// <param name="other">promotion to compare with.</param>
        /// <returns>bool.</returns>
        public bool Equals(PromotionFlat other)
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
