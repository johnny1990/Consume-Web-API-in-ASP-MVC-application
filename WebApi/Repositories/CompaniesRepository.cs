using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Contracts;
using WebApi.Models;


namespace WebApi.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        ApplicationDBEntities ctx;

        public CompaniesRepository(ApplicationDBEntities _ctx)
        {
            this.ctx = _ctx;
        }

        public IQueryable<Company> AllCompanies
        {
            get { return ctx.Companies; }
        }

        public Company Find(int? id)
        {
            Company objC = new Company();
            objC = ctx.Companies.Where(p => p.Id == id).FirstOrDefault();
            return objC;
        }

        public void Insert(Company company)
        {
            ctx.Companies.Add(company);
        }

        public void Update(Company company)
        {
            ctx.Entry(company).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int id)
        {
            var c = ctx.Companies.Find(id);
            ctx.Companies.Remove(c);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }
    }
}
