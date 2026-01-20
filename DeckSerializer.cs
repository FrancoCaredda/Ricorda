internal static class DeckSerializer
{
    public static readonly string Folder;

    static DeckSerializer()
    {
        Folder = "decks";
    }

    public static void Serialize(Deck deck)
    {
        string name = deck.Name;
        string targetLanguage = deck.TargetLanguage;
        string language = deck.Language;

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), Folder);
        string fileName = Path.Combine(folderPath, $"{deck.DeckID}.txt");

        Directory.CreateDirectory(folderPath);

        using StreamWriter writer = new(fileName);
        writer.WriteLine(name);
        writer.WriteLine(targetLanguage);
        writer.WriteLine(language);

        foreach ((var key, var value) in deck)
        {
            writer.Write($"{value.Word}|{value.Definition}");
        }
    }

    public static Deck? Deserialize(string fileName)
    {
        using StreamReader reader = new(fileName);

        string? name = reader.ReadLine();
        string? targetLanguage = reader.ReadLine();
        string? language = reader.ReadLine();

        if (name is null || targetLanguage is null || language is null)
        {
            return null;
        }

        var deck = new Deck(name, targetLanguage, language);

        string? entry = reader.ReadLine();
        
        while (entry is not null)
        {
            string[] values = entry.Split('|');

            if (values.Length >= 2)
            {
                deck.AddTerm(values[0], values[1]);
            }
            else
            {
                throw new IOException("Failed to read an entry");
            }

            entry = reader.ReadLine();
        }  

        return deck;      
    }
}