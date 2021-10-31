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
        public void PomotionConditionHasIntPromotionIDField()
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
        public void AddsPromotionThrowsException()
        {
            Promotion promo = new  Promotion();
            Assert.Throws<Exception>(() => { promo.AddPromotionCondition(); });
          

        }

    }
}