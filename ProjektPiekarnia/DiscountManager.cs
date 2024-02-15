using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektPiekarnia
{
    public class DiscountManager
    {
        private Dictionary<string, decimal> discounts;

        public DiscountManager()
        {
            // Inicjalizacja rabatów
            discounts = new Dictionary<string, decimal>();
            discounts.Add("indywidualny", 0.0m); // Rabat 0% dla klienta indywidualnego
            discounts.Add("firma", 0.15m); // Rabat 15% dla firmy
        }

        // Metoda zwracająca rabat dla danego rodzaju klienta
        public decimal GetDiscount(string customerType)
        {
            if (discounts.ContainsKey(customerType))
            {
                return discounts[customerType];
            }
            else
            {
                // Domyślny rabat, jeśli rodzaj klienta nie został znaleziony
                return 0m;
            }
        }
    }
}