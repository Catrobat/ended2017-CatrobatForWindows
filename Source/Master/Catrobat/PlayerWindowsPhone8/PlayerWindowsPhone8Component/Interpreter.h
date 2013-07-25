#pragma once

#include <map>
#include "Object.h"

class FormulaTree;

enum Childs {
	LeftChild, 
	RightChild, 
	LeftAndRightChild, 
	NoChild
};

class Interpreter
{
private:
	static Interpreter *__instance;

public:
	Interpreter(void);
	~Interpreter(void);
	static Interpreter *Instance();

	double EvaluateFormula(FormulaTree *tree, Object *object);

	int EvaluateFormulaToInt(FormulaTree *tree, Object *object);
	float EvaluateFormulaToFloat(FormulaTree *tree, Object *object);
	bool EvaluateFormulaToBool(FormulaTree *tree, Object *object);

	void ReadAcceleration();

private:
	// Sensors
	Windows::Devices::Sensors::Accelerometer^ m_accelerometer;
    Windows::Devices::Sensors::AccelerometerReading^ m_accReading;

    // HelperFunctions
    double InterpretOperator(FormulaTree *tree, Object *object);
	double InterpretFunction(FormulaTree *tree, Object *object);
	bool TestChilds(FormulaTree *tree, Childs childs);
	double CalculateMax(double value1, double value2);
	double CalculateMin(double value1, double value2);
	double CalculateRand(double value1, double value2);
	double RoundDoubleToInt(double value);
};

