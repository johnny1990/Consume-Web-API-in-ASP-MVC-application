using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Description;
using Contract;

namespace WebApi.Controllers
{
    public class CompaniesAPIController : ApiController
    {
        private IDataAccessRepository<Company, int> _repository;
        //Inject the DataAccessRepository using Construction Injection 
        public CompaniesAPIController(IDataAccessRepository<Company, int> r)
        {
            _repository = r;
        }
        public IEnumerable<Company> Get()
        {
            return _repository.GetEmp();
        }

        [ResponseType(typeof(Company))]
        public IHttpActionResult Get(int id)
        {
            return Ok(_repository.GetEmp(id));
        }

        [ResponseType(typeof(Company))]
        public IHttpActionResult Post(Company comp)
        {
            _repository.PostEmp(comp);
            return Ok(comp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Company comp)
        {
            _repository.PutEmp(id, comp);
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
