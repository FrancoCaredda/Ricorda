var polishDeck = new Deck("Polish Common Words", "Polish", "English");

polishDeck.AddTerm("Iść", "To go");
polishDeck.AddTerm("Chodzić", "To walk");
polishDeck.AddTerm("Test", ".");

var englishDeck = new Deck("Emotions and Health", "English", "English");
englishDeck.AddTerm("Healthy", "The state when you feel well");
englishDeck.AddTerm("To be sick", "to be ill");

Console.WriteLine(polishDeck);
Console.WriteLine(englishDeck);