using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulado", leilao);
            leilao.InciaPregao();
            leilao.RecebeLance(fulano, 800);
            //act - método sob teste
            leilao.RecebeLance(fulano, 1000);
            //assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEspereda, double[] ofertas)
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulado", leilao);
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

            leilao.TerminaPregao();
            //act - método sob teste
            leilao.RecebeLance(fulano, 1000);
            //assert

            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdEspereda, qtdeObtida);
        }
    }
}
