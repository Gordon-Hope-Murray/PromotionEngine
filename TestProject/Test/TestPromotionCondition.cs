using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using PromotionEngine;

namespace TestProject
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

        [Test]
        public void PomotionConditionisEquatable()
        {
            PromotionCondition pc1 = new PromotionCondition() { SkuId = 'A', Quantity = 3, SubstituteUnitPrice = 130 };
            PromotionCondition pc2 = new PromotionCondition() { SkuId = 'B', Quantity = 2, SubstituteUnitPrice = 70 };

            if (pc1.Equals(pc2))
            {
                Assert.Fail();
            }

            pc1.SkuId = pc2.SkuId;

            if (!pc1.Equals(pc2))
            {
                Assert.Fail();
            }
        }

    }
}
