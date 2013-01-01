#include "commandhandler.h"
#include <unistd.h>
#include <cassert>
#include <iostream>
#include <sstream>

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
		std::cout << "Missing command" << std::endl << std::endl
			<< "Usage: bootcamp [command] [options]\n"
			<< "Possible commands:\n"
			<< "\tadd\t\tAdd a new game\n"
			<< "\tremove, rm\tRemove a game\n"
		  << "\tlist, ls\tShow list of games\n"
			<< "\trun\t\tRun a game\n"
			<< "\n\nUse 'bootcamp [command] -h' for specific options\n";

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
	std::string name;
	std::string category;
	Platform platform;
	std::string path;
	std::string args;
	std::string iso;

	int c;
	while ((c = getopt(argc, argv, "hn:p:c:x:a:i:")) != -1)
	{
		switch (c)
		{
			case 'h':
				std::cout << "Usage: bootcamp add -n <Name> -c <Category> -p <Platform> -x <Executable>\n\n"
					<< "\t-n\tName of the game\n"
					<< "\t-c\tGame category (like RPG, FPS, ...)\n"
					<< "\t-p\tPlatform. One of the following (case insensitive):\n"
					<< "\t\t\tWindows\n"
					<< "\t\t\tDosbox\n"
					<< "\t\t\tC64\n"
					<< "\t\t\tScummVM\n"
					<< "\t\t\tGameboy\n"
					<< "\t\t\tNintendo64\n"
					<< "\t\t\tSuperNintendo\n"
					<< "\t\t\tGameAndWatch\n"
					<< "\t\t\tZMachine\n"
					<< "\t-x\tPath to the executable\n";
				return;

			case 'n':
				name = optarg;
				break;
			case 'p':
				platform = Controller::getPlatformFromString(optarg);
				break;
			case 'c':
				category = optarg;
				break;
			case 'x':
				path = optarg;
				break;
			case 'a':
				args = optarg;
				break;
			case 'i':
				iso = optarg;
				break;
		}
	}

	_controller->addGame(name, category, platform, path, args, iso);
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
	if (argc != 1)
	{
		std::cout << "Usage: list" << std::endl;
		return;
	}

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
