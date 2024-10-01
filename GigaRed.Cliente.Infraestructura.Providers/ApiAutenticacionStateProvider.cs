using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GigaRed.Cliente.Infraestructura.Providers
{
    public class ApiAutenticacionStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private const string AuthTokenKey = "authToken";

        public ApiAutenticacionStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(AuthTokenKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                // Si no hay token, el usuario no está autenticado
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Si hay un token, extraemos la información del usuario
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}