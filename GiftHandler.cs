// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning och JSON-hantering.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Publik klass som hanterar sökning på rim utifrån kategori.
public class GiftHandler
{
    // Privat fältvariabel som lagrar deserialiserad rimdata (nullvärden hanteras med ?-operatorn).
    private RimList? rimList;

    // Konstruktor som hanterar inläsning av data från JSON-filen.
    public GiftHandler()
    {
        // Skapa en instans av FileHandler med sökvägen till JSON-filen.
        FileHandler fileHandler = new FileHandler("rimsamling.json");

        // Anropa metoden för att läsa in rimlistan.
        rimList = fileHandler.LoadRimList();

        // Om rimlistan är null, hantera detta lämpligt.
        if (rimList == null || rimList.Rimsamling == null)
        {
            Console.WriteLine("Tyvärr, inga rim hittades.");
        }
    }

    // Metod som hanterar sökning påjulklapp utifrån kategori.
    public void SearchForGifts()
    {
        // Rensar konsollen på tidigare meny.
        Console.Clear();

        // Skapar en ordbok för att översätta användarval till faktiska kategorinamn.
        var categories = new Dictionary<string, string>
        {
            { "1", "Kläder" },
            { "2", "Kroppsvård" },
            { "3", "Leksaker" },
            { "4", "Upplevelser" },
            { "5", "Verktyg" },
            { "6", "Ätbart" },
            { "7", "Till hemmet" },
            { "8", "Media" },
            { "9", "Hälsa" }
        };

        // Initierar en boolean som har true som default-värde. 
        bool continueSearch = true;

        // Loop som kör tills dess att användaren väljer att avsluta sökningen.
        while (continueSearch)
        {
            // Rensar konsollen på tidigare meny.
            Console.Clear();

            // Visar "menyn" för sök utifrån kategori.
            Console.WriteLine("\nVÄLKOMMEN ATT SÖKA EFTER JULKLAPPAR!");
            Console.WriteLine("\nHär kan du ange en kategori och se vilka julklappar som finns lagrade i den.");
            
            // Ber användaren skriva in vilken julklapp det önskas rim för.
            Console.WriteLine("\nDessa kategorier finns tillgängliga:\n");

            // Loopar genom kategorierna och visar dem med nyckel och värde.
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Key} - {category.Value}");
            }

            // Ber användaren göra ett val och läser in det (hanterar nullvärden med ?).
            Console.WriteLine("\nAnge ditt val med en siffra mellan 1 och 9. Vill du avbryta spelet, ange 0.\n");
            string? userChoice = Console.ReadLine();

            // Avslutar sökningen om användaren väljer 0.
            if (userChoice == "0")
            {
                // Bekräftar val och ber användaren trycka på ENTER för att gå tillbaka till huvudmenyn.
                Console.WriteLine("\nDu valde att avbryta. Tryck på ENTER för att gå till huvudmenyn.");
                Console.ReadLine();

                // Boolean sätts till false.
                continueSearch = false;

                // Hoppar ur loopen.
                break;
            }
            // Hittar värdet för angiven kategori-nyckel (hanterar nullvärden med !).
            else if (categories.TryGetValue(userChoice!, out var selectedCategory))
            {
                // Konsollen rensas på tidigare information.
                Console.Clear();

                // Hittar alla julklappar i den valda kategorin och tar bort dubbletter av julklappar.
                var giftsInSelectedCategory = rimList!.Rimsamling
                    .Where(item => item.Kategori == selectedCategory)
                    // Grupperar efter julklappsnamn för att ta bort dubbletter.
                    .GroupBy(item => item.Julklapp)
                    .Select(group => group.First())
                    // Konverterar till lista.
                    .ToList();

                // Kontrollerar om julklappar för angiven kategori finns.
                if (giftsInSelectedCategory.Count > 0)
                {
                    Console.WriteLine($"\nVarsågod! Dessa julklappar finns lagrade i kategorin '{selectedCategory}':\n");

                    // Loopar igenom objekten och visar julklappar i matchande kategori.
                    foreach (var item in giftsInSelectedCategory)
                    {
                        Console.WriteLine($"- {item.Julklapp}");

                        // Lägger en tom rad mellan varje julklapp.
                        Console.WriteLine();
                    }
                }
                // Om inga julklappar hittas...
                else
                {
                    // Meddelar användaren om att julklappar saknas.
                    Console.WriteLine($"\nTyvärr, inga julklappar hittades i kategorin '{selectedCategory}'.");

                    // Rensar konsollen på tidigare information.
                    Console.Clear();
                }
            }
            // Om ogiltigt val anges...
            else
            {
                // Hanterar felaktigt val med vägledande meddelande.
                Console.WriteLine("\nEtt felaktigt val har gjorts. Tryck på ENTER och försök igen!");
                Console.ReadLine();

                // Rensar konsollen.
                Console.Clear();

                // Går tillbaka till sökloopen så användaren kan göra ett nytt val.
                continue;
            }

            // Initerar en boolean med default-värde false för kontroll av val.
            bool validSelection = false;

            // Hanterar användarens val om ny sökning eller avsluta. Loopar tills ett giltigt val görs.
            while (!validSelection)
            {
                // Frågar användaren om hen vill göra en ny sökning.
                Console.WriteLine("\nVill du göra en ny sökning på julklappar? Ange 1 för JA eller ange 0 för NEJ.\n");

                // Läser in användarens val (hanterar null-värde med ?).
                string? selectedOption = Console.ReadLine();

                // Kontrollerar användarens svar.
                if (selectedOption! == "1")
                {
                    // Bekräftar val och ber användaren trycka på ENTER för att göra en ny sökning.
                    Console.WriteLine("\nVad trevligt att du vill utforska fler kategorier! Tryck på ENTER för att göra en ny sökning.");
                    Console.ReadLine();

                    // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                    validSelection = true;
                }
                else if (selectedOption! == "0")
                {
                    // Bekräftar val och ber användaren trycka på ENTER för att avsluta.
                    Console.WriteLine("\nDu valde att avsluta. Tryck på ENTER för att gå till huvudmenyn.");
                    Console.ReadLine();

                    // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                    validSelection = true;

                    // Boolean för yttre loop sätts till false och sökning avslutas.
                    continueSearch = false;
                }
                else
                {
                    // Hanterar felaktigt val med vägledande meddelande.
                    Console.WriteLine("\nEtt felaktigt val har gjorts. Tryck på ENTER och försök igen!");
                    Console.ReadLine();

                    // Rensar konsollen.
                    Console.Clear();
                }
            }
        }
    }
}