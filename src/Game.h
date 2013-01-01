#pragma once

#include <string>

enum Platform
{
	UNDEFINED,

	WINDOWS,
	DOSBOX,
	C64,
	SCUMMVM,
	GAMEBOY,
	NINTENDO64,
	SUPERNINTENDO,
	GAMEANDWATCH,
	ZMACHINE
};

class Game
{
public:
	Game();

	void setName(const std::string& name);
	void setPath(const std::string& path);
	void setArgs(const std::string& args);
	void setPlatform(Platform platform);
	void setCategory(const std::string& category);
	void setISO(const std::string& iso);
	void setFavorite();
	void unsetFavorite();
	
	const std::string& getName() const;
	const std::string& getPath() const;
	const std::string& getArgs() const;
	Platform getPlatform() const;
	const std::string& getCategory() const;
	const std::string& getISO() const;
	bool isFavorite() const;

	bool operator==(Game const & other) const;
	bool operator!=(Game const & other) const;

	friend std::ostream& operator<< (std::ostream& out, const Game& game);
	friend std::istream& operator>> (std::istream& in, Game& game);

private:
	std::string _name;
	std::string _path;
	std::string _args;
	Platform _platform;
	std::string _category;
	std::string _iso;
	bool _favorite;
};
