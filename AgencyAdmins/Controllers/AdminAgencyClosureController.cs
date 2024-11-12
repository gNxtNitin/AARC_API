using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAgencyClosureController : ControllerBase
    {
        [HttpGet]
        [Route("AAClosures")]
        public async Task<string> getAAClosures()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_AA_closures_get", lstParams);
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


        [HttpPost]
        [Route("AAClosures")]
        public async Task<int> addAAClosure(Closure closure)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sClosureDate", closure.ClosureDate));
                lstParams.Add(new SqlParameter("@sClosureReason", closure.ClosureReason));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_closures_add", lstParams);
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

        [HttpDelete]
        [Route("AAClosures")]
        public async Task<int> delAAClosure(Closure closure)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sClosureDate", closure.ClosureDate));
                lstParams.Add(new SqlParameter("@sClosureReason", closure.ClosureReason));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_closures_del", lstParams);
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
    }
}
