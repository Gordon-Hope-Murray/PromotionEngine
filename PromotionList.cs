namespace PromotionEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PromotionList : IEnumerable<PromotionBase>
    {
        private List<PromotionBase> promotions = new List<PromotionBase>();

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="index">int.</param>
        /// <returns>PromotionBase.</returns>
        public PromotionBase this[int index]
        {
            get => this.promotions[index];
            set => this.promotions[index] = value;
        }

        public void Add(PromotionBase promotion)
        {
            this.promotions.Add(promotion);
        }

        public int Count()
        {
            return this.promotions.Count();
        }

        public IEnumerator<PromotionBase> GetEnumerator()
        {
            return ((IEnumerable<PromotionBase>)this.promotions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.promotions).GetEnumerator();
        }
    }
}
