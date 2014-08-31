#pragma once

#include <map>
#include "Object.h"
#include "CompassProvider.h"
#include "InclinationProvider.h"
#include "AccelerometerProvider.h"
#include "LoudnessCapture.h"

class FormulaTree;

enum Childs {
    LeftChild, 
    RightChild, 
    LeftAndRightChild, 
    NoChild
};

enum Inclination {
	Pitch,
	Roll,
	Yaw
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
    float ReadCompass();
	float ReadInclination(Inclination inclinationType);

private:
    // Sensors
    //Windows::Devices::Sensors::Accelerometer^ m_accelerometer;
    //Windows::Devices::Sensors::AccelerometerReading^ m_accReading;

    // HelperFunctions
    double InterpretOperator(FormulaTree *tree, Object *object);
    double InterpretFunction(FormulaTree *tree, Object *object);
	double InterpretSensor(FormulaTree *tree, Object *object);
    bool TestChilds(FormulaTree *tree, Childs childs);
    double CalculateMax(double value1, double value2);
    double CalculateMin(double value1, double value2);
    double CalculateRand(double value1, double value2);
    double CalculateModulo(double dividend, double divisor);
    double CalculateCosinus(double degree);
    double RoundDoubleToInt(double value);
	bool OnlyIntegerValues(double value1, double value2);

    CompassProvider* m_compassProvider;
	InclinationProvider^ m_inclinationProvider;
	AccelerometerProvider* m_accelerometerProvider;
	LoudnessCapture^ m_loudnessProvider;
};
