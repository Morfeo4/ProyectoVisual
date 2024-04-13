using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVisual.Models
{
    public class Estadisticas
    {
        public static List<double> CalcularPorcentajesConsumoAguaPorEstrato(List<Cliente> ListaClientes)
        {
            List<double> porcentajes = new List<double>();

            List<double> consumoTotalEstrato = new List<double>();
            List<double> excesoConsumoEstrato = new List<double>();

            for (int i = 0; i < 6; i++)
            {
                consumoTotalEstrato.Add(0);
                excesoConsumoEstrato.Add(0);
            }

            foreach (Cliente cliente in ListaClientes)
            {
                consumoTotalEstrato[cliente.Estrato - 1] += cliente.ConsumoAgua;
                excesoConsumoEstrato[cliente.Estrato - 1] += Math.Max(cliente.ConsumoAgua - cliente.PromedioAgua, 0);
            }

            for (int i = 0; i < consumoTotalEstrato.Count; i++)
            {
                double porcentaje = 0;
                if (consumoTotalEstrato[i] > 0)
                {
                    porcentaje = (excesoConsumoEstrato[i] / consumoTotalEstrato[i]) * 100;
                }
                porcentajes.Add(porcentaje);
            }

            return porcentajes;
        }

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

        public static int EncontrarEstratoMayorAhorroAgua(List<Cliente> ListaClientes)
        {
            int[] ahorroPorEstrato = new int[6];

            foreach (Cliente cliente in ListaClientes)
            {
                int ahorro = cliente.PromedioAgua - cliente.ConsumoAgua;
                if (ahorro > 0 && cliente.Estrato >= 1 && cliente.Estrato <= 6)
                {
                    ahorroPorEstrato[cliente.Estrato - 1] += ahorro;
                }
            }

            int estratoMayorAhorro = Array.IndexOf(ahorroPorEstrato, ahorroPorEstrato.Max()) + 1;

            return estratoMayorAhorro;
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

        public static Cliente EncontrarClienteMayorConsumoAguaPorPeriodo(List<Cliente> ListaClientes, int periodoCliente)
        {
            double mayorConsumoPeriodo = 0;
            Cliente clienteMayorConsumoPeriodo = null;

            foreach (Cliente cliente in ListaClientes)
            {
                if (cliente.PeriodoDeConsumo == periodoCliente && cliente.ConsumoAgua > mayorConsumoPeriodo)
                {
                    mayorConsumoPeriodo = cliente.ConsumoAgua;
                    clienteMayorConsumoPeriodo = cliente;
                }
            }

            return clienteMayorConsumoPeriodo;
        }

    }
}