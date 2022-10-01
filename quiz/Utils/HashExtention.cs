using System.Security.Cryptography;
using System.Text;

namespace quiz.Utils;
public static class HashExtention
{
    public static string Sha256(this string input)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(inputBytes);

        return Encoding.UTF8.GetString(hashBytes);
    }
}