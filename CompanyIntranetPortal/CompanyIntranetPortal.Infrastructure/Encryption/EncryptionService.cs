using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using EnrollmentPortal.Infrastructure.Helpers;
using EnrollmentPortal.Infrastructure.Options;
using EnrollmentPortal.Infrastructure.Encryption;

namespace CompanyIntranet;

public class EncryptionService : IEncryptionService
{
    private const int _passwordSaltKeySize = 5;
    private const string _defaultHashAlgorithm = "SHA512";
    private readonly SecuritySettings _securitySettings;

    public EncryptionService(IOptions<SecuritySettings> options)
    {
        _securitySettings = options.Value;
    }

    #region Utilities
    private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
        {
            var toEncrypt = Encoding.Unicode.GetBytes(data);
            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();
        }

        return ms.ToArray();
    }

    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.Unicode);
        return sr.ReadToEnd();
    }
    #endregion

    public string EncryptText(string plainText, string encryptionPrivateKey = "")
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        if (string.IsNullOrEmpty(encryptionPrivateKey))
            encryptionPrivateKey = _securitySettings.EncryptionKey;

        using var provider = TripleDES.Create();
        provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey[0..16]);
        provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey[8..16]);

        var encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
        return Convert.ToBase64String(encryptedBinary);
    }

    public string DecryptText(string cipherText, string encryptionPrivateKey = "")
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        if (string.IsNullOrEmpty(encryptionPrivateKey))
            encryptionPrivateKey = _securitySettings.EncryptionKey;

        using var provider = TripleDES.Create();
        provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey[0..16]);
        provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey[8..16]);

        var buffer = Convert.FromBase64String(cipherText);
        return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
    }

    public string CreateSaltKey(int? size = null)
    {
        if (!size.HasValue) size = _passwordSaltKeySize;

        using var provider = RandomNumberGenerator.Create();
        var buff = new byte[size.Value];
        provider.GetBytes(buff);

        return Convert.ToBase64String(buff);
    }

    public string CreatePasswordHash(string password, string? hashAlgorithm = null)
    {
        return HashHelper.CreateHash(Encoding.UTF8.GetBytes(password), hashAlgorithm ?? _defaultHashAlgorithm);
    }
}
