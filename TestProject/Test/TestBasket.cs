namespace TestProject
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using PromotionEngine;

    public class TestBasket
    {
        private List<Promotion> promotions;
        private Dictionary<char, int> skusd;

        [SetUp]
        public void Setup()
        {
            this.skusd = new Dictionary<char, int>()
            {
                { 'A', 50 },
                { 'B', 30 },
                { 'C', 20 },
                { 'D', 15 },
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
                    },
                },
                new Promotion
                {
                    PromotionID = 3,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'C',
                            new PromotionCondition { SkuId = 'C', Quantity = 1, }
                        },
                        {
                            'D',
                            new PromotionCondition { SkuId = 'D', Quantity = 1, }
                        },
                    },
                    SubstituteUnitPrice = 30,
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

        [TestCase(2, 1, 1, 0, ExpectedResult = 150)]
        [TestCase(2, 2, 1, 0, ExpectedResult = 180)]
        [TestCase(2, 1, 2, 0, ExpectedResult = 170)]
        [TestCase(2, 1, 2, 1, ExpectedResult = 185)]

        public int GetsPriceWithoutPromotion(int unitsA, int unitsB, int unitsC, int unitsD)
        {
            Basket basket = new Basket();

            foreach (Promotion p in this.promotions)
            {
                basket.AddPromotion(p);
            }

            basket.SetQuantity('A', unitsA);
            basket.SetQuantity('B', unitsB);
            basket.SetQuantity('C', unitsC);
            basket.SetQuantity('D', unitsD);

            int price = basket.CalculateCost(this.skusd);
            return price;
        }


        [TestCase(3, 0, 0, 0, ExpectedResult = 130)]
        [TestCase(1, 0, 0, 0, ExpectedResult = 50)]
        [TestCase(0, 1, 0, 0, ExpectedResult = 30)]
        [TestCase(0, 2, 0, 0, ExpectedResult = 45)]
        [TestCase(4, 3, 0, 0, ExpectedResult = 255)]
        [TestCase(2, 1, 2, 1, ExpectedResult = 185)]

        public int GetsTotalPriceWithPromotion(int unitsA, int unitsB, int unitsC, int unitsD)
        {
            Basket basket = new Basket();

            foreach (Promotion p in this.promotions)
            {
                basket.AddPromotion(p);
            }

            basket.SetQuantity('A', unitsA);
            basket.SetQuantity('B', unitsB);
            basket.SetQuantity('C', unitsC);
            basket.SetQuantity('D', unitsD);

            int price = basket.CalculateCostWithPromotions(this.skusd);
            return price;
        }

        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(4, 1, 2)]
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
