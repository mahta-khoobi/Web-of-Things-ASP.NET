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

   
        //http://localhost:55692/devicemvc
        public DeviceMVCController()
        {
            ListOfCourses = new List<ClassTime> {
                new ClassTime
                { ClassCode=202,
                ClassName="202",
                CourseCode=123,
                CourseDay=DayOfWeek.Monday,
                CourseTime= DateTime.Now,
                Device_Ref=1,
                Actuator_Ref=1
                }
                ,
                new ClassTime{
                ClassCode =501,
                ClassName="501",
                CourseCode=434,
                CourseDay=DayOfWeek.Saturday,
                CourseTime= DateTime.Now,
                Device_Ref=1,
                Actuator_Ref=1
                },
                new ClassTime
                {
                ClassCode=101,
                ClassName="101",
                CourseCode=234,
                CourseDay=DayOfWeek.Sunday,
                CourseTime= DateTime.Now,
                Device_Ref=1,
                Actuator_Ref=1
                
                },
                new ClassTime
                {
                ClassCode=202,
                ClassName="202",
                CourseCode=234,
                CourseDay=DayOfWeek.Tuesday,
                CourseTime= DateTime.Now,
                Device_Ref=1,
                Actuator_Ref=1
                
                }

               };
        }

      public List<Models.POCO.ClassTime> ListOfCourses { get; set; }
            
        #region [-Index()-]
    
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
        public ActionResult SensorDetails(int id)
        {
            Models.POCO.SensorViewModel sensor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55692/api/");
                //HTTP GET
                var responseTask = client.GetAsync("sensor?device_ref=" + id.ToString());
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
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

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

        #region [-Get_PicturesEverySpecificDay(): Get-]
        public ActionResult Get_PicturesEverySpecificDay()
        {
            Models.EF.Actuator CameraActuator = new Models.EF.Actuator();
            foreach (var course in ListOfCourses)
            {
                if(course.CourseTime.Hour==DateTime.Now.Hour && course.CourseDay == DateTime.Now.DayOfWeek)
                {
                    CameraActuator.Id = course.Actuator_Ref;
                    CameraActuator.Name = "Camera Actuator";
                    CameraActuator.Device_Ref = course.Device_Ref;
                    CameraActuator.Command = "Write";
                    
                    Edit(course.ClassCode);
                }
            }
            return View();
        }
        #endregion








    }
}