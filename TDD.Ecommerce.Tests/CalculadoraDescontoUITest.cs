using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;



namespace TDD.Ecommerce.Tests
{
    [TestFixture]
    public class CalculadoraDescontoUITest
    {
        IWebDriver Driver { get; set; }
        string UrlSite { get; set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            this.Driver = new OpenQA.Selenium.IE.InternetExplorerDriver(); 
            this.UrlSite = System.Configuration.ConfigurationManager.AppSettings.Get("UrlSite");
        }

        [Test]
        public void DeveCalcularValorDescontoCom10PorCento()
        {            
            Driver.Navigate().GoToUrl(this.UrlSite);

            var link = this.Driver.FindElements(By.XPath(".//li")).ElementAt(2);
            link.Click();            
            
            var txtValorPedido = this.Driver.FindElement(By.XPath("//input[@id='txtValor']"));
            txtValorPedido.SendKeys("500");

            var botaoCalcular = this.Driver.FindElement(By.XPath("//input[@id='btnCalcular']"));
            botaoCalcular.Click();

            var divValorPedidoComDesconto = this.Driver.FindElement(By.XPath("//div[@id='divValorPedidoComDesconto']"));
            
            Assert.AreEqual(450, Convert.ToDouble(divValorPedidoComDesconto.Text));
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            this.Driver.Dispose();
        }
    }
}
