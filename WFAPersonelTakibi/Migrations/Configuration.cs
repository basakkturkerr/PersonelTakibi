namespace WFAPersonelTakibi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WFAPersonelTakibi.Dal.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WFAPersonelTakibi.Dal.ProjectContext context)
        {
            
        }
    }
}
