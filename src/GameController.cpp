#include "GameController.h"
#include "PlatformController.h"
#include "ListFilter.h"
#include <cassert>
#include <sstream>
#include <iomanip>

GameController::GameController()
: _filter(NULL)
, _categoryFilter("")
, _platformFilter(UNDEFINED)
{
}

void GameController::setFilter(ListFilter* filter)
{
	_filter = filter;
}

void GameController::addGame(const std::string& name, const std::string& category, Platform platform, const std::string& path, const std::string& args, const std::string& ISO)
{
	Game g;
	g.setName(name);
	g.setCategory(category);
	g.setPlatform(platform);
	g.setPath(path);
	g.setArgs(args);
	g.setISO(ISO);

	_games.push_back(g);
}

void GameController::removeGame(int index)
{
	_games.erase(_games.begin() + index);
}

void GameController::filter(const std::string& category, Platform platform)
{
	_categoryFilter = category;
	_platformFilter = platform;
}

int GameController::getRowCount() const
{
	assert(_filter);

	_filter->setList(_games);
	std::vector<int> filteredList = _filter->getFilteredList(_categoryFilter, _platformFilter);

	return filteredList.size();
}

int GameController::getColumnCount() const
{
	return MAX_COLUMNS;
}

std::string GameController::data(int row, int column) const
{
	assert(_filter);

	_filter->setList(_games);
	std::vector<int> filteredList = _filter->getFilteredList(_categoryFilter, _platformFilter);
	assert(row >= 0 && row < filteredList.size());

	switch (column)
	{
		case INDEX: return getID(filteredList[row]+1);
		case NAME: return getName(filteredList[row]+1);
		case CATEGORY: return getCategory(filteredList[row]+1);
		case PLATFORM: return PlatformController::getStringFromPlatform(getPlatform(filteredList[row]+1));
	}
}

std::string GameController::getID(int index) const
{
	std::stringstream ss;
	ss << std::setfill(' ') << std::setw(4) << index;
	return ss.str();
}

const std::string& GameController::getName(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getName();
}

const std::string& GameController::getCategory(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getCategory();
}

Platform GameController::getPlatform(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getPlatform();
}

std::string GameController::getPath(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getPath();
}

const std::string& GameController::getArgs(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getArgs();
}

std::string GameController::getISO(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getISO();
}

std::ostream& operator<< (std::ostream& out, const GameController& controller)
{
	for (std::vector<Game>::const_iterator it = controller._games.begin(); it != controller._games.end(); ++it)
	{
		out << *it << std::endl;
	}
}

std::istream& operator>> (std::istream& in, GameController& controller)
{
	while (in.good())
	{
		Game g;
		in >> g;

		if (!g.getName().empty())
			controller._games.push_back(g);
	}
}
