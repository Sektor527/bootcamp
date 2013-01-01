#include "game.h"

#include <sstream>

Game::Game()
: _favorite(false), _platform(UNDEFINED)
{}

void Game::setName(const std::string& name)
{
	_name = name;
}

void Game::setPath(const std::string& path)
{
	_path = path;
}

void Game::setArgs(const std::string& args)
{
	_args = args;
}

void Game::setPlatform(Platform platform)
{
	_platform = platform;
}

void Game::setCategory(const std::string& category)
{
	_category = category;
}

void Game::setISO(const std::string& iso)
{
	_iso = iso;
}

void Game::setFavorite()
{
	_favorite = true;
}

void Game::unsetFavorite()
{
	_favorite = false;
}

const std::string& Game::getName() const
{
	return _name;
}

const std::string& Game::getPath() const
{
	return _path;
}

const std::string& Game::getArgs() const
{
	return _args;
}

Platform Game::getPlatform() const
{
	return _platform;
}

const std::string& Game::getCategory() const
{
	return _category;
}

const std::string& Game::getISO() const
{
	return _iso;
}

bool Game::isFavorite() const
{
	return _favorite;
}

bool Game::operator==(Game const & other) const
{
	return _path == other._path &&
	       _args == other._args &&
	       _platform == other._platform;
}

bool Game::operator!=(Game const & other) const
{
	return !(*this == other);
}

std::ostream& operator<< (std::ostream& out, const Game& game)
{
	out << game._name << "|"
	    << game._path << "|"
			<< game._args << "|"
			<< game._platform << "|"
			<< game._category << "|"
			<< game._iso << "|"
			<< game._favorite;

	return out;
}

std::istream& operator>> (std::istream& in, Game& game)
{
	std::string platform;
	std::string favorite;

	std::getline(in, game._name, '|');
	std::getline(in, game._path, '|');
	std::getline(in, game._args, '|');
	std::getline(in, platform, '|');
	std::getline(in, game._category, '|');
	std::getline(in, game._iso, '|');
	std::getline(in, favorite);

	// convert platform
	std::stringstream pstream(platform);
	int value;
	pstream >> value;
	game._platform = static_cast<Platform>(value);
	
	// convert favorite
	std::stringstream fstream(favorite);
	fstream >> game._favorite;

	return in;
}
