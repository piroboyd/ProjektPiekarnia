// Klasa reprezentująca produkty
public class Product
{
    public string Nazwa { get; }
    public int Waga { get; }
    public decimal Cena { get; }
    public int MąkaPszenna { get; }
    public int MąkaŻytnia { get; }
    public int Mleko { get; }
    public int Woda { get; }
    public int Drożdże { get; }
    public decimal Jajka { get; }
    public int Cukier { get; }
    public int Sól { get; }
    public int Masło { get; }
    public int SłonecznikPrażony { get; }
    public int Nadzienie { get; }

    public Product(string nazwa, int waga, decimal cena, int mąkaPszenna, int mąkaŻytnia, int mleko, int woda, int drożdże, decimal jajka, int cukier, int sól, int masło, int słonecznikPrażony, int nadzienie)
    {
        Nazwa = nazwa;
        Waga = waga;
        Cena = cena;
        MąkaPszenna = mąkaPszenna;
        MąkaŻytnia = mąkaŻytnia;
        Mleko = mleko;
        Woda = woda;
        Drożdże = drożdże;
        Jajka = jajka;
        Cukier = cukier;
        Sól = sól;
        Masło = masło;
        SłonecznikPrażony = słonecznikPrażony;
        Nadzienie = nadzienie;
    }
}