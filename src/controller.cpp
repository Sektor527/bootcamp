#include "controller.h"
#include <cassert>
#include <algorithm>
#include <sstream>
#include <iomanip>

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

int Controller::getRowCount() const
{
	return _games.size();
}

int Controller::getColumnCount() const
{
	return MAX_COLUMNS;
}

std::string Controller::data(int row, int column) const
{
	switch (column)
	{
		case INDEX: return getID(row);
		case NAME: return getName(row);
		case CATEGORY: return getCategory(row);
		case PLATFORM: return getStringFromPlatform(getPlatform(row));
	}
}

std::string Controller::getID(int index) const
{
	std::stringstream ss;
	ss << std::setfill(' ') << std::setw(4) << index + 1;
	return ss.str();
}

const std::string& Controller::getName(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getName();
}

const std::string& Controller::getCategory(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getCategory();
}

Platform Controller::getPlatform(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getPlatform();
}

std::string Controller::getPath(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getPath();
}

const std::string& Controller::getArgs(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getArgs();
}

std::string Controller::getISO(int index) const
{
	assert(index >= 0 && index < _games.size());
	return _games[index].getISO();
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
