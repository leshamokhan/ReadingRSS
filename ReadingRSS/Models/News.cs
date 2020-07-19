using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadingRSS.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public int SourceID { get; set; }
        public virtual Source Source { get; set; }
    }
}