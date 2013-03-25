#include "controller.h"
#include "commandhandler.h"
#include "listfilter.h"
#include <fstream>
#include <iostream>

std::string extractDirectory(const std::string& path);
void load(const std::string& path, Controller* c);
void save(const std::string& path, Controller* c);

int main(int argc, char** argv)
{
	const std::string workingdir = extractDirectory(argv[0]);
	const std::string configPath = workingdir + "bootcamp\\bootlist_new.cfg";

	argc--; argv++;

	Launcher* l = new Launcher;
	Controller* c = new Controller;
	ListFilter* f = new ListFilter;

	load(configPath, c);

	c->setFilter(f);
	l->setWorkingDir(workingdir);
	CommandHandler handler;
	handler.setController(c);
	handler.setLauncher(l);
	handler.parse(argc, argv);

	//save(configPath, c);

	return 0;
}

std::string extractDirectory(const std::string& path)
{
	size_t lastseparator = path.find_last_of('\\');
	return path.substr(0, lastseparator+1);
}

void load(const std::string& path, Controller* c)
{
	std::ifstream file(path.c_str());

	file >> *c;
}

void save(const std::string& path, Controller* c)
{
	std::ofstream file(path.c_str());

	file << *c;
}
