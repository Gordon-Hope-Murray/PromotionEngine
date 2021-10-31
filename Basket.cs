using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Basket 
    {
        public List<Promotion> appliedPromotions { get; set; }

        public void ApplyPromotion (Promotion promotion)
        { this.appliedPromotions.Add(promotion); }

        public float GetPrice ()
        {
            throw new Exception("Getprice is not implemented for Basket");
        }
    }
}
