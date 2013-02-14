#include "UIManager.h"
#include "Controller.h"
#include <curses.h>

UIManager::UIManager()
: _controller(NULL)
{
	initscr();
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
	getch();
}
