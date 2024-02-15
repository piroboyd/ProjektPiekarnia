using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ProductsManager
{
    private List<Products> productsList;
    private string filePath; // Dodajemy pole przechowujące ścieżkę do pliku.
    public ProductsManager(string filePath)
    {
        this.filePath = filePath;
        productsList = new List<Products>();
    }

    // Metoda wczytująca produkty z pliku CSV
    public void LoadProductsFromCSV()
    {
        string[] lines = File.ReadAllLines(filePath);

        // Pomijam nagłówek, zaczytuje wartości z kolejnych linii pliku CSV.
        foreach (var line in lines.Skip(1))
        {
            string[] values = line.Split(';');

            // Sprawdzamy, czy tablica values ma odpowiednią liczbę elementów,
            // czyli w skrócie czy plik CSV jest dobrze uzupełniony w każdej linii.
            if (values.Length == 14)
            {
                // Tworzymy obiekt klasy Products i dodajemy go do listy produktów productlist.
                Products product = new Products(
                    values[0], // Nazwa
                    int.Parse(values[1]), // Waga
                    decimal.Parse(values[2]), // Cena
                    int.Parse(values[3]), // Mąka Pszenna
                    int.Parse(values[4]), // Mąka Żytnia
                    int.Parse(values[5]), // Mleko
                    int.Parse(values[6]), // Woda
                    int.Parse(values[7]), // Drożdże
                    decimal.Parse(values[8]), // Jajka
                    int.Parse(values[9]), // Cukier
                    int.Parse(values[10]), // Sól
                    int.Parse(values[11]), // Masło
                    int.Parse(values[12]), // Słonecznik Prażony
                    int.Parse(values[13]) // Nadzienie
                );

                productsList.Add(product);
            }
            else
            {
                Console.WriteLine($"Błąd: Nieprawidłowa liczba pól w wierszu: {line}");
            }

        }
    }

    // Metoda usuwająca produkt z listy produktów i zapisująca zmiany do pliku CSV.
    public void RemoveProduct(string productName, FileManager fileManager, string filePath)
    {
        // Znajdź produkt o podanej nazwie.
        Products productToRemove = productsList.FirstOrDefault(p => p.
        Nazwa.Equals(productName, StringComparison.OrdinalIgnoreCase));

        if (productToRemove != null)
        {
            // Usuń produkt z listy.
            productsList.Remove(productToRemove);
            Console.WriteLine($"Produkt '{productName}' został usunięty.");

            // Zapisz zmiany do pliku CSV.
            fileManager.SaveProductsToCSV(productsList, filePath);
        }
        else
        {
            Console.WriteLine($"Nie znaleziono produktu o nazwie '{productName}'.");
        }
    }

    public void AddNewProduct(ProductsManager productManager, FileManager fileManager, string filePath)
    {
        Console.WriteLine("\nDodawanie nowego produktu:");

        // Pobieramy od użytkownika dane produktu
        Console.Write("Nazwa: ");
        string nazwa = Console.ReadLine();
        Console.Write("Waga: ");
        int waga = int.Parse(Console.ReadLine());
        Console.Write("Cena: ");
        decimal cena = decimal.Parse(Console.ReadLine());
        Console.Write("Mąka pszenna: ");
        int mąkaPszenna = int.Parse(Console.ReadLine());
        Console.Write("Mąka żytnia: ");
        int mąkaŻytnia = int.Parse(Console.ReadLine());
        Console.Write("Mleko: ");
        int mleko = int.Parse(Console.ReadLine());
        Console.Write("Woda: ");
        int woda = int.Parse(Console.ReadLine());
        Console.Write("Drożdże: ");
        int drożdże = int.Parse(Console.ReadLine());
        Console.Write("Jajka: ");
        decimal jajka = decimal.Parse(Console.ReadLine());
        Console.Write("Cukier: ");
        int cukier = int.Parse(Console.ReadLine());
        Console.Write("Sól: ");
        int sól = int.Parse(Console.ReadLine());
        Console.Write("Masło: ");
        int masło = int.Parse(Console.ReadLine());
        Console.Write("Słonecznik prażony: ");
        int słonecznikPrażony = int.Parse(Console.ReadLine());
        Console.Write("Nadzienie: ");
        int nadzienie = int.Parse(Console.ReadLine());

        // Tworzymy nowy obiekt produktu i dodajemy do listy
        Products product = new Products(nazwa, waga, cena, mąkaPszenna,
            mąkaŻytnia, mleko, woda, drożdże, jajka, cukier, sól, masło, słonecznikPrażony, nadzienie);

        productsList.Add(product);

        fileManager.SaveProductsToCSV(productsList, filePath);

        Console.WriteLine($"Produkt '{product.Nazwa}' został dodany.");

    }

    // Metoda zwracająca listę produktów
    public List<Products> GetProducts()
    {
        return productsList;
    }
}


