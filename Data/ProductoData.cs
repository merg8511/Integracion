using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entidades;

namespace Data
{
    public static class ProductoData
    {
        public static List<Producto> listaProducto = new List<Producto>{
           new Producto {Id = 1, NombreProducto = "Producto 1", Categoria="Computadoras", Marca="HP", Precio=2000},
           new Producto {Id = 2, NombreProducto = "Producto 2", Categoria="Computadoras", Marca="DELL", Precio=2000},
           new Producto {Id = 1, NombreProducto = "Producto 3", Categoria="Computadoras", Marca="LENOVO", Precio=2000},
           new Producto {Id = 1, NombreProducto = "Producto 4", Categoria="Computadoras", Marca="ASUS", Precio=2000},
        };
    }
}