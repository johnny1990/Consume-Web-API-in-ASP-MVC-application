using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Contracts
{
    public interface IEmployeesRepository
    {
        IQueryable<Employee> AllEmployees { get; }
        Employee Find(int? id);
        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        void Save();
    }
}
