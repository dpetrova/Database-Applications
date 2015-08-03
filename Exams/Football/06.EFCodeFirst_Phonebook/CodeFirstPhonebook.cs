using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.EFCodeFirst_Phonebook
{
    class CodeFirstPhonebook
    {
        static void Main()
        {
            //Create an Entity Framework (EF) code first data model for keeping phonebook holding contacts with phones and emails.
            //It should have several entities:
            //•	Contacts have name and optionally position, company, emails, phones, site (URL) and notes (free text).
            //•	Emails hold email address.
            //•	Phones hold phone number.
            //Seed your database with a few contacts, using the EF migrations framework. 
            //It is OK to drop the database in case of model changes or use any other migration strategy
            //like automatic upgrade to the latest DB schema.
            //To test your data model, list all contacts along with their phones and emails.
           
            var context = new PhonebookContext();

            //set database
            //var count = context.Contacts.Count();
            //Console.WriteLine(count);

            //list all contacts along with their phones and emails
            var people = context.Contacts
                .Select(c => new
                {
                    ContactName = c.Name,
                    ContactPhones = c.Phones.Select(p => p.PhoneNumber),
                    ContactEmails = c.Emails.Select(e => e.EmailAddress)
                }).ToList();
            foreach (var person in people)
            {
                Console.WriteLine("--" + person.ContactName);
                Console.WriteLine("Phones: " + String.Join(", ", person.ContactPhones));
                Console.WriteLine("Emails: " + String.Join(", ", person.ContactEmails));
            }

        }
    }
}
