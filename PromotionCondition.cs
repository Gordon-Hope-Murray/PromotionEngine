namespace PromotionEngine
{
    using System;

    public class PromotionCondition : IEquatable<PromotionCondition>, IPromotionCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionCondition"/> class.
        /// </summary>
        public PromotionCondition()
        {
        }

        public char SkuId { get; set; }

        public int Quantity { get; set; }

        public int SubstituteUnitPrice { get; set; }

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
