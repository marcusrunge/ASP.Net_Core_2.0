﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApplication
{
    public class TicketItem
    {
        public int Id { get; set; }
        public string Concert { get; set; }
        public string Artist { get; set; }
        public bool Available { get; set; }
    }

    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<TicketItem> TicketItems { get; set; }
    }
}
