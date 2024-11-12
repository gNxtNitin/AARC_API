using AALib;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Data.SqlClient;
using System.Text;

namespace AgencyAdmins.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {

        public IConfiguration _configuration;

        public ForgetPasswordController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> SendPassword(MailRequest mailRequest)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            Encryption encryption = new Encryption();
            int iret = 0;
            try
            {
                var password = RandomPassword();
                EmailSMTP emailServer = new EmailSMTP();
                emailServer.SendEmail(mailRequest.Email, "Admin Agency Password", "Your new Passwor id: " + password);

                List<SqlParameter> lstParams = new List<SqlParameter>();
                var encryptedPassword = encryption.EncryptValue(password);

                lstParams.Add(new SqlParameter("@Email", mailRequest.Email));
                lstParams.Add(new SqlParameter("@Password", encryptedPassword));
                iret = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_update_Password", lstParams);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                LogException logException = new LogException();
                logException.logAARCErr(ex, mailRequest.Email);
                BadRequest("Error occured");
            }
            return Ok(iret);
        }

        [Route("SendSupportEmail")]
        [HttpPost]
        public async Task<IActionResult> SendSupportEmail(SupportEmail objemail)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            Encryption encryption = new Encryption();
            int iret = 0;
            try
            {
                var password = RandomPassword();
                EmailSMTP emailServer = new EmailSMTP();
                emailServer.SendSupportEmail(objemail.Emailid, objemail.Emailsubject, objemail.EmailBody, objemail.DocName, objemail.Cat, objemail.Attachment);

                //List<SqlParameter> lstParams = new List<SqlParameter>();
                //var encryptedPassword = encryption.EncryptValue(password);

                //lstParams.Add(new SqlParameter("@Email", mailRequest.Email));
                //lstParams.Add(new SqlParameter("@Password", encryptedPassword));
                //iret = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_update_Password", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                //LogException logException = new LogException();
                //logException.logAARCErr(ex, mailRequest.Email);
                BadRequest("Error occured");
            }
            return Ok(iret);
        }

        [Route("UpdatePasswordEmail")]
        [HttpPost]
        public async Task<IActionResult> UpdatePasswordEmail(updatePassword objupwd)
        {
            AALib.clsAA oLib = new AALib.clsAA();
            Encryption encryption = new Encryption();
            int iret = 0;
            try
            {
                var password = RandomPassword();
                EmailSMTP emailServer = new EmailSMTP();
                emailServer.SendEmail(objupwd.Emailid, objupwd.Emailsubject, objupwd.EmailBody);

                List<SqlParameter> lstParams = new List<SqlParameter>();
                //var encryptedPassword = encryption.EncryptValue(objupwd.NewPassword);

                lstParams.Add(new SqlParameter("@sAgencyID", objupwd.AgencyID));
                lstParams.Add(new SqlParameter("@iCarrierID", objupwd.CarrierID));
                lstParams.Add(new SqlParameter("@sUserID", objupwd.LoggedinUserid));
                lstParams.Add(new SqlParameter("@producerCode", objupwd.ProducerCode));
                lstParams.Add(new SqlParameter("@branchCode", objupwd.BranchCode));
                lstParams.Add(new SqlParameter("@Password", objupwd.NewPassword));
                iret = oLib.AAExecuteNonQuery(clsAA.validDBs.AARC, "aarc_update_Procedure_Code_Password", lstParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                //LogException logException = new LogException();
                //logException.logAARCErr(ex, mailRequest.Email);
                BadRequest("Error occured");
            }
            return Ok(iret);
        }

        private string RandomPassword()
        {
            int length = 7;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
    }
}
