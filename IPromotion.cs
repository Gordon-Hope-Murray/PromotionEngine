using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IPromotion
    {
        bool IsApplied { get; set; }
        Dictionary<char, PromotionCondition> PromotionConditions { get; set; }
        int PromotionID { get; set; }
        int SubstituteUnitPrice { get; set; }

        void AddPromotionCondition(PromotionCondition promotionCondition);
        void ApplyPromotion(Basket basket);
        bool Equals(PromotionBase other);
        int NoOftimespromotionCanBeApplied(Basket basket);
    }
}