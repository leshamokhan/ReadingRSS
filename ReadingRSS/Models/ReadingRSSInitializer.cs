using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ReadingRSS.Models
{
    public class ReadingRSSInitializer : DropCreateDatabaseIfModelChanges<ReadingRSSContext>
    {
        protected override void Seed(ReadingRSSContext context)
        {
            Source source = new Source { Name = "Хабрахабр", URL = "https://habr.com/ru/rss/all/all/" };
            Source source1 = new Source { Name = "Интерфакс", URL = "https://interfax.by/news/feed/" };

            context.Sources.Add(source);
            context.Sources.Add(source1);

            context.SaveChanges();
        }
    }
}
