#include "gmock/gmock.h"
#include "Game.h"
#include "PlatformController.h"

class PlatformControllerTests : public ::testing::Test
{
public:
	PlatformController c;
};

TEST_F(PlatformControllerTests, GetPlatformCount)
{
	EXPECT_EQ(MAX_PLATFORMS, c.getRowCount());
}

TEST_F(PlatformControllerTests, GetPlatform)
{
	EXPECT_EQ(C64, c.getPlatform(2));
}

TEST_F(PlatformControllerTests, GetName)
{
	EXPECT_STREQ("c64", c.getString(2).c_str());
}
