using System.Text;

namespace Networkie.Application.Common.Extensions;

public static class EncodingExtension
{
    public static string EncodeBase64(this string value)
    {
        var valueBytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(valueBytes);
    }

    public static string DecodeBase64(this string value)
    {
        var valueBytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(valueBytes);
    }
}