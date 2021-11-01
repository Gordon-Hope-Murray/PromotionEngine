namespace PromotionEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// Promotion Interface.
    /// </summary>
    public interface IPromotion
    {
        /// <summary>
        /// Gets or Sets PromotionConditions.
        /// </summary>
        Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        /// <summary>
        /// Gets or Sets PromotionID.
        /// </summary>
        int PromotionID { get; set; }

        /// <summary>
        /// Adds Promotion Condition.
        /// </summary>
        /// <param name="promotionCondition">The promotionConditionbeing Added.</param>
        void AddPromotionCondition(PromotionCondition promotionCondition);
    }
}