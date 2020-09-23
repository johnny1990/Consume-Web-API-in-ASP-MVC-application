using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using Unity;

namespace Repository
{
    public class clsDataAccessRepository : IDataAccessRepository<Employee, int>
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public ApplicationDBEntities ctx { get; set; }

        //Get all Employees
        public IEnumerable<Employee> GetEmp()
        {
            return ctx.Employees.ToList();
        }
        //Get Specific Employee based on Id
        public Employee GetEmp(int id)
        {
            return ctx.Employees.Find(id);
        }

        //Create a new Employee
        public void PostEmp(Employee entity)
        {
            ctx.Employees.Add(entity);
            ctx.SaveChanges();
        }
        //Update Exisitng Employee
        public void PutEmp(int id, Employee entity)
        {
            var Emp = ctx.Employees.Find(id);
            if (Emp != null)
            {
                Emp.FirstName = entity.FirstName;
                Emp.LastName = entity.LastName;
                Emp.Serial = entity.Serial;
                Emp.CompanyId = entity.CompanyId;

                ctx.SaveChanges();
            }
        }
        //Delete an Employee based on Id
        public void DeleteEmp(int id)
        {
            var Emp = ctx.Employees.Find(id);
            if (Emp != null)
            {
                ctx.Employees.Remove(Emp);
                ctx.SaveChanges();
            }
        }


        //Get all Companies
        public IEnumerable<Company> GetComp()
        {
            return ctx.Companies.ToList();
        }
        //Get Specific Company based on Id
        public Company GetComp(int id)
        {
            return ctx.Companies.Find(id);
        }

        //Create a new Company
        public void PostComp(Company entitycomp)
        {
            ctx.Companies.Add(entitycomp);
            ctx.SaveChanges();
        }
        //Update Exisitng Company
        public void PutComp(int id, Company entitycomp)
        {
            var Comp = ctx.Companies.Find(id);
            if (Comp != null)
            {
                Comp.UserId = entitycomp.UserId;
                Comp.Name = entitycomp.Name;

                ctx.SaveChanges();
            }
        }
        //Delete an Company based on Id
        public void DeleteComp(int id)
        {
            var Comp = ctx.Companies.Find(id);
            if (Comp != null)
            {
                ctx.Companies.Remove(Comp);
                ctx.SaveChanges();
            }
        }      
    }
}
