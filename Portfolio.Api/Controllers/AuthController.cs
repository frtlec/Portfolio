using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portfolio.Api.Controllers
{
    // Eski IdentityServer4 "/connect/token" endpoint'inin basit JWT karsiligi.
    // Angular tarafindaki auth.service.ts hicbir degisiklik yapmadan bu endpoint'i
    // ayni parametre/response sekliyle cagirabiliyor.
    [ApiController]
    [Route("connect/token")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityDataContext _dbContext;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, IdentityDataContext dbContext, TokenService tokenService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Token([FromForm] TokenRequestForm form)
        {
            return form.grant_type switch
            {
                "client_credentials" => HandleClientCredentials(form),
                "password" => await HandlePassword(form),
                "refresh_token" => await HandleRefreshToken(form),
                _ => BadRequest(new TokenErrorResponse { Error = "unsupported_grant_type" }),
            };
        }

        private IActionResult HandleClientCredentials(TokenRequestForm form)
        {
            if (!OAuthClients.IsValidPublicClient(form.client_id, form.client_secret))
            {
                return BadRequest(new TokenErrorResponse { Error = "invalid_client" });
            }

            var claims = OAuthClients.PublicScopes.Select(scope => new Claim("scope", scope));

            var (token, expiresAtUtc) = _tokenService.CreateAccessToken(claims, OAuthClients.PublicAccessTokenLifetime);

            return Ok(new TokenResponse
            {
                AccessToken = token,
                ExpiresIn = (int)OAuthClients.PublicAccessTokenLifetime.TotalSeconds,
                Scope = string.Join(" ", OAuthClients.PublicScopes),
            });
        }

        private async Task<IActionResult> HandlePassword(TokenRequestForm form)
        {
            if (!OAuthClients.IsValidAdminClient(form.client_id, form.client_secret))
            {
                return BadRequest(new TokenErrorResponse { Error = "invalid_client" });
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(form.username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, form.password))
            {
                return BadRequest(new TokenErrorResponse { Errors = new List<string> { "Email veya şifreniz yanlış" } });
            }

            return Ok(await IssueUserTokens(user));
        }

        private async Task<IActionResult> HandleRefreshToken(TokenRequestForm form)
        {
            if (!OAuthClients.IsValidAdminClient(form.client_id, form.client_secret))
            {
                return BadRequest(new TokenErrorResponse { Error = "invalid_client" });
            }

            RefreshToken existing = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == form.refresh_token);
            if (existing == null || !existing.IsActive)
            {
                return BadRequest(new TokenErrorResponse { Error = "invalid_grant" });
            }

            ApplicationUser user = await _userManager.FindByIdAsync(existing.UserId);
            if (user == null)
            {
                return BadRequest(new TokenErrorResponse { Error = "invalid_grant" });
            }

            // TokenUsage.ReUse: ayni refresh_token degeri korunur, rotate edilmez.
            var claims = await BuildUserClaims(user);
            var (token, _) = _tokenService.CreateAccessToken(claims, OAuthClients.AdminAccessTokenLifetime);

            return Ok(new TokenResponse
            {
                IdToken = "",
                AccessToken = token,
                ExpiresIn = (int)OAuthClients.AdminAccessTokenLifetime.TotalSeconds,
                Scope = string.Join(" ", OAuthClients.AdminScopes),
                RefreshToken = existing.Token,
            });
        }

        private async Task<TokenResponse> IssueUserTokens(ApplicationUser user)
        {
            var claims = await BuildUserClaims(user);
            var (token, expiresAtUtc) = _tokenService.CreateAccessToken(claims, OAuthClients.AdminAccessTokenLifetime);

            string refreshTokenValue = TokenService.GenerateOpaqueToken();
            _dbContext.RefreshTokens.Add(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshTokenValue,
                UserId = user.Id,
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.Add(OAuthClients.AdminRefreshTokenLifetime),
            });
            await _dbContext.SaveChangesAsync();

            return new TokenResponse
            {
                AccessToken = token,
                ExpiresIn = (int)OAuthClients.AdminAccessTokenLifetime.TotalSeconds,
                Scope = string.Join(" ", OAuthClients.AdminScopes),
                RefreshToken = refreshTokenValue,
            };
        }

        private async Task<List<Claim>> BuildUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            };

            claims.AddRange(OAuthClients.AdminScopes.Select(scope => new Claim("scope", scope)));

            foreach (string role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
