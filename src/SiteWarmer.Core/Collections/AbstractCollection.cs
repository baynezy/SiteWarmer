namespace SiteWarmer.Core.Collections;

/// <summary>
/// An abstract utility class to create collections of objects in a predictable order
/// </summary>
/// <typeparam name="T">The Type of objects in the collection</typeparam>
public abstract class AbstractCollection<T>
{
    /// <summary>
    /// The list of items in the collection. This is protected so that it can be accessed by child classes, but not by external code.
    /// </summary>
    protected readonly IList<T> Items;

    /// <summary>
    /// Create a new instance of the AbstractCollection class
    /// </summary>
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