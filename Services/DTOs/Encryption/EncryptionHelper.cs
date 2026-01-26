using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Services.DTOs.Encryption
{
    public class EncryptionHelper
    {
        public EncryptionKeyUpdated GetKey()
        {
            var ek = new EncryptionKeyUpdated();
            try
            {
                ek.SecretKey = "128bl3$$1ng$";
                ek.Salt = "bl3$$1ng$128";
                ek.KeySize = 256;

                using var aes = CreateAlgorithm(ek.SecretKey, ek.Salt, ek.KeySize);
                // Copy the key and IV so we can dispose the Aes instance
                ek.Key = aes.Key;
                ek.IV = aes.IV;
                ek.WithEncryption = true;
            }
            catch
            {
                ek.WithEncryption = false;
            }
            return ek;
        }

        public string Encrypt(string plainText, EncryptionKeyUpdated ek)
        {
            if (!ek.WithEncryption || string.IsNullOrEmpty(plainText))
                return plainText ?? string.Empty;

            try
            {
                using var aes = Aes.Create();
                aes.Key = ek.Key;
                aes.IV = ek.IV;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] inputBuffer = Encoding.Unicode.GetBytes(plainText);
                    cs.Write(inputBuffer, 0, inputBuffer.Length);
                    cs.FlushFinalBlock();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return "ERROR";
            }
        }

        public string Decrypt(string cipherText, EncryptionKeyUpdated ek)
        {
            if (!ek.WithEncryption || string.IsNullOrWhiteSpace(cipherText))
                return cipherText ?? string.Empty;

            try
            {
                using var aes = Aes.Create();
                aes.Key = ek.Key;
                aes.IV = ek.IV;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                string cleanedInput = cipherText.Trim().Replace(" ", "+");
                byte[] encryptedBytes = Convert.FromBase64String(cleanedInput);

                using var ms = new MemoryStream(encryptedBytes);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);

                byte[] outputBuffer = new byte[ms.Length];
                int readBytes = cs.Read(outputBuffer, 0, outputBuffer.Length);

                return Encoding.Unicode.GetString(outputBuffer, 0, readBytes);
            }
            catch
            {
                return cipherText;
            }
        }

        private Aes CreateAlgorithm(string secretKey, string salt, int keySize)
        {
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            var keyBuilder = new Rfc2898DeriveBytes(secretKey, saltBytes, 1000, HashAlgorithmName.SHA1);

            var aes = Aes.Create();
            aes.KeySize = keySize;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.IV = keyBuilder.GetBytes(aes.BlockSize / 8);
            aes.Key = keyBuilder.GetBytes(aes.KeySize / 8);

            return aes;
        }
    }

   
}