#pragma once

#include "game.h"

class Launcher
{
public:
	void setWorkingDir(const std::string& path);
	virtual void launch(const std::string& path, Platform platform, const std::string& iso);

	struct Emulator
	{
		Platform id;
		std::string workingdir;
		std::string command;
	};
	static Emulator emulators[];

private:
	std::string convertPath(const std::string& path);
	std::string removeFileSpec(const std::string& path);

private:
	std::string m_workingDir;
};
