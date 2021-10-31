using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PromotionEngine;

namespace TestProject
{
    class TestStockKeepingUnit
    {

        [Test]
        public void PomotionConditionHasCharStockKeepingUnitIdPriceField()
        {
            StockKeepingUnit sku = new StockKeepingUnit();
            if ((sku.GetType().GetProperty("StockKeepingUnitId") == null) || (sku.GetType().GetProperty("StockKeepingUnitId").PropertyType != typeof(char)))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

        [Test]
        public void PomotionConditionHasIntUnitPriceField()
        {
            StockKeepingUnit sku = new StockKeepingUnit();
            if ((sku.GetType().GetProperty("UnitPrice") == null) || (sku.GetType().GetProperty("UnitPrice").PropertyType != typeof(int)))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

    }
}
