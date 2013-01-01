#pragma once
#include "game.h"
#include <string>
#include <vector>

class Controller
{
public:
	enum Columns
	{
		INDEX,
		NAME,
		CATEGORY,
		PLATFORM,

		MAX_COLUMNS
	};

	virtual void addGame(const std::string& name, const std::string& category, Platform platform, const std::string& path, const std::string& args, const std::string& ISO);
	virtual void removeGame(int index);

	virtual int getRowCount() const;
	virtual int getColumnCount() const;

	virtual std::string data(int row, int column) const;

	virtual std::string getID(int index) const;
	virtual const std::string& getName(int index) const;
	virtual const std::string& getCategory(int index) const;
	virtual Platform getPlatform(int index) const;
	virtual std::string getPath(int index) const;
	virtual const std::string& getArgs(int index) const;
	virtual std::string getISO(int index) const;

	static Platform getPlatformFromString(std::string platform);
	static std::string getStringFromPlatform(Platform platform);

	friend std::ostream& operator<< (std::ostream& out, const Controller& controller);
	friend std::istream& operator>> (std::istream& in, Controller& controller);

private:
	std::vector<Game> _games;
};
