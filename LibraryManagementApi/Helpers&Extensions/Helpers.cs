using LibraryManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementApi.Helpers_Extensions
{
    public static class Helpers
    {
        public static string GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var value = user.FindFirstValue(claimType);
            return value;
        }
    }
}
