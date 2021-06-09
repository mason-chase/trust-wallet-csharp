using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrustWallet.Asset.Utilities
{
    public static class ExtensionsSha256
    {
        /// <summary>
        /// Calculate sha256 of a string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSHA256(this string value)
        {
            StringBuilder Sb = new();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        /// <summary>
        /// Calculate sha256 of a byte arrayy value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSHA256(this byte[] value)
        {

            StringBuilder Sb = new();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Byte[] result = hash.ComputeHash(value);

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
