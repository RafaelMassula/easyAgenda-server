using System.Security.Cryptography;

namespace EasyAgendaService
{
  public class PasswordService
  {
    private readonly string _password;
    private readonly byte[] _salt = new byte[16];
    private static readonly RNGCryptoServiceProvider _rng = new();
    public string HashPassword { get; set; } = string.Empty;
    public PasswordService(string password)
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
    public bool VerifyHashPassword(string hashedPassword)
    {
      byte[] buffer4;

      if (hashedPassword == null)
        return false;

      byte[] src = Convert.FromBase64String(hashedPassword);

      if(src.Length != 36)
        return false;

      byte[] dst = new byte[16];
      Array.Copy(src, 0, dst, 0, 16);
      byte[] buffer3 = new byte[20];
      Array.Copy(src, 16, buffer3, 0, 20);

      using Rfc2898DeriveBytes bytes = new(_password, dst, 1000);
      buffer4 = bytes.GetBytes(20);

      return buffer3.SequenceEqual(buffer4);
    }
  }
}
