#pragma once
#include <vector>
#include "Game.h"

class ListFilter
{
public:
	enum Criterium
	{
		CATEGORY
	};

	void setList(const std::vector<Game>& list);
	std::vector<Game> getFilteredList(Criterium criterium, const std::string& value) const;

private:
	std::vector<Game> _list;
};
