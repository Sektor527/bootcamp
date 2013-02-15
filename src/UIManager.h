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

	Controller* _controller;

	int _scrollPos;
	int _maxRows;
	int _firstRowPosition;

	bool _quit;
};
