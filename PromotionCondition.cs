using System;

namespace PromotionEngine
{
    public class PromotionCondition : IEquatable<PromotionCondition>
    {
        public char SkuId { get; set; }

        public int Quantity { get; set; }
        public int SubstituteUnitPrice { get; set; }
        
        public PromotionCondition()
        { }

        public bool Equals(PromotionCondition other)
        {
           if (this.SkuId == other.SkuId )
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
