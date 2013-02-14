#include "controller.h"
#include "ListFilter.h"
#include <cassert>
#include <algorithm>
#include <sstream>
#include <iomanip>

Controller::Controller()
: _filter(NULL)
, _categoryFilter("")
, _platformFilter(UNDEFINED)
{
}

void Controller::setFilter(ListFilter* filter)
{
	_filter = filter;
}

void Controller::addGame(const std::string& name, const std::string& category, Platform platform, const std::string& path, const std::string& args, const std::string& ISO)
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

void Controller::removeGame(int index)
{
	_games.erase(_games.begin() + index);
}

void Controller::filter(const std::string& category, Platform platform)
{
	_categoryFilter = category;
	_platformFilter = platform;
}

int Controller::getRowCount() const
{
	assert(_filter);

	_filter->setList(_games);
	std::vector<int> filteredList = _filter->getFilteredList(_categoryFilter, _platformFilter);

	return filteredList.size();
}

int Controller::getColumnCount() const
{
	return MAX_COLUMNS;
}

std::string Controller::data(int row, int column) const
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
		case PLATFORM: return getStringFromPlatform(getPlatform(filteredList[row]+1));
	}
}

std::string Controller::getID(int index) const
{
	std::stringstream ss;
	ss << std::setfill(' ') << std::setw(4) << index;
	return ss.str();
}

const std::string& Controller::getName(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getName();
}

const std::string& Controller::getCategory(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getCategory();
}

Platform Controller::getPlatform(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getPlatform();
}

std::string Controller::getPath(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getPath();
}

const std::string& Controller::getArgs(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getArgs();
}

std::string Controller::getISO(int index) const
{
	assert(index >= 1 && index <= _games.size());
	return _games[index-1].getISO();
}

Platform Controller::getPlatformFromString(std::string platform)
{
	std::transform(platform.begin(), platform.end(), platform.begin(), ::tolower);

	if (platform == "windows")       return WINDOWS;
	if (platform == "dosbox")        return DOSBOX;
	if (platform == "c64")           return C64;
	if (platform == "scummvm")       return SCUMMVM;
	if (platform == "gameboy")       return GAMEBOY;
	if (platform == "nintendo64")    return NINTENDO64;
	if (platform == "supernintendo") return SUPERNINTENDO;
	if (platform == "gameandwatch")  return GAMEANDWATCH;
	if (platform == "zmachine")      return ZMACHINE;

	return UNDEFINED;
}

std::string Controller::getStringFromPlatform(Platform platform)
{
	switch (platform)
	{
		case WINDOWS:       return "windows";
		case DOSBOX:        return "dosbox";
		case C64:           return "c64";
		case SCUMMVM:       return "scummvm";
		case GAMEBOY:       return "gameboy";
		case NINTENDO64:    return "nintendo64";
		case SUPERNINTENDO: return "supernintendo";
		case GAMEANDWATCH:  return "gameandwatch";
		case ZMACHINE:      return "zmachine";
	}
}

std::ostream& operator<< (std::ostream& out, const Controller& controller)
{
	for (std::vector<Game>::const_iterator it = controller._games.begin(); it != controller._games.end(); ++it)
	{
		out << *it << std::endl;
	}
}

std::istream& operator>> (std::istream& in, Controller& controller)
{
	while (in.good())
	{
		Game g;
		in >> g;

		if (!g.getName().empty())
			controller._games.push_back(g);
	}
}
