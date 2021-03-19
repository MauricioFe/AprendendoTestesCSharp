using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh", valorDestino);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.InciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            //act - método sob teste
            leilao.TerminaPregao();
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }


        [Theory] //métodos que usam a notação theory são obrigados a passar dados de entrada
        [InlineData(new double[] { 800, 900, 1000, 1200 }, 1200)]
        [InlineData(new double[] { 800, 900, 1000, 990 }, 1000)]
        [InlineData(new double[] { 800 }, 800)]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double[] ofertas, double valorEsperado)
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.InciaPregao();
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            //act - método sob teste
            leilao.TerminaPregao();

            //assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            //assert
            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //act - método sob teste
                () => leilao.TerminaPregao()
                );
            var msgEsperada = "Não é possível terminar o pregão sem que le tenha comçado. Para isso utiliza o método iniciaPregao";

            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            leilao.InciaPregao();
            //act - método sob teste
            leilao.TerminaPregao();

            //assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
