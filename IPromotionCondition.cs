namespace PromotionEngine
{
    public interface IPromotionCondition
    {
        int PromotionConditionID { get; set; }
        int Quantity { get; set; }
        char SkuId { get; set; }
        int SubstituteUnitPrice { get; set; }

        bool Equals(IPromotionCondition other);
        int NoOftimesPromotionConditionCanBeApplied(IBasket basket);
    }
}