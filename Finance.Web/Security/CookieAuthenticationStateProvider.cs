﻿using Finance.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Finance.Web.Security
{
    public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
        private bool _isAuthenticated = false;
        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;
            var user = new ClaimsPrincipal( new ClaimsIdentity());
            
            var userInfo = await GetUser();
            if (userInfo is null)
                return new AuthenticationState(user);

            var claims = await GetClaims(userInfo);

            var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider)); 
            user = new ClaimsPrincipal(id);
            _isAuthenticated = true;
            return new AuthenticationState(user);

        }

        public void NotifyAuthenticationStateChanged()
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        private async Task<User?> GetUser()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User?>("v1/identity/manage/info");
            }
            catch
            {

                return null;
            }
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name,user.Email),
                new(ClaimTypes.Email,user.Email)
            };

            claims.AddRange(
                user.Claims.Where(x =>
                x.Key != ClaimTypes.Name &&
                x.Key != ClaimTypes.Email)
                .Select(x => new Claim(x.Key, x.Value))
            );

            RoleClaim[]? roles;
            try
            {
                roles = await _httpClient.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            }
            catch 
            {
                return claims;                
            }

            foreach (var role in roles ?? [])
            {
                if (!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
                    claims.Add(new Claim(role.Type, role.Value,role.ValueType,role.Issuer,role.OriginalIssuer));    
            }
            
            return claims;
        }
    }
}
