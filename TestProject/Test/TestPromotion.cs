namespace TestProject
{
    using System;
    using NSubstitute;
    using NUnit.Framework;
    using PromotionEngine;

    public class TestPromotion
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PomotionHasIntPromotionIDField()
        {
            Promotion pc = new Promotion();
            if ((pc.GetType().GetProperty("PromotionID") == null) || (pc.GetType().GetProperty("PromotionID").PropertyType != typeof(int)))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void PomotionisEquatable()
        {
            Promotion promo1 = new Promotion() { PromotionID = 1 };
            Promotion promo2 = new Promotion() { PromotionID = 2 };

            if (promo1.Equals(promo2))
            {
                Assert.Fail();
            }

            promo2.PromotionID = promo1.PromotionID;

            if (!promo1.Equals(promo2))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AddsPromotionConditions()
        {
            Promotion promo = new Promotion();
            promo.AddPromotionCondition(new PromotionCondition { SkuId = 'A', Quantity = 3, SubstituteUnitPrice = 130 });

            if (promo.PromotionConditions.Count != 1)
            {
                Assert.Fail();
            }

            if (promo.PromotionConditions['A'].SkuId != 'A')
            {
                Assert.Fail();
            }

            // Check an Argument Excpetion gets thrown if you try Adding More than 1 Condition for an SKU
            Assert.Throws<ArgumentException>(() => { promo.AddPromotionCondition(new PromotionCondition { SkuId = 'A', Quantity = 4, SubstituteUnitPrice = 150 }); }, null, new ArgumentException("An item with the same key has already been added. Key: A not implemented in Promotion"));
        }
    }
}