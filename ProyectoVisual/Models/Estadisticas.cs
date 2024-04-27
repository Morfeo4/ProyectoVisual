using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVisual.Models
{
    public class Estadisticas
    {
       

        public static Cliente EncontrarClienteMayorDiferencia(List<Cliente> ListaClientes)
        {
            Cliente clienteMayorDiferencia = null;
            double mayorDifencia = 0;

            foreach (Cliente cliente in ListaClientes)
            {
                double diferencia = cliente.ConsumoEnergia - cliente.MetaAhorro;

                if (diferencia > mayorDifencia)
                {
                    clienteMayorDiferencia = cliente;
                    mayorDifencia = diferencia;
                }
            }

            return clienteMayorDiferencia;
        }

       

        public static Tuple<int, int> EncontrarEstratoMayorMenorConsumoEnergia(List<Cliente> ListaClientes)
        {
            double[] consumoEnergiaPorEstrato = new double[6];

            for (int i = 0; i < consumoEnergiaPorEstrato.Length; i++)
            {
                consumoEnergiaPorEstrato[i] = 0;
            }

            foreach (Cliente cliente in ListaClientes)
            {
                int indiceEstrato = cliente.Estrato - 1;
                consumoEnergiaPorEstrato[indiceEstrato] += cliente.ConsumoEnergia;
            }

            int estratoMayorConsumo = Array.IndexOf(consumoEnergiaPorEstrato, consumoEnergiaPorEstrato.Max()) + 1;
            int estratoMenorConsumo = Array.IndexOf(consumoEnergiaPorEstrato, consumoEnergiaPorEstrato.Min()) + 1;

            return Tuple.Create(estratoMayorConsumo, estratoMenorConsumo);
        }

        public static double CalcularValorTotalPagar(List<Cliente> ListaClientes)
        {
            double totalValorEnergia = 0;
            double totalValorAgua = 0;

            foreach (Cliente cliente in ListaClientes)
            {
                double valorParcialEnergia = cliente.ConsumoEnergia * Calcular.valorKilovatio;
                double valorIncentivoEnergia = (cliente.MetaAhorro - cliente.ConsumoEnergia) * Calcular.valorKilovatio;
                double valorPagarEnergia = valorParcialEnergia - valorIncentivoEnergia;

                double valorPagarAgua;
                if (cliente.ConsumoAgua > cliente.PromedioAgua)
                {
                    double excesoAgua = cliente.ConsumoAgua - cliente.PromedioAgua;
                    double castigoExceso = excesoAgua * (2 * Calcular.valorM3agua);
                    double costoPromedio = cliente.PromedioAgua * Calcular.valorM3agua;
                    valorPagarAgua = costoPromedio + castigoExceso;
                }
                else
                {
                    valorPagarAgua = cliente.ConsumoAgua * Calcular.valorM3agua;
                }

                totalValorEnergia += valorPagarEnergia;
                totalValorAgua += valorPagarAgua;
            }

            double totalValorServicios = totalValorEnergia + totalValorAgua;

            return totalValorServicios;
        }
    }
}