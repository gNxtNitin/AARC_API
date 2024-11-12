using AALib;
using AgencyAdmins.Helper;
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
    public class AgencyCarrierController : ControllerBase
    {
        [Route("carriers/{AgencyID}")]
        [HttpGet]
        public async Task<string> getAgencyCarriers(string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_Carriers", lstParams);
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


        [Route("{AgencyID}/{UserID}")]
        [HttpGet]
        public async Task<string> getAgencyCarriersByTeamBranch(string AgencyID, string UserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                lstParams.Add(new SqlParameter("@sUserID", UserID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_carriers_by_team_branch", lstParams);
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


        [HttpPost]
        public async Task<int> addAARCAgencyCarrier(AgencyCarrier agencyCarrier)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyCarrier.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", agencyCarrier.CarrierID));
                lstParams.Add(new SqlParameter("@sProducerCode", agencyCarrier.ProducerCode));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_carrier_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }

            return iRet;
        }


        [HttpDelete]
        public async Task<int> delAARCAgencyCarrier(string AgencyID, int CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_carrier_del", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }

            return iRet;
        }

        [HttpGet("AllCarriers")]
        public async Task<string> getAllAARCCarriers()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_getall", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, "");
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [HttpGet("carrier/{CarrierID}")]
        public async Task<string> getCarrierInfoByID(int CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_get_by_id", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, CarrierID.ToString());
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [HttpGet("contacts/{CarrierID}/{AgencyID}")]
        public async Task<string> getCarrierContacts(string CarrierID, string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_get_contacts", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, CarrierID.ToString());
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [HttpGet("ContactInfo/{CarrierID}/{ContactID}")]
        public async Task<string> getCarrierContactInfo(string CarrierID, string ContactID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sContactID", ContactID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_get_contacts_by_id", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, CarrierID.ToString() + ";" + ContactID);
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("addCarrier")]
        [HttpPost]
        public async Task<int>  addAARCCarrier(Carrier carrier)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", carrier.CarrierID));
                lstParams.Add(new SqlParameter("@sCarrierName", carrier.CarrierName));
                lstParams.Add(new SqlParameter("@sCarrierMAddr1", carrier.CarrierMAddr1));
                lstParams.Add(new SqlParameter("@sCarrierMAddr2", carrier.CarrierMAddr2));
                lstParams.Add(new SqlParameter("@sCarrierMCity", carrier.CarrierMCity));
                lstParams.Add(new SqlParameter("@sCarrierMState", carrier.CarrierMState));
                lstParams.Add(new SqlParameter("@sCarrierMZip", carrier.CarrierMZip));
                lstParams.Add(new SqlParameter("@sCarrierAddr1", carrier.CarrierAddr1));
                lstParams.Add(new SqlParameter("@sCarrierAddr2", carrier.CarrierAddr2));
                lstParams.Add(new SqlParameter("@sCarrierCity", carrier.CarrierCity));
                lstParams.Add(new SqlParameter("@sCarrierState", carrier.CarrierState));
                lstParams.Add(new SqlParameter("@sCarrierZip", carrier.CarrierZip));
                lstParams.Add(new SqlParameter("@sCarrierSpeedDial", carrier.CarrierSpeedDial));
                lstParams.Add(new SqlParameter("@sCarrierPhone", carrier.CarrierPhone));
                lstParams.Add(new SqlParameter("@sCarrierFax", carrier.CarrierFax));
                lstParams.Add(new SqlParameter("@sCarrierWeb", carrier.CarrierWeb));
                lstParams.Add(new SqlParameter("@sCarrierEmail", carrier.CarrierEmail));
                lstParams.Add(new SqlParameter("@sCarrierNotes", carrier.CarrierNotes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_add", lstParams);
                //if (iRet == 1)
                //{
                List<SqlParameter> lstParams2 = new List<SqlParameter>();
                lstParams2.Add(new SqlParameter("@sCarrierName", carrier.CarrierName));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_get_id_by_name", lstParams2);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, carrier.CarrierID.ToString() + ";" + carrier.CarrierName + ";" + carrier.CarrierMAddr1 + ";" +
                    carrier.CarrierMAddr2 + ";" + carrier.CarrierMCity + ";" + carrier.CarrierMState + ";" + carrier.CarrierMZip + ";" +
                     carrier.CarrierAddr1 + ";" + carrier.CarrierAddr2 + ";" + carrier.CarrierCity + ";" + carrier.CarrierState +
                    ";" + carrier.CarrierZip + ";" + carrier.CarrierPhone + ";" + carrier.CarrierFax + ";" + carrier.CarrierWeb + ";" + carrier.CarrierEmail + ";" + carrier.CarrierNotes);
            }
            finally
            {
            }

            return int.Parse(dtRet.Rows[0][0].ToString());
        }

        [Route("updateCarrier")]
        [HttpPost]
        public async Task<int> updAARCCarrier(Carrier carrier)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", carrier.CarrierID));
                lstParams.Add(new SqlParameter("@sCarrierName", carrier.CarrierName));
                lstParams.Add(new SqlParameter("@sCarrierMAddr1", carrier.CarrierMAddr1));
                lstParams.Add(new SqlParameter("@sCarrierMAddr2", carrier.CarrierMAddr2));
                lstParams.Add(new SqlParameter("@sCarrierMCity", carrier.CarrierMCity));
                lstParams.Add(new SqlParameter("@sCarrierMState", carrier.CarrierMState));
                lstParams.Add(new SqlParameter("@sCarrierMZip", carrier.CarrierMZip));
                lstParams.Add(new SqlParameter("@sCarrierAddr1", carrier.CarrierAddr1));
                lstParams.Add(new SqlParameter("@sCarrierAddr2", carrier.CarrierAddr2));
                lstParams.Add(new SqlParameter("@sCarrierCity", carrier.CarrierCity));
                lstParams.Add(new SqlParameter("@sCarrierState", carrier.CarrierState));
                lstParams.Add(new SqlParameter("@sCarrierZip", carrier.CarrierZip));
                lstParams.Add(new SqlParameter("@sCarrierSpeedDial", carrier.CarrierSpeedDial));
                lstParams.Add(new SqlParameter("@sCarrierPhone", carrier.CarrierPhone));
                lstParams.Add(new SqlParameter("@sCarrierFax", carrier.CarrierFax));
                lstParams.Add(new SqlParameter("@sCarrierWeb", carrier.CarrierWeb));
                lstParams.Add(new SqlParameter("@sCarrierEmail", carrier.CarrierEmail));
                lstParams.Add(new SqlParameter("@sCarrierNotes", carrier.CarrierNotes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException log = new LogException();
                log.logAARCErr(ex, carrier.CarrierID.ToString() + ";" + carrier.CarrierName + ";" + carrier.CarrierMAddr1 + ";" + carrier.CarrierMAddr2 + ";" + carrier.CarrierMCity + ";" + carrier.CarrierMState +
                    ";" + carrier.CarrierMZip + ";" + carrier.CarrierAddr1 + ";" + carrier.CarrierAddr2 + ";" + carrier.CarrierCity + ";" + carrier.CarrierState +
                   ";" + carrier.CarrierZip + ";" + carrier.CarrierPhone + ";" + carrier.CarrierFax + ";" + carrier.CarrierWeb + ";" + carrier.CarrierEmail + ";" + carrier.CarrierNotes);
            }
            finally
            {
            }

            return iRet;
        }

        [HttpDelete]
        [Route("archiveCarrier/{CarrierID}")]
        public async Task<int> arcAARCCarrier(int CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_archive", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID.ToString());
            }
            finally
            {
            }

            return iRet;
        }

    }
}
