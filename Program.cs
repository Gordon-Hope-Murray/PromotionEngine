using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            List<StockKeepingUnit> skus = new List<StockKeepingUnit>
            {
                new StockKeepingUnit { StockKeepingUnitId = 'A', UnitPrice = 50 },
                new StockKeepingUnit { StockKeepingUnitId = 'B', UnitPrice = 30 },
                new StockKeepingUnit { StockKeepingUnitId = 'C', UnitPrice = 20 },
                new StockKeepingUnit { StockKeepingUnitId = 'D', UnitPrice = 15 }

            };



            List<Promotion> promotions = new List<Promotion>
            {
                new Promotion
                {
                    PromotionID = 1,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'A',
                            new PromotionCondition {SkuId ='A', Quantity= 3, SubstitueUnitPrice= 130 }
                        }
                    }
                },

                new Promotion
                {
                    PromotionID = 2,
                    PromotionConditions = new Dictionary<char, PromotionCondition>
                    {
                        {
                            'B',
                            new PromotionCondition {SkuId ='B', Quantity = 2, SubstitueUnitPrice = 45 }
                        },
                        {
                            'D',
                            new PromotionCondition {SkuId ='D', Quantity = 1, SubstitueUnitPrice = 130 }
                        }
                    }
                }
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
