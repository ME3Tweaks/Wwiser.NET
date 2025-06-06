using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ME3Tweaks.Wwiser;

public class BiDirectionalMap<T1, T2> : IEnumerable<KeyValuePair<T1, T2>> 
    where T1 : notnull 
    where T2 : notnull
{
    private readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
    private readonly Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

    public BiDirectionalMap()
    {
        Forward = new Indexer<T1, T2>(_forward);
        Reverse = new Indexer<T2, T1>(_reverse);
    }

    public Indexer<T1, T2> Forward { get; private set; }
    public Indexer<T2, T1> Reverse { get; private set; }

    public void Add(T1 t1, T2 t2)
    {
        _forward.Add(t1, t2);
        _reverse.Add(t2, t1);
    }

    public void Remove(T1 t1)
    {
        T2 revKey = Forward[t1];
        _forward.Remove(t1);
        _reverse.Remove(revKey);
    }
    
    public void Remove(T2 t2)
    {
        T1 forwardKey = Reverse[t2];
        _reverse.Remove(t2);
        _forward.Remove(forwardKey);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        return _forward.GetEnumerator();
    }

    public class Indexer<T3, T4>(Dictionary<T3, T4> dictionary) 
        where T3 : notnull
        where T4 : notnull
    {
        public T4 this[T3 index]
        {
            get => dictionary[index];
            set => dictionary[index] = value;
        }

        public bool Contains(T3 key)
        {
            return dictionary.ContainsKey(key);
        }
        
        public bool TryGetValue(T3 key, [MaybeNullWhen(false)] out T4 value)
        {
            return dictionary.TryGetValue(key, out value);
        }
    }
}