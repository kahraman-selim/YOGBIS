namespace YOGBIS.Common.SessionOperations
{
    public class SessionContext
    {
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? Aktif { get; set; }
        public string Rol { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
