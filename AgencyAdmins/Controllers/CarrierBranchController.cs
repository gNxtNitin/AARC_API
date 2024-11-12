using AALib;
using AgencyAdmins.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierBranchController : ControllerBase
    {
        [HttpGet]
        [Route("type/{CarrierID}")]
        public async Task<string>  getAllAARCCarrierBranchesTypes(string CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_getall_carrierbranchestypes", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex,CarrierID);
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
        [Route("addBranch/{CarrierID}")]
        public async Task<int> addAARCCarrierAllBranchesTypes(int CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_add_all_branch_or_type", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID.ToString());
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [HttpPost]
        [Route("addBranch/{CarrierID}/{BTID}")]
        public async Task<int> addAARCCarrierBranch(int CarrierID, string BTID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sBTID", BTID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_add_branch", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID + ";" + BTID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [HttpDelete]
        [Route("deleteCarrierBranch/{CarrierID}/{BTID}")]
        public async Task<int> delAARCCarrierBranch(int CarrierID, string BTID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sBTID", BTID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_del_branch", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID + ";" + BTID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpPut]
        [Route("updateBranch/{CarrierID}/{BTID}")]
        public async Task<int> updAARCCarrierType(int CarrierID, string BTID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sBTID", BTID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_upd_type", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID + ";" + BTID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }
    }
}
