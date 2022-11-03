using System;
using System.Collections.Generic;

public class DisposableList : IDisposable
{
    private List<IDisposable> _list = new List<IDisposable>();

    public void Clear()
    {
        _list.Clear();
    }

    public void Add(IDisposable disposable)
    {
        _list.Add(disposable);
    }

    public void Dispose()
    {
        foreach (var lDisposable in _list)
        {
            lDisposable.Dispose();
        }
        Clear();
    }
}

public static class Utils
{
    public static void AddTo(this IDisposable disposable, DisposableList disposableList)
    {
        disposableList.Add(disposable);
    }
}