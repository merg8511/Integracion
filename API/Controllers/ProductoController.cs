using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Entidades;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> GetProductos()
        {
            List<ProductoDto> lista = await _context
                .Productos.Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(o => o.Promocion)
                .Select(p => new ProductoDto
                {
                    NombreProducto = p.NombreProducto,
                    Categoria = p.Categoria.Nombre,
                    Marca = p.Marca.Nombre,
                    Precio = p.Precio,
                    Costo = p.Costo,
                    PrecioActual = p.Promocion == null
                                 ? p.Precio
                                 : p.Promocion.NuevoPrecio,
                    TextoPromocional = p.Promocion == null
                                       ? null
                                       : p.Promocion.TextoPromocional
                })
                .ToListAsync();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _context
                .Productos.Include(c => c.Categoria)
                .Include(m => m.Marca)
                .Include(o => o.Promocion)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return Ok(
                new ProductoDto
                {
                    NombreProducto = producto.NombreProducto,
                    Categoria = producto.Categoria.Nombre,
                    Marca = producto.Marca.Nombre,
                    Costo = producto.Costo,
                    Precio = producto.Precio,
                    PrecioActual = producto.Promocion == null
                                                    ? producto.Precio
                                                    : producto.Promocion.NuevoPrecio,
                    TextoPromocional = producto.Promocion == null
                                                    ? null
                                                    : producto.Promocion.TextoPromocional
                }
            );
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            var productoBD = await _context.Productos.FindAsync(id);

            if (productoBD == null)
                return NotFound();

            productoBD.NombreProducto = producto.NombreProducto;
            productoBD.CategoriaId = producto.CategoriaId;
            productoBD.MarcaId = producto.MarcaId;
            productoBD.Costo = producto.Costo;
            productoBD.Precio = producto.Precio;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            var productoBD = await _context.Productos.FindAsync(id);
            if (productoBD == null)
                return NotFound();

            _context.Productos.Remove(productoBD);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
