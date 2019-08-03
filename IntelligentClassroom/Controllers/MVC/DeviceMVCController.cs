using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using IntelligentClassroom.Models.POCO;
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

        #region [-Create()-]
        public ActionResult Create()
        {
            return View();
        } 
        #endregion

    }
}