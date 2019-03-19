using System;
using HtmlAgilityPack;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace HopkinsHomes
{
    public static class ScreenScrapeFunction
    {
        [FunctionName("ScreenScrapeFunction")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string url = "http://html-agility-pack.net/";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            doc.DocumentNode.SelectNodes("table");
            string apiKey = Environment.GetEnvironmentVariable("SEND_GRID_KEY");
            log.LogInformation($"apiKey = {apiKey}");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
