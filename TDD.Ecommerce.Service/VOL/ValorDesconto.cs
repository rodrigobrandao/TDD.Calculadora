using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Ecommerce.Service.VOL
{
    public enum TipoDescontoEnum
    {
        CompraBasica,
        CompraGrande
    }

    public class ValorDesconto
    {       
        public double TaxaDesconto { get; }
        public double ValorPedido { get; }

        public ValorDesconto(double valorPedido, double desconto)
        {
            this.ValorPedido = valorPedido;
            this.TaxaDesconto = desconto;
        }
    }
}
