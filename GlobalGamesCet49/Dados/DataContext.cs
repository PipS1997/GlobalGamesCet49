using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalGamesCet49.Dados.Entidades
{
    public class DataContext : DbContext
    {

        public DbSet<PedidoContacto> PedidoContactos { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
