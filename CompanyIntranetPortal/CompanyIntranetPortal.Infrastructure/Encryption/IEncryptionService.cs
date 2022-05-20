namespace EnrollmentPortal.Infrastructure.Encryption;

public interface IEncryptionService
{
    string EncryptText(string plainText, string encryptionPrivateKey = "");
    string DecryptText(string cipherText, string encryptionPrivateKey = "");
    string CreateSaltKey(int? size = null);
    string CreatePasswordHash(string password, string? hashAlgorithm = null);
}
