// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning, JSON-hantering och datahantering. 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class Program
{
    // Initierar en statisk boolean med true som default.
    static bool appIsRunning = true;

    // Main.
    static void Main(string[] args)
    {
        // Ser till att all output och input i konsollen hanterar svenska tecken korrekt.
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.InputEncoding = System.Text.Encoding.Unicode;

        // Skapar en instans av RhymeHandler.
        RhymeHandler rhymeHandler = new RhymeHandler();

        // Skapar en instans av GiftHandler.
        GiftHandler giftHandler = new GiftHandler();

        // Skapar en instans av GameHandler.
        GameHandler gameHandler = new GameHandler();

        // Huvudloop som körs tills användaren väljer att avsluta applikationen.
        while (appIsRunning)
        {
            // Anropar huvudmenyn och skickar in instansen.
            ShowStartMenu(rhymeHandler, giftHandler, gameHandler); 
        }
    }

    // Metod som visar och hanterar huvudmenyn. 
    static void ShowStartMenu(RhymeHandler rhymeHandler, GiftHandler giftHandler, GameHandler gameHandler)
    {
        // Initierar input-variabel (hanterar null-värde med ?).
        string? userInput;

        // Rensar konsollen innan varje "meny" visas.
        Console.Clear();

        // Skriver ut en "meny" för julklappsrims-applikationen.
        Console.WriteLine("\nVÄLKOMMEN TILL JENNY'S JULKLAPPSRIM!");
        // Anropar nedräknare för julafton.
        ShowDaysUntilChristmas();
        // Fortsätter utskrift av meny...
        Console.WriteLine("\nVad önskar du göra?");
        Console.WriteLine("\n1 - Söka rim för en specifik julklapp.");
        Console.WriteLine("2 - Söka julklappar utifrån kategori.");
        Console.WriteLine("3 - Spela 'Gissa julklappen'.");
        Console.WriteLine("\n0 - Avsluta applikationen.\n");

        // Läser in användarens input.
        userInput = Console.ReadLine();

        // Olika cases körs beroende på användarens input.
        switch (userInput)
        {
            // Söker rim för en julklapp.
            case "1":
                // Bekräftar valet och ber användaren trycka på ENTER för att starta sökningen.
                Console.WriteLine("\nDu valde att söka rim för en specifik julklapp. Tryck på ENTER för att gå vidare.");
                Console.ReadLine();

                // Anropar metoden som hanterar sökning efter rim.
                rhymeHandler.SearchForRhymes();
                break;

            // Söker julklappar utifrån kategori.
            case "2":
                // Bekräftar valet och ber användaren trycka på ENTER för att starta sökningen.
                Console.WriteLine("\nDu valde att söka julklappar utifrån kategori. Tryck på ENTER för att gå vidare.");
                Console.ReadLine();

                // Anropar funktionen som hanterar sökning efter rim.
                giftHandler.SearchForGifts();
                break;

            // Startar spelet "Gissa julklappen".
            case "3":
                // Bekräftar valet och ber användaren trycka på ENTER för att visa spelmeny för gissnings-spelet.");
                Console.WriteLine("\nDu valde att spela 'Gissa julklappen'. Tryck på ENTER för att gå vidare till spelet.");
                Console.ReadLine();

                // Anropar funktionen som hanterar spelmenyn.
                gameHandler.ShowGameMenu();
                break;

            // Avslutar applikationen.
            case "0":
                // Bekräftar valet och ber användaren trycka på ENTER för att stänga ner appen.
                Console.WriteLine("\nDu valde att avsluta och applikationen stängs ner. Lycka till med dina julklappsrim!");

                // Boolean sätts till false och huvudloopen avslutas.
                appIsRunning = false;
                break;

            // Visar ett felmeddelande vid ogiltigt val.
            default:
                // Ber användaren trycka på ENTER för att gå vidare med valet som gjorts.
                Console.WriteLine("\nEtt felaktigt val har gjorts. Tryck på ENTER för att försöka igen!");
                Console.ReadLine();
                break;
        }
    }

    // Funktion som visar en nedräkning till julafton i hela dagar.
    static void ShowDaysUntilChristmas()
    {
        // Skapar en variabel som innehåller dagens datum och tid.
        DateTime today = DateTime.Now;

        // Skapar en variabel för julafton utifrån årets datum.
        DateTime christmas = new DateTime(today.Year, 12, 24);

        // Kontrollerar om datumet är efter detta årets julafton. Då sätts julafton till att vara nästa års julafton.
        if (today > christmas)
        {
            // Lägger till ett år till julaftonsdatumet
            christmas = christmas.AddYears(1);
        }

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
            Console.WriteLine($"\nIdag är det bara {daysUntilChristmas.Days} dagar kvar till julafton!\n");
        }
    }
}