namespace BookCatalog.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public static class UserStore
    {
        public static List<User> Users = new List<User>
    {
        new User {UserId = 1, Username = "a", Password = "1", Role="Admin" },
        new User {UserId = 2, Username = "b", Password = "2", Role="User"}
    };
    }
}
