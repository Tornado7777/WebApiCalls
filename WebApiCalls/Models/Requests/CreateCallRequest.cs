using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using WebApiCalls.Data;

namespace WebApiCalls.Models.Requests
{
    public class CreateCallRequest
    {
        //public string FromPhone { get; set; }

        public string ToPhone { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }
    }
}
