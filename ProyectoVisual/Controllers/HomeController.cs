using ProyectoVisual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            ViewBag.Mensaje = "Cliente ingresado correctamente";
         
            return View("IngresarCliente");
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

        [HttpPost]
        public ActionResult EditarInformacionCliente(Cliente clienteEditado)
        {
            gestionCliente.EditarInformacionCliente(clienteEditado);

            
            return RedirectToAction("EditarInformacion");
        }


        [HttpPost]
        public ActionResult EliminarCliente(string nombreCliente)
        {
        
            var clienteEliminar = GestionCliente.ListaClientes.Find(c => c.Nombre == nombreCliente);


            if (clienteEliminar != null)
            {
                GestionCliente.ListaClientes.Remove(clienteEliminar);

                return RedirectToAction("ClienteEliminado");
            }
            else
            {
      
                return RedirectToAction("ClienteNoEncontrado");
            }
        }



        [HttpGet]
        public ActionResult CalcularValorPagarServicios(int cedulaCliente)
        {

            double valorTotal = CalcularValor(cedulaCliente);

  
            return Json(valorTotal, JsonRequestBehavior.AllowGet);
        }

        private double CalcularValor(int cedulaCliente)
        {
     
            double valorServicios = 0.0;

            return valorServicios;
        }

        public ActionResult CalcularPromedioConsumoEnergia()
        {
            double promedioConsumoGeneral = CalcularPromedio();

            
            ViewBag.PromedioConsumoEnergia = promedioConsumoGeneral;

           
            return View();
        }

        private double CalcularPromedio()
        {

            double sumatoriaConsumoEnergia = 0;

            foreach (Cliente cliente in GestionCliente.ListaClientes)
            {
                sumatoriaConsumoEnergia += cliente.ConsumoEnergia;
            }

      
            double promedioConsumoGeneral = sumatoriaConsumoEnergia / GestionCliente.ListaClientes.Count;
            return promedioConsumoGeneral;
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
            var porcentajes = EstadisticasAgua.CalcularPorcentajesConsumoAguaPorEstrato(gestionCliente.ObtenerClientes());
            return View(porcentajes);
        }

        public ActionResult MostrarClienteMayorDiferencia()
        {
            var cliente = Estadisticas.EncontrarClienteMayorDiferencia(gestionCliente.ObtenerClientes());
            return View(cliente);
        }

        public ActionResult MostrarEstratoMayorAhorroAgua()
        {
            var estrato = EstadisticasAgua.EncontrarEstratoMayorAhorroAgua(gestionCliente.ObtenerClientes());
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
            var cliente = EstadisticasAgua.EncontrarClienteMayorConsumoAguaPorPeriodo(gestionCliente.ObtenerClientes(), periodoCliente);
            return View(cliente);
        }

    }

  
}