using System.Threading.Tasks;

namespace GigaRed.Cliente.Dominio.Interfaces
{
    
    public interface ITokenStorage
    {
        Task GuardarToken(string token);
        Task<string?> ObtenerToken();
        Task LimpiarToken();
    }
}
