//Zu beachten ist, dass Falscheingaben wie Tippfehler nicht abgefangen werden. Das ist für eine vorab-Version unnötiger Aufwand.


//  ________  ________  ________        ___  _______   ________ _________  ___       __   _______   ________  ___       __   ________  ___       ________ 
// |\   __  \|\   __  \|\   __  \      |\  \|\  ___ \ |\   ____\\___   ___\\  \     |\  \|\  ___ \ |\   __  \|\  \     |\  \|\   __  \|\  \     |\  _____\
// \ \  \|\  \ \  \|\  \ \  \|\  \     \ \  \ \   __/|\ \  \___\|___ \  \_\ \  \    \ \  \ \   __/|\ \  \|\  \ \  \    \ \  \ \  \|\  \ \  \    \ \  \__/ 
//  \ \   ____\ \   _  _\ \  \\\  \  __ \ \  \ \  \_|/_\ \  \       \ \  \ \ \  \  __\ \  \ \  \_|/_\ \   _  _\ \  \  __\ \  \ \  \\\  \ \  \    \ \   __\
//   \ \  \___|\ \  \\  \\ \  \\\  \|\  \\_\  \ \  \_|\ \ \  \____   \ \  \ \ \  \|\__\_\  \ \  \_|\ \ \  \\  \\ \  \|\__\_\  \ \  \\\  \ \  \____\ \  \_|
//    \ \__\    \ \__\\ _\\ \_______\ \________\ \_______\ \_______\  \ \__\ \ \____________\ \_______\ \__\\ _\\ \____________\ \_______\ \_______\ \__\ 
//     \|__|     \|__|\|__|\|_______|\|________|\|_______|\|_______|   \|__|  \|____________|\|_______|\|__|\|__|\|____________|\|_______|\|_______|\|__|




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Werwolf_Schulprojekt
{

    //Mitspieler Klasse.. (Was besitzt ein Spieler an Eigenschaften)
    class Mitspieler
    {
        public string Spielername { get; set; }
        public string Spielerrolle { get; set; }
        public int Votes { get; set; }
    }

    public class Program
    {
        //Eine neue Liste an Mitspielern wird erstellt.. => Als Klassenvariable, um von überall erreichbar zu sein
        private static List<Mitspieler> Mitspieler = new List<Mitspieler>();

        public static void Main(string[] args)
        {

            //Titel und Header blabla..
            Console.Title =   "Werwolf";
            Console.WriteAscii("Werwolf", Color.Crimson);
            Console.WriteLine("******************************************", Color.Crimson);
            Console.WriteLine("------------------------------------------", Color.DarkRed);
            Console.WriteLine("-----------Schulprojekt: Werwolf----------", Color.Crimson);
            Console.WriteLine("------------------------------------------", Color.DarkRed);
            Console.WriteLine("-----Drück eine Taste um fortzufahren-----", Color.Crimson);
            Console.WriteLine("------------------------------------------", Color.DarkRed);
            Console.WriteLine("******************************************", Color.Crimson);
            Console.ReadLine();

            //Mitspieler der Liste hinzufügen
            Mitspieler.Add(new Mitspieler { Spielername = "Du",        Spielerrolle = "Dorfbewohner", Votes = 0 });
            Mitspieler.Add(new Mitspieler { Spielername = "Spieler 1", Spielerrolle = "Dorfbewohner", Votes = 0 });
            Mitspieler.Add(new Mitspieler { Spielername = "Spieler 2", Spielerrolle = "Dorfbewohner", Votes = 0 });
            Mitspieler.Add(new Mitspieler { Spielername = "Spieler 3", Spielerrolle = "Dorfbewohner", Votes = 0 });
            Mitspieler.Add(new Mitspieler { Spielername = "Spieler 4", Spielerrolle = "Dorfbewohner", Votes = 0 });

            //Zufällige Zuweisung der Werwolfrolle und der daraufhin logische Output
            Random rnd = new Random();
            int whoswerwolf = rnd.Next(0, 5);
            Mitspieler[whoswerwolf].Spielerrolle = "Werwolf";
            if (whoswerwolf == 0)
            {
                Console.WriteLine("Du bist ein Werwolf. \nDrücke eine Taste für die erste Nacht.");
            }
            else
            {
                Console.WriteLine("Du bist ein Dorfbewohner. \nDrücke eine Taste für die erste Nacht.");
            }
            Console.ReadLine();
            NachtRunde();
        }

        //Nachdem die Rollen verteilt sind wird die erste Nachrunde gestartet
        //2 mögliche Fälle: Du bist Werwolf oder Dorfbewohner (Weitere Rollen brauchen weiteren if() bzw. Case)
        private static void NachtRunde()
        {
            if (Mitspieler[0].Spielerrolle == "Werwolf" && Mitspieler.Count() > 1)
            {
                //Auflistung der lebenden Spieler und Opfer-Eingabe seitens des Werwolfes, welches daraufhin aus der Liste entfernt wird
                Console.WriteLine("Es ist Nacht und du bist Werwolf - Gib einen Namen ein, wen du diese Nacht gerne umbringen willst\n");
                foreach (var spieler in Mitspieler.Where(i => i.Spielername != "Du"))
                {
                    Console.WriteLine(spieler.Spielername);
                }
                string eingabe = Console.ReadLine();

                Mitspieler.RemoveAll(i => i.Spielername == eingabe);
            }
            //else.. Wenn Dorfbewohner => Auflistung der lebenden Spieler, lediglich als Info.. Keine Interaktion
            else
            {
                Console.WriteLine("Du schläfst..");
            }

            Console.WriteLine("Es leben noch folgende Spieler:");
            foreach (var spieler in Mitspieler)
            {
                Console.WriteLine(spieler.Spielername);
            }
            Console.ReadLine();

            //Überprüfung ob das Spiel vorbei ist (Wird nach jeder Tag sowie Nachtrunde ausgeführt
            //Könnte als eigene Methode ausgelagert werden ¯\_(ツ)_/¯
            if (Mitspieler.Count() > 1 && Mitspieler.Exists(i => i.Spielername == "Du"))
            {
                TagRunde();
            }
            else
            {
                Spielende();
            }
        }
        //Tagesrunden Methode.. Was Tags halt so getan wird => Mir fällt beim Kommentieren auf, dass die Bürgermeisterfunktion fehlt. Das dürfte dann auch das Votingproblem lösen.
        private static void TagRunde()
        {
            //Votinglogik
            Console.WriteLine("Es ist Tag. Gib einen Namen ein, für wen du zum erhängen votest.");
            foreach (var spieler in Mitspieler.Where(i => i.Spielername != "Du"))
            {
                Console.WriteLine(spieler.Spielername);
            }
            string uservote = Console.ReadLine();
            Console.WriteLine("Du hast für {0} gevotet", uservote);
            var gevotedindex = Mitspieler.FindIndex(i => i.Spielername == uservote);
            Mitspieler[gevotedindex].Votes = Mitspieler[gevotedindex].Votes + 1;

            var nochzuvergebenevotes = Mitspieler.Count() - 1;

            for (int i = 0; i <= nochzuvergebenevotes; i++)
            {
                Random rnd = new Random();
                var gevoteter = rnd.Next(Mitspieler.Count());
                Mitspieler[gevoteter].Votes = Mitspieler[gevoteter].Votes + 1;
            }

            var maxvotes = Mitspieler.Max(i => i.Votes);

            var sterbenderspieler = Mitspieler.FindIndex(i => i.Votes == maxvotes);
            Mitspieler.RemoveAll(i => i.Votes == sterbenderspieler);

            //same like on top.. ¯\_(ツ)_/¯
            if (Mitspieler.Count() > 1 && Mitspieler.Exists(i => i.Spielername == "Du"))
            {
                NachtRunde();
            }
            else
            {
                Spielende();
            }
        }

        //Hier könnte ihr schöner End-Text stehen
        private static void Spielende()
        {
            Console.WriteLine("Die Runde ist vorbei");
            Console.ReadLine();
        }
    }
}
