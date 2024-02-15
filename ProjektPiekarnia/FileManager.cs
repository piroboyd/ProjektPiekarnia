using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ProjektPiekarnia;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class FileManager
{

    // Ta metoda służy do zapisywania zamówienia do pliku tekstowego.
    // Przyjmuje argumenty: obiekt Order reprezentujący zamówienie, ścieżkę do pliku, typ klienta oraz menedżera rabatów.
    // Metoda przechodzi przez każdy element zamówienia, zapisując nazwę produktu i jego ilość.
    // Następnie oblicza ilość składników potrzebnych do zamówienia i zapisuje je wraz z ich sumaryczną ilością.
    // Na koniec oblicza łączną cenę zamówienia, uwzględniając rabat, i zapisuje ją w pliku.
    public void SaveOrderToFile(Order order, string filePath, string customerType, DiscountManager discountManager)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Zamówienie dla klienta typu: {customerType}");
            writer.WriteLine($"Naliczono rabat w wysokości: {discountManager.GetDiscount(customerType):P}");
            writer.WriteLine("\nZamówienie:\n");

            Dictionary<string, decimal> allIngredients = new Dictionary<string, decimal>();

            foreach (var item in order.OrderItems)
            {
                writer.WriteLine($"{item.Key.Nazwa}: {item.Value} szt.");

                foreach (var property in typeof(Products).GetProperties())
                {
                    if (property.Name != "Nazwa" && property.Name != "Waga" && property.Name != "Cena")
                    {
                        string ingredientName = property.Name;
                        decimal ingredientValue = Convert.ToDecimal(property.GetValue(item.Key));

                        if (allIngredients.ContainsKey(ingredientName))
                        {
                            allIngredients[ingredientName] += ingredientValue * item.Value;
                        }
                        else
                        {
                            allIngredients[ingredientName] = ingredientValue * item.Value;
                        }
                    }
                }
            }

            writer.WriteLine("\nSkładniki potrzebne do skompletowania zamówienia:");
            foreach (var ingredient in allIngredients)
            {
                writer.WriteLine($"{ingredient.Key}: {ingredient.Value}");
            }

            writer.WriteLine($"Łączna cena zamówienia: {order.CalculateTotal(customerType, discountManager)} zł");
        }
    }

    public void SaveProductsToCSV(List<Products> productsList, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Nazwa;Waga;Cena;Maka pszenna;Maka Zytnia;Mleko;Woda;Drozdze;Jajka;Cukier;Sol;Maslo;Slonecznik prazony;Nadzienie");

            foreach (var product in productsList)
            {
                writer.WriteLine($"{product.Nazwa};{product.Waga};{product.Cena};{product.MąkaPszenna};{product.MąkaŻytnia};{product.Mleko};{product.Woda};{product.Drożdże};{product.Jajka};{product.Cukier};{product.Sól};{product.Masło};{product.SłonecznikPrażony};{product.Nadzienie}");
            }
        }
    }
}
