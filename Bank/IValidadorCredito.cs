namespace Bank
{
    public interface IValidadorCredito
    {
        bool ValidarCredito(string cpf, decimal valor);
    }

    public class ValidadorCredito : IValidadorCredito
    {
        public bool ValidarCredito(string cpf, decimal valor)
        {
            //Do logic
            return true;
        }
    }
}