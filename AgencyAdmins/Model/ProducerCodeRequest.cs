using System.ComponentModel.DataAnnotations;

namespace AgencyAdmins.Model
{
    public class ProducerCodeRequest
    {
        public string AgencyID { get; set; }
        public string CarrierID { get; set; }
        public string ProducerCode { get; set; }
        public string PCBranch { get; set; }
        public string UID { get; set; }
        public string PWD { get; set; }
        public string GrantedUsers { get; set; }
        public string? NewProducerCode { get; set; }
        public string? NewPCBranch { get; set; }

    }
}
