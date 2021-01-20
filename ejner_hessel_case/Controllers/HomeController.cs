using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ejner_hessel_case.Models;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections;

namespace ejner_hessel_case.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var input = Request.Query["rssurl"].ToString();
            var sortType = Request.Query["sortType"].ToString();
            string url =  input == "" ?  "https://www.dr.dk/nyheder/service/feeds/allenyheder/#" : input;
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<News> NewsList = new List<News>();
            foreach (SyndicationItem item in feed.Items)
            {
                var newsitem = new News();
                newsitem.Id = item.Id;
                newsitem.Title = item.Title.Text;
                newsitem.Description = item.Summary == null ? "" : item.Summary.Text;
                newsitem.ArticleLink = item.Links[0].Uri.ToString();
                newsitem.Publication = item.PublishDate.DateTime;
                NewsList.Add(newsitem);

            }
       
            switch (sortType)
            {
                case "a-z":
                     NewsList = NewsList.OrderBy(x=> x.Title).ToList();
                    break;
                case "z-a":
                     NewsList = NewsList.OrderByDescending(x=> x.Title).ToList();
                    break;
                case "oldest":
                     NewsList = NewsList.OrderBy(s => s.Publication).ToList();
                    break; 
            }
            IEnumerable<News> enumerable = NewsList;
            return View(enumerable);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
