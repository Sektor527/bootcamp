#include "UIManager.h"
#include "Controller.h"
#include <curses.h>
#include <sstream>

UIManager::UIManager()
: _controller(NULL)
{
	initscr();
	raw();
	noecho();
	keypad(stdscr, true);
}

UIManager::~UIManager()
{
	endwin();
}

void UIManager::setController(Controller* controller)
{
	_controller = controller;
}

void UIManager::start()
{
	refresh();

	std::stringstream s;
	s << _controller->getRowCount() << " games:";

	mvprintw(1, 5, s.str().c_str());
	for (int i = 1; i <= _controller->getRowCount(); ++i)
	{
		mvprintw(i+2, 1, _controller->getID(i).c_str());
		mvprintw(i+2, 7, _controller->getName(i).c_str());
		mvprintw(i+2, 60, _controller->getCategory(i).c_str());
	}

	refresh();
	getch();

	clear();
	refresh();
}
