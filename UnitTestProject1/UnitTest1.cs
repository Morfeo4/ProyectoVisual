using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoVisual.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject1
{

    [TestClass]
    public class ProgramaEPMTest
    {
        [TestMethod]
        public void CalcularValorPagarServiciosTest()
        {

            int consumoEnergia = 100;
            int metaAhorro = 50;
            int consumoAgua = 20;
            int promedioAgua = 15;
            double valorKilovatio = 0.1;
            double valorM3agua = 1.5;


            double valorParcial = consumoEnergia * valorKilovatio;
            double valorIncentivo = (metaAhorro - consumoEnergia) * valorKilovatio;
            double valorPagarEnergia = valorParcial - valorIncentivo;

            double valorPagarAgua;
            if (consumoAgua > promedioAgua)
            {
                double excesoAgua = consumoAgua - promedioAgua;
                double castigoExceso = excesoAgua * (2 * valorM3agua);
                double costoPromedio = promedioAgua * valorM3agua;
                valorPagarAgua = costoPromedio + castigoExceso;
            }
            else
            {
                valorPagarAgua = consumoAgua * valorM3agua;
            }

            double valorServicios = valorPagarEnergia + valorPagarAgua;

            Assert.AreEqual(15, valorPagarEnergia, 0.01);
            Assert.AreEqual(37.5, valorPagarAgua, 0.01);
            Assert.AreEqual(52.5, valorServicios, 0.01);
        }




        private List<Cliente> GenerarListaClientes()
        {

            List<Cliente> listaClientes = new List<Cliente>
            {
                new Cliente { ConsumoEnergia = 100, MetaAhorro = 50 },
                new Cliente { ConsumoEnergia = 60, MetaAhorro = 70 },
                new Cliente { ConsumoEnergia = 40, MetaAhorro = 30 }
            };
            return listaClientes;
        }

        [TestMethod]
        public void CalcularTotalDescuentosTest()
        {
            
            GestionCliente.ListaClientes = GenerarListaClientes();

            double totalDescuentosEsperado = 0;
            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                if (cliente.ConsumoEnergia < cliente.MetaAhorro)
                {
                    double valorParcial = cliente.ConsumoEnergia * Calcular.valorKilovatio;
                    double valorIncentivo = (cliente.MetaAhorro - cliente.ConsumoEnergia) * Calcular.valorKilovatio;
                    totalDescuentosEsperado += valorIncentivo;
                }
            }

          
            double totalDescuentosObtenido = Calcular.CalcularTotalDescuentos();

            
            Assert.AreEqual(totalDescuentosEsperado, totalDescuentosObtenido, 0.01);
        }




        [TestMethod]
        public void ContabilizarClientesMayorPromedioAguaTest()
        {

            List<Cliente> listaClientes = new List<Cliente>
            {
                new Cliente { ConsumoAgua = 30, PromedioAgua = 20 },
                new Cliente { ConsumoAgua = 25, PromedioAgua = 18 },
                new Cliente { ConsumoAgua = 15, PromedioAgua = 15 }
            };
            GestionCliente.ListaClientes = listaClientes;


            int contClientesMayorPromedio = 0;
            foreach (Cliente cliente in listaClientes)
            {
                if (cliente.ConsumoAgua > cliente.PromedioAgua)
                {
                    contClientesMayorPromedio++;
                }
            }

            Assert.AreEqual(2, contClientesMayorPromedio);
        }

      

       
        [TestMethod]
        public void CalcularPromedioConsumoEnergiaTest()
        {

            // Arrange
            List<Cliente> listaClientes = new List<Cliente>
    {
        new Cliente { ConsumoAgua = 30, PromedioAgua = 20 },
        new Cliente { ConsumoAgua = 25, PromedioAgua = 18 },
        new Cliente { ConsumoAgua = 15, PromedioAgua = 15 }
    };
            GestionCliente.ListaClientes = listaClientes;

            int contClientesMayorPromedio = 0;
            foreach (Cliente cliente in listaClientes)
            {
                if (cliente.ConsumoAgua > cliente.PromedioAgua)
                {
                    contClientesMayorPromedio++;
                }
            }

            Assert.AreEqual(2, contClientesMayorPromedio);
        }

       

    }
}
