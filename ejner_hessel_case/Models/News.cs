using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ejner_hessel_case.Models
{
    public class News
    {
        public  string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Publication { get; set; }
        public string ArticleLink { get; set; }
    }
}
