#include "UIManager.h"
#include <curses.h>

UIManager::UIManager()
{
	initscr();
}

UIManager::~UIManager()
{
	endwin();
}

void UIManager::start()
{
	getch();
}
