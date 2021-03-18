using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {

        [Theory] //métodos que usam a notação theory são obrigados a passar dados de entrada
        [InlineData(new double[] { 800, 900, 1000, 1200 }, 1200)]
        [InlineData(new double[] { 800, 900, 1000, 990 }, 1000)]
        [InlineData(new double[] { 800 }, 800)]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double[] ofertas, double valorEsperado)
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }
            //act - método sob teste
            leilao.TerminaPregao();

            //assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
        
        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            //act - método sob teste
            leilao.TerminaPregao();

            //assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
