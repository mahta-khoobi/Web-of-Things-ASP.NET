using IntelligentClassroom.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace IntelligentClassroom.Controllers.MVC
{
    public class SensorMVCController : Controller
    {
       
        //http://localhost:55692/sensormvc

        #region [-Index()-]
        public ActionResult Index()
        {
            IEnumerable < SensorViewModel> sensors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("sensor");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SensorViewModel>>();
                    readTask.Wait();

                    sensors = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    sensors = Enumerable.Empty<SensorViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(sensors);
        }
#endregion

        #region [-Create(): Get-]
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region [-Create(): Post()-]
        [HttpPost]
        public ActionResult Create(Models.EF.Sensor sensor)
        {
           

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/sensor");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Models.EF.Sensor>("sensor", sensor);
                
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(sensor);
        }
        #endregion

        #region [-Edit(): Get-]
        public ActionResult Edit(int id)
        {
            Models.EF.Sensor sensor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("sensor?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Models.EF.Sensor>();
                    readTask.Wait();

                    sensor = readTask.Result;
                }
            }

            return View(sensor);
        }
        #endregion

        #region [-Edit(): Post-]
        [HttpPost]
        public ActionResult Edit(Models.EF.Sensor sensor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/sensor");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Models.EF.Sensor>("sensor", sensor);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(sensor);
        }
        #endregion

        #region [-Delete(): Post-]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("sensor/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                     return RedirectToAction("Index");
                }
                else
                {
                     return RedirectToAction("Error");
                }

            }

      

        }
        #endregion

        #region [-Error(): Get-]
        public ActionResult Error()
        {
            return View();
        }
        #endregion

        public ActionResult GetValue()
        {
            return View();
        }
    }
}