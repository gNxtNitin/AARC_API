using AALib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UWGLController : ControllerBase
    {
        [Route("UWGLSubjectsByFactors/{Factor}")]
        [HttpGet]
        public async Task<string> getUWGLSubjectsByFactors(string Factor)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFactor", Factor));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_uwgl_get_all_subj_by_factor", lstParams);
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

            return JsonConvert.SerializeObject(dtRet);
        }


        [Route("UWGLAllFactors")]
        [HttpGet]
        public async Task<string> getUWGLAllFactors()
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_uwgl_get_all_factors", lstParams);
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

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("UWGLData")]
        [HttpGet]
        public async Task<string> getUWGLData(UWGLData uWGLData)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sAgencyID", uWGLData.AgencyID));
                lstParams.Add(new SqlParameter("@sFactor", uWGLData.Factor));
                lstParams.Add(new SqlParameter("@sSubj", uWGLData.Subj));
                lstParams.Add(new SqlParameter("@sFactor2", uWGLData.Factor2));
                lstParams.Add(new SqlParameter("@sSubj2", uWGLData.Subj2));
                lstParams.Add(new SqlParameter("@sFactor3", uWGLData.Factor3));
                lstParams.Add(new SqlParameter("@sSubj3", uWGLData.Subj3));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_uwgl_get_guidelines", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, uWGLData.AgencyID + ";" + uWGLData.Factor + ";" + uWGLData.Subj + ";" + uWGLData.Factor2 + ";" + uWGLData.Subj2 + ";" +
                    uWGLData.Factor3);
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("UWGLGuideLineByCarrier/{Factor}/{Subj}/{CarrierID}")]
        [HttpGet]
        public async Task<string> getUWGLGuideLineByCarrier(string Factor, string Subj, string CarrierID)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            DataTable dtRet = null;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFactor", Factor));
                lstParams.Add(new SqlParameter("@sSubj", Subj));
                lstParams.Add(new SqlParameter("@sCarrierID", CarrierID));
                dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_uwgl_get_guideline_by_carrier", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Factor + ";" + Subj + ";" + CarrierID);
            }
            finally
            {
                oLib = null;
            }

            return JsonConvert.SerializeObject(dtRet);
        }

        [Route("updAARCUWGLFactor")]
        [HttpPut]
        public async Task<int> updAARCUWGLFactor(UWGLFactor uWGLFactor)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sCurFactor", uWGLFactor.CurFactor));
                lstParams.Add(new SqlParameter("@sNewFactor", uWGLFactor.NewFactor));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_uwgl_factor_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, uWGLFactor.CurFactor + ";" + uWGLFactor.NewFactor);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("addAARCUWGLSubject/{Factor}/{Subj}")]
        [HttpPost]
        public async Task<int> addAARCUWGLSubject(string Factor, string Subj)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFactor", Factor));
                lstParams.Add(new SqlParameter("@sSubj", Subj));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_uwgl_subject_add", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Factor + ";" + Subj);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

        [Route("updAARCUWGLSubject/{Factor}/{CurSubject}/{NewSubject}")]
        [HttpPut]
        public async Task<int> updAARCUWGLSubject(string Factor, string CurSubject, string NewSubject)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFactor", Factor));
                lstParams.Add(new SqlParameter("@sCurSubject", CurSubject));
                lstParams.Add(new SqlParameter("@sNewSubject", NewSubject));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_uwgl_subject_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Factor + ";" + CurSubject + ";" + NewSubject);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }
       
        [Route("updAARCUWGLSubject/{Factor}/{Subject}/{Carrier}/{Guideline}")]
        [HttpPost]
        public async Task<int> updAARCUWGLGuideline(string Factor, string Subject, string Carrier, string Guideline)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            int iRet = 0;
            try
            {
                List<SqlParameter> lstParams = new List<SqlParameter>();
                lstParams.Add(new SqlParameter("@sFactor", Factor));
                lstParams.Add(new SqlParameter("@sSubject", Subject));
                lstParams.Add(new SqlParameter("@sCarrierID", Carrier));
                lstParams.Add(new SqlParameter("@sGuideline", Guideline));
                iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_uwgl_guideline_upd", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                new LogException().logAARCErr(ex, Factor + ";" + Subject + ";" + Carrier + ";" + Guideline + ";" + Guideline);
            }
            finally
            {
                oLib = null;
            }

            return iRet;
        }

    }
}
