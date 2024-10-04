using System.Security.Cryptography;
using System.Text;

namespace Application.Common;
public class PasswordEncryptor
{
    public PasswordEncryptor()
    {}

    public string EncryptPassword(string password)
    {
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {      
        string attemptedHashedPassword = EncryptPassword(password);
        return attemptedHashedPassword == hashedPassword;
    }
}
