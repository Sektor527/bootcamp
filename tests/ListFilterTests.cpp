#include "gmock/gmock.h"
#include "ListFilter.h"
#include "Game.h"

using ::testing::Test;

class ListFilterTests : public ::testing::Test
{
public:
	virtual void SetUp()
	{
		Game g1;
		g1.setName("rpg1");
		g1.setCategory("Roleplaying Game");
		categoryList.push_back(g1);

		Game g2;
		g2.setName("rpg2");
		g2.setCategory("roleplaying");
		categoryList.push_back(g2);

		Game g3;
		g3.setName("rogue");
		g3.setCategory("Roguelike");
		categoryList.push_back(g3);
	}

	std::vector<Game> categoryList;
	ListFilter f;
};

TEST_F(ListFilterTests, EmptyCategoryFilter)
{
	f.setList(categoryList);
	std::vector<Game> list = f.getFilteredList(ListFilter::CATEGORY, "");

	ASSERT_EQ(3, list.size());
}
