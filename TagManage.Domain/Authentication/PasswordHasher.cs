using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TagManage.Domain.Authentication
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Haszowanie podanego hasła
                byte[] hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
                return hashedPassword;
            }
        }
    }
}
