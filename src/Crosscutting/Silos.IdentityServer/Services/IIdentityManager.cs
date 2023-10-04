using Silos.IdentityServer.API.Controllers.Requests;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace Silos.IdentityServer.Services;

public interface IIdentityManager
{
    Task<TokenResponse> AuthUserByCredentials(LoginRequest request);
    Task<IdentityResult> RegisterNewUser(RegisterUserRequest request);
}
