using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using _06.EFCodeFirst_Phonebook;

namespace _07.ImportFromJSON
{
    class ImportFromJSON
    {
        static void Main()
        {
            //Problem 7.	Import Contacts from JSON

            //Write a C# application based on your EF code first data model for importing into the DB
            //a set of phonebook contacts given in the file contacts.json.
            //The only mandatory property is name. All the others are optional.
            //You should parse the JSON and throw an exception in case of incorrect data, e.g. when a required element is missing
            //or an invalid value is given. The size of the JSON file will be less than 10 MB. Use a JSON parser by choice.
            //You should correctly import the contacts into the DB.
            //You should correctly import the emails and phones into the DB.
            //Insert each contact in a separate transaction. A contact should either be inserted correctly along with all its emails
            //and phones, or no part of it should be inserted at all. 
            //Print as output a single line for each contact from the input JSON: either "Contact <name> imported" 
            //or "Error: <error message>". Error messages should describe briefly the problem (as free text) and may optionally
            //include exception stack-trace.
            
            var context = new PhonebookContext();

            //JsonConvert.DeserializeObject() не поддържа десериализация на масиви от обекти
            string objectsArray = File.ReadAllText("../../contacts.json");

            JArray contacts = JArray.Parse(objectsArray);

            foreach (JToken contact in contacts) //JToken представлява един ред от JSON-a и мога да си го викам по име
            {
                Contact dbContact = new Contact();
                //var name = contact["name"] ?? null;
                if (contact["name"] == null)
                {
                    Console.WriteLine("Error: Name is required");
                    continue;
                }
                dbContact.Name = contact["name"].ToString();

                if (contact["company"] != null)
                {
                    dbContact.Company = contact["company"].ToString();
                }

                if (contact["position"] != null)
                {
                    dbContact.Position = contact["position"].ToString();
                }

                if (contact["site"] != null)
                {
                    dbContact.SiteUrl = contact["site"].ToString();
                }

                if (contact["notes"] != null)
                {
                    dbContact.Notes = contact["notes"].ToString();
                }

                if (contact["phones"] != null)
                {
                    foreach (var phone in contact["phones"])
                    {
                        Phone dbPhone = new Phone()
                        {
                            PhoneNumber = phone.ToString()
                        };
                        dbContact.Phones.Add(dbPhone);
                    }
                }

                if (contact["emails"] != null)
                {
                    foreach (var email in contact["emails"])
                    {
                        Email dbEmail = new Email()
                        {
                            EmailAddress = email.ToString()
                        };
                        dbContact.Emails.Add(dbEmail);
                    }
                }

                context.Contacts.Add(dbContact);
                Console.WriteLine("Contact {0} imported", dbContact.Name);
                context.SaveChanges();
            }

            
            
        }
    }
}
