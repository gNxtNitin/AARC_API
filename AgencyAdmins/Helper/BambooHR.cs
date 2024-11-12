using AgencyAdmins.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AgencyAdmins.Helper
{
    public class BambooHR
    {
        private IConfiguration _configuration;
        public BambooHR(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<WhosOutResponseModel>> GetWhosOutList()
        {
            List<WhosOutResponseModel> whosOutList = null;
            try
            {
                string url = _configuration.GetValue<string>("BambooApiBaseUrl") + _configuration.GetValue<string>("Whos_out");
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(_configuration.GetValue<string>("BambooApiKey") + ":password")));
                HttpResponseMessage response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                List<WhosOutResponseModel> resp = JsonConvert.DeserializeObject<List<WhosOutResponseModel>>(responseString);
                if (resp != null)
                {
                    whosOutList = resp;
                }
            }
            catch (Exception ex)
            {

            }
            return whosOutList;
        }

        public async Task<string> GetEmpImg(int EmpId)
        {
            var responseString = string.Empty;
            try
            {
                string url = (_configuration.GetValue<string>("BambooApiBaseUrl") + _configuration.GetValue<string>("Get_photo")).Replace("{EmpId}", Convert.ToString(EmpId));
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(_configuration.GetValue<string>("BambooApiKey") + ":password")));
                HttpResponseMessage response = await client.GetAsync(url);
                responseString = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

            }
            return responseString;
        }

        public async Task<BambooEmpRoot> GetEmployeeFullInfoList()
        {
            BambooEmpRoot EmployeeInfoList = new BambooEmpRoot();
            try
            {
                string url = _configuration.GetValue<string>("BambooApiEmployeesDirectoryUrl");
                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(_configuration.GetValue<string>("BambooApiKey") + ":password")));
                HttpResponseMessage response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                BambooEmpRoot resp = JsonConvert.DeserializeObject<BambooEmpRoot>(responseString);
                if (resp.employees.Count>0)
                {
                    EmployeeInfoList = resp;
                }
            }
            catch (Exception ex)
            {

            }
            return EmployeeInfoList;
        }
    }
}
