#pragma once

#include <map>
#include "Object.h"
#include "CompassProvider.h"
#include "InclinationProvider.h"
#include "AccelerometerProvider.h"
#include "LoudnessCapture.h"

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

class FormulaTree;
class Interpreter
{
private:
    static Interpreter *__instance;

public:
    Interpreter(void);
    ~Interpreter(void);
    static Interpreter *Instance();

    double EvaluateFormula(FormulaTree *tree, std::shared_ptr<Object> object);

    int EvaluateFormulaToInt(FormulaTree *tree, std::shared_ptr<Object> object);
    float EvaluateFormulaToFloat(FormulaTree *tree, std::shared_ptr<Object> object);
    bool EvaluateFormulaToBool(FormulaTree *tree, std::shared_ptr<Object> object);

    void ReadAcceleration();
    float ReadCompass();
	float ReadInclination(Inclination inclinationType);

private:
    // Sensors
    //Windows::Devices::Sensors::Accelerometer^ m_accelerometer;
    //Windows::Devices::Sensors::AccelerometerReading^ m_accReading;

    // HelperFunctions
    double InterpretOperator(FormulaTree *tree, std::shared_ptr<Object> object);
    double InterpretFunction(FormulaTree *tree, std::shared_ptr<Object> object);
	double InterpretSensor(FormulaTree *tree, std::shared_ptr<Object> object);
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
