using System.Collections;

record struct Term(string ID, string Word, string Definition);

class Deck(string name, string targetLanguage, string language) : IEnumerable<Term>
{
    private string? _deckId = null;
    private readonly List<Term> _terms = [];
    private int _nextTermId = 0;

    public string Name { get; set; } = name;
    public string TargetLanguage { get; } = targetLanguage;
    public string Language { get; } = language;

    public IEnumerator<Term> GetEnumerator() => _terms.GetEnumerator();

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

        _terms.Add(new Term(GetNextTermId(), word, definition));
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

        int index = _terms.FindIndex(term => term.ID == id);

        if (index != -1)
        {
            _terms[index] = new Term(id, newWord, newDefinition);

            return true;
        }

        return false;
    }

    public bool RemoveTerm(string id)
    {
        int index = _terms.FindIndex(term => term.ID == id);

        if (index != -1)
        {
            _terms.RemoveAt(index);

            return true;
        }

        return false;
    }

    public override string? ToString()
    {
        return _deckId;
    }

    private string GetNextTermId()
    {
        _deckId ??= string.Concat(Name.Trim()
                                      .ToLowerInvariant()
                                      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(word => word[0])
                                      .ToArray());

        return $"{_deckId}-{_nextTermId++}";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}