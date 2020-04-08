using WebGigHub.Core.Models;
using WebGigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebGigHub.IntegrationTest
{
    [SetUpFixture]
    class GlobalSetup
    {
        private readonly ApplicationDbContext _context;

        public GlobalSetup() => _context = new ApplicationDbContext();

        [OneTimeSetUp]
        public void SetUp()
        {
            MigrateDbToLastedVersion();
            Seed();
        }

        private static void MigrateDbToLastedVersion()
        {
            var configuration = new WebGigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        private void Seed()
        {
            if (_context.Users.Any())
                return; 

            _context.Users.Add(new ApplicationUser { UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-" });
            _context.Users.Add(new ApplicationUser { UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-" });
            _context.SaveChanges();

        }
    }
}
