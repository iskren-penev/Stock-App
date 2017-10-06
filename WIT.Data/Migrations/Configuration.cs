namespace WIT.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WIT.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<WitContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WitContext context)
        {

            this.SeedRoles(context);
            this.SeedDefaultUsers(context);
            this.SeedWarehouses(context);
            this.SeedStockEntries(context);
        }

        private void SeedRoles(WitContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            //add roles
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Moderator"))
            {
                var role = new IdentityRole("Moderator");
                manager.Create(role);
            }


            if (!context.Roles.Any(role => role.Name == "User"))
            {
                var role = new IdentityRole("User");
                manager.Create(role);
            }
        }

        private void SeedDefaultUsers(WitContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            //default admin user
            if (!(context.Users.Any(u => u.UserName == "admin@mail.com")))
            {
                var user = new User
                {
                    UserName = "admin@mail.com",
                    Email = "admin@mail.com",
                    CompanyName = "Admin Company"
                };
                var result = userManager.Create(user, "asdasd1");
                if (result.Succeeded)
                {
                    userManager.AddToRoles(user.Id, "Admin", "User");
                }
            }
            //test user
            if (!(context.Users.Any(u => u.UserName == "test@mail.com")))
            {
                var user = new User
                {
                    UserName = "test@mail.com",
                    Email = "test@mail.com",
                    CompanyName = "Test Company"
                };
                var result = userManager.Create(user, "asdasd1");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                }
            }
        }

        private void SeedWarehouses(WitContext context)
        {
            if (!context.Warehouses.Any())
            {
                context.Warehouses.AddOrUpdate(wh => wh.Name, new Warehouse()
                {
                    Name = "Grain Storage",
                    Address = "15, Hristo Botev blvd., Varna, Bulgaria",
                    Capacity = 175
                });

                context.Warehouses.AddOrUpdate(wh => wh.Name, new Warehouse()
                {
                    Name = "Liquid Fertilizer Storage",
                    Address = "51, Ivan Vazov street, Varna, Bulgaria",
                    Capacity = 333
                });

                context.SaveChanges();
            }
            
        }

        private void SeedStockEntries(WitContext context)
        {
            if (!context.StockEntries.Any())
            {
                Warehouse grainWarehouse = context.Warehouses.FirstOrDefault(wh => wh.Name == "Grain Storage");
                Warehouse fertilizerWarehouse = context.Warehouses.FirstOrDefault(wh => wh.Name == "Liquid Fertilizer Storage");

                User adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@mail.com");
                User testUser = context.Users.FirstOrDefault(u => u.Email == "test@mail.com");

                #region grain storage entries
                Record grainRecord1 = new Record()
                {
                    Distributor = adminUser,
                    AmountBefore = grainWarehouse.CurrentStock
                };
                StockEntry grainEntry1 = new StockEntry()
                {
                    Amount = 125,
                    EntryDate = DateTime.Now,
                    EntryType = "Input",
                };
                grainWarehouse.CurrentStock += grainEntry1.Amount;
                grainRecord1.AmountAfter = grainWarehouse.CurrentStock;
                grainEntry1.Record = grainRecord1;
                grainWarehouse.Entries.Add(grainEntry1);
                context.StockEntries.Add(grainEntry1);
                context.SaveChanges();

                grainRecord1.StockEntryId = grainEntry1.Id;
                context.SaveChanges();

                Record grainRecord2 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = grainWarehouse.CurrentStock
                };
                StockEntry grainEntry2 = new StockEntry()
                {
                    Amount = 57.75,
                    EntryDate = DateTime.Now,
                    EntryType = "Output",
                };
                grainWarehouse.CurrentStock -= grainEntry2.Amount;
                grainRecord2.AmountAfter = grainWarehouse.CurrentStock;
                grainEntry2.Record = grainRecord2;
                grainWarehouse.Entries.Add(grainEntry2);
                context.StockEntries.Add(grainEntry2);
                context.SaveChanges();

                grainRecord2.StockEntryId = grainEntry2.Id;
                context.SaveChanges();

                Record grainRecord3 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = grainWarehouse.CurrentStock
                };
                StockEntry grainEntry3 = new StockEntry()
                {
                    Amount = 30.25,
                    EntryDate = DateTime.Now,
                    EntryType = "Output",
                };
                grainWarehouse.CurrentStock -= grainEntry3.Amount;
                grainRecord3.AmountAfter = grainWarehouse.CurrentStock;
                grainEntry3.Record = grainRecord3;
                grainWarehouse.Entries.Add(grainEntry3);
                context.StockEntries.Add(grainEntry3);
                context.SaveChanges();

                grainRecord3.StockEntryId = grainEntry3.Id;
                context.SaveChanges();

                Record grainRecord4 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = grainWarehouse.CurrentStock
                };
                StockEntry grainEntry4 = new StockEntry()
                {
                    Amount = 34,
                    EntryDate = DateTime.Now,
                    EntryType = "Input",
                };
                grainWarehouse.CurrentStock += grainEntry4.Amount;
                grainRecord4.AmountAfter = grainWarehouse.CurrentStock;
                grainEntry4.Record = grainRecord4;
                grainWarehouse.Entries.Add(grainEntry4);
                context.StockEntries.Add(grainEntry4);
                context.SaveChanges();

                grainRecord4.StockEntryId = grainEntry4.Id;
                context.SaveChanges();


                Record grainRecord5 = new Record()
                {
                    Distributor = adminUser,
                    AmountBefore = grainWarehouse.CurrentStock
                };
                StockEntry grainEntry5 = new StockEntry()
                {
                    Amount = 46,
                    EntryDate = DateTime.Now,
                    EntryType = "Input"
                };
                grainWarehouse.CurrentStock += grainEntry5.Amount;
                grainRecord5.AmountAfter = grainWarehouse.CurrentStock;
                grainEntry5.Record = grainRecord5;
                grainWarehouse.Entries.Add(grainEntry5);
                context.StockEntries.Add(grainEntry5);
                context.SaveChanges();

                grainRecord5.StockEntryId = grainEntry5.Id;
                context.SaveChanges();

                #endregion

                #region fertilizer storage entries

                Record fertilizerRecord1 = new Record()
                {
                    Distributor = adminUser,
                    AmountBefore = fertilizerWarehouse.CurrentStock
                };
                StockEntry fertilizerEntry1 = new StockEntry()
                {
                    Amount = 325,
                    EntryDate = DateTime.Now,
                    EntryType = "Input"
                };
                fertilizerWarehouse.CurrentStock += fertilizerEntry1.Amount;
                fertilizerRecord1.AmountAfter = fertilizerWarehouse.CurrentStock;
                fertilizerEntry1.Record = fertilizerRecord1;
                fertilizerWarehouse.Entries.Add(fertilizerEntry1);
                context.StockEntries.Add(fertilizerEntry1);
                context.SaveChanges();

                fertilizerRecord1.StockEntryId = fertilizerEntry1.Id;
                context.SaveChanges();

                Record fertilizerRecord2 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = fertilizerWarehouse.CurrentStock
                };
                StockEntry fertilizerEntry2 = new StockEntry()
                {
                    Amount = 275,
                    EntryDate = DateTime.Now,
                    EntryType = "Output"
                };
                fertilizerWarehouse.CurrentStock -= fertilizerEntry2.Amount;
                fertilizerRecord2.AmountAfter = fertilizerWarehouse.CurrentStock;
                fertilizerEntry2.Record = fertilizerRecord2;
                fertilizerWarehouse.Entries.Add(fertilizerEntry2);
                context.StockEntries.Add(fertilizerEntry2);
                context.SaveChanges();

                fertilizerRecord2.StockEntryId = fertilizerEntry2.Id;
                context.SaveChanges();

                Record fertilizerRecord3 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = fertilizerWarehouse.CurrentStock
                };
                StockEntry fertilizerEntry3 = new StockEntry()
                {
                    Amount = 25,
                    EntryDate = DateTime.Now,
                    EntryType = "Output"
                };
                fertilizerWarehouse.CurrentStock -= fertilizerEntry3.Amount;
                fertilizerRecord3.AmountAfter = fertilizerWarehouse.CurrentStock;
                fertilizerEntry3.Record = fertilizerRecord3;
                fertilizerWarehouse.Entries.Add(fertilizerEntry3);
                context.StockEntries.Add(fertilizerEntry3);
                context.SaveChanges();

                fertilizerRecord3.StockEntryId = fertilizerEntry3.Id;
                context.SaveChanges();

                Record fertilizerRecord4 = new Record()
                {
                    Distributor = testUser,
                    AmountBefore = fertilizerWarehouse.CurrentStock
                };
                StockEntry fertilizerEntry4 = new StockEntry()
                {
                    Amount = 135,
                    EntryDate = DateTime.Now,
                    EntryType = "Input"
                };
                fertilizerWarehouse.CurrentStock += fertilizerEntry4.Amount;
                fertilizerRecord4.AmountAfter = fertilizerWarehouse.CurrentStock;
                fertilizerEntry4.Record = fertilizerRecord4;
                fertilizerWarehouse.Entries.Add(fertilizerEntry4);
                context.StockEntries.Add(fertilizerEntry4);
                context.SaveChanges();

                fertilizerRecord4.StockEntryId = fertilizerEntry4.Id;
                context.SaveChanges();


                Record fertilizerRecord5 = new Record()
                {
                    Distributor = adminUser,
                    AmountBefore = fertilizerWarehouse.CurrentStock
                };
                StockEntry fertilizerEntry5 = new StockEntry()
                {
                    Amount = 145,
                    EntryDate = DateTime.Now,
                    EntryType = "Input"
                };
                fertilizerWarehouse.CurrentStock += fertilizerEntry5.Amount;
                fertilizerRecord5.AmountAfter = fertilizerWarehouse.CurrentStock;
                fertilizerEntry5.Record = fertilizerRecord5;
                fertilizerWarehouse.Entries.Add(fertilizerEntry5);
                context.StockEntries.Add(fertilizerEntry5);
                context.SaveChanges();

                fertilizerRecord5.StockEntryId = fertilizerEntry5.Id;
                context.SaveChanges();

                #endregion
            }

        }
    }
}
