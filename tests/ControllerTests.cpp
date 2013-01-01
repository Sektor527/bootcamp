#include "gmock/gmock.h"
#include "controller.h"

TEST(ControllerTests, AddGame)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ(1, c.getRowCount());
}

TEST(ControllerTests, AddTwoGames)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");

	ASSERT_EQ(2, c.getRowCount());
}

TEST(ControllerTests, GetID)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("   1", c.getID(0));
}

TEST(ControllerTests, GetName)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("Darksun: Shattered Lands", c.getName(0));
}

TEST(ControllerTests, GetCategory)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("Roleplaying", c.getCategory(0));
}

TEST(ControllerTests, GetPlatform)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ(DOSBOX, c.getPlatform(0));
}

TEST(ControllerTests, GetPath)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");

	ASSERT_EQ("path/to/game.exe", c.getPath(0));
}

TEST(ControllerTests, GetArguments)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "-arg hello", "");

	ASSERT_EQ("-arg hello", c.getArgs(0));
}

TEST(ControllerTests, GetISO)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "-arg hello", "path/to/ISO");

	ASSERT_EQ("path/to/ISO", c.getISO(0));
}

TEST(ControllerTests, GetSecondName)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");

	ASSERT_EQ("Dwarf Fortress", c.getName(1));
}

TEST(ControllerTests, RemoveGame)
{
	Controller c;
	c.addGame("Darksun: Shattered Lands", "Roleplaying", DOSBOX, "path/to/game.exe", "", "");
	c.addGame("Dwarf Fortress", "Roleplaying", WINDOWS, "path/to/game.exe", "", "");
	c.removeGame(0);

	ASSERT_EQ(1, c.getRowCount());
	ASSERT_EQ("Dwarf Fortress", c.getName(0));
}

TEST(ControllerTests, StreamInCorrectNumber)
{
	Controller c;

	std::stringstream in;
	in << "Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1" << std::endl;
	in << "Dwarf Fortress|programs/df/df.exe||3|Roguelike||1" << std::endl;

	in >> c;

	ASSERT_EQ(2, c.getRowCount());
}

TEST(ControllerTests, StreamInCorrectOrder)
{
	Controller c;

	std::stringstream in;
	in << "Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1" << std::endl;
	in << "Dwarf Fortress|programs/df/df.exe||3|Roguelike||1" << std::endl;

	in >> c;

	ASSERT_EQ("Darksun: Shattered Lands", c.getName(0));
	ASSERT_EQ("Dwarf Fortress", c.getName(1));
}

TEST(ControllerTests, StreamOut)
{
	Controller c;
	c.addGame("n1", "c1", WINDOWS, "p1", "a1", "i1");
	c.addGame("n2", "c2", DOSBOX, "p2", "a2", "i2");

	std::stringstream out;
	out << c;

	ASSERT_EQ("n1|p1|a1|1|c1|i1|0\nn2|p2|a2|2|c2|i2|0\n", out.str());
}
