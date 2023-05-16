﻿using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementApi.Helpers_Extensions
{
    public static class Helpers
    {
        public static string ToMD5Hash(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return Encoding.ASCII.GetBytes(str).ToMD5Hash();
        }

        private static string ToMD5Hash(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (var md5 = MD5.Create())
            {
                return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
            }
        }
    }
}
