﻿using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class PatioTeste : IDisposable
    {

        private Veiculo veiculo;
        private Patio estacionamento;
        public ITestOutputHelper SaidaConsoleTeste;
        private Operador operador;

        public PatioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado");
            this.veiculo = new Veiculo();
            this.estacionamento = new Patio();

            operador = new Operador();
            operador.Nome = "Rafael F.C";
            estacionamento.OperadorPatio = operador;
        }

        [Fact]
        public void ValidaFaturamento()
        {
            //ARRANGE
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Rafael Cavicchioli";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Cruze";
            veiculo.Placa = "ASD-9999";

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);
            
            //ACT
            double faturamento = estacionamento.TotalFaturado();

            //ASSERT
            Assert.Equal(2, faturamento);
        }


        [Theory]
        [InlineData("Bruna Cassilha","BAF-2503","Branco","Cruze LTZ")]
        [InlineData("Rafael Cavicchioli", "RAF-2503", "Preto", "Cruze Sport6")]
        [InlineData("Felipe Francovig", "FEL-2805", "Branco", "T-Cross")]
        public void ValidaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //ARRANGE
            //var estacionamento = new Patio();
           // var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //ACT
            double faturamento = estacionamento.TotalFaturado();

            //ASSERT
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("Bruna Cassilha", "BAF-2503", "Branco", "Cruze LTZ")]
        public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string proprietario, string placa, string cor, string modelo)
        {
            //ARRANGE
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //ACT
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //ASSERT
            Assert.Contains("### Ticket Estacimento Alura ###", consultado.Ticket);
        }

        [Fact]
        public void AlterarDadosVeiculo()
        {
            //ARRANGE
            //var estacionamento = new Patio();
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Rafael Cavicchioli";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Cruze";
            veiculo.Placa = "ASD-9999";
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo();
            veiculoAlterado.Proprietario = "Rafael Cavicchioli";
            veiculoAlterado.Tipo = TipoVeiculo.Automovel;
            veiculoAlterado.Cor = "Branco"; //ALTERADO
            veiculoAlterado.Modelo = "Cruze";
            veiculoAlterado.Placa = "ASD-9999";

            //ACT
            Veiculo alterado = estacionamento.AlteraDadosVeiculo(veiculoAlterado);

            //ASSERT
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose Invocado");
        }
    }
}
