using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;
using SimpleImpersonation;
using System.IO;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        const string gsDoxUn = "Amckee"; /*"AARCDOX";*/
        const string gsDoxPw = "Kp01q165Jw1xcY";

        [Route("AgenciesByUser/{userID}")]
        [HttpGet]
        public async Task<string> getAgenciesByUserID(string userID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", userID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_getall_by_userid", lstParams);
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

        [Route("Agencies")]
        [HttpGet]
        public async Task<string> getAllAARCAgencies()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_getall", lstParams);
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

        [Route("GetAgency/{AgencyID}")]
        [HttpGet]
        public async Task<string> getAgencyInfoByID(string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_by_id", lstParams);
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


        [Route("GetAgencyContacts/{agencyID}")]
        [HttpGet]
        public async Task<string> getAgencyContacts(string agencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_contacts", lstParams);
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

        [Route("GetAgencyContactInfo/{agencyID}/{contactID}")]
        [HttpGet]
        public async Task<string> getAgencyContactInfo(string agencyID, string contactID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyID));
                lstParams.Add(new SqlParameter("@sContactID", contactID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_get_contacts_by_id", lstParams);
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

        [Route("addAgency")]
        [HttpPost]
        public async Task<int> addAARCAgency(Agency agency)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agency.AgencyID));
                lstParams.Add(new SqlParameter("@sAgencyName", agency.AgencyName));
                lstParams.Add(new SqlParameter("@sAgencyMAddr1", agency.AgencyMAddr1));
                lstParams.Add(new SqlParameter("@sAgencyMAddr2", agency.AgencyMAddr2));
                lstParams.Add(new SqlParameter("@sAgencyMCity", agency.AgencyMCity));
                lstParams.Add(new SqlParameter("@sAgencyMState", agency.AgencyMState));
                lstParams.Add(new SqlParameter("@sAgencyMZip", agency.AgencyMZip));
                lstParams.Add(new SqlParameter("@sAgencyAddr1", agency.AgencyAddr1));
                lstParams.Add(new SqlParameter("@sAgencyAddr2", agency.AgencyAddr2));
                lstParams.Add(new SqlParameter("@sAgencyCity", agency.AgencyCity));
                lstParams.Add(new SqlParameter("@sAgencyState", agency.AgencyState));
                lstParams.Add(new SqlParameter("@sAgencyZip", agency.AgencyZip));
                lstParams.Add(new SqlParameter("@sAgencySpeedDial", agency.AgencySpeedDial));
                lstParams.Add(new SqlParameter("@sAgencyPhone", agency.AgencyPhone));
                lstParams.Add(new SqlParameter("@sAgencyText", agency.AgencyText));
                lstParams.Add(new SqlParameter("@sAgencyFax", agency.AgencyFax));
                lstParams.Add(new SqlParameter("@sAgencyWeb", agency.AgencyWeb));
                lstParams.Add(new SqlParameter("@sAgencyEmail", agency.AgencyEmail));
                lstParams.Add(new SqlParameter("@sAgencyNotes", agency.AgencyNotes));
                lstParams.Add(new SqlParameter("@sAgencyLicense", agency.AgencyLicense));
                lstParams.Add(new SqlParameter("@sAgencyStateAppts", agency.AgencyStateAppts));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_add", lstParams);
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

        [Route("updateAgency")]
        [HttpPut]
        public async Task<int> updAARCAgency(Agency agency)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agency.AgencyID));
                lstParams.Add(new SqlParameter("@sAgencyName", agency.AgencyName));
                lstParams.Add(new SqlParameter("@sAgencyMAddr1", agency.AgencyMAddr1));
                lstParams.Add(new SqlParameter("@sAgencyMAddr2", agency.AgencyMAddr2));
                lstParams.Add(new SqlParameter("@sAgencyMCity", agency.AgencyMCity));
                lstParams.Add(new SqlParameter("@sAgencyMState", agency.AgencyMState));
                lstParams.Add(new SqlParameter("@sAgencyMZip", agency.AgencyMZip));
                lstParams.Add(new SqlParameter("@sAgencyAddr1", agency.AgencyAddr1));
                lstParams.Add(new SqlParameter("@sAgencyAddr2", agency.AgencyAddr2));
                lstParams.Add(new SqlParameter("@sAgencyCity", agency.AgencyCity));
                lstParams.Add(new SqlParameter("@sAgencyState", agency.AgencyState));
                lstParams.Add(new SqlParameter("@sAgencyZip", agency.AgencyZip));
                lstParams.Add(new SqlParameter("@sAgencySpeedDial", agency.AgencySpeedDial));
                lstParams.Add(new SqlParameter("@sAgencyPhone", agency.AgencyPhone));
                lstParams.Add(new SqlParameter("@sAgencyText", agency.AgencyText));
                lstParams.Add(new SqlParameter("@sAgencyFax", agency.AgencyFax));
                lstParams.Add(new SqlParameter("@sAgencyWeb", agency.AgencyWeb));
                lstParams.Add(new SqlParameter("@sAgencyEmail", agency.AgencyEmail));
                lstParams.Add(new SqlParameter("@sAgencyNotes", agency.AgencyNotes));
                lstParams.Add(new SqlParameter("@sAgencyLicense", agency.AgencyLicense));
                lstParams.Add(new SqlParameter("@sAgencyStateAppts", agency.AgencyStateAppts));

                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_upd", lstParams);
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

        [Route("agencyIdExist/{agencyID}")]
        [HttpGet]
        public async Task<int> AgencyIDExists(string agencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_agency_exists", lstParams);
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

            return dtRet.Rows.Count;
        }

        [Route("archiveAgency/{agencyID}")]
        [HttpDelete]
        public async Task<int> ArchiveAgency(string agencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", agencyID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_archive", lstParams);
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

        [Route("AgencyDox")]
        [HttpPost]
        public async Task<int> addAgencyDox(String sAttachment, String sAttachmentFilename)
        {
            int iRet = 0;
            try
            {
                UserCredentials credentials = new UserCredentials("AAADVANIC", gsDoxUn, gsDoxPw);
                Impersonation.RunAsUser(credentials, LogonType.Network, () =>
                {
                    byte[] bytes = Convert.FromBase64String(sAttachment);
                    object path = AppDomain.CurrentDomain.GetData("APPBASE");
                    string filePath = String.Format("{0}AARCDOX\\AgencyInfoDox\\{1}", path, Path.GetFileName(sAttachmentFilename.Replace("~", "\\")));
                    //store data into a file in Add_Data folder
                    System.IO.File.WriteAllBytes(filePath, bytes);
                });
                iRet = 1;
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

        [Route("UpdateAgencyId/{CurID}/{NewID}")]
        [HttpPut]
        public async Task<int> updAgencyID(string sCurID, string sNewID)
        {

            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCurID", sCurID));
                lstParams.Add(new SqlParameter("@sNewID", sNewID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_id_upd", lstParams);

                UserCredentials credentials = new UserCredentials("AAADVANIC", gsDoxUn, gsDoxPw);
                Impersonation.RunAsUser(credentials, LogonType.Network, () =>
                {
                    object path = AppDomain.CurrentDomain.GetData("APPBASE");
                    string sSrc = String.Format("{0}AARCDOX\\AgencyInfoDox\\{1}", path, string.Concat(sCurID, ".pdf"));
                    string sDest = String.Format("{0}AARCDOX\\AgencyInfoDox\\{1}", path, string.Concat(sNewID, ".pdf"));

                    //store data into a file in Add_Data folder
                    System.IO.File.Copy(sSrc, sDest, true);
                    System.IO.File.Delete(sSrc);
                });

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


        [Route("ArchiveAgency/{AgencyID}")]
        [HttpPut]
        public async Task<int> arcAARCAgency(string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_agency_archive", lstParams);
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

    }
}
