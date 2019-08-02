using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IntelligentClassroom.Models.DTO
{
    public class SensorCrud
    {
        #region [-Insert(Models.EF.Sensor ref_Sensor)-]
        public async Task Insert(Models.EF.Sensor ref_Sensor)
        {
            using (var context = new Models.EF.WebofThingsEntities1())
            {
                try
                {

                    context.Sensor.Add(ref_Sensor);
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }

                }
            }
        }
        #endregion

        #region [-SelectAll()-]
        public async Task<List<Models.EF.Sensor>> SelectAll()
        {
            using (var context = new EF.WebofThingsEntities1())
            {

                try
                {
             
                    var q = context.Sensor.ToListAsync();

                    return await q;
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                }
            }

        }
        #endregion

        #region [-Remove(EF.Sensor ref_Device)-]
        public async Task Remove(EF.Sensor ref_Sensor)
        {
            using (var context = new EF.WebofThingsEntities1())
            {
                try
                {
                    var itemToRemove = context.Sensor.SingleOrDefault(x => x.Id == ref_Sensor.Id);
                    if (itemToRemove != null)
                    {
                        context.Sensor.Remove(itemToRemove);
                        await context.SaveChangesAsync();
                    }
                }

                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }

                }
            }
        }
        #endregion

        #region [-Update(EF.Sensor ref_Sensor)-]
        public async Task Update(EF.Sensor ref_Sensor)
        {
            using (var context = new EF.WebofThingsEntities1())
            {
                try
                {
                    context.Entry(ref_Sensor).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
        }
        #endregion
    }
}