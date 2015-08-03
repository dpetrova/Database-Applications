using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using _01.DatabaseFirstDiablo;

namespace _03.ExportFinishedGamesAsXML
{
    class ExportXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var finishedGames = context.Games
                .Where(g => g.IsFinished == true)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Select(g => new
                {
                    name = g.Name,
                    duration = g.Duration, //.ToString() ?? "N/A",
                    users = g.UsersGames
                        .Select(u => new
                        {
                            username = u.User.Username,
                            ipAddress = u.User.IpAddress
                        })
                }).ToList();

            //foreach (var game in finishedGames)
            //{
            //    Console.WriteLine("--" + game.name + "--" + game.duration);
            //    foreach (var user in game.users)
            //    {
            //        Console.WriteLine(user.username + "; " + user.ipAddress);
            //    }
            //}

            XElement games = new XElement("games");
            foreach (var game in finishedGames)
            {
                XElement xmlGame =
                    new XElement("game",
                        new XAttribute("name", game.name));
                         XElement
                xmlUsers = new XElement("users");
                        foreach (var user in game.users)
                        {
                            XElement xmlUser =
                                new XElement("user",
                                    new XAttribute("username", user.username),
                                    new XAttribute("ip-address", user.ipAddress));
                           xmlUsers.Add(xmlUser);
                        }
                        xmlGame.Add(xmlUsers);
                
                //check if game has duration
                if (game.duration != null)
                {
                    xmlGame.Add(new XAttribute("duration", game.duration));
                }
                games.Add(xmlGame);
            }

            Console.WriteLine(games);

            //export data to .xml file
            games.Save("../../finished-games.xml");
        }
    }
}
