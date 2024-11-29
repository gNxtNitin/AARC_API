using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Utilities;
using Swashbuckle.Swagger;
using System;
using System.Data;
using System.Text;
using System.Text.Json.Nodes;
using System.Web.Helpers;
using System.Web.WebPages;
using static AALib.clsAAUserInfo;
using static System.Net.Mime.MediaTypeNames;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BambooHRController : ControllerBase
    {
        public BambooHR? _service;
        public BambooHRController(IConfiguration _configuration)
        {
            _service = new BambooHR(_configuration);
        }

        [HttpGet("GetWhosOutList")]
        public async Task<List<WhosOutResponseModel>> GetWhosOutList()
        {

            BambooEmpRoot EmpInfo = new BambooEmpRoot();
            BambooHrResponse resp = new BambooHrResponse();
            List<WhosOutResponseModel> whosOutList = null;
            EmpInfo = await _service.GetEmployeeFullInfoList();
            //var result = await _service.GetWhosOutList();
            //var result = (await _service.GetWhosOutList()).Where(p => p.Start == DateTime.Now.ToString("yyyy-MM-dd")).ToList();
            var result = (await _service.GetWhosOutList()).Where(p => Convert.ToDateTime(p.Start) <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) && Convert.ToDateTime(p.End) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))).ToList();
            //var result = (await _service.GetWhosOutList()).Where(p => p.Start == "2024-11-2").ToList();
            if (result.Count > 0)
            {
                resp.code = 200;
                resp.msg = "Success";
                string strserialize = JsonConvert.SerializeObject(result);
                resp.data = strserialize;

                foreach (WhosOutResponseModel employee in result)
                {
                    foreach (var item in EmpInfo.employees)
                    {
                        if (Convert.ToString(employee.EmployeeId) == item.id)
                        {
                            employee.Image = item.photoUrl;
                        }
                    }
                    int diff = (Convert.ToDateTime(employee.End) - DateTime.Now).Days;
                    if (diff > 0)
                    {
                        employee.ShowDate = true;
                        DateTime givenDate = Convert.ToDateTime(employee.End);
                        string dayName = givenDate.ToString("dddd");
                        string monthName = givenDate.ToString("MMM");
                        int date = givenDate.Day;
                        employee.OutUntilMessage = dayName + "," + monthName + " " + date;
                    }
                }
            }
            return result;
        }

        [HttpGet("GetEmpImg")]
        public async Task<IActionResult> GetEmpImg(int EmpId)
        {
            BambooHrResponse resp = new BambooHrResponse();
            var result = await _service.GetEmpImg(EmpId);
            if (result != null)
            {
                resp.code = 200;
                resp.msg = "Success";
                string strserialize = JsonConvert.SerializeObject(result);
                resp.data = strserialize;
            }
            return Ok(resp);
        }

        [HttpGet("GetEmployeeInfoList")]
        public async Task<BambooEmpRoot> GetEmployeeFullInfoList()
        {
            BambooHrResponse resp = new BambooHrResponse();
            var result = await _service.GetEmployeeFullInfoList();
            if (result.employees.Count > 0)
            {
                resp.code = 200;
                resp.msg = "Success";
                string strserialize = JsonConvert.SerializeObject(result);
                resp.data = strserialize;
            }
            return result;
        }

        [HttpGet("GetDailyNotificationsList")]
        public async Task<List<DailyNotifications>> GetDailyNotificationsList()
        {
            //AALib.clsAAUserInfo.DailyNotifications objNot = new AALib.clsAAUserInfo.DailyNotifications();
            List<DailyNotifications> whosOutList = new List<DailyNotifications>();
            AALib.clsAAUserInfo oUI = new AALib.clsAAUserInfo();
            try
            {
                DataTable dtUI = oUI.DailyNotificationsList();
                foreach (DataRow dtRow in dtUI.Rows)
                {
                    DailyNotifications obj1 = new DailyNotifications();
                    obj1.Id = Convert.ToInt32(dtRow["ID"]);
                    obj1.Name = Convert.ToString(dtRow["Name"]);
                    obj1.EmployeeId = Convert.ToString(dtRow["Userid"]);
                    obj1.StartDate = Convert.ToString(dtRow["StartDate"]);
                    obj1.EndDate = Convert.ToString(dtRow["EndDate"]);
                    //int diff = (Convert.ToDateTime(obj1.EndDate) - Convert.ToDateTime(DateTime.Now)).Days;
                    //if (diff > 0)
                    //{
                    //    obj1.ShowDate = true;
                    //    DateTime givenDate = Convert.ToDateTime(obj1.EndDate);
                    //    string dayName = givenDate.ToString("dddd");
                    //    string monthName = givenDate.ToString("MMM");
                    //    int date = givenDate.Day;
                    //    obj1.OutUntilMessage = dayName + "," + monthName + " " + date;
                    //}
                    whosOutList.Add(obj1);
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
            return whosOutList;
        }
    }
}
