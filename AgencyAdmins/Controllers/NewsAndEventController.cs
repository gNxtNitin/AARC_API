using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAndEventController : ControllerBase
    {

        [HttpGet]
        public async Task<string> getNewsEvents()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_newsevent_get", lstParams);
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

        [HttpPost]
        public async Task<int>  addAARCNewsEvent(NewsEvent newsEvent)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iIsNews", newsEvent.IsNews));
                lstParams.Add(new SqlParameter("@sContent", newsEvent.Content));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_newsevent_add", lstParams);
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

        [Route("{NewsID}")]
        [HttpDelete]
        public async Task<int> deleteNewsEvents(string NewsID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@iNewsID", NewsID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_newsevent_del", lstParams);
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

        [HttpPut]
        public async Task<int> moveNewsEvents(int bMoveUp, int iNewsID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@bMoveUp", bMoveUp));
                lstParams.Add(new SqlParameter("@iNewsID", iNewsID));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_newsevent_move", lstParams);
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
