var deck = new Deck("Common phrases", "Polish", "English");
deck.AddTerm("Jak się masz?", "How are you?");

DeckSerializer.Serialize(deck);