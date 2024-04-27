using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProyectoVisual.Models
{
    public class GestionCliente
    {

        public static List<Cliente> ListaClientes { get; set; }
        public int SumatoriaConsumoEnergia { get; set; }

        public GestionCliente()
        {
            ListaClientes = new List<Cliente>();
            SumatoriaConsumoEnergia = 0;
        }

        public List<Cliente> ObtenerClientes()
        {
            return ListaClientes;
        }
        public Cliente MostrarClientePorNombre(string nombreCliente)
        {
            return ListaClientes.FirstOrDefault(c => c.Nombre == nombreCliente);
        }
        public void IngresarNuevoCliente(Cliente nuevoCliente)
        {
  
            SumatoriaConsumoEnergia += nuevoCliente.ConsumoEnergia;
            ListaClientes.Add(nuevoCliente);
        }

        public Cliente ObtenerClientePorNombre(string nombreCliente)
        {
            return ListaClientes.Find(c => c.Nombre == nombreCliente);
        }

        public void EditarInformacionCliente(Cliente clienteEditado)
        {

            var clienteExistente = ListaClientes.Find(c => c.Nombre == clienteEditado.Nombre);
            if (clienteExistente != null)
            {

                clienteExistente.Apellido = clienteEditado.Apellido;
                clienteExistente.Cedula = clienteEditado.Cedula;
                clienteExistente.Estrato = clienteEditado.Estrato;
                clienteExistente.MetaAhorro = clienteEditado.MetaAhorro;
                clienteExistente.ConsumoEnergia = clienteEditado.ConsumoEnergia;
                clienteExistente.PromedioAgua = clienteEditado.PromedioAgua;
                clienteExistente.ConsumoAgua = clienteEditado.ConsumoAgua;
                clienteExistente.PeriodoDeConsumo = clienteEditado.PeriodoDeConsumo;
            }
        }

        public void EliminarCliente(string nombreCliente)
        {
            var clienteEliminar = ListaClientes.Find(c => c.Nombre == nombreCliente);
            if (clienteEliminar != null)
            {
                ListaClientes.Remove(clienteEliminar);
            }
        }



    }
}