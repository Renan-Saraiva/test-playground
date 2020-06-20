using NUnit.Framework;
using Bank;
using System;
using Moq;

namespace BankTest
{
    [TestFixture]
    public class ContaTest
    {
        private Conta conta;

        //[OneTimeSetUp]
        //public void OneTimeSetUpSetUp()
        //{
        //    //Executa o m�todo antes de todos os teste
        //    conta = new Conta("123", 300, new ValidadorCreditoFake());
        //}

        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    //Executa o m�todo depois de todos os teste
        //    conta = null;
        //}

        [SetUp]
        public void SetUp()
        {
            //Executa o m�todo antes de cada teste

            var cpf = "55500";
            var saldo = 200;

            var mock = new Mock<IValidadorCredito>();

            //O It.IsAny retorna o valor passado no Returns para qualquer valor passado (MatchArguments)

            //mock.Setup(x => x.ValidarCredito(It.IsAny<string>(), It.Is<decimal>(i => i <= 0))).Throws<InvalidOperationException>();
            
            mock.Setup(x => x.ValidarCredito(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);

            //Maneira para avaliar o par�metro e considerar o retorno do 'Returns' s� se a condi��o for verdadeira
            //It.Is<decimal>(i => i <= 5000);

            //mock.Setup(x => x.ValidarCredito(cpf)).Returns(true);
            //Vantagem => N�o precisamos implementar toda a classe, e podemos dar um comportamento especifico de acordo com o cen�rio de teste
            //Quando n�o fazemos o setup, o Mock coloca o valor default do retorno, VALOR DEFAULT OBJECT � NULL

            //Podemos garantir se o m�todo do mock foi realmente chamado usando o m�todo abaixo
            //� poss�vel usar o MatchArguments e tbm verificar a quantidade de vezes que foi executada com o m�todo Times
            //mock.Verify(x => x.ValidarCredito(It.IsAny<string>(), It.IsAny<decimal>()), Times.Once());

            conta = new Conta(cpf, saldo, mock.Object);
        }

        [TearDown] //Executa o m�todo depois de cada teste
        public void TearDown()
        {
            conta = null;
        }

        [Test]
        [Category("Principal")]
        [TestCase(100, true, Description = "Teste com valor positivo")] //Podemos definir atravez do TestCase a varia��o dos par�metros desejados, n�o necessitando assim duplicar cen�rio de testes
        [TestCase(-100, false, Description = "Teste com valor negativo")] //Para cada TestCase n�o chama novamente o SetUp e TearDown
        public void testSacar(decimal valor, bool esperado)
        {
            Assert.AreEqual(conta.Sacar(valor), esperado);
        }

        [Test]
        [Category("Principal")]
        public void testSacarSemSaldo()
        {
            Assert.IsFalse(conta.Sacar(250));
        }

        [Test]
        [Category("Testando entradas")]
        public void testSacarValorZero()
        {
            Assert.IsFalse(conta.Sacar(0));
        }

        [Test]        
        public void testDepositoNegativo()
        {
            //Valida sem heran�a
            Assert.Throws<ArgumentOutOfRangeException>(delegate { conta.Depositar(-10); });

            //Valida com heran�a
            Assert.Catch<Exception>(delegate { conta.Depositar(-10); });
        }

        [Test]
        [Timeout(1000)] //Falha se passar do tempo
        public void testTimeOut()
        {
            //Valida sem heran�a
            Assert.Throws<ArgumentOutOfRangeException>(delegate { conta.Depositar(-10); });

            //Valida com heran�a
            Assert.Catch<Exception>(delegate { conta.Depositar(-10); });
        }

        [Test]
        public void testSolicitarEmprestimo()
        {
            this.conta.SolicitarEmprestimo(300);

            Assert.AreEqual(500, this.conta.GetSaldo());
        }

        [Test]
        [Ignore("")]
        public void testSolicitarEmprestimoComException()
        {
            Assert.Throws<InvalidOperationException>(delegate { this.conta.SolicitarEmprestimo(-999); });
        }
    }
}




/*

TDD 

- Inicia o desenvolvimento escrevendo todos os testes falhando
- Desenvolve a necessidade para passar todos os testes
- Refatora o c�digo com a seguran�a de n�o quebrar o c�digo
 

- Foco nos requisitos
- C�digo mais simples e limpo
- Testes desde o inicio do desenvolvimento

Boas pr�ticas 


TODO TESTE DEVE SER INDEPENDENTE, N�O DEPENDER DE OUTROS TESTES

Um teste deve testar um �nico cen�rio

Varios cen�rios = vartios testes

Um bom teste deve evitar ifs e loops, o caminho deve ser linear, direto e �nico

Um teste com ifs testa mais de um cen�rio


 */