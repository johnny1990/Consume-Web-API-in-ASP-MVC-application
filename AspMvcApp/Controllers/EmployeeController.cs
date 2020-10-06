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
using WebApi.Contracts;

namespace AspMvcApp.Controllers
{   /*[Authorize]*/
    public class EmployeeController : Controller
    {
        HttpClient client;
        //The URL of the WEB API Service
        string url = "http://localhost:57000/api/EmployeeAPI";

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: EmployeeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employees = JsonConvert.DeserializeObject<List<Employee>>(responseData);

                return View(Employees);
            }
            return View("Error");
        }

        public async Task<ActionResult> Create()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var Employees = JsonConvert.DeserializeObject<List<Company>>(responseData);

            List<int?> compIds = new List<int?>();

            foreach(var item in Employees)
            {
                compIds.Add(item.Id);
            }

            var distincts = compIds.Distinct();

            ViewBag.CompanyId = new SelectList(distincts.ToList(), "", "");
            return View(new Employee());
        }

        //The Post method
        [HttpPost]
        public async Task<ActionResult> Create(Employee Emp)
        {

            HttpResponseMessage responseMessage =  await client.PostAsJsonAsync(url, Emp);
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

                var Employee = JsonConvert.DeserializeObject<Employee>(responseData);

                #region DropDownCategoryId
                HttpResponseMessage responseMessage2 = await client.GetAsync(url);
                var responseData2 = responseMessage2.Content.ReadAsStringAsync().Result;
                var Employees = JsonConvert.DeserializeObject<List<Company>>(responseData2);
                List<int?> compIds = new List<int?>();

                foreach (var item in Employees)
                {
                    compIds.Add(item.Id);
                }

                var distincts = compIds.Distinct();
                ViewBag.CompanyId = new SelectList(distincts.ToList(), "", "");
                #endregion

                return View(Employee);
            }           
            return View("Error");
        }

        //The PUT Method
        [HttpPost]
        public async Task<ActionResult> Edit(Employee Emp)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" , Emp);
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

                var Employee = JsonConvert.DeserializeObject<Employee>(responseData);

                return View(Employee);
            }
            return View("Error");
        }

        //The DELETE method
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Employee Emp)
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