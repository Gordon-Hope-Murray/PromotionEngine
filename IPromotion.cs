namespace PromotionEngine
{
    using System.Collections.Generic;

    public interface IPromotion
    {
        bool IsApplied { get; set; }

        Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        int PromotionID { get; set; }

        int SubstituteUnitPrice { get; set; }

        void AddPromotionCondition(IPromotionCondition promotionCondition);

        void ApplyPromotion(IBasket basket);

        bool Equals(IPromotion other);

        int NoOftimespromotionCanBeApplied(IBasket basket);
    }
}