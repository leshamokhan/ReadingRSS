using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ReadingRSS.Models;

namespace ReadingRSS.Controllers
{
    public class NewsController : Controller
    {
        private ReadingRSSContext db = new ReadingRSSContext();

        // GET: News
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SourceParm = String.IsNullOrEmpty(sortOrder) ? "source_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var newss = db.News.Include(n => n.Source);

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString != "Все")
                {
                    newss = newss.Where(s => s.Source.Name.Contains(searchString));
                }
            }

            switch (sortOrder)
            {
                case "source_desc":
                    newss = newss.OrderBy(n => n.SourceID);
                    break;
                case "Date":
                    newss = newss.OrderBy(n => n.PubDate);
                    break;
                case "date_desc":
                    newss = newss.OrderByDescending(n => n.PubDate);
                    break;
                default:
                    newss = newss.OrderByDescending(n => n.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(newss.ToPagedList(pageNumber, pageSize));
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
