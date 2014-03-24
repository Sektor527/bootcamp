#include "gmock/gmock.h"
#include "CategoryController.h"
#include "ListFilter.h"

class CategoryControllerTests : public ::testing::Test
{
protected:
	virtual void SetUp()
	{
		gc.setFilter(&f);
		cc.setGameController(&gc);
	}

	ListFilter f;
	GameController gc;
	CategoryController cc;
};

TEST_F(CategoryControllerTests, EmptyGamesListGivesNoCategories)
{
	EXPECT_EQ(0, cc.getRowCount());
}

TEST_F(CategoryControllerTests, CategoryCount)
{
	gc.addGame("game1", "cat1", DOSBOX, "path", "", "");
	gc.addGame("game2", "cat2", DOSBOX, "path", "", "");
	gc.addGame("game3", "cat1", DOSBOX, "path", "", "");
	gc.addGame("game4", "cat3", DOSBOX, "path", "", "");

	EXPECT_EQ(3, cc.getRowCount());
}

TEST_F(CategoryControllerTests, CategoryValues)
{
	gc.addGame("game1", "cat1", DOSBOX, "path", "", "");
	gc.addGame("game2", "cat2", DOSBOX, "path", "", "");
	gc.addGame("game3", "cat1", DOSBOX, "path", "", "");
	gc.addGame("game4", "cat3", DOSBOX, "path", "", "");

	EXPECT_EQ("cat1", cc.getName(1));
	EXPECT_EQ("cat2", cc.getName(2));
	EXPECT_EQ("cat3", cc.getName(3));
}
