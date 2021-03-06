#pragma once
#include "game.h"
#include <string>
#include <vector>

class ListFilter;

class GameController
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

	GameController();

	void setFilter(ListFilter* filter);

	virtual void addGame(const std::string& name, const std::string& category, Platform platform, const std::string& path, const std::string& args, const std::string& ISO);
	virtual void removeGame(int index);
	virtual void filter(const std::string& category, Platform platform);

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

	friend std::ostream& operator<< (std::ostream& out, const GameController& controller);
	friend std::istream& operator>> (std::istream& in, GameController& controller);

private:
	std::vector<Game> _games;

	ListFilter* _filter;
	std::string _categoryFilter;
	Platform _platformFilter;
};
