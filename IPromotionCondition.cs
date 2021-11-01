namespace PromotionEngine
{
    public interface IPromotionCondition
    {
        int Quantity { get; set; }

        char SkuId { get; set; }

        int SubstituteUnitPrice { get; set; }

        bool Equals(PromotionCondition other);
    }
}