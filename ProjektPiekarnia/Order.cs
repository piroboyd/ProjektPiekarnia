using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPiekarnia
{
    public class Order
    {
        private Dictionary<Product, int> orderItems;

        public IReadOnlyDictionary<Product, int> OrderItems => orderItems;

        // Konstruktor
        public Order()
        {
            orderItems = new Dictionary<Product, int>();
        }

        // Metoda dodająca produkt do zamówienia
        public void AddProduct(Product product, int quantity)
        {
            if (orderItems.ContainsKey(product))
            {
                orderItems[product] += quantity;
            }
            else
            {
                orderItems[product] = quantity;
            }
        }

        // Metoda usuwająca produkt z zamówienia
        public void RemoveProduct(Product product)
        {
            if (orderItems.ContainsKey(product))
            {
                orderItems.Remove(product);
            }
        }

        // Metoda obliczająca łączną cenę zamówienia uwzględniającą rabat
        public decimal CalculateTotal(string customerType, DiscountManager discountManager)
        {
            decimal total = 0;
            foreach (var item in orderItems)
            {
                total += item.Key.Cena * item.Value;
            }

            // Pobierz rabat dla danego rodzaju klienta
            decimal discount = discountManager.GetDiscount(customerType);

            // Oblicz rabat i zwróć łączną cenę zamówienia
            return Math.Round(total * (1 - discount), 2);
        }

        // Metoda używana do usuwania produktów po zapisaniu ich do pliku lub wyjscia z menu wyboru
        // produktów do danego zamówienia
        public void ClearOrderItems()
        {
            orderItems.Clear();
        }
    }
}