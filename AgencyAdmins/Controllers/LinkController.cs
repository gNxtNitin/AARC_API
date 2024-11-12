using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        [Route("AllLinks/{UserID}")]
        [HttpGet]
        public async Task<string> getAARCLinx(string UserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", UserID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_linx_get", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, UserID);
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

        [Route("AddLink")]
        [HttpPost]
        public async Task<int> addAARCLinx(Link link)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", link.UserID));
                lstParams.Add(new SqlParameter("@sType", link.Type));
                lstParams.Add(new SqlParameter("@sDisplay", link.Display));
                lstParams.Add(new SqlParameter("@sLinx", link.Linx));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_linx_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, link.UserID + ";" + link.Type + ";" + link.Display + ";" + link.Linx);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("deleteLink")]
        [HttpDelete]
        public async Task<int> delAARCLinx(Link link)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", link.UserID));
                lstParams.Add(new SqlParameter("@sType", link.Type));
                lstParams.Add(new SqlParameter("@sDisplay", link.Display));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_linx_del", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, link.UserID + ";" + link.Type + ";" + link.Display + ";" + link.Linx);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


    }
}
