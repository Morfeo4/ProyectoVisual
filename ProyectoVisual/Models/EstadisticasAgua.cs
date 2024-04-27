using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVisual.Models
{
    public class EstadisticasAgua
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