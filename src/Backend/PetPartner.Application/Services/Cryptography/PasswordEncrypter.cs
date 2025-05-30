﻿using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PetPartner.Application.Services.Cryptography;

public class PasswordEncrypter
{
    public string Encrypt(string password)
    {
        var additionalKey = "nottoday";
        var newPassword = $"{password}{additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
