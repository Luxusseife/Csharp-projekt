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

        // Skapar en instans av RhymeSearchHandler.
        RhymeHandler rhymeHandler = new RhymeHandler();

        // Huvudloop som körs tills användaren väljer att avsluta applikationen.
        while (appIsRunning)
        {
            // Anropar huvudmenyn och skickar in instansen.
            ShowStartMenu(rhymeHandler); 
        }
    }

    // Funktion som visar och hanterar huvudmenyn. Instanser 
    static void ShowStartMenu(RhymeHandler rhymeHandler)
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
                SearchForGifts();
                break;

            // Startar spelet "Gissa julklappen".
            case "3":
                // Bekräftar valet och ber användaren trycka på ENTER för att visa spelmeny för gissnings-spelet.");
                Console.WriteLine("\nDu valde att spela 'Gissa julklappen'. Tryck på ENTER för att gå vidare till spelet.");
                Console.ReadLine();

                // Anropar funktionen som hanterar spelmenyn.
                ShowGameMenu();
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

    /* Funktion som hanterar sökning på rim utifrån angiven julklapp.
    static void SearchForRhymes()
    {
        // Rensar konsollen på tidigare meny.
        Console.Clear();

        // Läser in JSON-filen som en sträng.
        string jsonFile = File.ReadAllText("rimsamling.json");

        // Deserialiserar JSON-strängen till ett RimList-objekt (hanterar nullvärden med ?)
        RimList? rimList = JsonSerializer.Deserialize<RimList>(jsonFile);

        // Kontrollerar om värdet är null och hanterar det med ett meddelande.
        if (rimList == null || rimList.Rimsamling == null)
        {
            Console.WriteLine("Tyvärr, inga rim hittades.");
            return;
        }

        // Initierar en boolean som har true som default-värde. 
        bool continueSearch = true;

        // Loop som kör tills dess att användaren väljer att avsluta sökningen.
        while (continueSearch)
        {
            // Rensar konsollen på tidigare info.
            Console.Clear();

            // Visar "meny" för sökning och ger information om hur en sökning går till.
            Console.WriteLine("\nVÄLKOMMEN ATT SÖKA RIM!");
            Console.WriteLine("\nHär kan du ange en specifik julklapp och se rim som passar till den.");
            Console.WriteLine("Efter avslutad sökning får du möjlighet att göra en ny sökning, så om din julklapp inte finns lagrad, prova en annan!");
            Console.WriteLine("Om du vill avbryta sökningen, ange 0.");

            // Ber användaren skriva in vilken julklapp det önskas rim för.
            Console.WriteLine("\nAnge den julklapp du önskar söka rim för:\n");

            // Initierar variabel för användarinput, en specifik julklapp.
            string? specifiedGift = Console.ReadLine()?.ToLower();

            // Kontrollerar om input är null eller tom.
            if (string.IsNullOrEmpty(specifiedGift))
            {
                // Visar meddelande om att input är nödvändig.
                Console.WriteLine("\nDu måste ange en julklapp för att söka rim.");

                // Fortsätter exekvering av kod.
                continue;
            }
            // Kontrollerar om input är 0, då avbryts sökningen.
            else if (specifiedGift == "0")
            {
                // Visar meddelande om avrbuten sökning.
                Console.WriteLine("\nDu valde att avbryta sökningen. Tryck på ENTER för att gå till huvudmenyn.");
                Console.ReadLine();

                // Boolean sätts till false och användaren skickas till huvudmenyn.
                continueSearch = false;
            }
            // Om giltig julklapp angivits...
            else
            {
                // Hittar alla rim för julklappen som matchar användarens input.
                var rhymesForGift = rimList.Rimsamling.FindAll(rhyme => rhyme.Julklapp.ToLower().Contains(specifiedGift));

                // Kontrollerar om rim för angiven julklapp finns.
                if (rhymesForGift.Count > 0)
                {
                    // Rensar konsollen.
                    Console.Clear();

                    // Skriver ut meddelande om att rim hittats och visar det/dem.
                    Console.WriteLine($"\nVarsågod, här visas rim för den julklapp du angav:\n");

                    // Loopar igenom de rim som innehåller angiven julklapp.
                    foreach (var item in rhymesForGift)
                    {
                        Console.WriteLine($"Julklapp: {item.Julklapp}");
                        Console.WriteLine($"Rim: {item.Rim}");

                        // Lägger en tom rad mellan varje post.
                        Console.WriteLine();
                    }
                }
                // Om inga rim hittas...
                else
                {
                    // Rensar konsollen på tidigare information.
                    Console.Clear();

                    // Meddelar användaren om att rim saknas.
                    Console.WriteLine("\nInga rim hittades för denna julklapp.");
                }

                // Initerar en boolean med default-värde false för kontroll av val.
                bool validSelection = false;

                // Hanterar användarens val om ny sökning eller avsluta. Loopar tills ett giltigt val görs.
                while (!validSelection)
                {
                    // Frågar användaren om hen vill göra en ny sökning.
                    Console.WriteLine("\nVill du göra en ny sökning? Ange 1 för JA eller ange 0 för NEJ.\n");

                    // Läser in användarens val (hanterar null-värde med ?).
                    string? selectedOption = Console.ReadLine();

                    // Kontrollerar användarens svar.
                    if (selectedOption == "1")
                    {
                        // Bekräftar val och ber användaren trycka på ENTER för att göra en ny sökning.
                        Console.WriteLine("\nVad trevligt att du vill söka efter fler rim! Tryck på ENTER för att göra en ny sökning.");
                        Console.ReadLine();

                        // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                        validSelection = true;
                    }
                    else if (selectedOption == "0")
                    {
                        // Bekräftar val och ber användaren trycka på ENTER för att avsluta.
                        Console.WriteLine("\nDu valde att avsluta. Tryck på ENTER för att gå till huvudmenyn.");
                        Console.ReadLine();

                        // Boolean sätts till true då detta är ett giltigt val. Avslutar loopen.
                        validSelection = true;

                        // Boolean sätts till false och sökning avslutas.
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
    }*/

    // Funktion som hanterar sökning på julklappar utifrån angiven kategori.
    static void SearchForGifts()
    {
        // Rensar konsollen på tidigare meny.
        Console.Clear();

        // Läser in JSON-filen som en sträng.
        string jsonFile = File.ReadAllText("rimsamling.json");

        // Deserialiserar JSON-strängen till ett RimList-objekt (hanterar nullvärden med ?)
        RimList? rimList = JsonSerializer.Deserialize<RimList>(jsonFile);

        // Kontrollerar om värdet är null och hanterar det med ett meddelande.
        if (rimList == null || rimList.Rimsamling == null)
        {
            Console.WriteLine("Tyvärr, inga rim hittades.");
            return;
        }

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
            Console.WriteLine("Efter avslutad sökning får du möjlighet att göra en ny sökning om du önskar utforska fler kategorier.");

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
                var giftsInSelectedCategory = rimList.Rimsamling
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
                Console.WriteLine("\nVill du göra en ny sökning? Ange 1 för JA eller ange 0 för NEJ.\n");

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

    // Funktion som visar spelmenyn och hanterar användarens val.
    static void ShowGameMenu()
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

    // Funktion som startar och kör gissningsspelet (hanterar null-värde med ?).
    static void StartGuessingGame(string? userChoice)
    {
        // Läser in JSON-filen som en sträng.
        string jsonFile = File.ReadAllText("rimsamling.json");

        // Deserialiserar JSON-strängen till ett RimList-objekt (hanterar nullvärden med ?)
        RimList? rimList = JsonSerializer.Deserialize<RimList>(jsonFile);

        // Kontrollerar om värdet är null och hanterar det med ett meddelande.
        if (rimList == null || rimList.Rimsamling == null)
        {
            Console.WriteLine("Tyvärr, inga rim hittades.");
        }
        // Om värdet inte är null exekveras koden vidare.
        else
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
                var rhymesInSelectedCategory = rimList.Rimsamling.Where(r => r.Kategori == selectedCategory).ToList();

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
}