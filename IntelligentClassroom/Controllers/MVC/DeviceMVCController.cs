using System;
using System.Collections.Generic;
using System.Linq;
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
            DeviceViewModel device = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("device?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DeviceViewModel>();
                    readTask.Wait();

                    device = readTask.Result;
                }
            }

            return View(device);
        } 
        #endregion



    }
}