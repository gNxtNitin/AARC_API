namespace AgencyAdmins.Model
{
    public class CarrierContact
    {
        public int CarrierID { get; set; }
        public string? ContactID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string[] ArrayAgencyID { get; set; }
        public string? AgencyID { get; set; }
        public string Suffix { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string MName { get; set; }
        public string? SimilarIDs { get; set; }
    }
}
