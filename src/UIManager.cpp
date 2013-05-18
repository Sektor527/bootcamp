#include "UIManager.h"
#include "GameController.h"
#include <curses.h>
#include <sstream>

UIManager::UIManager()
: _controller(NULL)
, _scrollPos(0)
, _quit(false)
{
	initscr();
	raw();
	noecho();
	keypad(stdscr, true);
}

UIManager::~UIManager()
{
	clear();
	refresh();
	endwin();
}

void UIManager::setController(GameController* controller)
{
	_controller = controller;
}

void UIManager::start()
{
	while(!_quit)
	{
		showGameList();
		processInput();
	}
}

void UIManager::processInput()
{
	char input = getch();
	switch (input)
	{
		case 'q': _quit = true; break;
		case 'k': _scrollPos = std::max(_scrollPos-1, 0); break;
		case 'j': _scrollPos = std::min(_scrollPos+1, _controller->getRowCount() - getWindowHeight()); break;
	}
}

void UIManager::showGameList() const
{
	mvprintw(getWindowPosY(), 3, _scrollPos > 0 ? "^" : " ");
	mvprintw(getWindowHeight() - 1, 3, _scrollPos < _controller->getRowCount() - getWindowHeight() ? "v" : " ");

	for (int i = 0; i < std::min(_controller->getRowCount(), getWindowHeight() - 2); ++i)
	{
		move(i+getWindowPosY()+1, 0);
		clrtoeol();
		
		mvprintw(i+getWindowPosY()+1,  1, _controller->data(i + _scrollPos, 0).c_str());
		mvprintw(i+getWindowPosY()+1,  7, _controller->data(i + _scrollPos, 1).c_str());
		mvprintw(i+getWindowPosY()+1, 60, _controller->data(i + _scrollPos, 2).c_str());
	}

	refresh();
}

int UIManager::getWindowPosX() const
{
	int x, y;
	getbegyx(stdscr, y, x);
	return x;
}

int UIManager::getWindowPosY() const
{
	int x, y;
	getbegyx(stdscr, y, x);
	return y;
}

int UIManager::getWindowWidth() const
{
	int x, y;
	getmaxyx(stdscr, y, x);
	return x;
}

int UIManager::getWindowHeight() const
{
	int x, y;
	getmaxyx(stdscr, y, x);
	return y;
}
