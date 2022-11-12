using System;
using System.Collections.Generic;
using System.Linq;
using WebApiCalls.Data;
using WebApiCalls.Services;

namespace WebApiCalls.Services.Impl
{
    public class CallRepository : ICallsRepository
    {
        #region Services

        private readonly WebApiCallsDbContext _context;

        #endregion

        #region Constructor

        public CallRepository(WebApiCallsDbContext context)
        {
            _context = context;
        }

        #endregion

        public int Create(Call data)
        {
            _context.Calls.Add(data);
            _context.SaveChanges();
            return data.CallId;
        }

        public void Delete(int id)
        {
            Call call = GetById(id);
            if (call == null)
                throw new Exception("Call not found.");
            _context.Calls.Remove(call);
            _context.SaveChanges();
        }

        public IEnumerable<Call> GetAll()
        {
            var res = _context.Calls.ToArray<Call>();
            return res;
        }

        public Call GetById(int id)
        {

            var call = _context.Calls.Find(id);
            if (call == null) throw new KeyNotFoundException("User not found");
            return call;
        }

        public void Update(Call data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            Call call = GetById(data.CallId);

            if(call != null)
            {
                call.FromId = data.FromId;
                call.ToId = data.ToId;
                call.TimeStart = data.TimeStart;
                call.TimeEnd = data.TimeEnd;
                _context.SaveChanges();
            }
            
        }
    }
}
