using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoVisual.Models
{
    public class Cliente
    {
        int cedula, estrato, metaAhorro, consumoEnergia, consumoAgua, promedioAgua, periodoDeConsumo;
        string nombre, apellido;

        public Cliente()
        {
        }

        public Cliente(int cedula, int estrato, int metaAhorro, int consumoEnergia, int consumoAgua, int promedioAgua, int periodoDeConsumo, string nombre, string apellido)
        {
            this.Cedula = cedula;
            this.Estrato = estrato;
            this.MetaAhorro = metaAhorro;
            this.ConsumoEnergia = consumoEnergia;
            this.ConsumoAgua = consumoAgua;
            this.PromedioAgua = promedioAgua;
            this.PeriodoDeConsumo = periodoDeConsumo;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }

        public int Cedula { get => cedula; set => cedula = value; }
        public int Estrato { get => estrato; set => estrato = value; }
        public int MetaAhorro { get => metaAhorro; set => metaAhorro = value; }
        public int ConsumoEnergia { get => consumoEnergia; set => consumoEnergia = value; }
        public int ConsumoAgua { get => consumoAgua; set => consumoAgua = value; }
        public int PromedioAgua { get => promedioAgua; set => promedioAgua = value; }
        public int PeriodoDeConsumo { get => periodoDeConsumo; set => periodoDeConsumo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }

    }
}