using NUnit.Framework;
using PromotionEngine;
using NSubstitute;
using NSubstituteAutoMocker;
using System;

namespace Test
{
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
            Promotion promo1 = new Promotion() {PromotionID = 1 };
            Promotion promo2 = new Promotion() {PromotionID = 2 };

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
        public void AddsPromotionThrowsException()
        {
            Promotion promo = new  Promotion();
            Assert.Throws<Exception>(() => { promo.AddPromotionCondition(); });
          

        }

    }
}