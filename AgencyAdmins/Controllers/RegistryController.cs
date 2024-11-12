using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {

        [Route("GetRegistery/{key}")]
        [HttpGet]
        public async Task<string> getAARegistryVal(string key)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sKey", key));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_registry_get_val", lstParams);
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


        [Route("GetRegisteryLike/{likeKey}")]
        [HttpGet]
        public async Task<string> getAARegistryLike(string likeKey)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sLikeKey", likeKey));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_registry_get_like", lstParams);
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
