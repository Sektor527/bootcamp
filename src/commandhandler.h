#pragma once
#include "controller.h"
#include "Launcher.h"

class CommandHandler
{
public:
	void setController(Controller* controller);
	void setLauncher(Launcher* launcher);
	void parse(int argc, char** argv);

private:
	void parseAdd(int argc, char** argv);
	void parseRemove(int argc, char** argv);
	void parseList(int argc, char** argv);
	void parseRun(int argc, char** argv);

private:
	Controller* _controller;
	Launcher* _launcher;
};
