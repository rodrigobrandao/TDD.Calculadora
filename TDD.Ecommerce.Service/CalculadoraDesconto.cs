using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Ecommerce.Service
{
    public class CalculadoraDesconto
    {
        public double Calcular(double valorCompra, VOL.ValorDesconto valorDescontoCompraBasica, VOL.ValorDesconto valorDescontoCompraGrande)
        {           
            if (valorCompra >= valorDescontoCompraGrande.ValorPedido)
                return valorCompra * valorDescontoCompraGrande.TaxaDesconto;

            if (valorCompra >= valorDescontoCompraBasica.ValorPedido)
                return valorCompra * valorDescontoCompraBasica.TaxaDesconto;
                        
            return valorCompra;
        }
    }
}
