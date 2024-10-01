namespace GigaRed.Cliente.Dominio.Entidades
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<ErrorRegla>? Errors { get; set; }

    }
}
