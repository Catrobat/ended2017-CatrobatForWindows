#pragma once
class Script
{
public:
	enum TypeOfScript
	{
		StartScript,
		BroadcastScript,
		WhenScript
	};
protected:
	Script(TypeOfScript scriptType);
	~Script();

private:
	TypeOfScript m_scriptType;
};

