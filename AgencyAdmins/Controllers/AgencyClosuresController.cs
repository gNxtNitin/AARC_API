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
    public class AgencyClosuresController : ControllerBase
    {
        [Route("{AgencyID}")]
        [HttpGet]
        public async Task<string> getAgencyClosures(string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_biz_holidays", lstParams);
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
        public async Task<int>  addAgencyClosure(AgencyClosure agencyClosure)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyClosure.AgencyID));
                lstParams.Add(new SqlParameter("@dHolDate", agencyClosure.ClosureDate));
                lstParams.Add(new SqlParameter("@sHolReason", agencyClosure.ClosureReason));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_biz_holidays_add", lstParams);
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
        public async Task<int> delAgencyClosure(AgencyClosure agencyClosure)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyClosure.AgencyID));
                lstParams.Add(new SqlParameter("@dHolDate", agencyClosure.ClosureDate));
                lstParams.Add(new SqlParameter("@sHolReason", agencyClosure.ClosureReason));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_biz_holidays_del", lstParams);
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
