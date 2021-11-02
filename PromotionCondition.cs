namespace PromotionEngine
{
    using System;

    /// <summary>
    /// PromotionCondition.
    /// </summary>
    public class PromotionCondition : IEquatable<PromotionCondition>, IPromotionCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionCondition"/> class.
        /// </summary>
        public PromotionCondition()
        {
        }

        /// <summary>
        /// Gets Or Sets PromotionConditionID.
        /// </summary>
        public int PromotionConditionID { get; set; }

        /// <summary>
        /// Gets Or Sets SkuID.
        /// </summary>
        public char SkuId { get; set; }

        /// <summary>
        /// Gets Or Sets Quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets Or Sets SubstituteUnitPrice.
        /// </summary>
        public int SubstituteUnitPrice { get; set; }

        /// <summary>
        /// Implements Equatable.
        /// </summary>
        /// <param name="other">Promotion condition being compared to.</param>
        /// <returns>bool indicating equality.</returns>
        public bool Equals(PromotionCondition other)
        {
            if (this.SkuId == other.SkuId)
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
