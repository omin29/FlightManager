using System.Collections.Generic;
using Data.Shared;

namespace Data.Models
{
    /// <summary>
    /// Class for paging Reservations Index view
    /// </summary>
    public class ReservationIndexViewModel
    {
        public int[] pageRecordShow = new int[6]
        {
            10, 15, 20, 25, 30,50
        };
        public PagerViewModel Pager { get; set; }

        public IEnumerable<Reservation> Items { get; set; }
    }
}
