using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Services
{
    public class Hashing
    {
        public string CreateHashing(string password)
        {
            string hashedPassword = null;
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] passwordIn = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(passwordIn);
                hashedPassword = BitConverter.ToString(hash).Replace("-", string.Empty);
            }
            return hashedPassword;
        }
    }
}
