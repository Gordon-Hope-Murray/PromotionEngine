using NUnit.Framework;
using PromotionEngine;
using System.Collections.Generic;

namespace TestProject
{
    public class TestBasket
    {
        private List<Promotion> promotions;

        [SetUp]
        public void Setup()
        {
            this.promotions = new List<Promotion>
            {
                new Promotion
                {
                    PromotionID = 1,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'A',
                            new PromotionCondition {SkuId ='A', Quantity= 3, SubstituteUnitPrice= 130 }
                        }
                    }
                },

                new Promotion
                {
                    PromotionID = 2,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'B',
                            new PromotionCondition {SkuId ='B', Quantity = 2, SubstituteUnitPrice = 45 }
                        },
                        {
                            'D',
                            new PromotionCondition {SkuId ='D', Quantity = 1, SubstituteUnitPrice = 130 }
                        }
                    }
                }
            };
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

        [Test]
        public void RemovesItemToBasket()
        {
            Basket basket = new Basket();
            basket.AddItem('A');

            if (basket.StockKeepingUnits['A'] != 1)
            {
                Assert.Fail();
            }

            basket.RemoveItem('A');

            if (basket.StockKeepingUnits['A'] != 0)
            {
                Assert.Fail();
            }

        }


        [Test]
        public void SetsQuantityofItemInBasket()
        {
            Basket basket = new Basket();
            basket.SetQuantity('A',4);

            if (basket.StockKeepingUnits['A'] != 4)
            {
                Assert.Fail();
            }

        }

        [Test]
        public void AddsPromotions()
        {
            Basket basket = new Basket();

            foreach (Promotion p in this.promotions)
            {
                basket.AddPromotion(p);
            }          
        }
    }
}
