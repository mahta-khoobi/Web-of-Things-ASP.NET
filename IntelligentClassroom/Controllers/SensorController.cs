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
    public class SensorController : ApiController
    {
        #region [-ctor-]
        public SensorController()
        {

            Ref_SensorCrud = new Models.DTO.SensorCrud();
        }
        #endregion

        public Models.DTO.SensorCrud Ref_SensorCrud { get; set; }

        // GET: api/Device

        #region [-GetAllSensors()-]
        public async Task<IHttpActionResult> Get_AllSensors()
        {
            //var q = await Ref_SensorCrud.SelectAll();
           // return Ok(new { q });
            IList<Models.POCO.SensorViewModel> sensors = null;
            using (var db = new Models.EF.WebofThingsEntities1())
            {
                sensors = db.Sensor.Include("Device")
                    .Select(s => new Models.POCO.SensorViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Frequency = s.Frequency,
                        TimeStamp = s.TimeStamp,
                        Type = s.Type,
                        Unit = s.Unit,
                        Value = s.Value


                    }).ToList<Models.POCO.SensorViewModel>();
            }
            if (sensors.Count == 0)
            {
                return NotFound();
            }

              return  Ok(sensors);
        }
        #endregion

        #region [-Get_SensorById(int id)-]
        public IHttpActionResult Get_SensorById(int id)
        {

            Models.POCO.SensorViewModel sensor = null;

            using (var db = new Models.EF.WebofThingsEntities1())
            {
                sensor = db.Sensor
                    .Where(s => s.Id == id)
                    .Select(s => new Models.POCO.SensorViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Frequency = s.Frequency,
                        TimeStamp = s.TimeStamp,
                        Type=s.Type,
                        Unit=s.Unit,
                        Value=s.Value


                    }).FirstOrDefault<Models.POCO.SensorViewModel>();
            }

            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }
        #endregion

        #region [-Post_Sensor(JObject jObject)-]
        public async Task<IHttpActionResult> Post_Sensor(JObject jObject)
        {
            var sensor = jObject["sensor"].ToObject<Models.EF.Sensor>();
            await Ref_SensorCrud.Insert(sensor);
            return Ok();
        }
        #endregion

        #region [-Put_Sensor(JObject jObject)-]
        public async Task<IHttpActionResult> Put_Sensor(JObject jObject)
        {
            var sensor = jObject["sensor"].ToObject<Models.EF.Sensor>();
            await Ref_SensorCrud.Update(sensor);
            return Ok();

        }
        #endregion

        #region [-Delete_Sensor(JObject jObject)-]
        public async Task<IHttpActionResult> Delete_Sensor(JObject jObject)
        {
            var sensor = jObject["sensor"].ToObject<Models.EF.Sensor>();
            await Ref_SensorCrud.Remove(sensor);
            return Ok();
        }
        #endregion
    }
}
