#pragma once

#include <map>
#include "Object.h"
#include "CompassProvider.h"
#include "InclinationProvider.h"
#include "AccelerometerProvider.h"
#include "LoudnessCapture.h"

enum Childs
{
	LeftChild,
	RightChild,
	LeftAndRightChild,
	NoChild
};

enum Inclination
{
	Pitch,
	Roll,
	Yaw
};

class FormulaTree;
class Interpreter
{
private:
	static Interpreter *__instance;

public:
	Interpreter(void);
	~Interpreter(void);
	static Interpreter *Instance();

	double EvaluateFormula(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);

	int EvaluateFormulaToInt(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);
	float EvaluateFormulaToFloat(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);
	bool EvaluateFormulaToBool(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);

	void ReadAcceleration();
	float ReadCompass();
	float ReadInclination(Inclination inclinationType);

private:
	// Sensors
	//Windows::Devices::Sensors::Accelerometer^ m_accelerometer;
	//Windows::Devices::Sensors::AccelerometerReading^ m_accReading;

	// HelperFunctions
	double InterpretOperator(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);
	double InterpretFunction(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);
	double InterpretSensor(std::shared_ptr<FormulaTree> tree, ProjectStructure::Object* object);
	bool TestChilds(std::shared_ptr<FormulaTree> tree, Childs childs);
	double CalculateMax(double value1, double value2);
	double CalculateMin(double value1, double value2);
	double CalculateRand(double value1, double value2);
	double CalculateModulo(double dividend, double divisor);
	double CalculateCosinus(double degree);
	double RoundDoubleToInt(double value);
	bool OnlyIntegerValues(double value1, double value2);

	std::shared_ptr<CompassProvider> m_compassProvider;
	InclinationProvider^ m_inclinationProvider;
	std::shared_ptr<AccelerometerProvider> m_accelerometerProvider;
	LoudnessCapture^ m_loudnessProvider;
};
