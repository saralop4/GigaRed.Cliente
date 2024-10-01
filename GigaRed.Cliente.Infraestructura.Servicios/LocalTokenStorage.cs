using Blazored.LocalStorage;
using GigaRed.Cliente.Dominio.Interfaces;

namespace GigaRed.Cliente.Infraestructura.Servicios
{
    public class LocalTokenStorage : ITokenStorage
    {
        private readonly ILocalStorageService _localStorage;

        public LocalTokenStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task GuardarToken(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
        }

        public async Task<string> ObtenerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task LimpiarToken()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }
    }
}