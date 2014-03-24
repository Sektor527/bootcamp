#include <algorithm>
#include <cassert>
#include <sstream>
#include <iomanip>
#include "CategoryController.h"

CategoryController::CategoryController()
: _gameController(NULL)
{
}

void CategoryController::setGameController(GameController* gc)
{
	_gameController = gc;
}

int CategoryController::getRowCount() const
{
	std::vector<std::string> categories;

	for (int i = 1; i <= _gameController->getRowCount(); ++i)
	{
		if (std::find(categories.begin(), categories.end(), _gameController->getCategory(i)) == categories.end())
			categories.push_back(_gameController->getCategory(i));
	}

	return categories.size();
}

int CategoryController::getColumnCount() const
{
	return MAX_COLUMNS;
}

std::string CategoryController::data(int row, int  column) const
{
	switch (column)
	{
		case INDEX: return getID(row+1);
		case NAME: return getName(row+1);
	}
}

std::string CategoryController::getID(int index) const
{
	std::stringstream ss;
	ss << std::setfill(' ') << std::setw(4) << index;
	return ss.str();
}

std::string CategoryController::getName(int index) const
{
	assert(index >= 1 && index <= getRowCount());

	std::vector<std::string> categories;

	for (int i = 1; i <= _gameController->getRowCount(); ++i)
	{
		if (std::find(categories.begin(), categories.end(), _gameController->getCategory(i)) == categories.end())
			categories.push_back(_gameController->getCategory(i));
	}

	return categories[index-1];
}
