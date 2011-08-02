using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootCamp;
using NUnit.Framework;

namespace BootCampTests
{
	[TestFixture]
	public class AlphabeticGrouperTests
	{
		[Test]
		public void GroupingWithOne_NoDuplicates()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 1};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "bbb", "ccc", "ddd", "eee"});

			Assert.AreEqual(5, groups.Count);
			Assert.Contains("A", groups);
			Assert.Contains("B", groups);
			Assert.Contains("C", groups);
			Assert.Contains("D", groups);
			Assert.Contains("E", groups);
		}

		[Test]
		public void GroupingWithOne_Duplicates()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 1};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "aaaaa", "ccc", "b", "aa"});

			Assert.AreEqual(3, groups.Count);
			Assert.Contains("A", groups);
			Assert.Contains("B", groups);
			Assert.Contains("C", groups);
		}

		[Test]
		public void GroupingWithTwo_NoDuplicates()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 2};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "bbb", "ccc", "ddd", "eee"});

			Assert.AreEqual(3, groups.Count);
			Assert.Contains("A-B", groups);
			Assert.Contains("C-D", groups);
			Assert.Contains("E", groups);
		}

		[Test]
		public void GroupingWithTwo_Duplicates()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 2};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "aaaaa", "ccc", "b", "aa"});

			Assert.AreEqual(2, groups.Count);
			Assert.Contains("A", groups);
			Assert.Contains("B-C", groups);
		}

		[Test]
		public void GroupingNonOrdered()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 2};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "zzz", "bbb", "yyy", "mmm"});

			Assert.AreEqual(3, groups.Count);
			Assert.Contains("A-B", groups);
			Assert.Contains("M-Y", groups);
			Assert.Contains("Z", groups);
		}

		[Test]
		public void DescendedGroupingWithOne()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 1, Ascending = false};

			List<string> groups = grouper.Group(new List<string>() {"aaa", "bbb", "ccc", "ddd"});

			Assert.AreEqual(4, groups.Count);
			Assert.Contains("A", groups);
			Assert.Contains("B", groups);
			Assert.Contains("C", groups);
			Assert.Contains("D", groups);
		}

		[Test]
		public void DescendedGroupWithTwo()
		{
			AlphabeticGrouper grouper = new AlphabeticGrouper {MinimumItemsPerGroup = 3, Ascending = false};

			List<string> groups = grouper.Group(new List<string>() { "aaa", "zzz", "bbb", "xxx", "yyy", "mmm" });
			
			Assert.AreEqual(2, groups.Count);
			Assert.Contains("X-Z", groups);
			Assert.Contains("A-M", groups);
		}
	}
}
