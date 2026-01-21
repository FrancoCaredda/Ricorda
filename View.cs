internal abstract class View
{
    public abstract void Init();
    public abstract void Visualize();
}

internal class AppView(List<Deck?> decks) : View
{
    private readonly List<Deck?> _decks = decks;
    private readonly List<DeckView> _subViews = [];

    private enum AppCommands : int
    {
        Begin = 0,
        ShowDeck = 1,
        CreateDeck = 2,
        DeleteDeck = 3,
        ModifyDeck = 4,
        Other
    }

    public override void Init()
    {
        foreach (var deck in _decks)
        {
            _subViews.Add(new DeckView(deck));
        }
    }

    public override void Visualize()
    {
        string? command = null;
        int parsedCommand = (int)AppCommands.Other;

        do
        {
            VisualizeDecks();
            VisualizeMenu();

            Console.Write($"Select command in range from {(int)AppCommands.Begin + 1} to {((int)AppCommands.Other)}: ");
            command = Console.ReadLine();

            if (command is not null)
            {
                parsedCommand = int.Parse(command);
            }

            switch ((AppCommands)parsedCommand)
            {
                case AppCommands.ShowDeck:
                    Console.WriteLine("Select a deck using its ID: ");
                    command = Console.ReadLine();

                    int deckIndex = _decks.FindIndex(deck => deck?.DeckID == command);

                    if (deckIndex >= 0)
                    {
                        _subViews[deckIndex].Visualize();
                    }
                    else
                    {
                        Console.WriteLine($"No deck with ID ({command}) has been found!");
                    }

                    break;
                case AppCommands.CreateDeck:
                    break;
                case AppCommands.DeleteDeck:
                    break;
                case AppCommands.ModifyDeck:
                    break;
                default:
                    break;
            }

            // Console.ReadKey();
            // Console.Clear();
        } while (parsedCommand < (int)AppCommands.Other);
    }

    private void VisualizeDecks()
    {
        Console.WriteLine("Deck ID\t\tDeck Name\tTarget Language\t\tLanguage");

        foreach (var deck in _decks)
        {
            Console.WriteLine($"{deck?.DeckID}\t\t{deck?.Name}\t{deck?.TargetLanguage}\t\t\t{deck?.Language}");
        }
    }

    private void VisualizeMenu()
    {
        Console.WriteLine();

        var commands = Enum.GetNames<AppCommands>();
        for (int i = 1; i < commands.Length; i++)
        {
            Console.WriteLine($"{i}) {commands[i]}");
        }

        Console.WriteLine();
    }
}

internal class DeckView(Deck? deck) : View
{
    private readonly Deck? _deck = deck;

    public override void Init()
    {
    
    }

    public override void Visualize()
    {
        if (_deck is not null)
        {
            Console.WriteLine("Term ID\t\tTerm\t\t\tDefinition");
            foreach (var term in _deck)
            {
                Console.WriteLine($"{term.Key}\t\t{term.Value.Word}\t\t{term.Value.Definition}");       
            }
        } 
    }
}