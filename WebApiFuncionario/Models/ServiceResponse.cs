namespace WebApiFuncionario.Models
{
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public string Message { get; set; } = "";
        public bool Sucesso { get; set; } = true;
    }
}
