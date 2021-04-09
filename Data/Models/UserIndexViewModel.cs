using System.Collections.Generic;
using Data.Shared;

namespace Data.Models
{
    /// <summary>
    /// Class for paging Users Index view
    /// </summary>
    public class UserIndexViewModel
    {
        public int[] pageRecordShow = new int[6]
        {
            10, 15, 20, 25, 30,50
        };
        public PagerViewModel Pager { get; set; }

        public IEnumerable<User> Items { get; set; }
    }
}
