using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDD.Ecommerce.Service.Repository;

namespace TDD.Ecommerce.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CalculoEcommerce()
        {
            ViewBag.Message = "Carrinho compras";

            string valorPedido = Request.Form["txtValor"];
            if (valorPedido != "")
            {
                CalculadoraDescontoBLL bllCalculadoraDesconto = new CalculadoraDescontoBLL();
                ViewBag.ValorPedidoComDesconto = bllCalculadoraDesconto.Calcular(Convert.ToDouble(valorPedido));
            }

            return View();
        }
    }
}