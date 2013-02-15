#include "UIManager.h"
#include "Controller.h"
#include <curses.h>
#include <sstream>

UIManager::UIManager()
: _controller(NULL)
, _scrollPos(0)
, _maxRows(50)
, _firstRowPosition(3)
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

void UIManager::setController(Controller* controller)
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
		case 'j': _scrollPos = std::min(_scrollPos+1, _controller->getRowCount() - _maxRows); break;
	}
}

void UIManager::showGameList() const
{
	std::stringstream s;
	s << _controller->getRowCount() << " games";
	mvprintw(1, 5, s.str().c_str());

	mvprintw(_firstRowPosition - 1, 3, _scrollPos > 0 ? "^" : " ");
	mvprintw(_firstRowPosition + _maxRows, 3, _scrollPos < _controller->getRowCount() - _maxRows ? "v" : " ");

	for (int i = 0; i < std::min(_controller->getRowCount(), _maxRows); ++i)
	{
		move(i+_firstRowPosition, 0);
		clrtoeol();
		
		mvprintw(i+_firstRowPosition,  1, _controller->data(i + _scrollPos, 0).c_str());
		mvprintw(i+_firstRowPosition,  7, _controller->data(i + _scrollPos, 1).c_str());
		mvprintw(i+_firstRowPosition, 60, _controller->data(i + _scrollPos, 2).c_str());
	}

	refresh();
}

