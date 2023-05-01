namespace UserDemo.Service.Commons.Security
{
    public class PasswordHasher
    {
        public static class StringExtention
        { 
            public static string Hash(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool Verify(string password, string passwordHash)
            {
                return BCrypt.Net.BCrypt.Verify(password, passwordHash);

            }
        }
    }
}
