namespace SiteWarmer.Core.Collections;

/// <summary>
/// An abstract utility class to create collections of objects in a predictable order
/// </summary>
/// <typeparam name="T">The Type of objects in the collection</typeparam>
public abstract class AbstractCollection<T>
{
    protected readonly IList<T> Items;

    protected AbstractCollection()
    {
        Items = new List<T>();
    }

    /// <summary>
    /// Get the number of objects within this collection
    /// </summary>
    /// <returns>Number of objects in the collection</returns>
    public int Size()
    {
        return Items.Count;
    }

    /// <summary>
    /// Add a new item to the collection
    /// </summary>
    /// <param name="item">Item to add to the last position in the collection</param>
    public void Add(T item)
    {
        Items.Add(item);
    }
}