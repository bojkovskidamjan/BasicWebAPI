using Microsoft.EntityFrameworkCore;
using BasicWebAPI.Data;
using BasicWebAPI.Model;
using BasicWebAPI.Services.Interfaces;

namespace BasicWebAPI.Services.Implementation
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCountryAsync(Country country)
        {
            try
            {
                await _dbContext.Set<Country>().AddAsync(country);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while creating the country.", ex);
            }
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            try
            {
                return await _dbContext.Set<Country>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while fetching all countries.", ex);
            }
        }

        public void UpdateCountry(Country country)
        {
            try
            {
                _dbContext.Entry(country).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while updating the country.", ex);
            }
        }

        public void DeleteCountry(Country country)
        {
            try
            {
                _dbContext.Set<Country>().Remove(country);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while deleting the country.", ex);
            }
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            try
            {
                return await _dbContext.Countries.FindAsync(countryId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while fetching the country by ID.", ex);
            }
        }
    }
}
