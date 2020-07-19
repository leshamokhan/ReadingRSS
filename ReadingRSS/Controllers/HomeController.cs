using ReadingRSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Data.Entity;
using System.Net;
using System.Threading;

namespace ReadingRSS.Controllers
{
    public class HomeController : Controller
    {
        private ReadingRSSContext db = new ReadingRSSContext();

        public ActionResult Index()
        {     
            ViewBag.Habr = "Источник : Хабрахабр";
            ViewBag.HabrCom = ReadRSS("https://habr.com/ru/rss/all/all/", 1);

            ViewBag.Interfax = "Источник : Интерфакс";
            ViewBag.InterfaxCom = ReadRSS("https://interfax.by/news/feed/", 2);

            return View();
        }

        
        
        public string ReadRSS(string url, int id)
        {
            try
            {
                var rss = XDocument.Load(url);
                var posts = (from item in rss.Descendants("item")
                             select new News
                             {
                                 Link = item.Element("link").Value,
                                 Title = item.Element("title").Value,
                                 Description = item.Element("description").Value,
                                 PubDate = DateTime.Parse(item.Element("pubDate").Value)
                             }).Reverse();

                int countAll = 0;
                int countNew = 0;
                try
                {
                    var LastPost = db.News.Where(x => x.SourceID == id).OrderByDescending(x => x.PubDate).FirstOrDefault();
                    var LastNews = db.News.Where(x => x.PubDate == LastPost.PubDate).FirstOrDefault();
                    foreach (var item in posts)
                    {
                        if ((item.PubDate >= LastPost.PubDate) && (LastNews.Title != item.Title))
                        {
                            News news = new News { Title = item.Title, Link = item.Link, PubDate = item.PubDate, Description = item.Description, SourceID = id };
                            db.News.Add(news);
                            countNew++;
                        }
                        countAll++;
                    }
                }
                catch
                {
                    foreach (var item in posts)
                    {
                        News news = new News { Title = item.Title, Link = item.Link, PubDate = item.PubDate, Description = item.Description, SourceID = id };
                        db.News.Add(news);
                        countAll++;
                        countNew++;
                    }
                }
                db.SaveChanges();

                return "\nПрочитано новостей : " + countAll + "\nСохранено новостей : " + countNew;                               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "\nОшибка";
        }         
    }
}