// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning och JSON-hantering. 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        // Initierar input-variabel (hanterar null-värde med ?).
        string? userInput;

        // Loopen fortsätter tills användaren väljer att avsluta.
        do
        {
            // Rensar konsollen innan varje "meny" visas.
            Console.Clear();

            // Skriver ut en "meny" för julklappsrims-applikationen.
            Console.WriteLine("\nVÄLKOMMEN TILL JENNY'S JULKLAPPSRIM!");
            // Anropar nedräknare för julafton.
            ShowDaysUntilChristmas();
            // Fortsätter utskrift av meny...
            Console.WriteLine("\nVad önskar du göra?");
            Console.WriteLine("\n1 - Söka rim för en specfik julklapp.");
            Console.WriteLine("2 - Söka julklappar utifrån kategori.");
            Console.WriteLine("3 - Spela 'Gissa julklappen'.");
            Console.WriteLine("4 - Visa alla rim lagrade i JSON-filen.");
            Console.WriteLine("\n0 - Avsluta applikationen.\n");

            // Läser in användarens input.
            userInput = Console.ReadLine();

            // Olika cases körs beroende på användarens input.
            switch (userInput)
            {
                // Söker rim för en julklapp.
                case "1":
                    Console.WriteLine("\nFungerar inte att söka rim än...");
                    break;

                // Söker julklappar utifrån kategori.
                case "2":
                    Console.WriteLine("\nFungerar inte att söka julklappar än...");
                    break;

                // Startar spelet "Gissa julklappen".
                case "3":
                    Console.WriteLine("\nSpelet fungerar inte än...");
                    break;

                // Visa alla rim lagrade i JSON-filen.
                case "4":
                    // Läser in JSON-filen som en sträng.
                    string jsonFile = File.ReadAllText("rimsamling.json");

                    // Deserialiserar JSON-strängen till ett RimList-objekt (hanterar nullvärden med ett ?)
                    RimList? rimList = JsonSerializer.Deserialize<RimList>(jsonFile);

                    // Kontrollerar om värdet är null och hanterar det med ett meddelande.
                    if (rimList == null || rimList.Rimsamling == null)
                    {
                        Console.WriteLine("Tyvärr, inga rim hittades.");
                    }
                    else
                    {
                        // Loopar genom listan av rim och skriver ut varje post.
                        foreach (var item in rimList.Rimsamling)
                        {
                            Console.WriteLine($"Julklapp: {item.Julklapp}");
                            Console.WriteLine($"Rim: {item.Rim}");
                            Console.WriteLine($"Kategori: {item.Kategori}");

                            // Lägger en tom rad mellan varje post.
                            Console.WriteLine();
                        }
                    }
                    break;

                // Avslutar applikationen.
                case "0":
                    Console.WriteLine("\nAppen stängs nu ner. Lycka till med dina julklappsrim!");
                    break;

                // Visar ett felmeddelande vid ogiltigt val.
                default:
                    Console.WriteLine("\nEtt felaktigt val har gjorts. Försök igen!");
                    break;
            }

            // Ber användaren trycka på ENTER för att gå vidare med valet som gjorts.
            Console.WriteLine("\nTryck på ENTER för att fortsätta.");
            Console.ReadLine();

            // Loopen avslutas när användaren väljer att avsluta med 0.
        } while (userInput != "0");
    }

    // Funktion som visar en nedräkning till julafton i hela dagar.
    static void ShowDaysUntilChristmas()
    {
        // Skapar en variabel som innehåller dagens datum och tid.
        DateTime today = DateTime.Now;
        
        // Skapar en variabel för julafton utifrån årets datum.
        DateTime christmas = new DateTime(today.Year, 12, 24);

        // Beräknar skillnaden mellan dagens datum och julafton.
        TimeSpan daysUntilChristmas = christmas.Subtract(today);
        
        // Villkor för en dynamisk utskrift utifrån antalet dagar kvar till julafton.
        // Om det är julafton idag...
        if (daysUntilChristmas.Days == 0)
        {
            Console.WriteLine("\nIdag är det julafton! God jul!");
        }
        // Om det är julafton imorrn...
        else if (daysUntilChristmas.Days == 1)
        {
            Console.WriteLine("\nImorrn är det äntligen julafton!");
        }
        // Om det är flera dagar till julafton...
        else
        {
            Console.WriteLine($"\nIdag är det bara {daysUntilChristmas.Days} dagar kvar till julafton!");
        }
    }
}