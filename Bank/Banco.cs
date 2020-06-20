using System;
using System.Threading;

namespace Bank
{
    public class Conta
    {        
        private string CPF { get; set; }
        private decimal Saldo { get; set; }
        private IValidadorCredito Validador { get; set; }

        public Conta(string CPF, decimal saldo, IValidadorCredito validador)
        {
            this.CPF = CPF;
            this.Saldo = saldo;
            this.Validador = validador;
        }

        public bool Sacar(decimal valor)
        {
            if (valor <= 0)
                return false;

            if (this.Saldo >= valor)
            {
                this.Saldo -= valor;
                return true;
            }

            return false;
        }

        public void Depositar(decimal valor)
        {
            Thread.Sleep(100);

            if (valor <= 0)
                throw new ArgumentOutOfRangeException("Não pode ser negativo");

            this.Saldo += valor;
        }

        public bool SolicitarEmprestimo(decimal valor)
        {
            if (this.Validador.ValidarCredito(this.CPF, valor))
            {
                this.Saldo += valor;
                return true;
            }
            
            return false;               
        }

        public decimal GetSaldo()
        {
            return Saldo;
        }
    }
}
