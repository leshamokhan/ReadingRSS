using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReadingRSS.Models;
using PagedList;

namespace ReadingRSS.Controllers
{
    public class NewsAJAXController : Controller
    {
        private ReadingRSSContext db = new ReadingRSSContext();

        // GET: NewsAJAX
        public ActionResult Index()
        {
            return View();
        }



        // GET: NewsAJAX
        [HttpPost]
        public ActionResult GetAll(string source, string sort, int page)
        {
            if (page <= 1)
            {
                page = 1;
            }

            var news = db.News.Include(n => n.Source);

            if (source != "null")
            {
                if (source != "Все")
                {
                    news = news.Where(s => s.Source.Name == source);
                } 
            }

            switch (sort)
            {
                case "date":
                    news = news.OrderByDescending(s => s.PubDate);
                    break;
                case "source":
                    news = news.OrderBy(s => s.Source.Name);
                    break;
                default:
                    news = news.OrderByDescending(s => s.ID);
                    break;
            }

            int pages = 10 * Convert.ToInt32(page);

            news = news.Skip(pages).Take(10);
            
            return Json((from obj in news select new { SourceName = obj.Source.Name, Title = obj.Title, Description = obj.Description, PubDate = obj.PubDate.ToString() }), JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
