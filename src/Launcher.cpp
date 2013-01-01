#include "launcher.h"
#include <cassert>
#include <windows.h>
#include <iostream>
#include <sstream>
#include <stdlib.h>
#include <unistd.h>

Launcher::Emulator Launcher::emulators[] = {
	{ UNDEFINED,     "",                                      "" },
	{ WINDOWS,       "",                                      "" },
	{ DOSBOX,        "bootcamp\\emulators\\dosbox\\",         "dosbox.exe" },
	{ C64,           "bootcamp\\emulators\\ccs64\\",          "ccs64.exe" },
	{ SCUMMVM,       "",                                      "" },
	{ GAMEBOY,       "bootcamp\\emulators\\gameboy\\",        "visualboyadvance.exe" },
  { NINTENDO64,    "bootcamp\\emulators\\nintendo 64\\",    "project64.exe" },
	{ SUPERNINTENDO, "bootcamp\\emulators\\super nintendo\\", "snes9x.exe" },
	{ GAMEANDWATCH,  "",                                      "" },
	{ ZMACHINE,      "bootcamp\\emulators\\winfrotz\\",       "frotz.exe" },
};

void Launcher::launch(const std::string& path, Platform platform, const std::string& iso)
{
	std::string newPath = convertPath(path);

	std::stringstream cmd;
	std::stringstream workingDir;
	
	cmd << "\"";
	switch (platform)
	{
		case WINDOWS:
		case GAMEANDWATCH:
		{
			cmd << newPath;
			workingDir << removeFileSpec(newPath);
			break;
		}

		case DOSBOX:
		{
			workingDir << emulators[platform].workingdir;

			cmd << emulators[platform].command << " \"" << newPath << "\" -exit -noconsole -fullscreen";
			if (!iso.empty())
				cmd << " -c \"imgmount d '" << convertPath(iso) << "' -t iso\"";

			break;
		}

		case C64:
		case GAMEBOY:
		case NINTENDO64:
		case SUPERNINTENDO:
		case ZMACHINE:
		{
			workingDir << emulators[platform].workingdir;
			cmd << emulators[platform].command << " \"" << newPath << "\"";
			break;
		}

		case SCUMMVM:
		default:
		{
			assert(false);
			break;
		}
	}
	cmd << "\"";

	chdir(workingDir.str().c_str());
	system(cmd.str().c_str());
}

std::string Launcher::convertPath(const std::string& path)
{
	char absPath[1024];
	_fullpath(absPath, path.c_str(), 1024);

  std::string newPath = std::string(absPath);

	size_t pos = 0;
	while((pos = newPath.find('/', pos)) != std::string::npos)
	{
		newPath.replace(pos, 1, "\\");
		pos++;
	}

	return newPath;
}

std::string Launcher::removeFileSpec(const std::string& path)
{
	size_t pos = path.find_last_of('\\');
	return path.substr(0,pos);
}
