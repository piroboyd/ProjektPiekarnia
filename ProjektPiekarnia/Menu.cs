using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPiekarnia
{

    // Statyczna klasa menu żeby nie zaśmiecać Program.cs
    public class Menu
    {
        // Menu główne
        public static void DisplayMainMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Dodaj produkt do bazy danych");
            Console.WriteLine("2. Usuń produkt z bazy danych");
            Console.WriteLine("3. Dodaj produkt/y do zamówienia");
            Console.WriteLine("4. Wyświetl listę dostępnych produktów");
            Console.WriteLine("0. Zakończ program");
            Console.Write("\nWybierz opcję: ");
        }
        // Menu zamówień
        public static void DisplayOrderMenu()
        {
            Console.WriteLine("\nWybierz opcję:");
            Console.WriteLine("1. Dodaj produkt do zamówienia");
            Console.WriteLine("2. Usuń produkt z zamówienia");
            Console.WriteLine("3. Wyświetl listę wybranych obecnie produktów");
            Console.WriteLine("4. Zapisz zamówienie do pliku");
            Console.WriteLine("0. Zakończ zamówienie");
            Console.Write("\nWybierz opcję: ");
        }
        // Menu wyboru rodzaju klienta
        public static void DisplayCustomerType()
        {
            Console.WriteLine("Wybierz rodzaj klienta:");
            Console.WriteLine("1. Klient indywidualny");
            Console.WriteLine("2. Firma");
            Console.Write("\nWybierz opcję: ");
        }
    }
}