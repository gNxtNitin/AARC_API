using AALib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeZoneController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_timezone", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                oLib = null;
            }

            if (dtRet.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dtRet);
            }
            else
            {
                return "";
            }
        }

        [HttpPut]
        [Route("{timeZone}/{Id}")]
        public async Task<int> update(string timeZone, string Id)
        {

            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@agencyCode", Id));
                lstParams.Add(new SqlParameter("@timeZone", timeZone));

                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_timezone_update", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpGet]
        [Route("timeZoneById/{Id}")]
        public async Task<string> GetTimeZone(string Id)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@agencyCode", Id));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "get_timezone", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                oLib = null;
            }

            if (dtRet.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dtRet);
            }
            else
            {
                return "";
            }
        }
    }
}
