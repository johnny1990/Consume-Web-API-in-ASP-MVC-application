using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Description;
using Contract;

namespace WebApi.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        private IDataAccessRepository<Employee, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public EmployeeAPIController(IDataAccessRepository<Employee, int> r)
        {
            _repository = r;
        }
        public IEnumerable<Employee> Get()
        {
            return _repository.GetEmp();
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Get(int id)
        {
            return Ok (_repository.GetEmp(id)); 
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            _repository.PostEmp(emp);
            return Ok(emp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id , Employee emp)
        {
            _repository.PutEmp(id,emp);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            _repository.DeleteEmp(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
