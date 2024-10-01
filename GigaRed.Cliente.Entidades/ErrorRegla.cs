﻿namespace GigaRed.Cliente.Dominio.Entidades
{
    public class ErrorRegla
    {
        public string? PropertyName { get; set; }
        public string? ErrorMessage { get; set; }
        public string? AttemptedValue { get; set; }
        public object? CustomState { get; set; }
        public int Severity { get; set; }
        public string? ErrorCode { get; set; }
        public Dictionary<string, string>? FormattedMessagePlaceholderValues { get; set; }
    }
}
