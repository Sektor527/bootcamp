#include "UIManager.h"
#include <curses.h>

void UIManager::start()
{
	initscr();
	getch();
	endwin();
}
