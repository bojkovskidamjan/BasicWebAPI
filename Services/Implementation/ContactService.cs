using Microsoft.EntityFrameworkCore;
using BasicWebAPI.Data;
using BasicWebAPI.Model;
using BasicWebAPI.Services.Interfaces;

namespace BasicWebAPI.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateContactAsync(Contact contact)
        {
            await _dbContext.Set<Contact>().AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteContact(Contact contact)
        {
            _dbContext.Set<Contact>().Remove(contact);
            _dbContext.SaveChanges();
        }

        public async Task<List<Contact>> GetAllContactsAsync()
        {
            return await _dbContext.Set<Contact>().Include(c => c.Company).Include(c => c.Country).ToListAsync();

        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await _dbContext.Contacts.FindAsync(contactId);
        }

        public void UpdateContact(Contact contact)
        {
            _dbContext.Contacts.Update(contact);
            _dbContext.SaveChanges();
        }

        public async Task<List<Contact>> FilterContactsAsync(int? countryId, int? companyId)
        {
            IQueryable<Contact> query = _dbContext.Set<Contact>()
                .Include(c => c.Company)
                .Include(c => c.Country);

            if (countryId.HasValue)
            {
                query = query.Where(contact => contact.CountryId == countryId.Value);
            }

            if (companyId.HasValue)
            {
                query = query.Where(contact => contact.CompanyId == companyId.Value);
            }

            return await query.ToListAsync();
        }

        public IQueryable<Contact> AsQueryable()
        {
            return _dbContext.Contacts.AsQueryable();
        }
    }
}