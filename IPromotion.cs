using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IPromotion
    {
        Dictionary<char, PromotionCondition> PromotionConditions { get; set; }
        int PromotionID { get; set; }

        void AddPromotionCondition(PromotionCondition promotionCondition) ;
    }
}