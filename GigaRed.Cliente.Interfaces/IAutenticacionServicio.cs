using GigaRed.Cliente.Dominio.Entidades;
using GigaRed.Cliente.Dominio.Entidades.UsuarioDTOS;
using System.Threading.Tasks;

namespace GigaRed.Cliente.Dominio.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<ApiResponse<TokenDto>> IniciarSesion(IniciarSesionDto loginDto);
        Task CerrarSesion();
    }
}
