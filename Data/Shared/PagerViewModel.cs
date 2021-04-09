using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Shared
{
    /// <summary>
    /// Class for properties used for paging the Index views
    /// </summary>
    public class PagerViewModel
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public int ShowRecords { get; set; }
        public int lastPage { get; set; }
    }
}
