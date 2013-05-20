#pragma once
#include "Game.h"
#include <string>

class PlatformController
{
public:
	enum Columns
	{
		INDEX,
		NAME,

		MAX_COLUMNS
	};

	virtual int getRowCount() const;
	virtual int getColumnCount() const;

	virtual std::string data(int row, int column) const;

	virtual Platform getPlatform(int index) const;
	virtual std::string getString(int index) const;

	static Platform getPlatformFromString(std::string platform);
	static std::string getStringFromPlatform(Platform platform);
};
