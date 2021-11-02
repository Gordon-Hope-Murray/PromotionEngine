namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Promotion.
    /// </summary>
    public class Promotion : IPromotion, IEquatable<Promotion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Promotion"/> class.
        /// </summary>
        public Promotion()
        {
            this.PromotionConditions = new Dictionary<char, PromotionCondition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Promotion"/> class.
        /// </summary>
        /// <param name="promotionConditions">Set of promotion conditions to initialise with.</param>
        public Promotion(List<PromotionCondition> promotionConditions)
        {
        }

        /// <summary>
        /// Gets or sets promotionID property.
        /// </summary>
        public int PromotionID { get; set; }

        /// <summary>
        /// Gets or Sets PromotionConditions property.
        /// </summary>
        public Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        /// <summary>
        /// Adds Promotion condition.
        /// </summary>
        /// <param name="promotionCondition">PromotionCondition.</param>
        public void AddPromotionCondition(PromotionCondition promotionCondition)
        {
            this.PromotionConditions.Add(promotionCondition.SkuId, promotionCondition);
        }

        /// <summary>
        /// Implemts Equatable interface.
        /// </summary>
        /// <param name="other">promotion to compare with.</param>
        /// <returns>bool.</returns>
        public bool Equals(Promotion other)
        {
            if (this.PromotionID == other.PromotionID)
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
