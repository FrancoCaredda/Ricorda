using System.Text.Json;

internal static class DeckSerializer
{
    public static readonly string Folder;

    static DeckSerializer()
    {
        Folder = "decks";
    }

    public static void Serialize(Deck deck)
    {
        string jsonString = JsonSerializer.Serialize(deck);
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), Folder);

        Directory.CreateDirectory(folderPath);
        File.WriteAllText(Path.Combine(folderPath, $"{deck.DeckID}.json"), jsonString);
    }

    public static void Deserialize(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        var result = JsonSerializer.Deserialize<KeyValuePair<string, Term>[]>(jsonString);

        if (result is not null)
        {
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");        
            }
        }
    }
}