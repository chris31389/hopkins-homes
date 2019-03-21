using System;
using System.Collections.Generic;
using System.Linq;
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
            List<PlotListItem> displayedPlotListItems = GetPlotListItemsFromWebsite().ToList();

            string apiKey = Environment.GetEnvironmentVariable("SEND_GRID_KEY");
            log.LogInformation($"apiKey = {apiKey}");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        private static IEnumerable<PlotListItem> GetPlotListItemsFromWebsite()
        {
            string url = "https://www.hopkinshomes.co.uk/development/StJamesPark_ELY";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            List<HtmlNode> trNodes = doc.DocumentNode.SelectNodes("//*[@id='inner-wrapper']/div[1]/div[2]/table/tr").ToList();
            trNodes.RemoveRange(0, 2);
            trNodes.RemoveRange(trNodes.Count - 2, 2);
            IEnumerable<PlotListItem> plotListItems = trNodes.Select(trNode =>
            {
                List<HtmlNode> tdNodes = trNode.Elements("td").ToList();
                return new PlotListItem
                {
                    Id = int.TryParse(tdNodes[0].InnerHtml, out int plotId) ? plotId : -1,
                    HouseType = tdNodes[1].InnerHtml,
                    Beds = tdNodes[2].InnerHtml,
                    Garaging = tdNodes[3].InnerHtml,
                    Status = tdNodes[4].InnerHtml
                };
            });

            return plotListItems;
        }
    }
}
