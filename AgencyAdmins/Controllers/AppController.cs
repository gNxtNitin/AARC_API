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
    public class AppController : ControllerBase
    {
        [Route("{userID}")]
        [HttpGet]
        public async Task<string> Apps(string sUserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", sUserID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_apps_getall_by_userid", lstParams);
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

        [Route("UnavailableApps/{userID}")]
        [HttpGet]
        public async Task<string> getUserUnavailableApps(string sUserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", sUserID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_apps_getunavailable_by_userid", lstParams);
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


        [Route("Add/{userID}/{appID}/{secID}")]
        [HttpPost]
        public async Task<int> addUserApp(string userID, string appID, string secID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", userID));
                lstParams.Add(new SqlParameter("@sAppID", appID));
                lstParams.Add(new SqlParameter("@sSecID", secID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_apps_add_by_user_app_sec", lstParams);
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

        [Route("{userID}/{appID}/{secID}")]
        [HttpDelete]
        public async Task<int> delUserApp(string userID, string appID, string secID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", userID));
                lstParams.Add(new SqlParameter("@sAppID", appID));
                lstParams.Add(new SqlParameter("@sSecID", secID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_apps_del_by_user_app_sec", lstParams);
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
