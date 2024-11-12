using AALib;
using System.Data.SqlClient;

namespace AgencyAdmins.Helper
{
    public class LogException
    {
        public void logAARCErr(Exception exError, string sDetail)
        {
            AALib.clsAA oLib = new AALib.clsAA();

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@dErrDT", DateTime.Now));
            lstParams.Add(new SqlParameter("@sUserID", Environment.UserName.ToUpper().Trim()));
            lstParams.Add(new SqlParameter("@sHResult", exError.HResult.ToString()));
            lstParams.Add(new SqlParameter("@sInnerexp", (exError.InnerException is null ? "" : exError.InnerException.ToString())));
            lstParams.Add(new SqlParameter("@sErrmsg", exError.Message));
            lstParams.Add(new SqlParameter("@sErrsrc", exError.Source));
            lstParams.Add(new SqlParameter("@sStacktrace", exError.StackTrace));
            lstParams.Add(new SqlParameter("@sTargetname", exError.TargetSite.Name));
            lstParams.Add(new SqlParameter("@sDetail", sDetail));

            int iRet = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_error_log", lstParams);

        }
    }
}
