using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.Ecommerce.Service;
using TDD.Ecommerce.Service.Infra;
using TDD.Ecommerce.Service.Repository;

namespace TDD.Ecommerce.Tests
{
    [TestFixture]
    public class CalculadoraDescontoIntegratedTest
    {
        [TestCase]
        public void NaoRecebeDescontoAbaixoDoValorMinimo()
        {           
            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL();
            var result = bllCalculadoraDesconto.Calcular(400);
            var expected = 400;

            Assert.AreEqual(expected, result);
        }


        [TestCase]
        public void DeveAplicarDescontoDe10PorCentoQuandoValorIguall600reais()
        {            
            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL();
            var result = bllCalculadoraDesconto.Calcular(600);
            var expected = 540;

            Assert.AreEqual(expected, result);
        }

        [TestCase]
        public void DeveAplicarDescontoDe20PorCentoQuandoValorIgual1000reais()
        {
            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL();
            var result = bllCalculadoraDesconto.Calcular(1000);
            var expected = 800;

            Assert.AreEqual(expected, result);
        }
    }
}
