#include "ListFilter.h"
#include <algorithm>
#include <iostream>

void ListFilter::setList(const std::vector<Game>& list)
{
	_list = list;
}

std::vector<int> ListFilter::getFilteredList(const std::string& category, Platform platform) const
{
	std::vector<int> filteredList;

	int index = 0;
	for (std::vector<Game>::const_iterator it = _list.begin(); it != _list.end(); ++index, ++it)
	{
		bool match = true;

		if (!category.empty())
		{
			match &= contains(it->getCategory(), category);
		}

		if (platform != UNDEFINED)
		{
			match &= it->getPlatform() == platform;
		}

		if (match)
			filteredList.push_back(index);
	}

	return filteredList;
}

bool ListFilter::contains(std::string string, std::string substring) const
{
	std::transform(string.begin(), string.end(), string.begin(), ::tolower);
	std::transform(substring.begin(), substring.end(), substring.begin(), ::tolower);

	return string.find(substring) != std::string::npos;
}
