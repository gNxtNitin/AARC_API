using AALib;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerCodeController : ControllerBase
    {
        [HttpPut]
        public async Task<string> updProducerCode(ProducerCode producerCode)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCode.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", producerCode.CarrierID));
                lstParams.Add(new SqlParameter("@sOldCode", producerCode.OldCode));
                lstParams.Add(new SqlParameter("@sNewCode", producerCode.NewCode));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_producer_code_upd", lstParams);
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

        [HttpPost]
        public async Task<int> addProducerCode(ProducerCodeRequest producerCodeRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCodeRequest.AgencyID));
                lstParams.Add(new SqlParameter("@sCarrierID", producerCodeRequest.CarrierID));
                lstParams.Add(new SqlParameter("@sProducerCode", producerCodeRequest.ProducerCode));
                lstParams.Add(new SqlParameter("@sPCBranch", producerCodeRequest.PCBranch));
                lstParams.Add(new SqlParameter("@sUID", producerCodeRequest.UID));
                lstParams.Add(new SqlParameter("@sPWD", producerCodeRequest.PWD));
                lstParams.Add(new SqlParameter("@sGrantedUsers", producerCodeRequest.GrantedUsers));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_producer_code_add", lstParams);
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

        [Route("Update")]
        [HttpPut]
        public async Task<int> updProducerCode([FromBody] ProducerCodeRequest producerCodeRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCodeRequest.AgencyID));
                lstParams.Add(new SqlParameter("@sCarrierID", producerCodeRequest.CarrierID));
                lstParams.Add(new SqlParameter("@sOldProducerCode", producerCodeRequest.ProducerCode));
                lstParams.Add(new SqlParameter("@sOldPCBranch", producerCodeRequest.PCBranch));
                lstParams.Add(new SqlParameter("@sNewProducerCode", producerCodeRequest.NewProducerCode));
                lstParams.Add(new SqlParameter("@sNewPCBranch", producerCodeRequest.NewPCBranch));
                lstParams.Add(new SqlParameter("@sUID", producerCodeRequest.UID));
                lstParams.Add(new SqlParameter("@sPWD", producerCodeRequest.PWD));
                lstParams.Add(new SqlParameter("@sGrantedUsers", producerCodeRequest.GrantedUsers));

                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_producer_code_upd", lstParams);
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

        //[Route("{AgencyID}/{CarrierID}/{ProducerCode}/{PCBranch}")]
        [HttpDelete]
        public async Task<int> delProducerCode([FromBody] ProducerCodeRequest producerCodeRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCodeRequest.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", producerCodeRequest.CarrierID));
                lstParams.Add(new SqlParameter("@sProducerCode", producerCodeRequest.ProducerCode));
                lstParams.Add(new SqlParameter("@sPCBranch", producerCodeRequest.PCBranch));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_producer_code_del", lstParams);
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

        [Route("ProducerCodeGrantedUsers")]
        [HttpPost]
        public async Task<string>  getAllProducerCodeGrantedUsers([FromBody] ProducerCodeRequest producerCodeRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCodeRequest.AgencyID));
                lstParams.Add(new SqlParameter("@sCarrierID", producerCodeRequest.CarrierID));
                lstParams.Add(new SqlParameter("@sProducerCode", producerCodeRequest.ProducerCode));
                lstParams.Add(new SqlParameter("@sPCBranch", producerCodeRequest.PCBranch));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_producer_code_get_granted_users", lstParams);
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

        [Route("ProducerCodeDetails")]
        [HttpPost]
        public async Task<string> getProducerCodeDetails([FromBody] ProducerCodeRequest producerCodeRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", producerCodeRequest.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", producerCodeRequest.CarrierID));
                lstParams.Add(new SqlParameter("@sProducerCode", producerCodeRequest.ProducerCode));
                lstParams.Add(new SqlParameter("@sPCBranch", producerCodeRequest.PCBranch));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_producer_code_get_details", lstParams);
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

        [Route("{AgencyID}/{CarrierID}")]
        [HttpGet]
        public async Task<string> getAgencyCarrierProducerCodes(string AgencyID, string CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_producer_code_get", lstParams);
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
