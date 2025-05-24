namespace Networkie.Application.Abstractions.Providers;

public interface ICryptoProvider
{
    string Hash(string plainText, string salt);
    bool HashVerify(string plainText, string cipherText);
    string GenerateSalt();
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}