using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _01.DatabaseFirstDiablo;

namespace _02.ExportCharactersPlayersAsJSON
{
    class ExportJSON
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var characters = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    name = c.Name,
                    playedBy = c.UsersGames
                        .Select(u => u.User.Username)
                }).ToList();

            //foreach (var character in characters)
            //{
            //    Console.WriteLine("--" + character.name);
            //    foreach (var player in character.playedBy)
            //    {
            //        Console.WriteLine(player);
            //    }
            //}

            var json = JsonConvert.SerializeObject(characters, Formatting.Indented);

            //write the output in a JSON file named leagues-and-teams.json in the directory of the project
            File.WriteAllText("../../characters.json", json);
            Console.WriteLine(json);
           
        }
    }
}
