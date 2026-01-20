using System.Collections;

internal record struct Term(string Word, string Definition);

internal class Deck(string name, string targetLanguage, string language) : 
    IEnumerable<KeyValuePair<string, Term>>
{
    private string? _deckId = null;
    private readonly Dictionary<string, Term> _terms = [];
    private int _nextTermId = 0;

    public string Name { get; set; } = name;
    public string TargetLanguage { get; } = targetLanguage;
    public string Language { get; } = language;

    public string? DeckID
    {
        get
        {
            return _deckId;
        }
    }

    public void AddTerm(string word, string definition)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            throw new ArgumentException("the newWord parameter is null or empty");
        }

        if (string.IsNullOrWhiteSpace(definition))
        {
            throw new ArgumentException("the newDefinition parameter is null or empty");
        }

        _terms.Add(GetNextTermId(), new Term(word, definition));
    }

    public bool ModifyTerm(string id, string newWord, string newDefinition)
    {
        if (string.IsNullOrWhiteSpace(newWord))
        {
            throw new ArgumentException("the newWord parameter is null or empty");
        }

        if (string.IsNullOrWhiteSpace(newDefinition))
        {
            throw new ArgumentException("the newDefinition parameter is null or empty");
        }

        if (_terms.ContainsKey(id))
        {
            _terms[id] = new Term(newWord, newDefinition);

            return true;
        }

        return false;
    }
    
    public bool RemoveTerm(string id)
    {
        return _terms.Remove(id);
    }
    
    public override string? ToString()
    {
        return _deckId;
    }

    private string GetNextTermId()
    {
        // if the value is currently null assign a new value to it.
        _deckId ??= string.Concat(Name.Trim()
                                      .ToLowerInvariant()
                                      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(word => word[0])
                                      .ToArray());

        return $"{_deckId}-{_nextTermId++}";
    }

    // System.Collections.Generic.IEnumerable<T> extends System.Collections.IEnumerable
    // and hides the GetEnumerator method of the base interface using the new keyword.
    // Since the signatures of two methods are different, they both have to be implemented.
    // If they had the same signature, only one implementation would've been needed.
    public IEnumerator<KeyValuePair<string, Term>> GetEnumerator() => _terms.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}