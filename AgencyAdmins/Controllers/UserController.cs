using AALib;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using static AALib.clsAAUserInfo;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("Users")]
        [HttpGet]
        public async Task<string> GetAllUsers()
        {
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            DataTable dtRet = null;
            try
            {
                dtRet = oUI.GetAllUserIDs(AALib.clsAAUserInfo.validStatus.All);
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

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("UserInfo")]
        [HttpGet]
        public async Task<string> UserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string userId = "";
            if (identity != null)
            {
                userId = identity.FindFirst("UserId").Value;

            }
            AALib.clsAAUserInfo.AAUI aauiUser = new AALib.clsAAUserInfo.AAUI();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            try
            {
                aauiUser = oUI.GetUserInfo(userId);
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

            return JsonConvert.SerializeObject(aauiUser);
        }

        [Route("UserInfo/{userId}")]
        [HttpGet]
        public async Task<string> UserInfo(string userId)
        {
            AALib.clsAAUserInfo.AAUI aauiUser = new AALib.clsAAUserInfo.AAUI();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            try
            {
                aauiUser = oUI.GetUserInfo(userId);
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

            return JsonConvert.SerializeObject(aauiUser);
        }


        [Route("clone")]
        [HttpPost]
        public async Task<string> CloneUser(CloneUser clone)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            int iRet = 0;
            try
            {
                var aauiUser = oUI.GetUserInfoByEmail(clone.Email);
                if(aauiUser != null && aauiUser.Email != "")
                {
                    iRet = -1;
                }
                else
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sSrcID", clone.SrcID));
                    lstParams.Add(new SqlParameter("@sFN", clone.FN));
                    lstParams.Add(new SqlParameter("@sLN", clone.LN));
                    lstParams.Add(new SqlParameter("@sDestID", clone.DestID));
                    lstParams.Add(new SqlParameter("@email", clone.Email));
                    iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_clone", lstParams);
                    if (iRet != 0)
                    {
                        var password = "Welcome@123";
                        EmailSMTP emailServer = new EmailSMTP();
                        emailServer.SendEmail(clone.Email, "Admin Agency Password", "Your new Passwor id: " + password);
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
                oLib = null;
            }

            return JsonConvert.SerializeObject(iRet);
        }

        [Route("{sUserID}")]
        [HttpDelete]
        public async Task<int>  delAARCUser(string sUserID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", sUserID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_del", lstParams);
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

        [Route("adduser")]
        [HttpPost]
        public async Task<int> addAARCUser(User user)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", user.UserID));
                lstParams.Add(new SqlParameter("@sFname", user.FirstName));
                lstParams.Add(new SqlParameter("@sLname", user.LastName));
                lstParams.Add(new SqlParameter("@sMname", user.MiddleName));
                lstParams.Add(new SqlParameter("@sSuffix", user.Suffix));
                lstParams.Add(new SqlParameter("@sTeam", user.Team));
                lstParams.Add(new SqlParameter("@sSec", user.Sec));
                lstParams.Add(new SqlParameter("@sPhone", user.Phone));
                lstParams.Add(new SqlParameter("@sCell", user.Cell));
                lstParams.Add(new SqlParameter("@sEmail", user.Email));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_add", lstParams);
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

        [Route("updateUser")]
        [HttpPut]
        public async Task<int> updAARCUserInfo(User user)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", user.UserID));
                lstParams.Add(new SqlParameter("@sFname", user.FirstName));
                lstParams.Add(new SqlParameter("@sLname", user.LastName));
                lstParams.Add(new SqlParameter("@sMname", user.MiddleName));
                lstParams.Add(new SqlParameter("@sSuffix", user.Suffix));
                lstParams.Add(new SqlParameter("@sTeam", user.Team));
                lstParams.Add(new SqlParameter("@sSec", user.Sec));
                lstParams.Add(new SqlParameter("@sPhone", user.Phone));
                lstParams.Add(new SqlParameter("@sCell", user.Cell));
                lstParams.Add(new SqlParameter("@sEmail", user.Email));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd", lstParams);
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


        [Route("updateUserName")]
        [HttpPut]
        public static int updAARCUserName(User user)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", user.UserID));
                lstParams.Add(new SqlParameter("@sFname", user.FirstName));
                lstParams.Add(new SqlParameter("@sLname", user.LastName));
                lstParams.Add(new SqlParameter("@sMname", user.MiddleName));
                lstParams.Add(new SqlParameter("@sSuffix", user.Suffix));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd_name", lstParams);
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

        [Route("updateUserStatus/{userId}/{Available}/{Active}")]
        [HttpPut]
        public static int updAARCUserStatus(string sUserID, bool bAvailable, bool bActive)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", sUserID));
                lstParams.Add(new SqlParameter("@bAvailable", bAvailable));
                lstParams.Add(new SqlParameter("@bActive", bActive));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd_status", lstParams);
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

        [Route("updateUserStatus/{userId}/{Team}")]
        [HttpPut]
        public static int updAARCUserTeam(string userID, string team)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", userID));
                lstParams.Add(new SqlParameter("@sTeam", team));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd_team", lstParams);
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

        [Route("updateUserSecLvl/{userId}/{secLvl}")]
        [HttpPut]
        public async Task<int> updAARCUserSecLvl(string userID, string secLvl)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", userID));
                lstParams.Add(new SqlParameter("@sSecLvl", secLvl));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd_seclvl", lstParams);
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

        [Route("updateUserContactInfo")]
        [HttpPut]
        public async Task<int> updAARCUserContactInfo(Contact contact)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sUserID", contact.UserId));
                lstParams.Add(new SqlParameter("@sPhone", contact.Phone));
                lstParams.Add(new SqlParameter("@sCell", contact.Cell));
                lstParams.Add(new SqlParameter("@sEmail", contact.Email));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_user_upd_contact_info", lstParams);
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


        [Route("ActiveDirUsers")]
        [HttpGet]
        public async Task<List<AALib.clsAAUserInfo.AAADUserInfo>> getActiveDirUsers()
        {
            List<AALib.clsAAUserInfo.AAADUserInfo> sUsers = null;
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            try
            {
                List<AALib.clsAAUserInfo.AAADUserInfo> AAUsers = oUI.GetActiveDirUsers();
                sUsers = AAUsers; //JsonConvert.SerializeObject(AAUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
            }
            return sUsers;
        }

        [Route("UserProdCodes")]
        [HttpPost]
        public async Task<List<AALib.clsAAUserInfo.GetUserProdCodesInfo>> UserProdCodes(ProdCodes objprod)
        {
            List < AALib.clsAAUserInfo.GetUserProdCodesInfo > objlst= new List<AALib.clsAAUserInfo.GetUserProdCodesInfo>();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            DataTable dtRet = new DataTable();
            try
            {
                objlst = oUI.GetProdCodesInfo(objprod.carrierId, objprod.userid, objprod.password, objprod.branch);
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

            return (objlst);
        }

        [Route("DeleteUsers/{userid}")]
        [HttpGet]
        public async Task<int> DeleteUsers(string userid)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@userid", userid));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_make_user_inactive", lstParams);
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

