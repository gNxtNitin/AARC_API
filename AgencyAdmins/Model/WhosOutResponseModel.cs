namespace AgencyAdmins.Model
{
    public class WhosOutResponseModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Image { get; set; }
        public Boolean ShowDate { get; set; }
        public string OutUntilMessage { get; set; }
    }
}
