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
    public class ProducerCodeCredentialController : ControllerBase
    {
        [Route("{sUserID}/{sAgencyID}/{sCarrierID}")]
        [HttpGet]
        public async Task<string> getProducerCodeCreds(string sUserID, string sAgencyID, string sCarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", sAgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", sCarrierID));
                lstParams.Add(new SqlParameter("@sUserID", sUserID));

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_producer_code_creds_for_user", lstParams);
                //dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_producer_creds_get", lstParams);
                //dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_producer_creds_get", lstParams);
                //dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_producer_get_creds", lstParams);
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

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("{sUserID}/{sAgencyID}/{sCarrierID}/{sProdCode}/{sPW}")]
        [HttpPost]
        public async Task<string> updProducerCodeUserCreds(string sUserID, string sAgencyID, string sCarrierID, string sProdCode, string sPCUID, string sPW)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", sUserID));
                lstParams.Add(new SqlParameter("@sAgencyID", sAgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", sCarrierID));
                lstParams.Add(new SqlParameter("@sPC", sProdCode));
                lstParams.Add(new SqlParameter("@sPCUID", sPCUID));
                lstParams.Add(new SqlParameter("@sPW", sPW));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_producer_creds_upd", lstParams);
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

            return JsonConvert.SerializeObject(iRet);
        }

        [HttpGet]
        [Route("{SrcID}/{DestID}")]
        public async Task<string> ImportCreds(string SrcID, string DestID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sSrcID", SrcID));
                lstParams.Add(new SqlParameter("@sDestID", DestID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_producer_code_creds_import", lstParams);
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

            return JsonConvert.SerializeObject(iRet);
        }

    }
}
