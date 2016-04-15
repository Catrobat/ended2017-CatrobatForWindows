#include <xaudio2.h>
#include "pch.h"
#include <iostream>

class VoiceCallback : public IXAudio2VoiceCallback
{
public:
	HANDLE hBufferEndEvent;
	VoiceCallback() : hBufferEndEvent(CreateEventExW(NULL, TEXT("PlayEvent"), 0, EVENT_ALL_ACCESS)){}
	~VoiceCallback(){ CloseHandle(hBufferEndEvent); }

	//Called when the voice has just finished playing a contiguous audio stream.
	STDMETHOD_(void, OnStreamEnd)() { SetEvent(hBufferEndEvent); }

	//Unused methods are stubs
	STDMETHOD_(void, OnVoiceProcessingPassEnd)() { }
	STDMETHOD_(void, OnVoiceProcessingPassStart)(UINT32 SamplesRequired) {    }
	STDMETHOD_(void, OnBufferEnd)(void * pBufferContext)    { }
	STDMETHOD_(void, OnBufferStart)(void * pBufferContext) {    }
	STDMETHOD_(void, OnLoopEnd)(void * pBufferContext) {    }
	STDMETHOD_(void, OnVoiceError)(void * pBufferContext, HRESULT Error) { }
};