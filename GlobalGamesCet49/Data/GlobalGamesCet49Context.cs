using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GlobalGamesCet49.Dados.Entidades;

namespace GlobalGamesCet49.Data
{
    public class GlobalGamesCet49Context : DbContext
    {
        public GlobalGamesCet49Context (DbContextOptions<GlobalGamesCet49Context> options)
            : base(options)
        {
        }

        public DbSet<GlobalGamesCet49.Dados.Entidades.PedidoContacto> PedidoContacto { get; set; }
    }
}
