using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Promotion
    {
        public int PromotionID { get; set; }

        public Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        public Promotion()
        {
        }

        public Promotion(List<PromotionCondition> promotions)
        {

        }

        public void AddPromotionCondition()
        {
            throw new Exception("AddPromotionCondition not implemented in Promotion");
        }
}
    public class PromotionCondition
    {
        public char SkuId { get; set; }

        public int Quantity { get; set; }
        public int SubstituteUnitPrice { get; set; }
        
        public PromotionCondition()
        { }
    }
}
