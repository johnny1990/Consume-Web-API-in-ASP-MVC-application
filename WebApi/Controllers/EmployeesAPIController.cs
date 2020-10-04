using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Description;
using System.Linq;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        private IEmployeesRepository _repository;
  
        public EmployeeAPIController(IEmployeesRepository r)
        {
           _repository = r;
        }

        public IHttpActionResult GetEmployees()
        {
            return Ok(_repository.AllEmployees.ToList());
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_repository.AllEmployees.ToList().Where(p => p.Id == id));

        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            _repository.Insert(emp);
            return Ok(emp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Employee emp)
        {
            _repository.Update(emp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
