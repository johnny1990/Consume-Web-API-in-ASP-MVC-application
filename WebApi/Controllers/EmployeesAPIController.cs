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
            var rep = _repository.Find(id);
            return Ok(rep);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            _repository.Insert(emp);
            _repository.Save();
            return Ok(emp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Employee emp)
        {
            _repository.Update(emp);
            _repository.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
