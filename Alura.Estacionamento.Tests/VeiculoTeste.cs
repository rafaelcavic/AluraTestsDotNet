using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class VeiculoTeste : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTeste;

        public VeiculoTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado");
           this.veiculo = new Veiculo();
        }


        [Fact(DisplayName ="Teste Acelerar")]   
        [Trait("Funcionalidade","Acelerar")]
        public void TestaVeiculoAcelerar()
        {
            //Arrange
           //var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);

        }

        [Fact(DisplayName = "Teste Frear")]
        [Trait("Funcionalidade", "Frear")]
        public void TestaVeiculoFrear()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Frear(10);
            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact(Skip="Teste não implementado. Ignorar")]
        public void ValidaNomeProprietario()
        {
         
            
        }

        [Theory]
        [ClassData(typeof(Veiculo))]
        public void TestaVeiculoClass(Veiculo modelo)
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);
            modelo.Acelerar(10);

            //Assert
            Assert.Equal(modelo.VelocidadeAtual, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void DadosVeiculo()
        {
            //Arrange
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Rafael Cavicchioli";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Cruze";
            veiculo.Placa = "ASD-9999";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Tipo do Veículo: Automovel", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            //Arrange
            string nomeProprietario = "Lu";

            //Assert
            Assert.Throws<System.FormatException>(
                //Act
                () => new Veiculo(nomeProprietario)
                );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCartactereDaPlaca()
        {
            //Arrange
            string placa = "ASDU8888";

            //Assert
            var mensagem = Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
                );

            Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
        }

        [Fact]
        public void TestaSeOsUltimosCaracteresDaPlacaSaoSomenteNumeros()
        {
            //Arrange
            string placa = "ASDU8388";

            //Assert
            Assert.Throws<System.FormatException>(
                () => new Veiculo().Placa = placa
                );
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose incovado");
        }
    }
}
