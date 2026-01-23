namespace Services.Interfaces.Encryption;

public interface IEncryptionService
{
    ValueTask<string> GetCryptoJSDecryptionResultAsync(string cipherText, CancellationToken ct);
}