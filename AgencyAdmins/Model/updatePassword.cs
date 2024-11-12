namespace AgencyAdmins.Model
{
    public class updatePassword
    {
        public string Emailid { get; set; }
        public string Emailsubject { get; set; }
        public string EmailBody { get; set; }
        public string AgencyID { get; set; }
        public string CarrierID { get; set; }
        public string LoggedinUserid { get; set; }
        public string NewPassword { get; set; }
        public string ProducerCode { get; set; }
        public string BranchCode { get; set; }
    }
}
