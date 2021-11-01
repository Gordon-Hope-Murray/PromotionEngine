namespace PromotionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StockKeepingUnit
    {
        public StockKeepingUnit()
        {
        }

        public StockKeepingUnit(char stockKeepingUnitId, int unitPrice)
        {
            this.StockKeepingUnitId = stockKeepingUnitId;
            this.UnitPrice = unitPrice;
        }

        public char StockKeepingUnitId { get; set; }

        public int UnitPrice { get; set; }
    }
}
