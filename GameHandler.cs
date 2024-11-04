// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning och JSON-hantering.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Publik klass som hanterar sökning på rim utifrån specifik julklapp.
public class GameHandler
{
    // Privat fältvariabel som lagrar deserialiserad rimdata (nullvärden hanteras med ?-operatorn).
    private RimList? rimList;

    // Konstruktor som hanterar inläsning av data från JSON-filen.
    public GameHandler()
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

    // Metod som visar spelmenyn och hanterar användarens val.
    public void ShowGameMenu()
    {
        // Initierar val-variabel (hanterar null-värde med ?).
        string? userChoice;

        // Skapar en ny array med giltiga användar-val.
        var validChoices = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        // Rensar konsollen innan menyn visas.
        Console.Clear();

        // Skriver ut en "meny" för spelet "Gissa julklappen".
        Console.WriteLine("\nVÄLKOMMEN TILL SPELET 'GISSA JULKLAPPEN'!");
        Console.WriteLine("\nDu ska nu få välja en kategori att gissa julklappar från. Dessa kategorier finns tillgängliga:");
        Console.WriteLine("\n1 - Kläder");
        Console.WriteLine("2 - Kroppsvård");
        Console.WriteLine("3 - Leksaker");
        Console.WriteLine("4 - Upplevelser");
        Console.WriteLine("5 - Verktyg");
        Console.WriteLine("6 - Ätbart");
        Console.WriteLine("7 - Till hemmet");
        Console.WriteLine("8 - Media");
        Console.WriteLine("9 - Hälsa");
        Console.WriteLine("\nAnge ditt val med en siffra mellan 1 och 9. Vill du avbryta spelet, ange 0.\n");

        // Läser in användarens val.
        userChoice = Console.ReadLine();

        // Kontrollerar om valet är 0 och avslutar i så fall spelet.
        if (userChoice == "0")
        {
            // Bekräftar valet och ber användaren trycka på ENTER för att gå tillbaka till huvudmenyn.
            Console.WriteLine("\nDu valde att avbryta. Tryck på ENTER för att gå till huvudmenyn.");
            Console.ReadLine();

            // Returnerar till huvudloopen.
            return;
        }
        // Kontrollerar om valet är mellan 1 och 9 och går i så fall vidare med vald kategori.
        else if (validChoices.Contains(userChoice))
        {
            // Bekräftar valet och ber användaren trycka på ENTER för att läsa spelreglerna.
            Console.WriteLine($"\nDitt val av kategori har sparats. Tryck på ENTER för att se spelreglerna.");
            Console.ReadLine();

            // Anropar funktionen StartGuessingGame() för att starta spelet med den valda kategorin.
            StartGuessingGame(userChoice!);
        }
        // Visar ett felmeddelande vid ogiltigt val.
        else
        {
            // Ber användaren trycka på ENTER för att försöka igen.
            Console.WriteLine("\nEtt felaktigt val har gjorts. Tryck på ENTER och försök igen!");
            Console.ReadLine();

            // Anropar funktionen för spelmeny och startar om den.
            ShowGameMenu();
        }
    }

