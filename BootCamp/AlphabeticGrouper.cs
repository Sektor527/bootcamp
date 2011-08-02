using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootCamp
{
	internal class AlphabeticGrouper
	{
		public int MinimumItemsPerGroup { get; set; }
		public bool Ascending { get; set; }

		private class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
		{
			public int Compare(T x, T y)
			{
				return y.CompareTo(x);
			}
		}

		public AlphabeticGrouper()
		{
			Ascending = true;
			MinimumItemsPerGroup = 1;
		}

		public List<string> Group(List<string> elements)
		{
			SortedDictionary<string, int> groups;
			if (Ascending) groups = new SortedDictionary<string, int>();
			else groups = new SortedDictionary<string, int>(new DescendingComparer<string>());

			ExtractLetters(elements, groups);

			return GroupLetters(groups);
		}

		private void ExtractLetters(List<string> elements, SortedDictionary<string, int> groups)
		{
			foreach (string element in elements)
			{
				string letter = element.Substring(0, 1).ToUpper();
				if (groups.ContainsKey(letter))
					groups[letter]++;
				else
					groups[letter] = 1;
			}
		}

		private List<string> GroupLetters(SortedDictionary<string, int> groups)
		{
			List<string> result = new List<string>();

			KeyValuePair<string, int> regroup = new KeyValuePair<string, int>();

			foreach (KeyValuePair<string, int> group in groups)
			{
				regroup = MergeGroups(regroup, group);

				if (regroup.Value >= MinimumItemsPerGroup)
				{
					result.Add(regroup.Key);
					regroup = new KeyValuePair<string, int>();
					continue;
				}
			}

			if (regroup.Key != null)
				result.Add(regroup.Key);

			return result;
		}

		private KeyValuePair<string, int> MergeGroups(KeyValuePair<string, int> regroup, KeyValuePair<string, int> group)
		{
			if (regroup.Key == null)
				return group;

			if (regroup.Key.Substring(0, 1).CompareTo(group.Key) < 0)
				return new KeyValuePair<string, int>(regroup.Key.Substring(0, 1) + "-" + group.Key, regroup.Value + group.Value);
			else
				return new KeyValuePair<string, int>(group.Key + "-" + regroup.Key.Substring(regroup.Key.Length - 1, 1), regroup.Value + group.Value);
		}
	}
}
