using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using AgencyAdmins.Helper;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecutiyController : ControllerBase
    {

        [Route("{sCode}")]
        [HttpGet]
        public async Task<string> getSecLvlFromCode(string sCode)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCode", sCode));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_seclvl_from_code", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }
            return JsonConvert.SerializeObject(dtRet.Rows[0][0].ToString());
        }

        [HttpGet]
        [Route("allSecurities")]
        public async Task<string> getAllAARCSecurities()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_sec_getall", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

    }
}
