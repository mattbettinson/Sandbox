using System;
using Sandbox.Data;
using PunDataService = Sandbox.Console.PunClient.DataServices.PunDataService;

namespace Sandbox.Console.PunClient
{
    public class Program
    {
        private static PunDataService _service;

        static void Main(string[] args)
        {
            _service = new PunDataService();
            string input = "";
            while (input.ToUpper() != "Q")
            {
                System.Console.WriteLine("---------------");
                System.Console.WriteLine("L) List puns");
                System.Console.WriteLine("#) Show specific pun");
                System.Console.WriteLine("N) Enter a new pun");
                System.Console.WriteLine("Q) Quit");
                System.Console.Write("Please enter a command: ");
                input = System.Console.ReadLine().ToUpper();
                int index = 0;
                if (input == "L")
                {
                    ListPuns();
                }
                else if (input == "N")
                {
                    EnterPun();
                }
                else if (Int32.TryParse(input, out index))
                {
                    ShowPun(index);
                }
            }
        }

        private static void ShowPun(int index)
        {
            var pun = _service.GetPunByID(index);
            System.Console.WriteLine("{0}) {1}:", pun.PunID, pun.Title);
            System.Console.WriteLine("{0}", pun.Joke);
            System.Console.WriteLine("---------------");
            System.Console.WriteLine("E) Edit pun");
            System.Console.WriteLine("D) Delete pun");
            System.Console.WriteLine("C) Continue");
            System.Console.Write("Please enter a command: ");
            var input = System.Console.ReadLine().ToUpper();
            if (input == "E")
            {
                EditPun(index);
            }
            else if (input == "D")
            {
                DeletePun(index);
            }
        }

        private static void DeletePun(int index)
        {
            System.Console.WriteLine("---------------");
            System.Console.Write("Are you sure you want to delete this pun? (Y/N) ");
            var input = System.Console.ReadLine().ToUpper();
            if (input == "Y")
            {
                _service.DeletePun(index);
            }
        }

        private static void EditPun(int index)
        {
            System.Console.WriteLine("---------------");
            System.Console.Write("Name of pun? ");
            var name = System.Console.ReadLine();
            System.Console.Write("Pun? ");
            var joke = System.Console.ReadLine();
            var pun = new Pun
            {
                PunID = index,
                Title = name,
                Joke = joke
            };
            _service.UpdatePun(pun);
        }

        private static void EnterPun()
        {
            System.Console.WriteLine("---------------");
            System.Console.Write("Name of pun? ");
            var name = System.Console.ReadLine();
            System.Console.Write("Pun? ");
            var joke = System.Console.ReadLine();
            var pun = new Pun
            {
                Title = name,
                Joke = joke
            };
            _service.CreatePun(pun);
        }

        static void ListPuns()
        {
            var puns = _service.GetPuns();
            foreach (var pun in puns)
            {
                System.Console.WriteLine("{0}) {1}", pun.PunID, pun.Title);
            }
            System.Console.WriteLine("---------------");
        }
    }
}
