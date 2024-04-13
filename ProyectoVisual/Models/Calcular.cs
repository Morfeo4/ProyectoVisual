using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVisual.Models
{
    public class Calcular
    {

        public static int valorKilovatio = 850;
        public static int valorM3agua = 4600;

        public static double CalcularValorPagarServicios(int cedulaCliente)
        {
            Cliente cliente = GestionCliente.ListaClientes.Find(c => c.Cedula == cedulaCliente);

            double valorParcial = cliente.ConsumoEnergia * valorKilovatio;
            double valorIncentivo = (cliente.MetaAhorro - cliente.ConsumoEnergia) * valorKilovatio;
            double valorPagarEnergia = valorParcial - valorIncentivo;

            double valorPagarAgua;
            if (cliente.ConsumoAgua > cliente.PromedioAgua)
            {
                double excesoAgua = cliente.ConsumoAgua - cliente.PromedioAgua;
                double castigoExceso = excesoAgua * (2 * valorM3agua);
                double costoPromedio = cliente.PromedioAgua * valorM3agua;
                valorPagarAgua = costoPromedio + castigoExceso;
            }
            else
            {
                valorPagarAgua = cliente.ConsumoAgua * valorM3agua;
            }
            double valorServios = valorPagarEnergia + valorPagarAgua;

            return valorServios;
        }

        public static double CalcularPromedioConsumoEnergia()
        {
            double sumatoriaConsumoEnergia = 0;
            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                sumatoriaConsumoEnergia += cliente.ConsumoEnergia;
            }

            double promedioConsumoGeneral = sumatoriaConsumoEnergia / GestionCliente.ListaClientes.Count;
            return promedioConsumoGeneral;
        }

        public static double CalcularTotalDescuentos()
        {
            double totalDescuentosIncentivos = 0;
            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                if (cliente.ConsumoEnergia < cliente.MetaAhorro)
                {
                    double valorParcial = cliente.ConsumoEnergia * valorKilovatio;
                    double valorIncentivo = (cliente.MetaAhorro - cliente.ConsumoEnergia) * valorKilovatio;
                    totalDescuentosIncentivos += valorIncentivo;
                }

            }
            return totalDescuentosIncentivos;
        }

        public static double CalcularExcesoAgua()
        {
            double contConsumoagua = 0;
            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                contConsumoagua += cliente.ConsumoAgua;
            }
            double promedioConsumoagua = contConsumoagua / GestionCliente.ListaClientes.Count;
            double aguaMayorpromedio = 0;

            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                if (cliente.ConsumoAgua > promedioConsumoagua)
                {
                    aguaMayorpromedio += cliente.ConsumoAgua - promedioConsumoagua;
                }
            }
            return aguaMayorpromedio;
        }

        public static int ContabilizarClientesMayorPromedioAgua()
        {
            int contClientesMayorPromedio = 0;
            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                if (cliente.ConsumoAgua > cliente.PromedioAgua)
                {
                    contClientesMayorPromedio++;
                }
            }
            return contClientesMayorPromedio;
        }

    
    }
}