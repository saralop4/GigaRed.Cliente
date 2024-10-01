using GigaRed.Cliente.Dominio.Entidades;
using GigaRed.Cliente.Dominio.Entidades.UsuarioDTOS;
using GigaRed.Cliente.Dominio.Interfaces;
using GigaRed.Cliente.Infraestructura.Providers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GigaRed.Cliente.Aplicacion.Servicios
{
    public class AutenticacionServicio : IAutenticacionServicio
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly ApiAutenticacionStateProvider _autenticacionStateProvider;

        public AutenticacionServicio(HttpClient httpClient, ITokenStorage tokenStorage, ApiAutenticacionStateProvider autenticacionStateProvider)
        {
            _httpClient = httpClient;
            _tokenStorage = tokenStorage;
            _autenticacionStateProvider = autenticacionStateProvider;
        }

        public async Task<ApiResponse<TokenDto>> IniciarSesion(IniciarSesionDto loginDto)
        {
            // Realizas la petición HTTP al endpoint de inicio de sesión
            var httpResponse = await _httpClient.PostAsJsonAsync("Api/V1/Usuario/IniciarSesion", loginDto);

            // Verificas si la respuesta fue exitosa
            if (httpResponse.IsSuccessStatusCode)
            {
                // Deserializas la respuesta como ApiResponse<TokenDto>
                var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<TokenDto>>();

                // Verificas si la operación dentro de la respuesta fue exitosa
                if (apiResponse != null && apiResponse.IsSuccess)
                {
                    // Guardas el token y marcas al usuario como autenticado
                    await _tokenStorage.GuardarToken(apiResponse.Data.Token);
                    _autenticacionStateProvider.MarkUserAsAuthenticated(apiResponse.Data.Token);

                    // Retornas la respuesta completa para que la vista maneje los datos
                    return apiResponse;
                }

                // Si hay errores, se retornan tal como están para ser manejados en la vista
                return apiResponse;
            }

            // Si no hay éxito en la respuesta HTTP, retornas una respuesta de error
            return new ApiResponse<TokenDto>
            {
                IsSuccess = false,
                Message = "Error al comunicarse con el servidor"
            };
        }

        public async Task CerrarSesion()
        {
            await _tokenStorage.LimpiarToken();
            _autenticacionStateProvider.MarkUserAsLoggedOut();
        }
    }

}