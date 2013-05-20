#pragma once
#include "Launcher.h"

class GameController;
class PlatformController;

class CommandHandler
{
public:
	void setGameController(GameController* controller);
	void setPlatformController(PlatformController* controller);
	void setLauncher(Launcher* launcher);
	void parse(int argc, char** argv);

private:
	void parseAdd(int argc, char** argv);
	void parseRemove(int argc, char** argv);
	void parseList(int argc, char** argv);
	void parseListPlatform(int argc, char** argv);
	void parseRun(int argc, char** argv);

private:
	GameController* _gameController;
	PlatformController* _platformController;
	Launcher* _launcher;
};
