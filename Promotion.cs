using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Promotion : IPromotion , IEquatable<Promotion>
    {
        public int PromotionID { get; set; }

        public Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        public Promotion()
        {
            this.PromotionConditions = new Dictionary<char, PromotionCondition>() ;
        }

        public Promotion(List<PromotionCondition> promotions)
        {

        }

        public void AddPromotionCondition(PromotionCondition promotionCondition)
        {
            this.PromotionConditions.Add(promotionCondition.SkuId, promotionCondition);
        }

        public bool Equals(Promotion other)
        {
            if(this.PromotionID == other.PromotionID )
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
