using System;

namespace WebApiCalls.Models
{
    public class CallDto
    {
        public int CallId { get; set; }

        public string FromPhone { get; set; }

        public string ToPhone { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
