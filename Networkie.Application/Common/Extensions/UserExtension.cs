using Networkie.Entities;

namespace Networkie.Application.Common.Extensions;

public static class UserExtension
{
    public static string GetFullName(this User? user)
    {
        var fullName = string.Empty;
        
        if (user == null) return fullName;

        fullName = "";
        if (!string.IsNullOrWhiteSpace(user.FirstName))
            fullName += user.FirstName;
        if (!string.IsNullOrWhiteSpace(user.MiddleName))
            fullName += $" {user.MiddleName}";
        if (!string.IsNullOrWhiteSpace(user.LastName))
            fullName += $" {user.LastName}";
        
        return fullName;
    }
}