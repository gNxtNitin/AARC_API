using AALib;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleImpersonation;
using System.Data;
using System.Data.SqlClient;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        const string gsDoxUn = "Amckee"; /*"AARCDOX";*/
        const string gsDoxPw = "Kp01q165Jw1xcY";

        [HttpGet]
        public async Task<string> getAARCDox()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_doccats_get", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
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
        public async Task<int> addAARCDox(Document document)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {

                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sDoc", document.DocName));
                lstParams.Add(new SqlParameter("@sCat", document.Cat));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_doccats_add", lstParams);


                //UserCredentials credentials = new UserCredentials("AAADVANIC", gsDoxUn, gsDoxPw);
                //Impersonation.RunAsUser(credentials, LogonType.Network, () =>
                //{
                    byte[] bytes = Convert.FromBase64String(document.Attachment);
                    object path = AppDomain.CurrentDomain.GetData("APPBASE");
                    string filePath = String.Format("{0}AARCDOX\\{1}", path, Path.GetFileName(document.AttachmentFilename.Replace("~", "\\")));
                    //store data into a file in Add_Data folder
                    System.IO.File.WriteAllBytes(filePath, bytes);
                //});
                iRet = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("{Filename}/{Cat}")]
        [HttpDelete]
        public async Task<int> delAARCDox(String Filename, String Cat)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {

                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sDoc", Filename));
                lstParams.Add(new SqlParameter("@sCat", Cat));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_doccats_del", lstParams);

                UserCredentials credentials = new UserCredentials("AAADVANIC", gsDoxUn, gsDoxPw);
                //Impersonation.RunAsUser(credentials, LogonType.Network, () =>
                //{
                    object path = AppDomain.CurrentDomain.GetData("APPBASE");
                    string filePath = String.Format("{0}AARCDOX\\{1}", path, Path.GetFileName(Filename.Replace("~", "\\")));
                    System.IO.File.Delete(filePath);
                //});

                iRet = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [Route("{Filename}/{Cat}")]
        [HttpPut]
        public async Task<int> updAARCDox(String Filename, String Cat)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {

                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFilename", Filename));
                lstParams.Add(new SqlParameter("@sCat", Cat));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_doccats_upd", lstParams);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }


        [Route("DocCategory")]
        [HttpGet]
        public async Task<string>  getDocCats()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_AA_doccats_get", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, "");
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
