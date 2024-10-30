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
        // Ser till att all output och input i konsollen hanterar svenska tecken korrekt.
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        Console.InputEncoding = System.Text.Encoding.Unicode;

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
            //Console.WriteLine("4 - Visa alla rim lagrade i JSON-filen.");
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
                    ShowGameMenu();
                    break;

                /* Visa alla rim lagrade i JSON-filen.
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
                    break;*/

                // Avslutar applikationen.
                case "0":
                    Console.WriteLine("\nApplikationen stängs nu ner. Lycka till med dina julklappsrim!");
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
            Console.WriteLine($"\nIdag är det bara {daysUntilChristmas.Days} dagar kvar till julafton!");
        }
    }

    // Funktion som visar spelmenyn och hanterar användarens val.
    static void ShowGameMenu()
    {
        // Initierar val-variabel (hanterar null-värde med ?).
        string? userChoice;

        // Skapar en ny array med giltiga användar-val.
        var validChoices = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        // Loopen körs så länge villkoret, true, uppfylls.
        while (true)
        {
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

            userChoice = Console.ReadLine();

            // Kontrollerar om valet är 0 och avslutar i så fall spelet.
            if (userChoice == "0")
            {
                Console.WriteLine("\nDu skickas nu tillbaka till huvudmenyn.");
                // Hoppar ur loopen.
                break;
            }
            // Kontrollerar om valet är mellan 1 och 9 och går i så fall vidare med vald kategori.
            else if (validChoices.Contains(userChoice))
            {
                // Anropar funktionen StartGuessingGame() för att starta spelet med den valda kategorin.
                StartGuessingGame(userChoice!);
            }
            // Visar ett felmeddelande vid ogiltigt val.
            else
            {
                Console.WriteLine("\nEtt felaktigt val har gjorts. Försök igen!");
            }

            // Ber användaren trycka på ENTER för att gå vidare med valet som gjorts.
            Console.WriteLine("\nTryck på ENTER för att fortsätta.");
            Console.ReadLine();
        }
    }

    // Funktion som startar och kör gissningsspelet.
    static void StartGuessingGame(string? userChoice)
    {
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
            // Skapar en ordbok för att kunna slå upp kategori baserat på användarens sifferval.
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

            // Kontrollerar om användarvalet (nyckeln) finns i ordboken. Om den finns lagras kategorinamnet i selectedCategory. 
            if (categories.TryGetValue(userChoice, out var selectedCategory))
            {
                // Informerar användaren om den valda kategorin och förklarar hur spelet går till.
                Console.WriteLine($"\nDu valde '{selectedCategory}' och kommer nu få gissa julklappen för 10 rim ur den kategorin.");
                Console.WriteLine($"För varje rätt svar kommer du få ett poäng. Högsta möjliga resultat är 10/10. Lycka till!");

                // Ber användaren trycka på ENTER för att starta spelet.
                Console.WriteLine("\nTryck på ENTER för att fortsätta.");
                Console.ReadLine();

                // Filtrerar rimmen utifrån kategori och skapar en lista för vald kategori. 
                var rhymesInSelectedCategory = rimList.Rimsamling.Where(r => r.Kategori == selectedCategory).ToList();

                // Skapar en poängräknare där default-värdet är 0.
                int score = 0;

                // Loop som hanterar användarens gissningar. Visar max 10 rim från vald kategori.
                foreach (var item in rhymesInSelectedCategory.Take(10))
                {
                    // Rimmet skrivs ut och användaren får gissa.
                    Console.WriteLine($"\nRim: {item.Rim}");
                    Console.Write("\nGissa julklappen: ");

                    // Användarens gissning läses in.
                    string? guess = Console.ReadLine();

                    // Kontrollerar om gissningen matchar julklappens namn. 
                    if (guess.ToLower() == item.Julklapp.ToLower())
                    {
                        Console.WriteLine("\nDu svarade rätt!");
                        // Vid rätt svar ökas poängräknaren med 1.
                        score++;
                    }
                    // Om gissningen inte matchar...
                    else
                    {
                        Console.WriteLine($"\nDu svarade fel. Rätt svar var: {item.Julklapp}");
                    }
                }

                // Visar spelresultatet för användaren efter avslutat spel.
                Console.WriteLine($"\nNu är spelet slut. Ditt resultat blev {score}/10!");
            }
        }
    }
}