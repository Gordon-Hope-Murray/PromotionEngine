using NUnit.Framework;
using PromotionEngine;
using System.Collections.Generic;

namespace TestProject
{
    public class TestBasket
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasketHasPromotionsField()
        {
            Basket basket = new Basket();
            if (basket.GetType().GetProperty("AppliedPromotions") == null || basket.GetType().GetProperty("AppliedPromotions").PropertyType != typeof(List<Promotion>))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

        [Test]
        public void BasketHaStockKeepingUnitsField()
        {
            Basket basket = new Basket();
            if (basket.GetType().GetProperty("StockKeepingUnits") == null || basket.GetType().GetProperty("StockKeepingUnits").PropertyType != typeof(Dictionary<char, int>))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

        [Test]
        public void AddsItemToBasket()
        {
            Basket basket = new Basket();
            basket.AddItem('A');
            if (basket.StockKeepingUnits['A'] != 1)
            {
                Assert.Fail();
            }

        }



    }
}
