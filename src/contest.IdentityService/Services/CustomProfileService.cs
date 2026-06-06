using System.Security.Claims;
using contest.IdentityService.Models;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace contest.IdentityService.Services;

public class CustomProfileService: IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        var existingClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim("username",user.UserName)
        };
        context.IssuedClaims.AddRange(claims);
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
    }

    // Not used, so we put plug
    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}