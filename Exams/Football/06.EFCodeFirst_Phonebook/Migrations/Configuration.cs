namespace _06.EFCodeFirst_Phonebook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_06.EFCodeFirst_Phonebook.PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "_06.EFCodeFirst_Phonebook.PhonebookContext";
        }

        protected override void Seed(_06.EFCodeFirst_Phonebook.PhonebookContext context)
        {
            //check if the database has already data and if it has return
            if (context.Contacts.Any())
            {
                return;
            }

            //create data and add to database
            Contact peter = new Contact()
            {
                Name = "Peter Ivanov",
                Position = "CTO",
                Emails = new Email[]
                {
                    new Email() {EmailAddress = "peter@gmail.com"},
                    new Email() {EmailAddress = "peter_ivanov@yahoo.com"}
                },
                Phones = new Phone[]
                {
                    new Phone() {PhoneNumber = "+359 2 22 22 22"},
                    new Phone() {PhoneNumber = "+359 88 77 88 99"}
                },
                SiteUrl = "http://blog.peter.com",
                Notes = "Friend from school"
            };

            Contact maria = new Contact()
            {
                Name = "Maria",
                Phones = new Phone[]
                {
                    new Phone() {PhoneNumber = "+359 22 33 44 55"}
                }
            };
            
            Contact angie = new Contact()
            {
                Name = "Angie Stanton",
                Emails = new Email[]
                {
                   new Email() {EmailAddress = "info@angiestanton.com"}
                },
                SiteUrl = "http://angiestanton.com"
            };

            context.Contacts.AddOrUpdate(peter, maria, angie);
            context.SaveChanges();

        }
    }
}
