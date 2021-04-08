using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ProjectConnectDB
{
    public class ConnectContext : DbContext
    {
        public virtual DbSet<Employee> Employees {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=THEPC; Database=PayrollDB; User Id=sa; Password=P@ssw0rd123");
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Employee>().HasKey(emp => emp.EmpNo);
        }
    }
}