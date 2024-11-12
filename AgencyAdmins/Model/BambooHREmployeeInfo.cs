namespace AgencyAdmins.Model
{
    public class BambooHREmployeeInfo
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string preferredName { get; set; }
        public string jobTitle { get; set; }
        public string workPhone { get; set; }
        public string mobilePhone { get; set; }
        public string workEmail { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string linkedIn { get; set; }
        public object pronouns { get; set; }
        public object workPhoneExtension { get; set; }
        public bool photoUploaded { get; set; }
        public string photoUrl { get; set; }
        public int canUploadPhoto { get; set; }
    }

    public class Field
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }

    public class BambooEmpRoot
    {
        public List<Field> fields { get; set; }
        public List<BambooHREmployeeInfo> employees { get; set; }
    }


}
