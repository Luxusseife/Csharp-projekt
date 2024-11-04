// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning och JSON-hantering.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Publik klass som hanterar sökning på rim utifrån specifik julklapp.
public class RhymeHandler
{
    // Privat fältvariabel som lagrar deserialiserad rimdata (nullvärden hanteras med ?-operatorn).
    private RimList? rimList;

    // Konstruktor som hanterar inläsning av data från JSON-filen.
    public RhymeHandler()
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

    // Metod som hanterar sökning på rim utifrån angiven julklapp.
    public void SearchForRhymes()
    {
        // Rensar konsollen på tidigare meny.
        Console.Clear();

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
                    Console.WriteLine("\nVill du göra en ny sökning på rim? Ange 1 för JA eller ange 0 för NEJ.\n");

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
    }
}