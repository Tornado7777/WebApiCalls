using System.ComponentModel.DataAnnotations;

namespace WebApiCalls.Models.Requests
{
    public class CreateContactRequest
    {
        public string Phone { get; set; }

        public string FIO { get; set; }

        public string Company { get; set; }

        public string Description { get; set; }
    }
}
