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
    }
}