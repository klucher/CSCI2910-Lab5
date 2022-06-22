namespace CSCI2910_Lab5.models
{
    internal class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string OtherUserDetails { get; set; }
        public string AmountOfFine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Users() { }

        public Users(int id, string userName, string userAddress, string otherUserDetails, string amountOfFine, string email, string phoneNumber)
        {
            Id = id;
            UserName = userName;
            UserAddress = userAddress;
            OtherUserDetails = otherUserDetails;
            AmountOfFine = amountOfFine;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            string usersString = string.Empty;
            usersString += Id + " ";
            usersString += UserName + " ";
            return usersString;
        }
    }
}
