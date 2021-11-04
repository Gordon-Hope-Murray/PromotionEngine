using System.Collections.Generic;

namespace PromotionEngine
{
    public interface IBasket
    {
        PromotionList AppliedPromotions { get; set; }
        int PromotionCoveredTotalCost { get; set; }
        Dictionary<char, int> StockKeepingUnits { get; set; }
        Dictionary<char, int> StockKeepingUnitsNotcoveredByPromotion { get; set; }

        void AddItem(char stockKeepingUnit);
        void AddPromotion(PromotionBase promotion);
        void ApplyPromotions();
        int CalculateCost(Dictionary<char, int> priceList, Dictionary<char, int> quantities);
        int CalculateCostWithPromotions(Dictionary<char, int> priceList);
        int NoOftimespromotionCanBeApplied(IPromotion promotion);
        int NoOftimesPromotionConditionCanBeApplied(IPromotionCondition promotionCondition);
        void RemoveItem(char stockKeepingUnit);
        void SetQuantity(char stockKeepingUnit, int quantity);
    }
}