using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Networkie.Application.Abstractions.Providers;

namespace Networkie.Infrastructure.Providers;

public class CryptoProvider : ICryptoProvider
{
    private readonly byte[] _key;
    private readonly byte[] _iv;
    
    public CryptoProvider(IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("AES:Key")!);
        var iv = Encoding.UTF8.GetBytes(configuration.GetValue<string>("AES:IV")!);
        
        if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            throw new ArgumentException("Key must be 128, 192, or 256 bits");
        
        _key = key;
        _iv = iv;
    }
    
    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        return Convert.ToBase64String(cipherBytes);
    }

    public string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();
        var cipherBytes = Convert.FromBase64String(cipherText);
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
    
    public string Hash(string plainText, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainText, salt);
    }

    public bool HashVerify(string plainText, string cipherText)
    {
        return BCrypt.Net.BCrypt.Verify(plainText, cipherText);
    }

    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }
}