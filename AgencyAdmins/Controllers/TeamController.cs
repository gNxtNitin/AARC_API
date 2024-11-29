using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Helper;
using System.Security.Claims;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        [HttpGet]
        public async Task<string> getAllAARCTeams()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_getall", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [HttpGet]
        [Route("TeamByUser/{userId}")]
        public async Task<string> GetTeamsByUser(string userId)
        {

            AALib.clsAAUserInfo.AAUI aauiUser = new AALib.clsAAUserInfo.AAUI();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            DataTable dtRet = null;
            List<string> listTeams = new List<string>();
            try
            {
                aauiUser = oUI.GetUserInfo(userId);
                var teams = aauiUser.AATeam.Split(';');
                AALib.clsAA oLib = new AALib.clsAA();
                foreach (var team in teams)
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sID", team));
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_from_id", lstParams);
                    if (dtRet.Rows.Count > 0)
                    {
                        listTeams.Add(dtRet.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                oUI = null;
            }

            return JsonConvert.SerializeObject(listTeams);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<string> getTeamFromID(string Id)
        {
            string sRet = "";
            if (Id.Trim().Length == 0) { return "No Team Assigned"; }
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", Id));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_from_id", lstParams);
                if (dtRet.Rows.Count > 0)
                {
                    sRet = dtRet.Rows[0][0].ToString();
                }
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

        [Route("Team/{Id}/{Name}")]
        [HttpPost]
        public async Task<int> addAARCTeam(string Id, string Name)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", Id));
                lstParams.Add(new SqlParameter("@sName", Name));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Id + ";" + Name);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("Team/{Id}")]
        [HttpDelete]
        public async Task<int>  deleteAARCTeam(string Id)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", Id));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_del", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Id);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("TeamManager/{TeamId}")]
        [HttpGet]
        public async Task<string> getAllAARCTeamMgrs(string TeamId = "")
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                if (TeamId.Length > 0) { lstParams.Add(new SqlParameter("@sTeamID", TeamId)); }

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_user_getall_teammgrs", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamId);
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

        [Route("TeamManager")]
        [HttpGet]
        public async Task<string> getAllAARCTeamMgrs()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
               // if (TeamId.Length > 0) { lstParams.Add(new SqlParameter("@sTeamID", "")); }

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_user_getall_teammgrs", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                //new LogException().logAARCErr(ex, TeamId);
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
        [Route("addTeamMgr/{TeamID}/{MgrID}")]
        public async Task<int> addAARCTeamMgr(string TeamID, string MgrID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sMgrID", MgrID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_add_mgr", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + MgrID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpPost]
        [Route("addTeamMember/{TeamID}/{UIDs}")]
        public async Task<int> addAARCTeamMembers(string TeamID, string UIDs)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                UIDs = UIDs.Replace(',', ';');
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sUIDs", UIDs));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_add_users", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + UIDs);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("UpdateTeam/{Id}/{Name}")]
        [HttpPost]
        public async Task<int> updateAARCTeam(string Id, string Name)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", Id));
                lstParams.Add(new SqlParameter("@sName", Name));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_edit", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Id + ";" + Name);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [HttpDelete]
        [Route("deleteTeamMember/{TeamID}/{UserID}")]
        public async Task<int> delAARCTeamMember(string TeamID, string UserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sUserID", UserID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_del_user", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID.ToString() + ";" + UserID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [HttpDelete]
        [Route("deleteTeamMgr/{TeamID}/{MgrID}")]
        public async Task<int> delAARCTeamMgr(string TeamID, string MgrID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sMgrID", MgrID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_del_mgr", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID.ToString() + ";" + MgrID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [HttpGet]
        [Route("teamBranches/{TeamID}")]
        public async Task<string>  getAllAARCTeamBranches(string TeamID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_getall_teambranches", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID);
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
        [Route("addTeamBranche/{TeamID}/{BranchID}")]
        public async Task<int> addAARCTeamBranch(string TeamID, string BranchID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sBranchID", BranchID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_add_branch", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + BranchID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpDelete]
        [Route("deleteTeamBranche/{TeamID}/{BranchID}")]
        public async Task<int> delAARCTeamBranch(string TeamID, string BranchID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sBranchID", BranchID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_del_branch", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + BranchID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpGet]
        [Route("teamStaff/{ID}")]
        public async Task<string>  getTeamStaff(string ID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", ID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_get_staff", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, ID);
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

        [HttpGet]
        [Route("TeamAvailableMembers/{ID}")]
        public async Task<string> getTeamAvailableMembers(string ID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", ID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_get_available_staff", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, ID);
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

        [HttpGet]
        [Route("TeamAgencies/{TeamID}")]
        public async Task<string> getAllAARCTeamAgencies(string TeamID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                if (TeamID.Length > 0) { lstParams.Add(new SqlParameter("@sTeamID", TeamID)); }

                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_team_getall_teamagencies", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID);
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
        [Route("addTeamAgency/{TeamID}/{AgyIDs}")]
        public async Task<int> addAARCTeamAgency(string TeamID, string AgyIDs)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sAgyIDs", AgyIDs));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_add_agency", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + AgyIDs);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [HttpDelete]
        [Route("deleteTeamAgency/{TeamID}/{AgencyID}")]
        public async Task<int> delAARCTeamAgency(string TeamID, string AgencyID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTeamID", TeamID));
                lstParams.Add(new SqlParameter("@sAgencyID", AgencyID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_team_del_agency", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, TeamID + ";" + AgencyID);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


    }
}
