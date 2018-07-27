using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TDD.Ecommerce.Service.Repository
{
    public class CalculadoraDescontoBLL
    {
        protected Infra.IConnectionDB ConnectionDB { get; set; }

        public CalculadoraDescontoBLL()
        {
            this.ConnectionDB = new Infra.ConnectionDB();
        }
                
        public CalculadoraDescontoBLL(Infra.IConnectionDB connectionDB)
        {
            this.ConnectionDB = connectionDB;
        }

        private VOL.ValorDesconto RecuperarValorDesconto(VOL.TipoDescontoEnum tipoDesconto)
        {
            string comando = $@"SELECT * FROM TB_TAXA_DESCONTO WHERE ST_DESCRICAO = '{tipoDesconto.ToString()}'";

            //var x = this.ConnectionDB.Somar(5, 3);
            //var y = this.ConnectionDB.Somar(1, 1);

            DataRow dr = this.ConnectionDB.ExecuteDataRow(comando);
            
            if (dr == null) return null;

            var idDesconto = Convert.ToInt32(dr["ID_DESCONTO"].ToString());
            var taxaDesconto = Convert.ToDouble(dr["NM_TAXA_DESCONTO"].ToString());
            var valorCompra = Convert.ToDouble(dr["NM_VALOR_COMPRA"].ToString());
            
            return new VOL.ValorDesconto(valorCompra, taxaDesconto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valorCompra"></param>
        /// <returns></returns>
        public double Calcular(double valorCompra)
        {
            var valorDescontoCompraBasica = this.RecuperarValorDesconto(VOL.TipoDescontoEnum.CompraBasica);
            var valorDescontoCompraGrande = this.RecuperarValorDesconto(VOL.TipoDescontoEnum.CompraGrande);

            var calculadoraDesconto = new CalculadoraDesconto();
            double valorCompraComDesconto = calculadoraDesconto.Calcular(valorCompra, valorDescontoCompraBasica, valorDescontoCompraGrande);

            return valorCompraComDesconto;
        }

        
    }
}
