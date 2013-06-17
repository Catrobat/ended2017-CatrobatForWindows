#pragma once

#include <map>
#include "Object.h"

class FormulaTree;

class Interpreter
{
private:
	static Interpreter *__instance;

public:
	Interpreter(void);
	~Interpreter(void);
	static Interpreter *Instance();
	
	int EvaluateFormulaToInt(FormulaTree *tree, Object *object);
	float EvaluateFormulaToFloat(FormulaTree *tree, Object *object);
	bool EvaluateFormulaToBool(FormulaTree *tree, Object *object);

	void ReadAcceleration();

private:
	// Sensors
	Windows::Devices::Sensors::Accelerometer^ m_accelerometer;
    Windows::Devices::Sensors::AccelerometerReading^ m_accReading;

    // HelperFunctions
    int interpretOperator(FormulaTree *tree, Object *object);
    float interpretOperatorFloat(FormulaTree *tree, Object *object);
};

