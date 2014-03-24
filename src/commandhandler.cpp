#include "commandhandler.h"
#include "GameController.h"
#include "PlatformController.h"
#include "CategoryController.h"
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

void CommandHandler::setGameController(GameController* controller)
{
	_gameController = controller;
}

void CommandHandler::setPlatformController(PlatformController* controller)
{
	_platformController = controller;
}

void CommandHandler::setCategoryController(CategoryController* controller)
{
	_categoryController = controller;
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
		ui.setController(_gameController);
		ui.start();
		return;
	}

	std::string command(*argv);

	if (command == "add") parseAdd(argc, argv);
	if (command == "remove" || command == "rm") parseRemove(argc, argv);
	if (command == "list" || command == "ls") parseList(argc, argv);
	if (command == "lsp") parseListPlatform(argc, argv);
	if (command == "lsc") parseListCategory(argc, argv);
	if (command == "run") parseRun(argc, argv);
}

void CommandHandler::parseAdd(int argc, char** argv)
{
	_gameController->addGame(FLAGS_name, FLAGS_category, PlatformController::getPlatformFromString(FLAGS_platform), FLAGS_executable, FLAGS_arguments, FLAGS_iso);
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
	_gameController->removeGame(index);
}

void CommandHandler::parseList(int argc, char** argv)
{
	_gameController->filter(FLAGS_category, PlatformController::getPlatformFromString(FLAGS_platform));

	std::cout << _gameController->getRowCount() << " games" << std::endl;
	for (int i = 0; i < _gameController->getRowCount(); ++i)
	{
		std::cout << _gameController->data(i, GameController::INDEX) << " - "
		          << _gameController->data(i, GameController::NAME) << " "
							<< "(" << _gameController->data(i, GameController::CATEGORY) << ")"
							<< std::endl;
	}
}

void CommandHandler::parseListPlatform(int argc, char** argv)
{
	for (int i = 0; i < _platformController->getRowCount(); ++i)
	{
		std::cout << _platformController->data(i, PlatformController::NAME) << std::endl;
	}
}

void CommandHandler::parseListCategory(int argc, char** argv)
{
	for (int i = 0; i < _categoryController->getRowCount(); ++i)
	{
		std::cout << _categoryController->data(i, CategoryController::NAME) << std::endl;
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
	
	if (index < 0 || index > _gameController->getRowCount())
	{
		std::cout << "Error: given index does not exist" << std::endl;
		return;
	}

	_launcher->launch(_gameController->getPath(index), _gameController->getPlatform(index), _gameController->getISO(index));
}
