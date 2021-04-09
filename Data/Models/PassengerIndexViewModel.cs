using System.Collections.Generic;
using Data.Shared;

namespace Data.Models
{
    /// <summary>
    /// Class for paging Passengers Index view
    /// </summary>
    public class PassengerIndexViewModel
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int[] pageRecordShow = new int[6]
        {
            10, 15, 20, 25, 30,50
        };
        public PagerViewModel Pager { get; set; }

        public IEnumerable<Passenger> Items { get; set; }
    }
}
