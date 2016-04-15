#pragma once

#include "pch.h"
#include <xaudio2.h>

using namespace std;

class SoundManager
{
private:
	static SoundManager *__instance; // shared pointer?
	shared_ptr<IXAudio2> xAudio;
	shared_ptr<IXAudio2MasteringVoice> masteringVoice;
	map<string, IXAudio2SourceVoice*> runningVoices;
public:
	SoundManager();
	~SoundManager();
	static SoundManager *Instance();
	static void deleteInstance();
	bool Play(string fileName);
	shared_ptr<IXAudio2> getXAudio();
	shared_ptr<IXAudio2MasteringVoice> getMasteringVoice();
};