#include "PlatformController.h"
#include <cassert>
#include <sstream>
#include <algorithm>

int PlatformController::getRowCount() const
{
	return MAX_PLATFORMS;
}

int PlatformController::getColumnCount() const
{
	return 2;
}

std::string PlatformController::data(int row, int column) const
{
	assert(row >= 0 && row < getRowCount());

	switch (column)
	{
		case INDEX: 
		{
			std::stringstream str;
			str << row;
			return str.str();
		}
		case NAME: return getString(row);
	}
}

Platform PlatformController::getPlatform(int index) const
{
	return (Platform)(index + 1);
}

std::string PlatformController::getString(int index) const
{
	return getStringFromPlatform(getPlatform(index));
}

Platform PlatformController::getPlatformFromString(std::string platform)
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

std::string PlatformController::getStringFromPlatform(Platform platform)
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

