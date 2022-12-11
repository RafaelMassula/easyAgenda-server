using System.Security.Cryptography;

namespace EasyAgendaService
{
    public class PasswordEncryptionService
    {
        private readonly string _password;
        private readonly byte[] _salt = new byte[16];
        private static readonly RNGCryptoServiceProvider _rng = new();
        public string HashPassword { get; set; } = string.Empty;
        public PasswordEncryptionService(string password)
        {
            _password = password;
            _rng.GetBytes(_salt);
        }

        public string GetHashPassword()
        {
            SetHashPassword();
            return HashPassword;
        }
        private void SetHashPassword()
        {
            var pbkdf2 = new Rfc2898DeriveBytes(_password, _salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(_salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            this.HashPassword = Convert.ToBase64String(hashBytes);
        }
    }
}
