#include "commandhandler.h"
#include "UIManager.h"
#include <unistd.h>
#include <cassert>
#include <iostream>
#include <sstream>

#include <gflags/gflags.h>

DEFINE_string(name, "", "Name of the game");
DEFINE_string(category, "", "Game category (like RPG, FPS, ...)");
DEFINE_string(platform, "", "Platform (Windows, Dosbox, C64, ScummVM, Gameboy, Nintendo64, SuperNintendo, GameAndWatch, ZMachine)");
DEFINE_string(executable, "", "Path to the executable");
DEFINE_string(arguments, "", "Arguments for the executable");
DEFINE_string(iso, "", "Path to the ISO file");

void CommandHandler::setController(Controller* controller)
{
	_controller = controller;
}

void CommandHandler::setLauncher(Launcher* launcher)
{
	_launcher = launcher;
}

void CommandHandler::parse(int argc, char** argv)
{
	if (argc == 0)
	{
		UIManager ui;
		ui.setController(_controller);
		ui.start();
		return;
	}

	std::string command(*argv);

	if (command == "add") parseAdd(argc, argv);
	if (command == "remove" || command == "rm") parseRemove(argc, argv);
	if (command == "list" || command == "ls") parseList(argc, argv);
	if (command == "run") parseRun(argc, argv);
}

void CommandHandler::parseAdd(int argc, char** argv)
{
	_controller->addGame(FLAGS_name, FLAGS_category, Controller::getPlatformFromString(FLAGS_platform), FLAGS_executable, FLAGS_arguments, FLAGS_iso);
}

void CommandHandler::parseRemove(int argc, char** argv)
{
	if (argc != 2)
	{
		std::cout << "Usage: remove <index>" << std::endl;
		return;
	}

	std::stringstream ss(argv[1]);
	int index;
	ss >> index;
	_controller->removeGame(index);
}

void CommandHandler::parseList(int argc, char** argv)
{
	_controller->filter(FLAGS_category, Controller::getPlatformFromString(FLAGS_platform));

	std::cout << _controller->getRowCount() << " games" << std::endl;
	for (int i = 0; i < _controller->getRowCount(); ++i)
	{
		std::cout << _controller->data(i, Controller::INDEX) << " - "
		          << _controller->data(i, Controller::NAME) << " "
							<< "(" << _controller->data(i, Controller::CATEGORY) << ")"
							<< std::endl;
	}
}

void CommandHandler::parseRun(int argc, char** argv)
{
	if (argc != 2)
	{
		std::cout << "Usage: run <index>" << std::endl;
		return;
	}

	std::stringstream ss(argv[1]);
	int index;
	ss >> index;
	
	if (index < 0 || index > _controller->getRowCount())
	{
		std::cout << "Error: given index does not exist" << std::endl;
		return;
	}

	_launcher->launch(_controller->getPath(index), _controller->getPlatform(index), _controller->getISO(index));
}
