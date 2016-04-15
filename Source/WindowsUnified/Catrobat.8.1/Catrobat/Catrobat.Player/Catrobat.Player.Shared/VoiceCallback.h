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
	void OnStreamEnd() { SetEvent(hBufferEndEvent); }

	//Unused methods are stubs
	void OnVoiceProcessingPassEnd() { }
	void OnVoiceProcessingPassStart(UINT32 SamplesRequired) {    }
	void OnBufferEnd(void * pBufferContext)    { }
	void OnBufferStart(void * pBufferContext) {    }
	void OnLoopEnd(void * pBufferContext) {    }
	void OnVoiceError(void * pBufferContext, HRESULT Error) { }
};