using WebApiCalls.Data;
using WebApiCalls.Models;

namespace WebApiCalls.Services
{
    public interface ICallsRepository : IRepository<Call, int> { }
}
