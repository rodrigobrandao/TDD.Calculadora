using Moq;
using NUnit.Framework;
using System.Data;
using TDD.Ecommerce.Service;
using TDD.Ecommerce.Service.Repository;

namespace TDD.Ecommerce.Tests
{
    [TestFixture]
    public class CalculadoraDescontoUnitTest
    {
        private DataRow RetornarTaxaDescontoBasica(string tipoCompra)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID_DESCONTO");
            dt.Columns.Add("ST_DESCRICAO");
            dt.Columns.Add("NM_VALOR_COMPRA");
            dt.Columns.Add("NM_TAXA_DESCONTO");

            DataRow valorDescontoRow = dt.NewRow();

            switch (tipoCompra)
            {
                case "CompraBasica":
                {
                    valorDescontoRow["ID_DESCONTO"] = 10;
                    valorDescontoRow["ST_DESCRICAO"] = "CompraBasica";
                    valorDescontoRow["NM_VALOR_COMPRA"] = 500;
                    valorDescontoRow["NM_TAXA_DESCONTO"] = 0.90;
                    break;
                }
                case "CompraGrande":
                    {
                        valorDescontoRow["ID_DESCONTO"] = 20;
                        valorDescontoRow["ST_DESCRICAO"] = "CompraGrande";
                        valorDescontoRow["NM_VALOR_COMPRA"] = 1000;
                        valorDescontoRow["NM_TAXA_DESCONTO"] = 0.80;
                        break;
                    }
            }

            return valorDescontoRow;


        }

        [TestCase]
        public void NaoRecebeDescontoAbaixoDoValorMinimo()
        {
            var mockDB = new Mock<Service.Infra.IConnectionDB>();

            DataRow valorCompraBasicoRow = this.RetornarTaxaDescontoBasica("CompraBasica");
            string comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraBasica'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraBasicoRow);
                        
            DataRow valorCompraGrandeRow = this.RetornarTaxaDescontoBasica("CompraGrande");
            comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraGrande'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraGrandeRow);
                                    
            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL(mockDB.Object);
            var result = bllCalculadoraDesconto.Calcular(400);
            var expected = 400;

            Assert.AreEqual(expected, result);
        }

       
        [TestCase]
        public void DeveAplicarDescontoDe10PorCentoQuandoValorIguall600reais()
        {
            var mockDB = new Mock<Service.Infra.IConnectionDB>();

            DataRow valorCompraBasicoRow = this.RetornarTaxaDescontoBasica("CompraBasica");
            string comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraBasica'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraBasicoRow);

            DataRow valorCompraGrandeRow = this.RetornarTaxaDescontoBasica("CompraGrande");
            comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraGrande'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraGrandeRow);

            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL(mockDB.Object);
            var result = bllCalculadoraDesconto.Calcular(600);
            var expected = 540;

            Assert.AreEqual(expected, result);
        }

        [TestCase]
        public void DeveAplicarDescontoDe20PorCentoQuandoValorIgual1000reais()
        {
            var mockDB = new Mock<Service.Infra.IConnectionDB>();

            DataRow valorCompraBasicoRow = this.RetornarTaxaDescontoBasica("CompraBasica");
            string comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraBasica'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraBasicoRow);

            DataRow valorCompraGrandeRow = this.RetornarTaxaDescontoBasica("CompraGrande");
            comando = $"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = 'CompraGrande'";
            mockDB.Setup(s => s.ExecuteDataRow(comando)).Returns(valorCompraGrandeRow);
                        
            CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL(mockDB.Object);
            var result = bllCalculadoraDesconto.Calcular(1000);
            var expected = 800;

            Assert.AreEqual(expected, result);
        }

        //[TestCase, Ignore("Black-Friday crash")]
        //public void DeveAplicarDescontoDe5PorCentoQuandoCompraPorImpulso()
        //{
        //    var calculadoraDesconto = new CalculadoraDesconto();
        //    var result = calculadoraDesconto.Calcular(499);
        //    var expected = 474.99;

        //    Assert.AreEqual(expected, result);
        //}
    }
}
