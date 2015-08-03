using _06.EFCodeFirst_Phonebook.Migrations;

namespace _06.EFCodeFirst_Phonebook
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookContext : DbContext
    {
        // Your context has been configured to use a 'PhonebookContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_06.EFCodeFirst_Phonebook.PhonebookContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PhonebookContext' 
        // connection string in the application configuration file.
        public PhonebookContext()
            : base("name=PhonebookContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}