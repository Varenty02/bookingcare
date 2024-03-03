namespace bookingcare.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string RoleId { get; set; }
        //public int PositionId { get; set; }
        //public int GenderId { get; set; }
    }
    public class AccountCreateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string RoleId { get; set; }
        //public int PositionId { get; set; }
        //public int GenderId { get; set; }
    }
    public class AccountUpdateModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string RoleId { get; set; }
        //public int PositionId { get; set; }
        //public int GenderId { get; set; }
    }
}
