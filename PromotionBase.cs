namespace PromotionEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public enum PromotionType
    {
        /// <summary>
        /// Applies a flat subsitute value
        /// </summary>
        Flat,

        /// <summary>
        /// Calculates a percentage substitute value.
        /// </summary>
        Percentage,
    }

    /// <summary>
    /// Base Promotion Class.
    /// </summary>
    public abstract class PromotionBase : IEqualityComparer<PromotionBase>, IEnumerable<PromotionBase>
    {
        /// <summary>
        /// Gets or sets promotionID property.
        /// </summary>
        public int PromotionID { get; set; }

        /// <summary>
        /// Gets Or Sets PromotionType.
        /// </summary>
        public PromotionType PromotionType { get; set; }

        /// <summary>
        /// Gets Or Sets SubstituteUnitPrice.
        /// </summary>
        public int SubstituteUnitPrice { get; set; }

        /// <summary>
        /// Gets or Sets PromotionConditions property.
        /// </summary>
        public Dictionary<char, PromotionCondition> PromotionConditions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether promotion is to be applied to basket or not.
        /// </summary>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Adds Promotion condition.
        /// </summary>
        /// <param name="promotionCondition">PromotionCondition.</param>
        public void AddPromotionCondition(IPromotionCondition promotionCondition)
        {
            this.PromotionConditions.Add(promotionCondition.SkuId, (PromotionCondition)promotionCondition);
        }

        /// <summary>
        /// Apply Promotion Method Declaration.
        /// </summary>
        /// <param name="basket">Basket to Apply Promotion to.</param>
        public abstract void ApplyPromotion(IBasket basket);

        /// <summary>
        /// Implemts Equatable interface.
        /// </summary>
        /// <param name="other">promotion to compare with.</param>
        /// <returns>bool.</returns>
        public bool Equals(PromotionBase other)
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

        /// <summary>
        /// Implemts Equatable interface.
        /// </summary>
        /// <param name="x">PromotionBase X.</param>
        /// <param name="y">PromotionBase Y.</param>
        /// <returns>bool.</returns>
        public bool Equals([AllowNull] PromotionBase x, [AllowNull] PromotionBase y)
        {
            if (x.PromotionID == y.PromotionID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<PromotionBase> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int GetHashCode([DisallowNull] PromotionBase obj)
        {
            throw new System.NotImplementedException();
        }

        //public IEnumerator<PromotionBase> GetEnumerator()
        //{
        //    return (IEnumerator<PromotionBase>)(IEnumerator)this.GetEnumerator();
        //}

        /// <summary>
        /// No of times promotion can be applied.
        /// </summary>
        /// <param name="basket">basket to Apply Promotion to.</param>
        /// <returns>int.</returns>
        public int NoOftimespromotionCanBeApplied(IBasket basket)
        {
            List<int> numberOfTimesEachPromotionCanBeApplied = new List<int>();
            foreach (var pc in this.PromotionConditions)
            {
                numberOfTimesEachPromotionCanBeApplied.Add(pc.Value.NoOftimesPromotionConditionCanBeApplied((Basket)basket));
            }

            return numberOfTimesEachPromotionCanBeApplied.Min(z => z);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //
        //    return (IEnumerator<PromotionBase>)(IEnumerator)this.GetEnumerator();
        //}
    }
}