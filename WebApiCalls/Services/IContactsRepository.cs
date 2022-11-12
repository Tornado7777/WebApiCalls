using WebApiCalls.Models;
using WebApiCalls.Data;

namespace WebApiCalls.Services
{
    public interface IContactsRepository : IRepository<Contact, int>
    {
        public int FindIdPhone(string phone);
    }
}