    // Metod som startar och kör gissningsspelet (hanterar null-värde med ?).
    public void StartGuessingGame(string? userChoice)
    {
        // Skapar en ordbok för att kunna slå upp kategori baserat på användarens sifferval.
        var categories = new Dictionary<string, string>
            {
                // Ordbok med nyckel (användarval) och tillhörande värde (kategorinamn).
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

        // Kontrollerar om användarvalet (nyckeln) finns i ordboken. Om den finns lagras kategorinamnet i selectedCategory. 
        if (userChoice != null && categories.TryGetValue(userChoice, out var selectedCategory))
        {
            // Rensar konsollen på tidigare info.
            Console.Clear();

            // Informerar användaren om den valda kategorin och förklarar hur spelet går till.
            Console.WriteLine("\nSPELREGLER FÖR 'GISSA JULKLAPPEN'.");
            Console.WriteLine($"\nDu valde '{selectedCategory}' och kommer nu få gissa julklappen för 10 rim ur den kategorin.");
            Console.WriteLine("Du kan endast gissa en gång per rim och varje rätt svar ger ett poäng.");
            Console.WriteLine("Högsta möjliga resultat är 10/10. Lycka till!");

            // Ber användaren trycka på ENTER för att starta spelet.
            Console.WriteLine("\nTryck på ENTER för att starta spelet. Vill du avbryta spelet, ange 0.\n");

            // Kontrollerar om användaren skrivit in 0 som input.
            string? userCancel = Console.ReadLine();

            // Kontrollerar om användaren valt att avbryta spelet.
            if (userCancel?.ToLower() == "0")
            {
                Console.WriteLine("\nDu har valt att avbryta spelet. Tryck på ENTER för att gå till huvudmenyn.");
                Console.ReadLine();

                // Returnerar användaren till huvudmenyn.
                return;
            }

            // Rensar konsollen så att rimmen visas för sig.
            Console.Clear();

            // Filtrerar rimmen utifrån kategori och skapar en lista för vald kategori. 
            var rhymesInSelectedCategory = rimList!.Rimsamling.Where(r => r.Kategori == selectedCategory).ToList();

            // Skapar en poängräknare där default-värdet är 0.
            int score = 0;

            // Loop som hanterar användarens gissningar. Visar max 10 rim från vald kategori.
            foreach (var item in rhymesInSelectedCategory.Take(10))
            {
                // Rimmet skrivs ut och användaren får gissa.
                Console.WriteLine($"\nRim: {item.Rim}");
                Console.Write("\nGissa julklappen: ");

                // Användarens gissning läses in (hanterar null-värde med ?).
                string? guess = Console.ReadLine();

                // Kontrollerar om gissningen matchar julklappens namn (hanterar null-värde med ?). 
                if (guess?.ToLower() == item.Julklapp.ToLower())
                {
                    // Visar ett bekräftande meddelande.
                    Console.WriteLine("\nDu svarade rätt!\n");
                    // Vid rätt svar ökas poängräknaren med 1.
                    score++;
                }
                // Om gissningen inte matchar...
                else
                {
                    // Meddelar att fel svar angivits och visar det rätta svaret.
                    Console.WriteLine($"\nDu svarade fel. Rätt svar var: {item.Julklapp}\n");
                }
            }

            // Rensar konsollen på rim för att visa resultat och nya val efter spelslut.
            Console.Clear();

            // Visar spelresultatet för användaren efter avslutat spel.
            Console.WriteLine("SPELET ÄR SLUT!");
            Console.WriteLine($"\nDitt resultat blev {score}/10.\n");

            // Initerar en boolean med default-värde false för kontroll av val.
            bool validSelection = false;

            // Hanterar användarens val om ny spelomgång eller avsluta. Loopar tills ett giltigt val görs.
            while (!validSelection)
            {
                // Frågar användaren om hen vill spela en gång till.
                Console.WriteLine("\nVill du spela igen? Ange 1 för JA eller ange 0 för NEJ.\n");

                // Läser in användarens val (hanterar null-värde med ?).
                string? selectedOption = Console.ReadLine()?.ToLower();

                // Kontrollerar användarens svar.
                if (selectedOption == "1")
                {
                    // Bekräftar val och ber användaren trycka på ENTER för att starta om spelet.
                    Console.WriteLine("\nVad kul att du vill spela igen! Tryck på ENTER för att starta om spelet.");
                    Console.ReadLine();

                    // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                    validSelection = true;

                    // Anropar spelmeny-funktionen.
                    ShowGameMenu();
                }
                else if (selectedOption == "0")
                {
                    // Bekräftar val och ber användaren trycka på ENTER för att avsluta.
                    Console.WriteLine("\nDu valde att avsluta. Tryck på ENTER för att gå till huvudmenyn.");
                    Console.ReadLine();

                    // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                    validSelection = true;

                    // Returnerar till huvudloopen.
                    return;
                }
                else
                {
                    // Ber användaren trycka på ENTER för att försöka igen.
                    Console.WriteLine("\nEtt felaktigt val har gjorts. Tryck på ENTER och försök igen!");
                    Console.ReadLine();

                    // Rensar konsollen.
                    Console.Clear();
                }
            }
        }
    }
}