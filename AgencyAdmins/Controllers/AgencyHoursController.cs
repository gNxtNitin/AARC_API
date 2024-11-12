using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;
using AgencyAdmins.Helper;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyHoursController : ControllerBase
    {
        [Route("{AgencyID}")]
        [HttpGet]
        public async Task<string> getAgencyHours(string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_biz_hours", lstParams);
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
        public async Task<int>  updAgencyHours(AgencyHours agencyHours)
        {

            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyHours.AgencyID));
                lstParams.Add(new SqlParameter("@sAgencyMonHrs", agencyHours.AgencyMonHrs));
                lstParams.Add(new SqlParameter("@sAgencyTueHrs", agencyHours.AgencyTueHrs));
                lstParams.Add(new SqlParameter("@sAgencyWedHrs", agencyHours.AgencyWedHrs));
                lstParams.Add(new SqlParameter("@sAgencyThuHrs", agencyHours.AgencyThuHrs));
                lstParams.Add(new SqlParameter("@sAgencyFriHrs", agencyHours.AgencyFriHrs));
                lstParams.Add(new SqlParameter("@sAgencySatHrs", agencyHours.AgencySatHrs));
                lstParams.Add(new SqlParameter("@sAgencySunHrs", agencyHours.AgencySunHrs));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_biz_hours_upd", lstParams);
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

        [Route("updAAHours")]
        [HttpPut]
        public async Task<int> updAAHours(AgencyHours agencyHours)
        {

            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAAMonHrs", agencyHours.AgencyMonHrs));
                lstParams.Add(new SqlParameter("@sAATueHrs", agencyHours.AgencyTueHrs));
                lstParams.Add(new SqlParameter("@sAAWedHrs", agencyHours.AgencyWedHrs));
                lstParams.Add(new SqlParameter("@sAAThuHrs", agencyHours.AgencyThuHrs));
                lstParams.Add(new SqlParameter("@sAAFriHrs", agencyHours.AgencyFriHrs));
                lstParams.Add(new SqlParameter("@sAASatHrs", agencyHours.AgencySatHrs));
                lstParams.Add(new SqlParameter("@sAASunHrs", agencyHours.AgencySunHrs));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_hours_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, agencyHours.AgencyMonHrs + ";" + agencyHours.AgencyTueHrs + ";" + agencyHours.AgencyWedHrs + ";" + agencyHours.AgencyThuHrs + ";" +
                    agencyHours.AgencyFriHrs + ";" + agencyHours.AgencySatHrs + ";" + agencyHours.AgencySunHrs);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

    }
}
