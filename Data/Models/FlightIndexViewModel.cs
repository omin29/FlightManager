using System.Collections.Generic;
using Data.Shared;

namespace Data.Models
{
    /// <summary>
    /// Class for paging Flights Index view
    /// </summary>
    public class FlightIndexViewModel
    {
        public int[] pageRecordShow = new int[5]
        {
            10, 15, 20, 25, 30
        };
        public PagerViewModel Pager { get; set; }

        public IEnumerable<Flight> Items { get; set; }
    }
}
