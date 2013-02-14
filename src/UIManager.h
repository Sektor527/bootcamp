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
	Controller* _controller;
};
