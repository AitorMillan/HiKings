﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trabajo_ipo
{
    public class Pdi
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipologia { get; set; }
        public List<Uri> RutasFotos { get; set; } 
        public int posicionFoto { get; set; }

        public Pdi(string nombre, string descripcion, string tipologia, List<Uri> rutasFotos)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Tipologia = tipologia;
            RutasFotos = rutasFotos;
            posicionFoto = 0;
        }
    }
}
