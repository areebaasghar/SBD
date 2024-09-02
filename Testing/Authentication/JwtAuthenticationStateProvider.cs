using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Testing.Authentication
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public JwtAuthenticationStateProvider(HttpClient httpClient, ProtectedSessionStorage sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var tokenResult = await _sessionStorage.GetAsync<string>("authToken");
                var token = tokenResult.Success ? tokenResult.Value : null;
                
                if (string.IsNullOrEmpty(token))
                    return new AuthenticationState(_anonymous);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var claims = ParseClaimsFromJwt(token);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuth"));

                return new AuthenticationState(claimsPrincipal);
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _sessionStorage.SetAsync("authToken", token);
            var claims = ParseClaimsFromJwt(token);
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuth"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _sessionStorage.DeleteAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            // Normalize the username claim
            var claims = keyValuePairs.Select(kvp =>
                new Claim(kvp.Key, kvp.Key.Equals(ClaimTypes.Name, StringComparison.Ordinal)
                    ? kvp.Value.ToString()
                    : kvp.Value.ToString())
            );

            return claims;
        }


        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}