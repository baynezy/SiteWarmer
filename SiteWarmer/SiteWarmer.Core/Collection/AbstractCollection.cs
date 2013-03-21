using System.Collections.Generic;

namespace SiteWarmer.Core.Collection
{
	public abstract class AbstractCollection<T>
	{
		protected readonly IList<T> Items;

		protected AbstractCollection()
		{
			Items = new List<T>();
		}

		public int Size()
		{
			return Items.Count;
		}

		public void Add(T item)
		{
			Items.Add(item);
		}
	}
}
