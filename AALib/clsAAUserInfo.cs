using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using static AALib.clsAA;

namespace AALib
{
    public class clsAAUserInfo
    {
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Initiate Class
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public clsAAUserInfo()
        {

        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Constants and Private Variables
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Constants and Private Variables"
        private string psAAID = "";
        private string psUserID = "";
        private string psLast_name = "";
        private string psFirst_name = "";
        private string psMiddle_name = "";
        private string psSuffix = "";
        private string psTeam = "";
        private string psSecLevel = "";
        private bool pbActive = false;
        private bool pbAvailable = false;

        public enum validStatus { Inactive = 0, Active = 1, All = 2 }
        #endregion


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Class Properties
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Class Properties"

        // Class definition.
        /// -----------------------------------------------------------------------------
        /// <summary> AA Resource Center Users.</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public class AAUI
        {
            public string UserID = "";
            public string AAID = "";
            public string Last_name = "";
            public string First_name = "";
            public string Middle_name = "";
            public string Suffix = "";
            public string AATeam = "";
            public string AASecLevel = "";
            public string Phone = "";
            public string Cell = "";
            public string Email = "";
            public string Password = "";
            public bool IsActive = false;
            public bool IsAvailable = false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary> AA active Directory Users.</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public class AAADUsers
        {
            public List<AAADUserInfo> AcctName { get; set; }
        }

        public class AAADUserInfo
        {
            public string AcctName { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public string Email { get; set; }
        }

        /// -----------------------------------------------------------------------------
        /// <summary> AAID from infoUser. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>AARC Assigned ID</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string AAID
        {
            get
            {
                return psAAID;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>UserID from infoUser. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Property is equivalent to the Windows Authenticated User ID</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string UserID
        {
            get
            {
                return psUserID;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>last_name from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Last name of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string LastName
        {
            get
            {
                return psLast_name;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>first_name from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>First name of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string FirstName
        {
            get
            {
                return psFirst_name;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>middle_name from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>middle name of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string MiddleName
        {
            get
            {
                return psMiddle_name;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>suffix from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>suffix of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string Suffix
        {
            get
            {
                return psSuffix;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAteam from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Team code of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string AAteam
        {
            get
            {
                return psTeam;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAsecurity from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Security code of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public string AAsecurity
        {
            get
            {
                return psSecLevel;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAActive from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Active flag of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public bool IsActive
        {
            get
            {
                return pbActive;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>AAAvailable from User_Info. Available after GetUserInfo called.</summary>
        /// <returns>String</returns>
        /// <remarks>Available flag of the user information was retrieved for.</remarks>
        /// <history>[RINO F] 11/26/2017 Created</history>
        /// -----------------------------------------------------------------------------
        public bool IsAvailable
        {
            get
            {
                return pbAvailable;
            }
        }

        public class DailyNotifications
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string EmployeeId { get; set; }
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Image { get; set; }
            public Boolean ShowDate { get; set; }
            public string OutUntilMessage { get; set; }
        }

        #endregion

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //              Public Methods
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #region "Public Methods"


        public string getAAUser()
        {
            string sUser = "";
            try
            {
                WindowsIdentity wiUser = WindowsIdentity.GetCurrent();
                string sWkar = wiUser.Name;
                sUser = sWkar.Substring(sWkar.IndexOf("\\") + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            return sUser;
        }

        public DataTable GetAllUserIDs(validStatus asStatus)
        {
            DataTable dtUI = null;
            clsAA AADal = new clsAA();
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iActive", (int)asStatus));

                dtUI = AADal.AAExecuteTable(clsAA.validDBs.AARC, "aarc_user_getall_by_active_status", lstParams);


                //UInfo.AAID = dtUI.Rows[0]["AAID"].ToString().Trim();
                //UInfo.Last_name = dtUI.Rows[0]["last_name"].ToString().Trim();
                //UInfo.First_name = dtUI.Rows[0]["first_name"].ToString().Trim();
                //UInfo.Middle_name = dtUI.Rows[0]["middle_name"].ToString().Trim();
                //UInfo.Suffix = dtUI.Rows[0]["suffix"].ToString().Trim();
                //UInfo.AATeam = dtUI.Rows[0]["team_id"].ToString().Trim();
                //UInfo.AASecLevel = dtUI.Rows[0]["sec_level"].ToString().Trim();
                //UInfo.IsActive = (bool)dtUI.Rows[0]["active_flag"];
                //UInfo.IsAvailable = (bool)dtUI.Rows[0]["available_flag"];

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                AADal = null;
            }

            return dtUI;
        }

        public AAUI GetUserInfo(string sUserId)
        {
            AAUI UInfo = new AAUI();
            clsAA AADal = new clsAA();
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                // sUserId = "asmith";
                lstParams.Add(new SqlParameter("@userid", sUserId));

                DataTable dtUI = AADal.AAExecuteTable(clsAA.validDBs.AARC, "aarc_user_getinfo_by_userid", lstParams);

                UInfo.AAID = dtUI.Rows[0]["AAID"].ToString().Trim();
                UInfo.UserID = dtUI.Rows[0]["userid"].ToString().Trim();
                UInfo.Last_name = dtUI.Rows[0]["last_name"].ToString().Trim();
                UInfo.First_name = dtUI.Rows[0]["first_name"].ToString().Trim();
                UInfo.Middle_name = dtUI.Rows[0]["middle_name"].ToString().Trim();
                UInfo.Suffix = dtUI.Rows[0]["suffix"].ToString().Trim();
                UInfo.AATeam = dtUI.Rows[0]["team_id"].ToString().Trim();
                UInfo.AASecLevel = dtUI.Rows[0]["sec_level"].ToString().Trim();
                UInfo.Phone = dtUI.Rows[0]["phone"].ToString().Trim();
                UInfo.Cell = dtUI.Rows[0]["cell"].ToString().Trim();
                UInfo.Email = dtUI.Rows[0]["email"].ToString().Trim();
                UInfo.IsActive = (bool)dtUI.Rows[0]["active_flag"];
                UInfo.IsAvailable = (bool)dtUI.Rows[0]["available_flag"];
                UInfo.Password = dtUI.Rows[0]["password"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                AADal = null;
            }

            return UInfo;
        }


        public AAUI GetUserInfoByEmail(string email)
        {
            AAUI UInfo = new AAUI();
            clsAA AADal = new clsAA();
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                // sUserId = "asmith";
                lstParams.Add(new SqlParameter("@email", email));

                DataTable dtUI = AADal.AAExecuteTable(clsAA.validDBs.AARC, "aarc_user_getinfo_by_email", lstParams);

                UInfo.AAID = dtUI.Rows[0]["AAID"].ToString().Trim();
                UInfo.UserID = dtUI.Rows[0]["userid"].ToString().Trim();
                UInfo.Last_name = dtUI.Rows[0]["last_name"].ToString().Trim();
                UInfo.First_name = dtUI.Rows[0]["first_name"].ToString().Trim();
                UInfo.Middle_name = dtUI.Rows[0]["middle_name"].ToString().Trim();
                UInfo.Suffix = dtUI.Rows[0]["suffix"].ToString().Trim();
                UInfo.AATeam = dtUI.Rows[0]["team_id"].ToString().Trim();
                UInfo.AASecLevel = dtUI.Rows[0]["sec_level"].ToString().Trim();
                UInfo.Phone = dtUI.Rows[0]["phone"].ToString().Trim();
                UInfo.Cell = dtUI.Rows[0]["cell"].ToString().Trim();
                UInfo.Email = dtUI.Rows[0]["email"].ToString().Trim();
                UInfo.IsActive = (bool)dtUI.Rows[0]["active_flag"];
                UInfo.IsAvailable = (bool)dtUI.Rows[0]["available_flag"];
                UInfo.Password = dtUI.Rows[0]["password"].ToString().Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                AADal = null;
            }

            return UInfo;
        }

        public List<AAADUserInfo> GetActiveDirUsers()
        {
            List<AAADUserInfo> AAUsers = new List<AAADUserInfo>();
            try
            {

                //Get Active Directory Users
                using (var context = new PrincipalContext(ContextType.Domain, System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {

                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                            if (!(de.Properties["givenName"].Value == null))
                            {
                                AAADUserInfo AAUser = new AAADUserInfo
                                {
                                    AcctName = (de.Properties["samAccountName"].Value == null) ? "" : de.Properties["samAccountName"].Value.ToString(),
                                    Fname = (de.Properties["givenName"].Value == null) ? "" : de.Properties["givenName"].Value.ToString(),
                                    Lname = (de.Properties["sn"].Value == null) ? "" : de.Properties["sn"].Value.ToString(),
                                    Email = (de.Properties["userPrincipalName"].Value == null) ? "" : de.Properties["userPrincipalName"].Value.ToString()
                                };

                                AAUsers.Add(AAUser);
                            }
                            //ADUI.Fname = de.Properties["givenName"].Value.ToString();
                            //ADUI.Lname = de.Properties["sn"].Value.ToString();
                            //ADUI.AcctName = de.Properties["samAccountName"].Value.ToString();
                            //ADUI.Email = de.Properties["userPrincipalName"].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int i = 1;
            }
            finally
            {
            }

            return AAUsers;
        }

        public DataTable DailyNotificationsList()
        {
            DataTable dtUI = null;
            clsAA AADal = new clsAA();
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtUI = AADal.AAExecuteTable(clsAA.validDBs.AARC, "aarc_get_Daily_Notifications", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                AADal = null;
            }

            return dtUI;
        }

        #endregion
    }
}
