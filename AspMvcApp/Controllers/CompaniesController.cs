using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AspMvcApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Models;

namespace AspMvcApp.Controllers
{
    //[Authorize]
    public class CompaniesController : Controller
    {
        ApplicationDBEntities db = new ApplicationDBEntities();
           HttpClient client;
        HttpClient client2;
        //The URL of the WEB API Service
        string url = "http://localhost:57000/api/CompaniesAPI";
        string url2 = "http://localhost:57000/api/EmployeeAPI";
        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public CompaniesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client2 = new HttpClient();
            client2.BaseAddress = new Uri(url2);
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: CompaniesInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var comp = JsonConvert.DeserializeObject<List<Company>>(responseData);

                return View(comp);
            }
            return View("Error");
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var comp = JsonConvert.DeserializeObject<Company>(responseData);

                #region md
                HttpResponseMessage responseMessageEmp = await client2.GetAsync(url2);
                var responseDataEmp = responseMessageEmp.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<List<Employee>>(responseDataEmp);
                ViewBag.Employees = Employees.ToList().Where(p => p.CompanyId == id);
                #endregion
                return View(comp);
            }
            return View("Error");        
        }


        public async Task<ActionResult> Create()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var Companies = JsonConvert.DeserializeObject<List<User>>(responseData);

            List<int?> userIds = new List<int?>();

            foreach (var item in Companies)
            {
                userIds.Add(item.Id);
            }

            var distincts = userIds.Distinct();

            ViewBag.UserId = new SelectList(distincts.ToList(), "", "");

            return View(new Company());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Company comp)
        {

            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url, comp);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var comp = JsonConvert.DeserializeObject<Company>(responseData);

                #region DropDownCategoryId
                HttpResponseMessage responseMessage2 = await client.GetAsync(url);
                var responseData2 = responseMessage2.Content.ReadAsStringAsync().Result;
                var Companies = JsonConvert.DeserializeObject<List<User>>(responseData2);
                List<int?> userIds = new List<int?>();

                foreach (var item in Companies)
                {
                    userIds.Add(item.Id);
                }

                var distincts = userIds.Distinct();
                ViewBag.UserId = new SelectList(distincts.ToList(), "", "");
                #endregion

                return View(comp);
            }
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(Company comp)
        {
            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/", comp); 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var comp = JsonConvert.DeserializeObject<Company>(responseData);

                return View(comp);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Company comp)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
    }
}