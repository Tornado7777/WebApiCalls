using System;
using System.Collections.Generic;
using System.Linq;
using WebApiCalls.Data;
using WebApiCalls.Services;

namespace WebApiCalls.Services.Impl
{
    public class ContactRepository : IContactsRepository
    {
        #region Services

        private readonly WebApiCallsDbContext _context;

        #endregion

        #region Constructor

        public ContactRepository(WebApiCallsDbContext context)
        {
            _context = context;
        }

        #endregion
        public int FindIdPhone(string phone)
        {
            Contact contac = _context.Contacts
             .Where(x => x.Phone == phone)
             .FirstOrDefault();
            if (contac == null)
                return 0;
            return contac.ContactId ;
        }

        public int Create(Contact data)
        {
            if (FindIdPhone(data.Phone) != 0)
                throw new ArgumentException("Contact with this phone exists");
            _context.Contacts.Add(data);
            _context.SaveChanges();
            return data.ContactId;
        }

        public void Delete(int id)
        {
            Contact contact = GetById(id);
            if (contact == null)
                throw new Exception("Contact not found.");
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contacts.ToArray<Contact>();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.Find(id);

        }

        public void Update(Contact data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            Contact contact = GetById(data.ContactId);

            if(contact != null)
            {
                contact.FIO = data.FIO;
                contact.Phone = data.Phone;
                contact.Company = data.Company;
                contact.Description = data.Description;
                _context.SaveChanges();
            }
            
        }
    }
}
