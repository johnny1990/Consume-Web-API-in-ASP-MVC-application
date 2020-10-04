using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Description;
using System;
using WebApi.Contracts;
using System.Linq;

namespace WebApi.Controllers
{
    public class CompaniesAPIController : ApiController
    {
        private ICompaniesRepository _repository;

        public CompaniesAPIController(ICompaniesRepository r)
        {
            _repository = r;
        }

        public IHttpActionResult GetCompanies()
        {
            return Ok(_repository.AllCompanies.ToList());
        }

        [ResponseType(typeof(Company))]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var rep = _repository.Find(id);
                return Ok(rep);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            

        }

        [ResponseType(typeof(Company))]
        public IHttpActionResult Post(Company comp)
        {
            _repository.Insert(comp);
            return Ok(comp);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Company comp)
        {
            _repository.Update(comp);
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
