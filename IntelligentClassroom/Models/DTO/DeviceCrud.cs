using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IntelligentClassroom.Models.DTO
{
    public class DeviceCrud
    {

        #region [-Insert(DomainModels.DTO.EF.ProductCategory ref_ProductCategory)-]
        public async Task Insert(Models.EF.Device ref_Device)
        {
            using (var context = new Models.EF.WebofThingsEntities1())
            {
                try
                {

                    context.Device.Add(ref_Device);
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
        public async Task<List<Models.EF.Device>> SelectAll()
        {
            using (var context = new EF.WebofThingsEntities1())
            {

                try
                {
                    //to avoid server error
                    //context.Configuration.ProxyCreationEnabled = false;
                    var q = context.Device.ToListAsync();

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

        #region [-Remove(DomainModel.DTO.EF.ProductCategory ref_ProductCategory)-]
        public async Task Remove(EF.Device ref_Device)
        {
            using (var context = new EF.WebofThingsEntities1())
            {
                try
                {
                    var itemToRemove = context.Device.SingleOrDefault(x => x.Id == ref_Device.Id);
                    if (itemToRemove != null)
                    {
                        context.Device.Remove(itemToRemove);
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

        #region [-Update(DomainModel.DTO.EF.ProductCategory ref_ProductCategory)-]
        public async Task Update(EF.Device ref_Device)
        {
            using (var context = new EF.WebofThingsEntities1())
            {
                try
                {
                    context.Entry(ref_Device).State = EntityState.Modified;
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