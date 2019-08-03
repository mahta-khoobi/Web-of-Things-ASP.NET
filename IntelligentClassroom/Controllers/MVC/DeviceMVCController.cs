using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IntelligentClassroom.Models.POCO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntelligentClassroom.Controllers.MVC
{
    public class DeviceMVCController : Controller
    {
        #region [-Index()-]
        // GET: DeviceMVC
        //http://localhost:55692/devicemvc
        public ActionResult Index()
        {
            IEnumerable<DeviceViewModel> devices = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("device");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DeviceViewModel>>();
                    readTask.Wait();

                    devices = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    devices = Enumerable.Empty<DeviceViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(devices);
        }
        #endregion

        #region [-Details(): Get-]
        public ActionResult Details(int id)
        {
            Models.POCO.SensorViewModel sensor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("sensor?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Models.POCO.SensorViewModel>();
                    readTask.Wait();

                    sensor = readTask.Result;
                    return View(sensor);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }



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
        public ActionResult Create(Models.EF.Device device)
        {
            //var jObject = JObject.FromObject(device);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/device");

                //HTTP POST
                 var postTask = client.PostAsJsonAsync<Models.EF.Device>("device", device);
                //var data = JsonConvert.SerializeObject(device);
                //var content = new StringContent(data, Encoding.UTF8, "application/json");
                //var postTask = client.PostAsync(client.BaseAddress,content);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(device);
        }
        #endregion

        #region [-Edit(): Get-]
        public ActionResult Edit(int id)
        {
            Models.EF.Device device = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("device?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Models.EF.Device>();
                    readTask.Wait();

                    device = readTask.Result;
                }
            }

            return View(device);
        }
        #endregion

        #region [-Edit(): Post-]
        [HttpPost]
        public ActionResult Edit(Models.EF.Device device)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/device");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Models.EF.Device>("device", device);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(device);
        }
        #endregion

        #region [-Delete(): Post-]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("device/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [-Error(): Get-]
        public ActionResult Error()
        {
            return View();
        }
        #endregion





    }
}