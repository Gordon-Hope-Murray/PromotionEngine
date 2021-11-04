namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Promotion.
    /// </summary>
    public class PromotionFlat : PromotionBase, IPromotion, IEquatable<PromotionBase>, IEquatable<IPromotion>
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
        /// Apply Flat Promotion.
        /// </summary>
        /// <param name="basket">basket to Apply Flat Promotion.</param>
        public override void ApplyPromotion(IBasket basket)
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
        public bool Equals(IPromotion other)
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
