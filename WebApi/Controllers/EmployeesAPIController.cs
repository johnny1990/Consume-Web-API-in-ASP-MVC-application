using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Description;
using Contract;
using System.Linq;

namespace WebApi.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        ApplicationDBEntities ctx = new ApplicationDBEntities();
        //private IDataAccessRepository<Employee, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        //public EmployeeAPIController(IDataAccessRepository<Employee, int> r)
        //{
        //    _repository = r;
        //}

        public EmployeeAPIController()
        {

        }

        public IHttpActionResult Get()
        {
            return Ok(ctx.Employees.ToList());
        }

        //[ResponseType(typeof(Employee))]
        public IHttpActionResult GetById(int id)
        {
            return Ok(ctx.Employees.ToList().Where(p => p.Id == id));
            //return Ok (_repository.GetEmp(id)); 
        }

        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult Post(Employee emp)
        //{
        //    ctx.Employees.Add(emp);
        //    //_repository.PostEmp(emp);
        //    return Ok(emp);
        //}

        //[ResponseType(typeof(void))]
        //public IHttpActionResult Put(int id , Employee emp)
        //{   
        //    _repository.PutEmp(id,emp);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //[ResponseType(typeof(void))]
        //public IHttpActionResult Delete(int id)
        //{
        //    ctx.Employees.Remove(id);
        //    _repository.DeleteEmp(id);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}
