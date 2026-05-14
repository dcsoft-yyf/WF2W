using System.Collections;
using System.Data.Common;

namespace System.Data;

[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
public class EmulatedDbParameterCollection<TParameter> : DbParameterCollection where TParameter : DbParameter
{
    private readonly List<TParameter> _items = new();

    public override int Count => _items.Count;
    public override object SyncRoot => ((ICollection)_items).SyncRoot;

    public override int Add(object value)
    {
        if (value is not TParameter parameter)
        {
            throw new ArgumentException("Invalid parameter type.", nameof(value));
        }

        _items.Add(parameter);
        return _items.Count - 1;
    }

    public TParameter AddTyped(TParameter parameter)
    {
        _items.Add(parameter);
        return parameter;
    }

    public override void AddRange(Array values)
    {
        foreach (var item in values)
        {
            Add(item!);
        }
    }

    public override void Clear() => _items.Clear();
    public override bool Contains(object value) => value is TParameter parameter && _items.Contains(parameter);
    public override bool Contains(string value) => _items.Exists(x => string.Equals(x.ParameterName, value, StringComparison.OrdinalIgnoreCase));
    public override void CopyTo(Array array, int index) => ((ICollection)_items).CopyTo(array, index);
    public override IEnumerator GetEnumerator() => _items.GetEnumerator();
    public override int IndexOf(object value) => value is TParameter parameter ? _items.IndexOf(parameter) : -1;

    public override int IndexOf(string parameterName)
    {
        for (var i = 0; i < _items.Count; i++)
        {
            if (string.Equals(_items[i].ParameterName, parameterName, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    public override void Insert(int index, object value)
    {
        if (value is not TParameter parameter)
        {
            throw new ArgumentException("Invalid parameter type.", nameof(value));
        }

        _items.Insert(index, parameter);
    }

    public override void Remove(object value)
    {
        if (value is TParameter parameter)
        {
            _items.Remove(parameter);
        }
    }

    public override void RemoveAt(int index) => _items.RemoveAt(index);

    public override void RemoveAt(string parameterName)
    {
        var index = IndexOf(parameterName);
        if (index >= 0)
        {
            _items.RemoveAt(index);
        }
    }

    protected override DbParameter GetParameter(int index) => _items[index];

    protected override DbParameter GetParameter(string parameterName)
    {
        var index = IndexOf(parameterName);
        return index >= 0 ? _items[index] : throw new IndexOutOfRangeException(parameterName);
    }

    protected override void SetParameter(int index, DbParameter value)
    {
        if (value is not TParameter parameter)
        {
            throw new ArgumentException("Invalid parameter type.", nameof(value));
        }

        _items[index] = parameter;
    }

    protected override void SetParameter(string parameterName, DbParameter value)
    {
        var index = IndexOf(parameterName);
        if (index < 0)
        {
            Add(value);
            return;
        }

        SetParameter(index, value);
    }
}
