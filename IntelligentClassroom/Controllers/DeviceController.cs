using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntelligentClassroom.Controllers
{
    public class DeviceController : ApiController
    {
        
        List<Models.DTO.Device> Devices = new List<Models.DTO.Device>
        {
            new Models.DTO.Device{
                Id =1,
                Name ="RaspberryPi",
                Description ="My WoT Raspberry pi 2",
                CurrentStatus ="Live",
                Version ="v0.1",
                Resource= new Models.DTO.Resource
                {
                    Sensors = new List<Models.DTO.Sensor>{ new Models.DTO.Sensor { Id = 1, Name = "Temperature", Description = "My Pi temperature", Type = "float", Unit = "celsius", Value = "27.5", TimeStamp = DateTime.Today, Frequency = 5000 } },
                    Actuators = new List<Models.DTO.Actuator>()
                }
               
            },
            new Models.DTO.Device{
                Id =2,
                Name ="Camera",
                Description ="A simple WoT-connected camera",
                CurrentStatus ="Live",
                Version ="v0.1",
                Resource= new Models.DTO.Resource
                {
                    Sensors = new List<Models.DTO.Sensor>{ new Models.DTO.Sensor { Id = 1, Name = "Temperature", Description = "My Pi temperature", Type = "float", Unit = "celsius", Value = "27.5", TimeStamp = DateTime.Today, Frequency = 5000 } },
                    Actuators = new List<Models.DTO.Actuator>()
                }
               
            }
        };
        // GET: api/Device
        public IEnumerable<Models.DTO.Device> Get()
        {
            return Devices;
        }

        // GET: api/Device/5
        public IHttpActionResult Get(int id)
        {
            var devices = Devices.FirstOrDefault((p) => p.Id == id);
            if (devices == null)
            {
                return NotFound();
            }
            return Ok(devices);
        }

        // POST: api/Device
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Device/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Device/5
        public void Delete(int id)
        {
        }
    }
}
