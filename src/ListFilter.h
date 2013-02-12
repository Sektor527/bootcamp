#pragma once
#include <vector>
#include "Game.h"

class ListFilter
{
public:
	void setList(const std::vector<Game>& list);
	virtual std::vector<int> getFilteredList(const std::string& category, Platform platform) const;

private:
	bool contains(std::string string, std::string substring) const;

	std::vector<Game> _list;
};
