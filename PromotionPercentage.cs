using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
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
        public override void ApplyPromotion(Basket basket)
        {
          throw new Exception("PromotionPercentage.ApplyPromotion not implemented");
        }
    }
}
