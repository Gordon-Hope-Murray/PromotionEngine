namespace PromotionEngine
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            List<PromotionBase> promotions = new List<PromotionBase>
            {
                new PromotionFlat
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

                new PromotionFlat
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
