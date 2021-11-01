namespace TestProject
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using PromotionEngine;

    public class TestBasket
    {
        private List<Promotion> promotions;
        private List<StockKeepingUnit> skus;

        [SetUp]
        public void Setup()
        {
            this.skus = new List<StockKeepingUnit>
            {
                new StockKeepingUnit { StockKeepingUnitId = 'A', UnitPrice = 50 },
                new StockKeepingUnit { StockKeepingUnitId = 'B', UnitPrice = 30 },
                new StockKeepingUnit { StockKeepingUnitId = 'C', UnitPrice = 20 },
                new StockKeepingUnit { StockKeepingUnitId = 'D', UnitPrice = 15 },
            };

            this.promotions = new List<Promotion>
            {
                new Promotion
                {
                    PromotionID = 1,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'A',
                            new PromotionCondition { SkuId = 'A', Quantity = 3, SubstituteUnitPrice = 130 }
                        },
                    },
                },

                new Promotion
                {
                    PromotionID = 2,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'B',
                            new PromotionCondition { SkuId = 'B', Quantity = 2, SubstituteUnitPrice = 45 }
                        },
                        {
                            'D',
                            new PromotionCondition { SkuId = 'D', Quantity = 1, SubstituteUnitPrice = 130 }
                        },
                    },
                },
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
        public void RemovesItemFromBasket()
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
            basket.SetQuantity('A', 4);

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

        [Test]
        public void GetsPrice()
        {
            Basket basket = new Basket();

            foreach (Promotion p in this.promotions)
            {
                basket.AddPromotion(p);
            }

            basket.SetQuantity('A', 10);
            basket.SetQuantity('B', 10);
            basket.SetQuantity('C', 10);

            float price = basket.CalculateCost(this.skus);
        }

        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(4, 1, 1)]
        [TestCase(4, 2, 2)]
        public void GetsNoOftimesPromotionCanBeApplied(int unitsB, int unitsD, int exptResult)
        {
            Basket basket = new Basket();
            basket.StockKeepingUnits['B'] = unitsB;
            basket.StockKeepingUnits['D'] = unitsD;
            int result = basket.NoOftimespromotionCanBeApplied(this.promotions[1]);
            Assert.AreEqual(exptResult, result);
        }

        [TestCase(9, 3)]
        [TestCase(10, 3)]
        [TestCase(11, 3)]
        [TestCase(12, 4)]
        public void GetsNoOftimesPromotionConditionCanBeApplied(int units, int exptResult)
        {
            Basket basket = new Basket();
            basket.StockKeepingUnits['A'] = units;
            int result = basket.NoOftimesPromotionConditionCanBeApplied(this.promotions[0].PromotionConditions['A']);
            Assert.AreEqual(exptResult, result);
        }
    }
}
