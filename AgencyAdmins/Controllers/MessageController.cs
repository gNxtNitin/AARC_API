using AALib;
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
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public async Task<string> addAARCMsg(Message message)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            string sID = "";
            string sRet = "";
            int iIdx = 0;
            try
            {
                DateTime sNow = DateTime.Now;
                sID = message.Sndr + sNow.Year.ToString() + sNow.Month.ToString() +
                sNow.Day.ToString() + sNow.Hour.ToString() + sNow.Minute.ToString() +
                sNow.Second.ToString();

                string[] asRecips = message.Recips.Split(new[] { ";" }, StringSplitOptions.None);

                for (iIdx = 0; iIdx < asRecips.Length - 1; iIdx++)
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sID", sID));
                    lstParams.Add(new SqlParameter("@sSndr", message.Sndr));
                    lstParams.Add(new SqlParameter("@sRecip", asRecips[iIdx]));
                    lstParams.Add(new SqlParameter("@sMsg", message.Msg));
                    int iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_msg_add", lstParams);
                    lstParams = null;
                }

            }
            catch (Exception ex)
            {
                sID = "ERROR";
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                oLib = null;
            }

            return sID;
        }

        [Route("{ID}/{FromDT}/{ToDT}/{WildCard}/")]
        [HttpGet]
        public async Task<string>  getAARCUsersMsgs(string ID, string FromDT, string ToDT, string WildCard)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sID", ID));
                lstParams.Add(new SqlParameter("@sFromDT", FromDT));
                lstParams.Add(new SqlParameter("@sToDT", ToDT));
                lstParams.Add(new SqlParameter("@sWildCard", WildCard));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_msg_get_by_uid", lstParams);
                lstParams = null;

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
