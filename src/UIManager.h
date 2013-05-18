#pragma once

class GameController;

class UIManager
{
public:
	UIManager();
	~UIManager();

	void setController(GameController* controller);
	void start();

private:
	void processInput();
	void showGameList() const;

	int getWindowPosX() const;
	int getWindowPosY() const;
	int getWindowWidth() const;
	int getWindowHeight() const;

	GameController* _controller;

	int _scrollPos;

	bool _quit;
};
