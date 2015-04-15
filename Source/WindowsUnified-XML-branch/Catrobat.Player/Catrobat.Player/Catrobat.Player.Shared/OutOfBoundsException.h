#pragma once
#include "BaseException.h"

class OutOfBoundsException :
    public BaseException
{
public:
    OutOfBoundsException();
    virtual std::string GetName() override;
};

