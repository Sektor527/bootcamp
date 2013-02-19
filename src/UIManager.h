#pragma once

class Controller;

class UIManager
{
public:
	UIManager();
	~UIManager();

	void setController(Controller* controller);
	void start();

private:
	void processInput();
	void showGameList() const;

	int getWindowPosX() const;
	int getWindowPosY() const;
	int getWindowWidth() const;
	int getWindowHeight() const;

	Controller* _controller;

	int _scrollPos;

	bool _quit;
};
