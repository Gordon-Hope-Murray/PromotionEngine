namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents Promotion with Percentage based rules.
    /// </summary>
    public class PromotionPercentage : PromotionBase, IPromotion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionPercentage"/> class.
        /// </summary>
        public PromotionPercentage()
        {
            this.PromotionConditions = new Dictionary<char, PromotionCondition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionPercentage"/> class.
        /// </summary>
        /// <param name="promotionConditions">Set of promotion conditions to initialise with.</param>
        public PromotionPercentage(List<PromotionCondition> promotionConditions)
        {
        }

        /// <summary>
        /// Apply Percentage Promotion.
        /// </summary>
        /// <param name="basket">basket to Apply Promotion to.</param>
        public override void ApplyPromotion(IBasket basket)
        {
          throw new Exception("PromotionPercentage.ApplyPromotion not implemented");
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
