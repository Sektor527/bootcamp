#include "gmock/gmock.h"
#include "game.h"

class GameTests : public ::testing::Test
{
public:
	virtual void SetUp()
	{
		game.setName("Name");
		game.setPath("Path");
		game.setArgs("Args");
		game.setPlatform(WINDOWS);
		game.setCategory("Category");
		game.setISO("ISO");
		game.setFavorite();

		other.setName("Name");
		other.setPath("Path");
		other.setArgs("Args");
		other.setPlatform(WINDOWS);
		other.setCategory("Category");
		other.setISO("ISO");
		other.setFavorite();
	}

protected:
	Game game;
	Game other;
};

TEST_F(GameTests, GetName)
{
	ASSERT_EQ("Name", game.getName());
}

TEST_F(GameTests, GetPath)
{
	ASSERT_EQ("Path", game.getPath());
}

TEST_F(GameTests, GetArgs)
{
	ASSERT_EQ("Args", game.getArgs());
}

TEST_F(GameTests, GetPlatform)
{
	ASSERT_EQ(WINDOWS, game.getPlatform());
}

TEST_F(GameTests, GetCategory)
{
	ASSERT_EQ("Category", game.getCategory());
}

TEST_F(GameTests, GetISO)
{
	ASSERT_EQ("ISO", game.getISO());
}

TEST_F(GameTests, IsFavorite)
{
	ASSERT_TRUE(game.isFavorite());
}

TEST_F(GameTests, UnsetFavorite)
{
	game.unsetFavorite();
	ASSERT_FALSE(game.isFavorite());
}

TEST_F(GameTests, GamesAreEqual)
{
	ASSERT_EQ(game, other);
}

TEST_F(GameTests, GamesDifferInPath)
{
	other.setPath("other path");
	ASSERT_NE(game, other);
}

TEST_F(GameTests, GamesDifferInArguments)
{
	other.setArgs("other args");
	ASSERT_NE(game, other);
}

TEST_F(GameTests, GamesDifferInPlatform)
{
	other.setPlatform(DOSBOX);
	ASSERT_NE(game, other);
}

TEST_F(GameTests, GamesDontDifferInName)
{
	other.setName("other name");
	ASSERT_EQ(game, other);
}

TEST_F(GameTests, GamesDontDifferInISO)
{
	other.setISO("other ISO");
	ASSERT_EQ(game, other);
}

TEST_F(GameTests, GamesDontDifferInFavorite)
{
	other.unsetFavorite();
	ASSERT_EQ(game, other);
}

TEST_F(GameTests, GamesDontDifferInCategory)
{
	other.setCategory("other category");
	ASSERT_EQ(game, other);
}

TEST_F(GameTests, StreamOut)
{
	game.setName("Darksun: Shattered Lands");
	game.setPath("programs/dsun/dsun.exe");
	game.setArgs("-f -p test");
	game.setPlatform(DOSBOX);
	game.setISO("");
	game.setCategory("Roleplaying");
	game.setFavorite();
	std::stringstream out;
	out << game;

	ASSERT_EQ("Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1", out.str());
}

TEST_F(GameTests, StreamIn)
{
	std::stringstream in;
	in << "Darksun: Shattered Lands|programs/dsun/dsun.exe|-f -p test|2|Roleplaying||1";
	in >> game;
	
	ASSERT_EQ("Darksun: Shattered Lands", game.getName());
	ASSERT_EQ("programs/dsun/dsun.exe", game.getPath());
	ASSERT_EQ("-f -p test", game.getArgs());
	ASSERT_EQ(DOSBOX, game.getPlatform());
	ASSERT_EQ("", game.getISO());
	ASSERT_EQ("Roleplaying", game.getCategory());
	ASSERT_TRUE(game.isFavorite());
}
