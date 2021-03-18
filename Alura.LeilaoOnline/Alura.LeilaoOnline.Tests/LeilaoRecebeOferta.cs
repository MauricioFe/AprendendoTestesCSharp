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
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEspereda, double[] ofertas)
        {
            //Arranje - cenário de entrada
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulado", leilao);
            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
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
