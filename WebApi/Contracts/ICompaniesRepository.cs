using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Contracts
{
    public interface ICompaniesRepository
    { 
        IQueryable<Company> AllCompanies { get; }
        Company Find(int? id);
        void Insert(Company company);
        void Update(Company company);
        void Delete(int id);
        void Save();
    }
}
