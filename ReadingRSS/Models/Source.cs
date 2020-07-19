using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadingRSS.Models
{
    public class Source
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}