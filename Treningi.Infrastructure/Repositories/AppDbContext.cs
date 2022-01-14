﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Treningi.Core;
using Treningi.WebApp.Models;

namespace Treningi.Infrastructure.Repositories
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Competitor> Competitor { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ImageModel> Images { get; set; }


    }
}
