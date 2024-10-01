namespace GigaRed.Cliente.Dominio.Entidades.UsuarioDTOS
{
    public class IniciarSesionDto
    {
        public string Correo { get; set; } = null!;

        public string Contraseña { get; set; } = null!;
    }
}
