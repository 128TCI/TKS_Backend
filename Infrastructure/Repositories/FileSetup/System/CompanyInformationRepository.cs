using DomainEntities.DTO.FileSetUp.Employment;
using DomainEntities.DTO.FileSetUp.System;
using Infrastructure.IRepositories.FileSetUp.Employment;
using Infrastructure.IRepositories.FileSetUp.System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timekeeping.Infrastructure.Data;

namespace Infrastructure.Repositories.FileSetup.System
{
    public class CompanyInformationRepository : ICompanyInformationRepository
    {
        private readonly TimekeepingContext _context;

        public CompanyInformationRepository(TimekeepingContext context)
        {
            _context = context;
        }

        public async Task<CompanyInformationDTO> InsertAsync(CompanyInformationDTO companyInformation)
        {
            _context.tbl_fsCompanyInfo.Add(companyInformation);
            await _context.SaveChangesAsync();
            return companyInformation;
        }

        public async Task<CompanyInformationDTO> UpdateAsync(CompanyInformationDTO companyInformation)
        {
            _context.tbl_fsCompanyInfo.Update(companyInformation);
            await _context.SaveChangesAsync();
            return companyInformation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var companyInformation = await _context.tbl_fsCompanyInfo.FindAsync(id);
            if (companyInformation == null) return false;

            _context.tbl_fsCompanyInfo.Remove(companyInformation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyInformationDTO?> GetByIdAsync(int id)
        {
            return await _context.tbl_fsCompanyInfo.FindAsync(id);
        }

        public async Task<List<CompanyInformationDTO>> GetAllAsync()
        {
            return await _context.tbl_fsCompanyInfo.ToListAsync();
        }
    }
}
