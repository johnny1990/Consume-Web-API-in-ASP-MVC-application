using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {

        ApplicationDBEntities ctx;

        public EmployeesRepository(ApplicationDBEntities _ctx)
        {
            this.ctx = _ctx;
        }

        public IQueryable<Employee> AllEmployees
        {
            get { return ctx.Employees; }
        }

        public void Delete(int id)
        {
            var e = ctx.Employees.Find(id);
            ctx.Employees.Remove(e);
        }

        public Employee Find(int? id)
        {
            Employee objE= new Employee();
            objE = ctx.Employees.Where(p => p.Id == id).FirstOrDefault();
            return objE;
        }

        public void Insert(Employee employee)
        {
            ctx.Employees.Add(employee);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void Update(Employee employee)
        {
            ctx.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
