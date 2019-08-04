using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntelligentClassroom.Controllers
{
    public class DeviceController : ApiController
    {
        #region [-ctor-]
        public DeviceController()
        {

            Ref_DeviceCrud = new Models.DTO.DeviceCrud();
        } 
        #endregion

        public Models.DTO.DeviceCrud Ref_DeviceCrud { get; set; }

        // GET: api/Device

        #region [-Get_AllDevices()-]
        public IHttpActionResult Get_AllDevices()
        {
    

            IList<Models.POCO.DeviceViewModel> devices = null;

            using (var db = new Models.EF.WebofThingsEntities1())
            {
                devices = db.Device.Include("Sensor").Include("Actuator")
                            .Select(d => new Models.POCO.DeviceViewModel()
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Description = d.Description,
                                CurrentStatus = d.CurrentStatus,
                                Version = d.Version
                                //Sensor = d.Sensor == null || includeAddress == false ? null : new Models.POCO.SensorViewModel()
                                //{
                                       
                                //}
                            }).ToList<Models.POCO.DeviceViewModel>();
            }

            if (devices.Count == 0)
            {
                return NotFound();
            }

            return Ok(devices);
        }
        #endregion

        #region [-Get_DeviceById(int id)-]
        public IHttpActionResult Get_DeviceById(int id)
        {
          
            Models.POCO.DeviceViewModel device = null;

            using (var db = new Models.EF.WebofThingsEntities1())
            {
                device = db.Device.Include("Sensor").Include("Actuator")
                    .Where(s => s.Id == id)
                    .Select(s => new Models.POCO.DeviceViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        CurrentStatus=s.CurrentStatus,
                        Version=s.Version,
                        
                    }).FirstOrDefault<Models.POCO.DeviceViewModel>();
            }

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }
        #endregion

        #region [-Post_Device(Models.EF.Device device)-]
        public async Task<IHttpActionResult> Post_Device(Models.EF.Device device)
        {
            // var device = jObject["device"].ToObject<Models.EF.Device>();
            await Ref_DeviceCrud.Insert(device);
            return Ok();
        }
        #endregion

        #region [-Put_Device(Models.EF.Device device)-]
        public async Task<IHttpActionResult> Put_Device(Models.EF.Device device)
        {
          //  var device = jObject["device"].ToObject<Models.EF.Device>();
            await Ref_DeviceCrud.Update(device);
            return Ok();

        }
        #endregion

        #region [-Delete(int id)-]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new Models.EF.WebofThingsEntities1())
            {
                var device = ctx.Device
                    .Where(d => d.Id == id)
                    .FirstOrDefault();

                await Ref_DeviceCrud.Remove(device);
            }

            return Ok();
        } 
        #endregion


    }
}
