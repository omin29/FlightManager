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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public int ShowRecords { get; set; }
        public int LastPage { get; set; }
    }
}
