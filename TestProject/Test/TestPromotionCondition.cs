using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PromotionEngine;

namespace Test
{
    class TestPomotionCondition
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void PomotionConditionHasCharSkuIdField()
        {
            PromotionCondition pc = new PromotionCondition();
            if ((pc.GetType().GetProperty("SkuId") == null) || (pc.GetType().GetProperty("SkuId").PropertyType != typeof(char) ))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass(); 
            }
            
        }

        [Test]
        public void PomotionConditionHasIntQuantityField()
        {
            PromotionCondition pc = new PromotionCondition();
            if ((pc.GetType().GetProperty("Quantity") == null) || (pc.GetType().GetProperty("Quantity").PropertyType != typeof(int)))
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

        [Test]
        public void PomotionConditionHasIntSubstituteUnitPriceField()
        {
            PromotionCondition pc = new PromotionCondition();
            if ((pc.GetType().GetProperty("SubstituteUnitPrice") == null) || (pc.GetType().GetProperty("SubstituteUnitPrice").PropertyType != typeof(int)))
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
