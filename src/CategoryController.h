#pragma once
#include "GameController.h"

class CategoryController
{
public:
	enum Columns
	{
		INDEX,
		NAME,

		MAX_COLUMNS
	};

	CategoryController();

	void setGameController(GameController* gc);

	virtual int getRowCount() const;
	virtual int getColumnCount() const;
	
	virtual std::string data(int row, int  column) const;

	virtual std::string getID(int index) const;
	virtual std::string getName(int index) const;

private:
	GameController* _gameController;
};
