using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class StockKeepingUnit
    {
        public char StockKeepingUnitId { get; set; }
        public int UnitPrice { get; set; }

        public StockKeepingUnit()
        { }

        public StockKeepingUnit (char stockKeepingUnitId, int unitPrice)
        {
            StockKeepingUnitId = stockKeepingUnitId;
            UnitPrice = unitPrice;
        }
    }

}
