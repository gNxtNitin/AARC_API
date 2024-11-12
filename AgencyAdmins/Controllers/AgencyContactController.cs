using AALib;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyContactController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> addAARCAgencyContact(AgencyContact agencyContact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyContact.AgencyID));
                lstParams.Add(new SqlParameter("@sFName", agencyContact.FName));
                lstParams.Add(new SqlParameter("@sLName", agencyContact.LName));
                lstParams.Add(new SqlParameter("@sMName", agencyContact.MName));
                lstParams.Add(new SqlParameter("@sSuffix", agencyContact.Suffix));
                lstParams.Add(new SqlParameter("@sPhone", agencyContact.Phone));
                lstParams.Add(new SqlParameter("@sEmail", agencyContact.Email));
                lstParams.Add(new SqlParameter("@sNotes", agencyContact.Notes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_contact_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                return BadRequest("Error occured");
            }
            finally
            {
            }

            return Ok(iRet);
        }

        [Route("updAARCAgencyContact/{ContactID}")]
        [HttpPut]
        public async Task<int> updAARCAgencyContact(string ContactID, [FromBody]AgencyContact agencyContact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyContact.AgencyID));
                lstParams.Add(new SqlParameter("@sContactID", ContactID));
                lstParams.Add(new SqlParameter("@sFName", agencyContact.FName));
                lstParams.Add(new SqlParameter("@sLName", agencyContact.LName));
                lstParams.Add(new SqlParameter("@sMName", agencyContact.MName));
                lstParams.Add(new SqlParameter("@sSuffix", agencyContact.Suffix));
                lstParams.Add(new SqlParameter("@sPhone", agencyContact.Phone));
                lstParams.Add(new SqlParameter("@sEmail", agencyContact.Email));
                lstParams.Add(new SqlParameter("@sNotes", agencyContact.Notes));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_contact_upd", lstParams);
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

        [Route("UpdateAgencyContactOrder")]
        [HttpPut]
        public async Task<int> updAARCAgencyContactOrder(string sAgencyID, string sContactID, int iUp)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", sAgencyID));
                lstParams.Add(new SqlParameter("@sContactID", sContactID));
                lstParams.Add(new SqlParameter("@bMoveUp", iUp));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_contact_sort_upd", lstParams);
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
        public async Task<int> delAARCAgencyContact(string sAgencyID, string sContactID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", sAgencyID));
                lstParams.Add(new SqlParameter("@sContactID", sContactID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_contact_del", lstParams);
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

        [Route("updAAContactInfo")]
        [HttpPut]
        public async Task<int> updAAContactInfo(AdminAgencyContactInfo adminAgencyContactInfo)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAAAddr1", adminAgencyContactInfo.AAAddr1));
                lstParams.Add(new SqlParameter("@sAAAddr2", adminAgencyContactInfo.AAAddr2));
                lstParams.Add(new SqlParameter("@sAACity", adminAgencyContactInfo.AACity));
                lstParams.Add(new SqlParameter("@sAAState", adminAgencyContactInfo.AAState));
                lstParams.Add(new SqlParameter("@sAAZip", adminAgencyContactInfo.AAZip));
                lstParams.Add(new SqlParameter("@sAAPhone", adminAgencyContactInfo.AAPhone));
                lstParams.Add(new SqlParameter("@sAAFax", adminAgencyContactInfo.AAFax));
                lstParams.Add(new SqlParameter("@sAAWeb", adminAgencyContactInfo.AAWeb));
                lstParams.Add(new SqlParameter("@sAAEmail", adminAgencyContactInfo.AAEmail));
                lstParams.Add(new SqlParameter("@sAAAcct", adminAgencyContactInfo.AAAcct));
                lstParams.Add(new SqlParameter("@sAADwnld", adminAgencyContactInfo.AADwnld));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_contact_info_upd", lstParams);
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
