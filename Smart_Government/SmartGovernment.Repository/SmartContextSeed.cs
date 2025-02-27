using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartGovernment.Core.Entities;
using SmartGovernment.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartGovernment.Repository
{
    public class SmartContextSeed
    {
        public static async Task SeedAsync(SmartContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Ministries.Any())
                {

                    var ministrydata = File.ReadAllText("../SmartGovernment.Repository/Data/DataSeed/Ministries.json");

                    var ministries = JsonSerializer.Deserialize<List<Ministry>>(ministrydata);

                    foreach (var Ministry in ministries)
                        context.Set<Ministry>().Add(Ministry);


                }

                await context.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<SmartContextSeed>();
                logger.LogError(ex, ex.Message);
            }

        }


    }
}
