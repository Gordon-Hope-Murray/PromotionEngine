namespace TestProject
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using PromotionEngine;

    public class TestBasket
    {
        private List<PromotionBase> promotions;
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

            this.promotions = new List<PromotionBase>
            {
                new PromotionFlat
                {
                    PromotionID = 1,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'A',
                            new PromotionCondition { PromotionConditionID = 1, SkuId = 'A', Quantity = 3, }
                        },
                    },
                    SubstituteUnitPrice = 130,
                    PromotionType = PromotionType.Flat,
                },

                new PromotionFlat
                {
                    PromotionID = 2,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'B',
                            new PromotionCondition { PromotionConditionID = 2, SkuId = 'B', Quantity = 2, }
                        },
                    },
                    SubstituteUnitPrice = 45,
                    PromotionType = PromotionType.Flat,
                },
                new PromotionFlat
                {
                    PromotionID = 3,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'C',
                            new PromotionCondition { PromotionConditionID = 3, SkuId = 'C', Quantity = 1, }
                        },
                        {
                            'D',
                            new PromotionCondition { PromotionConditionID = 4, SkuId = 'D', Quantity = 1, }
                        },
                    },
                    SubstituteUnitPrice = 30,
                    PromotionType = PromotionType.Flat,
                },

            };
        }

        [Test]
        public void BasketHasPromotionsField()
        {
            Basket basket = new Basket();
            if (basket.GetType().GetProperty("AppliedPromotions") == null || basket.GetType().GetProperty("AppliedPromotions").PropertyType != typeof(PromotionList))
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

            foreach (PromotionBase p in this.promotions)
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

            basket.SetQuantity('A', unitsA);
            basket.SetQuantity('B', unitsB);
            basket.SetQuantity('C', unitsC);
            basket.SetQuantity('D', unitsD);

            int price = basket.CalculateCost(this.skusd, basket.StockKeepingUnits);
            return price;
        }

        [TestCase(3, 0, 0, 0, ExpectedResult = 130)]
        [TestCase(1, 0, 0, 0, ExpectedResult = 50)]
        [TestCase(0, 1, 0, 0, ExpectedResult = 30)]
        [TestCase(0, 2, 0, 0, ExpectedResult = 45)]
        [TestCase(4, 3, 0, 0, ExpectedResult = 255)]
        [TestCase(2, 1, 2, 1, ExpectedResult = 180)]

        public int GetsTotalPriceAfterApplyingPromotion(int unitsA, int unitsB, int unitsC, int unitsD)
        {
            Basket basket = new Basket();

            foreach (PromotionFlat p in this.promotions) //.Find(x => x.PromotionType == PromotionType.Flat))
            {
                p.IsApplied = true;
                basket.AddPromotion(p);
            }

            basket.SetQuantity('A', unitsA);
            basket.SetQuantity('B', unitsB);
            basket.SetQuantity('C', unitsC);
            basket.SetQuantity('D', unitsD);

            int price = basket.CalculateCostWithPromotions(this.skusd);
            return price;
        }

        [Test]
        public void ApplyingPromotionPercentageGeneratesException()
        {
            Basket basket = new Basket();

            PromotionPercentage promotionPercentage = new PromotionPercentage { PromotionID = 4, PromotionType = PromotionType.Percentage };

            basket.AddPromotion(promotionPercentage);

            Assert.IsTrue(basket.AppliedPromotions.Count() == 1);

            // Check an Exception gets thrown when trying to apply Promotion with Percentage
            Assert.Throws<Exception>(() => { basket.AppliedPromotions[0].ApplyPromotion(basket); }, null, new Exception("PromotionPercentage.ApplyPromotion not implemented"));
        }

        [TestCase(2, 1, ExpectedResult = 1)]
        [TestCase(2, 2, ExpectedResult = 1)]
        [TestCase(2, 3, ExpectedResult = 1)]
        [TestCase(3, 1, ExpectedResult = 1)]
        [TestCase(3, 2, ExpectedResult = 1)]
        [TestCase(3, 3, ExpectedResult = 1)]
        [TestCase(4, 1, ExpectedResult = 2)]
        [TestCase(4, 2, ExpectedResult = 2)]
        public int GetsNoOftimesPromotionCanBeApplied(int unitsB, int unitsD)
        {
            Basket basket = new Basket();
            basket.StockKeepingUnits['B'] = unitsB;
            basket.StockKeepingUnits['D'] = unitsD;
            int result = basket.NoOftimespromotionCanBeApplied((PromotionFlat)this.promotions[1]);
            return result;
        }

        [TestCase(9, ExpectedResult = 3)]
        [TestCase(10, ExpectedResult = 3)]
        [TestCase(11, ExpectedResult = 3)]
        [TestCase(12, ExpectedResult = 4)]
        public int GetsNoOftimesPromotionConditionCanBeApplied(int units)
        {
            Basket basket = new Basket();
            basket.StockKeepingUnits['A'] = units;
            int result = basket.NoOftimesPromotionConditionCanBeApplied(this.promotions[0].PromotionConditions['A']);
            return result;
        }
    }
}
