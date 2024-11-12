using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Model;
using AgencyAdmins.Helper;
using System.Web.Helpers;
using Org.BouncyCastle.Asn1.Ocsp;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TickerController : ControllerBase
    {
        //[HttpGet]
        //public async Task<string>  GetTickerItems()
        //{
        //    AALib.clsAA oLib = new AALib.clsAA();
        //    DataTable dtRet = null;
        //    try
        //    {
        //        List<SqlParameter> lstParams = new List<SqlParameter>();
        //        dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_AA_tickeritem_get", lstParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.InnerException);
        //    }
        //    finally
        //    {
        //        oLib = null;
        //    }

        //    if (dtRet.Rows.Count > 0)
        //    {
        //        return JsonConvert.SerializeObject(dtRet);
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        [HttpGet]
        public async Task<string> GetTickerItems()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_AA_tickeritem_get_agency", lstParams);
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

        //[HttpPost]
        //public async Task<int> addAARCTickerItem(TickerItem ticker)
        //{
        //    AALib.clsAA oLib = new AALib.clsAA();
        //    int iRet = 0;
        //    try
        //    {
        //        List<SqlParameter> lstParams = new List<SqlParameter>();
        //        lstParams.Add(new SqlParameter("@sTickerExpr", ticker.Expr));
        //        lstParams.Add(new SqlParameter("@sTickerItem", ticker.Item));
        //        iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_tickeritem_add", lstParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.InnerException);
        //        LogException logException = new LogException();
        //        logException.logAARCErr(ex, ticker.Item + ";" + ticker.Expr);
        //    }
        //    finally
        //    {
        //        oLib = null;
        //    }

        //    return iRet;
        //}


        [HttpPost]
        public async Task<int> addAARCTickerItem(TickerItem ticker)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTickerExpr", ticker.Expr));
                lstParams.Add(new SqlParameter("@sTickerItem", ticker.Item));
                lstParams.Add(new SqlParameter("@AgencyCodeList", String.Join(",", ticker.AgecnyCodeList)));

                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_tickeritem_agency_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, ticker.Item + ";" + ticker.Expr);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        //[HttpDelete]
        //public async Task<int> deleteTickerItem(TickerItem ticker)
        //{
        //    AALib.clsAA oLib = new AALib.clsAA();
        //    int iRet = 0;
        //    try
        //    {
        //        List<SqlParameter> lstParams = new List<SqlParameter>();
        //        lstParams.Add(new SqlParameter("@sTickerExpr", ticker.Expr));
        //        lstParams.Add(new SqlParameter("@sTickerItem", ticker.Item));
        //        iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_tickeritem_del", lstParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.InnerException);
        //    }
        //    finally
        //    {
        //        oLib = null;
        //    }

        //    return iRet;
        //}

        [HttpDelete]
        public async Task<int> deleteTickerItem(TickerItem ticker)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sTickerExpr", ticker.Expr));
                lstParams.Add(new SqlParameter("@sTickerItem", ticker.Item));
                lstParams.Add(new SqlParameter("@sAgencyCode", ticker.AgecnyCodeList.First()));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_AA_tickeritem_del_agency", lstParams);
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
