using System;

namespace Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
