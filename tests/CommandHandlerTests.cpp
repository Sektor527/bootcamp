#include "gmock/gmock.h"
#include <gflags/gflags.h>
#include "commandhandler.h"
#include "controller.h"
#include "launcher.h"
#include "game.h"
#include <unistd.h>

#pragma GCC diagnostic ignored "-Wwrite-strings"

using testing::_;
using testing::StrEq;
using testing::Return;
using testing::ReturnRef;

class MockController : public Controller
{
public:
	MOCK_CONST_METHOD0(getRowCount, int());
	MOCK_METHOD6(addGame, void(const std::string&, const std::string&, Platform, const std::string&, const std::string&, const std::string&));
	MOCK_METHOD1(removeGame, void(int));
	MOCK_CONST_METHOD1(getPath, std::string(int));
	MOCK_CONST_METHOD1(getPlatform, Platform(int));
	MOCK_CONST_METHOD1(getISO, std::string(int));
};

class MockLauncher : public Launcher
{
public:
	MOCK_METHOD3(launch, void(const std::string&, Platform, const std::string&));
};

class CommandHandlerAddTests : public testing::Test
{
public:
	void SetUp()
	{
		optind = 1;
		handler.setController(&controller);
	}

protected:
	CommandHandler handler;

	MockController controller;
};

TEST_F(CommandHandlerAddTests, Parse)
{
	EXPECT_CALL(controller, addGame(_,_,_,_,_,_));

	char* argv[] = { "add" };
	int argc = sizeof(argv)/sizeof(char*);

	char** argv2 = argv;
	google::ParseCommandLineFlags(&argc, &argv2, true);

	handler.parse(argc, argv);
}

TEST_F(CommandHandlerAddTests, ParseCorrectName)
{
	EXPECT_CALL(controller, addGame("name",_,_,_,_,_));

	char* argv[] = { "add", "--name=name" };
	int argc = sizeof(argv)/sizeof(char*);

	char** argv2 = argv;
	google::ParseCommandLineFlags(&argc, &argv2, true);

	handler.parse(argc, argv);
}

TEST_F(CommandHandlerAddTests, ParseCorrectNameWithMultipleWords)
{
	EXPECT_CALL(controller, addGame("name test",_,_,_,_,_));

	char* argv[] = { "add", "--name=name test" };
	int argc = sizeof(argv)/sizeof(char*);

	char** argv2 = argv;
	google::ParseCommandLineFlags(&argc, &argv2, true);

	handler.parse(argc, argv);
}

TEST_F(CommandHandlerAddTests, ParseCorrectCategory)
{
	EXPECT_CALL(controller, addGame(_,"category",_,_,_,_));

	char* argv[] = { "add", "--category=category" };
	int argc = sizeof(argv)/sizeof(char*);

	char** argv2 = argv;
	google::ParseCommandLineFlags(&argc, &argv2, true);

	handler.parse(argc, argv);
}

TEST_F(CommandHandlerAddTests, ParseCorrectPlatform)
{
	EXPECT_CALL(controller, addGame(_,_,C64,_,_,_));

	char* argv[] = { "add", "--platform=c64" };
	int argc = sizeof(argv)/sizeof(char*);

	char** argv2 = argv;
	google::ParseCommandLineFlags(&argc, &argv2, true);

	handler.parse(argc, argv);
}

TEST(CommandHandlerRemoveTests, Parse)
{
	MockController controller;
	EXPECT_CALL(controller, removeGame(_));

	CommandHandler handler;
	handler.setController(&controller);
	char* argv[] = { "remove", "1" };
	int argc = sizeof(argv)/sizeof(char*);
	handler.parse(argc, argv);
}

TEST(CommandHandlerRemoveTests, ParseCorrectParameter)
{
	MockController controller;
	EXPECT_CALL(controller, removeGame(27));

	CommandHandler handler;
	handler.setController(&controller);
	char* argv[] = { "remove", "27" };
	int argc = sizeof(argv)/sizeof(char*);
	handler.parse(argc, argv);
}

TEST(CommandHandlerRunTests, Parse)
{
	MockController controller;
	MockLauncher launcher;

	ON_CALL(controller, getRowCount()).WillByDefault(Return(100));
	ON_CALL(controller, getPath(_)).WillByDefault(Return("abc"));
	ON_CALL(controller, getPlatform(_)).WillByDefault(Return(DOSBOX));
	ON_CALL(controller, getISO(_)).WillByDefault(Return("iso"));

	EXPECT_CALL(launcher, launch("abc", DOSBOX, "iso"));

	CommandHandler handler;
	handler.setController(&controller);
	handler.setLauncher(&launcher);
	char* argv[] = { "run", "27" };
	int argc = sizeof(argv)/sizeof(char*);
	handler.parse(argc,argv);
}

TEST(CommandHandlerRunTests, PathWithSpaces)
{
	MockController controller;
	MockLauncher launcher;

	ON_CALL(controller, getRowCount()).WillByDefault(Return(100));
	ON_CALL(controller, getPath(_)).WillByDefault(Return("path to/my/game executable.exe"));
	ON_CALL(controller, getPlatform(_)).WillByDefault(Return(DOSBOX));
	ON_CALL(controller, getISO(_)).WillByDefault(Return("iso"));

	EXPECT_CALL(launcher, launch("path to/my/game executable.exe", _, _));

	CommandHandler handler;
	handler.setController(&controller);
	handler.setLauncher(&launcher);
	char* argv[] = { "run", "27" };
	int argc = sizeof(argv)/sizeof(char*);
	handler.parse(argc,argv);
}

TEST(CommandHandlerRunTests, IsoWithSpaces)
{
	MockController controller;
	MockLauncher launcher;

	ON_CALL(controller, getRowCount()).WillByDefault(Return(100));
	ON_CALL(controller, getPath(_)).WillByDefault(Return("abc"));
	ON_CALL(controller, getPlatform(_)).WillByDefault(Return(DOSBOX));
	ON_CALL(controller, getISO(_)).WillByDefault(Return("path to/my/iso file.cue"));

	EXPECT_CALL(launcher, launch(_, _, "path to/my/iso file.cue"));

	CommandHandler handler;
	handler.setController(&controller);
	handler.setLauncher(&launcher);
	char* argv[] = { "run", "27" };
	int argc = sizeof(argv)/sizeof(char*);
	handler.parse(argc,argv);
}
