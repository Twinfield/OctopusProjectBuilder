using System;
using System.Collections.Generic;


namespace OctopusProjectBuilder.Model
{
	public static class SortedListExtension
	{
		public static void TryAdd<TKey, TValue>(this SortedList<TKey, TValue> list, TKey key, TValue value)
		{
			if (list.ContainsKey(key))
				return;
			list.Add(key,value);
		}
	}
}
