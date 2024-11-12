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
    public class CarrierContactController : ControllerBase
    {
        [HttpGet]
        [Route("similarCarrierContact/{ContactID}")]
        public async Task<string>  getAARCSimilarCarrierContacts(string ContactID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iContactID", ContactID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_carrier_contact_get_similar", lstParams);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, ContactID);
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
        [Route("addCarrierContact")]
        public async Task<int>  addAARCCarrierContact(CarrierContact carrierContact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                foreach (string sAID in carrierContact.ArrayAgencyID)
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iCarrierID", carrierContact.CarrierID));
                    lstParams.Add(new SqlParameter("@sFName", carrierContact.FName));
                    lstParams.Add(new SqlParameter("@sLName", carrierContact.LName));
                    lstParams.Add(new SqlParameter("@sMName", carrierContact.MName));
                    lstParams.Add(new SqlParameter("@sSuffix", carrierContact.Suffix));
                    lstParams.Add(new SqlParameter("@sAgencyID", sAID.Trim()));
                    lstParams.Add(new SqlParameter("@sPhone", carrierContact.Phone));
                    lstParams.Add(new SqlParameter("@sEmail", carrierContact.Email));
                    lstParams.Add(new SqlParameter("@sNotes", carrierContact.Notes));
                    iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_contact_add", lstParams);

                    if (sAID.ToUpper() == "ALL AGENCIES")
                    {
                        return iRet;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, carrierContact.CarrierID.ToString() + ";" + carrierContact.FName + ";" + carrierContact.LName +
                    ";" + carrierContact.Suffix + ";" + carrierContact.Phone + ";" + carrierContact.Email + ";" + carrierContact.Notes);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpPost]
        [Route("updateCarrierContact")]
        public async Task<int> updAARCCarrierContact(CarrierContact carrierContact)
        {
            //Delete Contacts previously assigned to other Agencies, but no longer
            delAARCCarrierContactByAgencyID(carrierContact.ContactID, carrierContact.AgencyID);

            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                foreach (string sAID in carrierContact.ArrayAgencyID)
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iCarrierID", carrierContact.CarrierID));
                    lstParams.Add(new SqlParameter("@sContactID", carrierContact.ContactID));
                    lstParams.Add(new SqlParameter("@sFName", carrierContact.FName));
                    lstParams.Add(new SqlParameter("@sLName", carrierContact.LName));
                    lstParams.Add(new SqlParameter("@sMName", carrierContact.MName));
                    lstParams.Add(new SqlParameter("@sSuffix", carrierContact.Suffix));
                    lstParams.Add(new SqlParameter("@sAgencyID", sAID.Trim()));
                    lstParams.Add(new SqlParameter("@sPhone", carrierContact.Phone));
                    lstParams.Add(new SqlParameter("@sEmail", carrierContact.Email));
                    lstParams.Add(new SqlParameter("@sNotes", carrierContact.Notes));
                    lstParams.Add(new SqlParameter("@sSimilarIDs", string.Concat(";", carrierContact.SimilarIDs.Replace(" ", ""), ";")));

                    iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_contact_upd", lstParams);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, carrierContact.CarrierID.ToString() + ";" + carrierContact.FName + ";" + carrierContact.LName + ";" + carrierContact.Suffix + ";" +
                    carrierContact.AgencyID + ";" + carrierContact.Phone + ";" + carrierContact.Email + ";" + carrierContact.Notes);
            }
            finally
            {
            }

            return iRet;
        }


        [HttpDelete]
        [Route("{ContactID}/{AgencyID}")]
        public async Task<int>  delAARCCarrierContactByAgencyID(string sContactID, string sAgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iContactID", sContactID));
                lstParams.Add(new SqlParameter("@sAgencyIDs", string.Concat(";", sAgencyID.Replace(" ", ""), ";")));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_contact_del_by_agency_id", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, sAgencyID + ";" + sContactID);
            }
            finally
            {
            }

            return iRet;
        }

        [HttpDelete]
        [Route("{CarrierID}/{ContactID}/{DelSimilar}")]
        public async Task<int> delAARCCarrierContact(int CarrierID, string ContactID, bool DelSimilar)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                lstParams.Add(new SqlParameter("@sContactID", ContactID));
                lstParams.Add(new SqlParameter("@bDelSimilar", DelSimilar ? 1 : 0));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_carrier_contact_del", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, CarrierID.ToString() + ";" + ContactID);
            }
            finally
            {
            }

            return iRet;
        }


    }
}
