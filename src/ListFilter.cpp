#include "ListFilter.h"

void ListFilter::setList(const std::vector<Game>& list)
{
	_list = list;
}

std::vector<Game> ListFilter::getFilteredList(Criterium criterium, const std::string& value) const
{
	std::vector<Game> filteredList;

	for (std::vector<Game>::const_iterator it = _list.begin(); it != _list.end(); ++it)
	{
		filteredList.push_back(*it);
	}

	return filteredList;
}
