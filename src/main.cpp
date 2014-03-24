#include "GameController.h"
#include "PlatformController.h"
#include "CategoryController.h"
#include "commandhandler.h"
#include "listfilter.h"
#include <fstream>
#include <iostream>
#include <gflags/gflags.h>

std::string extractDirectory(const std::string& path);
void load(const std::string& path, GameController* c);
void save(const std::string& path, GameController* c);

int main(int argc, char** argv)
{
	google::ParseCommandLineFlags(&argc, &argv, true);

	const std::string workingdir = extractDirectory(argv[0]);
	const std::string configPath = workingdir + "bootcamp\\bootlist_new.cfg";

	argc--; argv++;

	Launcher l;
	l.setWorkingDir(workingdir);

	ListFilter f;

	GameController gc;
	gc.setFilter(&f);

	PlatformController pc;

	CategoryController cc;
	cc.setGameController(&gc);

	load(configPath, &gc);

	CommandHandler handler;
	handler.setGameController(&gc);
	handler.setPlatformController(&pc);
	handler.setCategoryController(&cc);
	handler.setLauncher(&l);
	handler.parse(argc, argv);

	save(configPath, &gc);

	return 0;
}

std::string extractDirectory(const std::string& path)
{
	size_t lastseparator = path.find_last_of('\\');
	return path.substr(0, lastseparator+1);
}

void load(const std::string& path, GameController* c)
{
	std::ifstream file(path.c_str());

	file >> *c;
}

void save(const std::string& path, GameController* c)
{
	std::ofstream file(path.c_str());

	file << *c;
}
