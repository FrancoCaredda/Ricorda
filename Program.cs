List<Deck?> decks = [DeckSerializer.Deserialize("decks/cp.txt")];

View appView = new AppView(decks);
appView.Init();
appView.Visualize();