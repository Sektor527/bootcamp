#include "gmock/gmock.h"
#include "ListFilter.h"
#include "Game.h"

using ::testing::Test;

class ListFilterTests : public ::testing::Test
{
public:
	virtual void SetUp()
	{
		std::vector<Game> list;

		Game g1;
		g1.setName("rpg1");
		g1.setCategory("Roleplaying Game");
		g1.setPlatform(WINDOWS);
		list.push_back(g1);

		Game g2;
		g2.setName("rpg2");
		g2.setCategory("Roleplaying");
		g2.setPlatform(DOSBOX);
		list.push_back(g2);

		Game g3;
		g3.setName("rogue1");
		g3.setCategory("Roguelike");
		g3.setPlatform(DOSBOX);
		list.push_back(g3);

		Game g4;
		g4.setName("rogue2");
		g4.setCategory("ROGUELIKE");
		g4.setPlatform(C64);
		list.push_back(g4);

		Game g5;
		g5.setName("fps");
		g5.setCategory("FPS");
		g5.setPlatform(WINDOWS);
		list.push_back(g5);

		f.setList(list);
	}

	ListFilter f;
};

TEST_F(ListFilterTests, EmptyFilter)
{
	std::vector<int> filtered = f.getFilteredList("", UNDEFINED);

	ASSERT_EQ(5, filtered.size());
}

TEST_F(ListFilterTests, SingleResultCategoryFilter)
{
	std::vector<int> filtered = f.getFilteredList("FPS", UNDEFINED);

	ASSERT_EQ(1, filtered.size());
}

TEST_F(ListFilterTests, SingleResultPlatformFilter)
{
	std::vector<int> filtered = f.getFilteredList("", C64);

	ASSERT_EQ(1, filtered.size());
}

TEST_F(ListFilterTests, SingleResultCategoryAndPlatformFilter)
{
	std::vector<int> filtered = f.getFilteredList("FPS", WINDOWS);

	ASSERT_EQ(1, filtered.size());
}

TEST_F(ListFilterTests, NoMatchesForCategoryFilter)
{
	std::vector<int> filtered = f.getFilteredList("No match", UNDEFINED);

	ASSERT_EQ(0, filtered.size());
}

TEST_F(ListFilterTests, NoMatchesForPlatformFilter)
{
	std::vector<int> filtered = f.getFilteredList("", SUPERNINTENDO);

	ASSERT_EQ(0, filtered.size());
}

TEST_F(ListFilterTests, NoMatchesForCategoryAndPlatformFilter)
{
	std::vector<int> filtered = f.getFilteredList("Roguelike", WINDOWS);

	ASSERT_EQ(0, filtered.size());
}

TEST_F(ListFilterTests, CategorySubstringFilter)
{
	std::vector<int> filtered = f.getFilteredList("Roleplaying", UNDEFINED);

	ASSERT_EQ(2, filtered.size());
}

TEST_F(ListFilterTests, CategoryIgnoreCaseFilter)
{
	std::vector<int> filtered = f.getFilteredList("roguelike", UNDEFINED);

	ASSERT_EQ(2, filtered.size());
}

TEST_F(ListFilterTests, CorrectIndexesAfterFilter)
{
	std::vector<int> filtered = f.getFilteredList("roguelike", UNDEFINED);

	ASSERT_EQ(2, filtered[0]);
	ASSERT_EQ(3, filtered[1]);
}
