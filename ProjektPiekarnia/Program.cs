using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using ProjektPiekarnia;

class Program
{

    static void Main()
    {
        // Definiujemy niestandardową kulturę z kropką jako separatorem dziesiętnym (wczytywanie z CSV).
        CultureInfo customCulture = new CultureInfo("pl-PL");
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        customCulture.NumberFormat.NumberGroupSeparator = ",";
        CultureInfo.CurrentCulture = customCulture;

        // Tworzymy obiekt FileManager
        FileManager fileManager = new FileManager();

        // Inicjalizacja obiektu ProductManager, podaję absolutną ścieżkę do pliku.
        // Wczytuje wszystkie produkty z bazy produktów CSV.
        string filePath = "Produkty3.csv";

        ProductsManager productManager = new ProductsManager(filePath);
        productManager.LoadProductsFromCSV();

        string customerType = "";
        while (customerType != "Indywidualny" && customerType != "Firma")
        {
            Menu.DisplayCustomerType();

            string customerTypeInput = Console.ReadLine();

            switch (customerTypeInput)
            {
                case "1":
                    // 1. Klient indywidualny
                    customerType = "Indywidualny";
                    break;
                case "2":
                    // 2. Firma
                    customerType = "Firma";
                    break;
                default:
                    Console.WriteLine("\nNieprawidłowa opcja. Spróbuj ponownie.\n");
                    break;
            }
        }

        // Inicjalizacja obiektu DiscountManager.
        DiscountManager discountManager = new DiscountManager();
        // Tworzymy nowe zamówienie
        Order order = new Order();

        // Menu główne
        while (true)
        {

            Menu.DisplayMainMenu();

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // 1. Dodaj produkt do bazy danych.
                    productManager.AddNewProduct(productManager, fileManager, filePath);
                    break;

                case "2":
                    // 2. Usuń produkt z bazy danych.
                    Console.Write("\nPodaj nazwę produktu do usunięcia: ");
                    string productName = Console.ReadLine();
                    productManager.RemoveProduct(productName, fileManager, filePath);
                    break;

                case "3":
                    // 3. Dodaj produkt/y do zamówienia -> Przejdź do Menu zamówień.
                    while (true)
                    {
                        Menu.DisplayOrderMenu();

                       
                        string choiceInput = Console.ReadLine().ToLower();

                        switch (choiceInput)
                        {
                            case "1":
                                // 1. Dodaj produkt do zamówienia.
                                Console.WriteLine("\nWybierz produkt do zamówienia:");
                                int indexA = 1;
                                foreach (var product in productManager.GetProducts())
                                {
                                    Console.WriteLine($"{indexA}. {product.Nazwa} - {product.Cena:C}");
                                    indexA++;
                                }
                                Console.WriteLine("0. Zakończ dodawanie");

                                Console.Write("\nWybierz numer produktu: ");
                                string productChoiceInput = Console.ReadLine();

                                if (int.TryParse(productChoiceInput, out int productChoice) && productChoice >= 1 && productChoice <= productManager.GetProducts().Count)
                                {
                                    int productIndex = productChoice - 1;
                                    Console.Write($"Podaj ilość dla produktu {productManager.GetProducts()[productIndex].Nazwa}: ");
                                    string quantityInput = Console.ReadLine();
                                    if (int.TryParse(quantityInput, out int quantity) && quantity > 0)
                                    {
                                        order.AddProduct(productManager.GetProducts()[productIndex], quantity);
                                        Console.WriteLine($"Dodano {quantity} szt. produktu {productManager.GetProducts()[productIndex].Nazwa} do zamówienia.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nieprawidłowa ilość. Spróbuj ponownie.");
                                    }
                                }
                                else if (productChoiceInput == "0")
                                {
                                    // Zakończ dodawanie
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                                }
                                break;
                            case "2":
                                // 2. Usuń produkt z zamówienia.
                                Console.Write("Podaj nazwę produktu do usunięcia: ");
                                string productNam = Console.ReadLine();
                                Products productToRemovee = order.OrderItems.Keys.FirstOrDefault(p => p.Nazwa.Equals(productNam, StringComparison.OrdinalIgnoreCase));
                                if (productToRemovee != null)
                                {
                                    order.RemoveProduct(productToRemovee);
                                    Console.WriteLine($"\nProdukt '{productNam}' został usunięty z zamówienia.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nNie znaleziono produktu o nazwie '{productNam}' w zamówieniu.");
                                }
                                break;
                            case "3":
                                // 3. Wyświetl listę wybranych obecnie produktów.
                                Console.WriteLine("\nWybrane obecnie produkty:");
                                foreach (var item in order.OrderItems)
                                {
                                    Console.WriteLine($"{item.Key.Nazwa}: {item.Value} szt.");
                                }
                                break;

                            case "4":
                                // 4. Zapisz zamówienie do pliku, wyczyść produkty z listy.

                                string fileName = $"Zamówienie_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                                fileManager.SaveOrderToFile(order, fileName, customerType, discountManager);
                                order.ClearOrderItems(); // Usuń wszystkie produkty z listy wybranych produktów
                                Console.WriteLine($"Zamówienie zostało zapisane do pliku: {fileName}");
                                break;

                            case "0":
                                // 0. Zakończ zamówienie, wyczyść produkty z listy.
                                break;
                            default:
                                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                                break;
                        }

                        if (choiceInput == "0")
                        {
                            order.ClearOrderItems(); // Usuń wszystkie produkty z listy wybranych produktów.
                            break; // Zakończ zamówienie.
                        }
                    }
                    break;

                case "4":
                    // 4. Wyświetl listę dostępnych produktów.
                    Console.WriteLine("\nLista dostępnych produktów:");
                    int productInd = 1;
                    foreach (var product in productManager.GetProducts())
                    {
                        Console.WriteLine($"{productInd}. {product.Nazwa} - {product.Cena:C}");
                        productInd++;
                    }
                    break;

                case "0":
                    // 0. Zakończ program.
                    Console.WriteLine("Program został zakończony.");
                    return;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }

        }


    }
}


