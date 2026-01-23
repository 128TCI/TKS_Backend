using Services.Interfaces.Encryption;
using System.Security.Cryptography;
using System.Text;

public class EncryptionService : IEncryptionService
{
    private readonly string _key = "1234567890123456";
    private readonly string _iv = "1234567890123456";

    public async ValueTask<string> GetCryptoJSDecryptionResultAsync(string cipherText, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(cipherText)) return string.Empty;

        try
        {
            // Convert.FromBase64String throws FormatException if string is plain text
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream ms = new MemoryStream(buffer);
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);

            return await sr.ReadToEndAsync();
        }
        catch (Exception ex) when (ex is FormatException || ex is CryptographicException)
        {
            // If it's not valid Base64 or decryption fails (wrong key/plain text),
            // we return the original string so your CreateAsync still works with plain text.
            return cipherText;
        }
    }
}