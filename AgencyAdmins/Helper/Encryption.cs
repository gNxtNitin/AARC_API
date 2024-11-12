using System.Text;

namespace AgencyAdmins.Helper
{
    public class Encryption
    {
        public string EncryptValue(string value)
        {
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(encode);
        }

        public string DecryptValue(string value)
        {
            var base64EncodedBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
