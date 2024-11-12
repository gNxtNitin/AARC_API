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
    public class DirectoryController : ControllerBase
    {
        [Route("DirectoryContacts/{AgencyID}/{CarrierID}")]
        [HttpGet]
        public async Task<string> getDirectoryContacts(string AgencyID, int CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", CarrierID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_directory_get_contacts", lstParams);
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

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("DirectoryContactInfo/{DirectoryID}")]
        [HttpGet]
        public async Task<string> getDirectoryContactInfo(int DirectoryID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iDirectoryID", DirectoryID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_directory_get_by_id", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                 new LogException().logAARCErr(ex, DirectoryID.ToString());
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("addDirectoryContacts")]
        [HttpPost]
        public async Task<int> addAARCDirectoryContact(DirectoryContact directoryContact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", directoryContact.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", directoryContact.CarrierID));
                lstParams.Add(new SqlParameter("@sFName", directoryContact.FName));
                lstParams.Add(new SqlParameter("@sLName", directoryContact.LName));
                lstParams.Add(new SqlParameter("@sMName", directoryContact.MName));
                lstParams.Add(new SqlParameter("@sSuffix", directoryContact.Suffix));
                lstParams.Add(new SqlParameter("@sPhone", directoryContact.Phone));
                lstParams.Add(new SqlParameter("@sEmail", directoryContact.Email));
                lstParams.Add(new SqlParameter("@sNotes", directoryContact.Notes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_Directory_contact_add", lstParams);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                 new LogException().logAARCErr(ex, directoryContact.AgencyID + ";" + directoryContact.CarrierID.ToString() + ";" + directoryContact.FName + ";" + directoryContact.LName + ";" + directoryContact.Suffix + ";" + directoryContact.Phone + ";" + directoryContact.Email + ";" + directoryContact.Notes);
            }
            finally
            {
            }

            return iRet;
        }

        [Route("DirectoryContacts")]
        [HttpPut]
        public async Task<int> updAARCDirectoryContact(DirectoryContact directoryContact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iDirectoryID", directoryContact.DirectoryID));
                lstParams.Add(new SqlParameter("@sFName", directoryContact.FName));
                lstParams.Add(new SqlParameter("@sLName", directoryContact.LName));
                lstParams.Add(new SqlParameter("@sMName", directoryContact.MName));
                lstParams.Add(new SqlParameter("@sSuffix", directoryContact.Suffix));
                lstParams.Add(new SqlParameter("@sPhone", directoryContact.Phone));
                lstParams.Add(new SqlParameter("@sEmail", directoryContact.Email));
                lstParams.Add(new SqlParameter("@sNotes", directoryContact.Notes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_Directory_contact_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                 new LogException().logAARCErr(ex, directoryContact.DirectoryID.ToString() + ";" + directoryContact.FName + ";" + directoryContact.LName + ";" + directoryContact.Suffix + ";" + directoryContact.Phone + ";" + directoryContact.Email + ";" + directoryContact.Notes);
            }
            finally
            {
            }

            return iRet;
        }

        [Route("DirectoryContacts")]
        [HttpDelete]
        public async Task<int> delAARCDirectoryContact(int iDirectoryID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iDirectoryID", iDirectoryID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_Directory_contact_del", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                 new LogException().logAARCErr(ex, iDirectoryID.ToString() + ";" + iDirectoryID);
            }
            finally
            {
            }

            return iRet;
        }

    }
}
