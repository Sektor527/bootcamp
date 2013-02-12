#include "controller.h"
#include "commandhandler.h"
#include "listfilter.h"
#include <fstream>

void load(Controller* c);
void save(Controller* c);

int main(int argc, char** argv)
{
	argc--; argv++;

	Launcher* l = new Launcher;
	Controller* c = new Controller;
	ListFilter* f = new ListFilter;

	load(c);

	c->setFilter(f);
	CommandHandler handler;
	handler.setController(c);
	handler.setLauncher(l);
	handler.parse(argc, argv);

	save(c);

	return 0;
}

void load(Controller* c)
{
	std::ifstream file("bootcamp\\bootlist_new.cfg");

	file >> *c;
}

void save(Controller* c)
{
	std::ofstream file("bootcamp\\bootlist_new.cfg");

	file << *c;
}
