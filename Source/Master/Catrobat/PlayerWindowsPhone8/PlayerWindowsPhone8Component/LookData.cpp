#include "pch.h"
#include "LookData.h"


LookData::LookData(string filename, string name) :
	m_filename(filename), m_name(name)
{
}

string LookData::Name()
{
	return m_name;
}

string LookData::FileName()
{
	return m_filename;
}
