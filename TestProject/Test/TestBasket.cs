using NUnit.Framework;
using PromotionEngine;
using System.Collections.Generic;

namespace TestProject1
{
    public class TestBasket    
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PomotionConditionHasIntPromotionIDField()
        {
            Basket basket = new Basket();
            if ((basket.GetType().GetProperty("appliedPromotions") == null) || (basket.GetType().GetProperty("appliedPromotions").PropertyType != typeof(List<Promotion>)))
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