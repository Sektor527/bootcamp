#include "gmock/gmock.h"
#include "Game.h"
#include "controller.h"
#include "ListFilter.h"
#include <vector>

using ::testing::DefaultValue;

class MockFilter : public ListFilter
{
public:
	MOCK_CONST_METHOD2(getFilteredList, std::vector<int>(const std::string&, Platform));
};

class ControllerTests : public ::testing::Test
{
public:
	virtual void SetUp()
	{
		filter = new ListFilter;
		c.setFilter(filter);
	}

	virtual void TearDown()
	{
		delete filter;
	}

	Controller c;
	ListFilter* filter;
};

TEST_F(ControllerTests, AddGame)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ(1, c.getRowCount());
}

TEST_F(ControllerTests, AddTwoGames)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");

	ASSERT_EQ(2, c.getRowCount());
}

TEST_F(ControllerTests, GetID)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("   1", c.getID(0));
}

TEST_F(ControllerTests, GetName)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("Darksun: Shattered Lands", c.getName(0));
}

TEST_F(ControllerTests, GetCategory)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("Roleplaying", c.getCategory(0));
}

TEST_F(ControllerTests, GetPlatform)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ(DOSBOX, c.getPlatform(0));
}

TEST_F(ControllerTests, GetPath)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("path/to/game.exe", c.getPath(0));
}

TEST_F(ControllerTests, GetArguments)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "-arg hello", "");

	ASSERT_EQ("-arg hello", c.getArgs(0));
}

TEST_F(ControllerTests, GetISO)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "-arg hello", "path/to/ISO");

	ASSERT_EQ("path/to/ISO", c.getISO(0));
}

TEST_F(ControllerTests, GetSecondName)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");

	ASSERT_EQ("Dwarf Fortress", c.getName(1));
}

TEST_F(ControllerTests, RemoveGame)
{
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");
	c.removeGame(0);

	ASSERT_EQ(1, c.getRowCount());
	ASSERT_EQ("Dwarf Fortress", c.getName(0));
}

TEST_F(ControllerTests, Filter)
{
	MockFilter f;
	EXPECT_CALL(f, getFilteredList("Test", C64));

	std::vector<int> emptylist;
	DefaultValue<std::vector<int> >::Set(emptylist);

	c.setFilter(&f);
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.filter("Test", C64);
	ASSERT_EQ(0, c.getRowCount());
}

TEST_F(ControllerTests, StreamInCorrectNumber)
{
	std::stringstream in;
	in << "Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1" << std::endl;
	in << "Dwarf Fortress|programs/df/df.exe||3|Roguelike||1" << std::endl;

	in >> c;

	ASSERT_EQ(2, c.getRowCount());
}

TEST_F(ControllerTests, StreamInCorrectOrder)
{
	std::stringstream in;
	in << "Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1" << std::endl;
	in << "Dwarf Fortress|programs/df/df.exe||3|Roguelike||1" << std::endl;

	in >> c;

	ASSERT_EQ("Darksun: Shattered Lands", c.getName(0));
	ASSERT_EQ("Dwarf Fortress", c.getName(1));
}

TEST_F(ControllerTests, StreamOut)
{
	c.addGame("n1", "c1", WINDOWS, "p1", "a1", "i1");
	c.addGame("n2", "c2", DOSBOX, "p2", "a2", "i2");

	std::stringstream out;
	out << c;

	ASSERT_EQ("n1|p1|a1|1|c1|i1|0\nn2|p2|a2|2|c2|i2|0\n", out.str());
}
