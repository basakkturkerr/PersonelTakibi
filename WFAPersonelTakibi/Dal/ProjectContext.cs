namespace WFAPersonelTakibi.Dal
{
    using System;
    using System.Data.Entity;
    using System.Linq; 
    using Models;

    public class ProjectContext : DbContext
    {

        public ProjectContext()
            : base("name=ProjectContext")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }
    }

}