namespace CarShop.Services
{
    using System.Text;
    using System.Security.Cryptography;

    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            //Create a SHA256
            using var sha256=SHA256.Create();

            // Compute Hash - return a byte array

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert bite[] to string:

            var build = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                build.Append(bytes[i].ToString("x2"));
            }

            return build.ToString();
        }
    }
}
