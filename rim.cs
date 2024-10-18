// Gemensamt namespace för alla filer i projektet.
namespace projekt
{
    // Klassen RimItem representerar en post med julklapp, rim och kategori.
    class RimItem
    {
        // Publika egenskaper för lagring av julklapp, rim och kategori. Default är en tom sträng.
        public string Julklapp { get; set; } = string.Empty;
        public string Rim { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
    }

    // Klassen RimList hanterar en samling av rim-poster. 
    class RimList
    {
        // En lista med alla rim-poster. Default-listan är en tom sträng.
        public List<RimItem> Rimsamling { get; set; } = new List<RimItem>();
    }
}