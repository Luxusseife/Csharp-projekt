// Gemensamt namespace för alla filer i projektet.
namespace projekt;

// Laddar in nödvändiga paket för filinläsning och JSON-hantering.
using System;
using System.IO;
using System.Text.Json;

// Publik klass som hanterar filinläsning.
public class FileHandler
{
    // En privat referens till JSON-filen.
    private string filePath;

    // Konstruktor som tar emot sökvägen till JSON-filen.
    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    // Metod som läser in listan med rim från JSON-filen.
    public RimList? LoadRimList()
    {
        // Kontrollerar om filen existerar innan inläsningsförsök.
        if (!File.Exists(filePath))
        {
            // Meddelar användaren att fil saknas.
            Console.WriteLine($"Filen '{filePath}' finns tyvärr inte. Kontrollera sökvägen.");

            // Returnerar ett null-värde.
            return null;
        }

        // Inläsningsförsök.
        try
        {
            // Läser in JSON-filen som en sträng..
            string jsonFile = File.ReadAllText(filePath);

            // Deserialiserar JSON-strängen till ett RimList-objekt.
            return JsonSerializer.Deserialize<RimList>(jsonFile);
        }
        // Om fel uppstår...
        catch (Exception error)
        {
            // Hanterar eventuella fel vid inläsning
            Console.WriteLine($"Ett fel uppstod vid inläsning av filen: {error.Message}");

            // Returnerar ett null-värde.
            return null;
        }
    }
}