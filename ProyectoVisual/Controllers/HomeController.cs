using ProyectoVisual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoVisual.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionCliente gestionCliente;

        public HomeController()
        {
            gestionCliente = new GestionCliente();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngresarCliente(Cliente cliente)
        {
            gestionCliente.IngresarNuevoCliente(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult MostrarClientes()
        {
            var clientes = gestionCliente.ObtenerClientes();
            return View(clientes);
        }


        public ActionResult ObtenerClientePorNombre(string nombreCliente)
        {
            var cliente = gestionCliente.ObtenerClientePorNombre(nombreCliente);

            if (cliente != null)
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public void EditarInformacionCliente(Cliente clienteEditado)
        {
            // Aquí podrías agregar lógica para validar la edición del cliente antes de actualizarlo en la lista
            var clienteExistente = gestionCliente.ObtenerClientePorNombre(clienteEditado.Nombre);

            if (clienteExistente != null)
            {
                // Actualizar los datos del cliente existente con los datos del cliente editado
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
            var clienteEliminar = gestionCliente.ObtenerClientePorNombre(nombreCliente);

            if (clienteEliminar != null)
            {
                GestionCliente.ListaClientes.Remove(clienteEliminar);
            }
        }

        public ActionResult CalcularValorPagarServicios(int cedulaCliente)
        {
            double valorServicios = Calcular.CalcularValorPagarServicios(cedulaCliente);
            return Content(valorServicios.ToString());
        }

        public ActionResult CalcularPromedioConsumoEnergia()
        {
            double promedio = Calcular.CalcularPromedioConsumoEnergia();
            return Content(promedio.ToString());
        }

        public ActionResult CalcularTotalDescuentos()
        {
            double totalDescuentos = Calcular.CalcularTotalDescuentos();
            return Content(totalDescuentos.ToString());
        }

        public ActionResult CalcularExcesoAgua()
        {
            double excesoAgua = Calcular.CalcularExcesoAgua();
            return Content(excesoAgua.ToString());
        }

        public ActionResult ContabilizarClientesMayorPromedioAgua()
        {
            int cantidadClientes = Calcular.ContabilizarClientesMayorPromedioAgua();
            return Content(cantidadClientes.ToString());
        }

        public ActionResult MostrarPorcentajesConsumoAguaPorEstrato()
        {
            var porcentajes = Estadisticas.CalcularPorcentajesConsumoAguaPorEstrato(gestionCliente.ObtenerClientes());
            return View(porcentajes);
        }

        public ActionResult MostrarClienteMayorDiferencia()
        {
            var cliente = Estadisticas.EncontrarClienteMayorDiferencia(gestionCliente.ObtenerClientes());
            return View(cliente);
        }

        public ActionResult MostrarEstratoMayorAhorroAgua()
        {
            var estrato = Estadisticas.EncontrarEstratoMayorAhorroAgua(gestionCliente.ObtenerClientes());
            return View(estrato);
        }

        public ActionResult MostrarEstratoMayorMenorConsumoEnergia()
        {
            var tupla = Estadisticas.EncontrarEstratoMayorMenorConsumoEnergia(gestionCliente.ObtenerClientes());
            return View(tupla);
        }

        public ActionResult MostrarValorTotalPagar()
        {
            var totalPagar = Estadisticas.CalcularValorTotalPagar(gestionCliente.ObtenerClientes());
            return View(totalPagar);
        }

        public ActionResult MostrarClienteMayorConsumoAguaPorPeriodo(int periodoCliente)
        {
            var cliente = Estadisticas.EncontrarClienteMayorConsumoAguaPorPeriodo(gestionCliente.ObtenerClientes(), periodoCliente);
            return View(cliente);
        }

    }

}