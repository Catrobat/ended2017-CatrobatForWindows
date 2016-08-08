//*********************************************************
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// This code is based on: 
// https://github.com/Microsoft/Windows-universal-samples.git
// Copyright (c) Microsoft. All rights reserved.
//*********************************************************

//
// WASAPICapture.h
//
#pragma once
#pragma comment(lib, "MFuuid.lib")
#pragma comment(lib, "Mfplat.lib")
#pragma comment(lib, "Mmdevapi.lib")

#include <Windows.h>
#include <wrl\implements.h>
#include <mfapi.h>
#include <AudioClient.h>
#include <mmdeviceapi.h>
#include "DeviceState.h"
#include "AudioData.h"
#include "pch.h"

using namespace Microsoft::WRL;
using namespace Windows::Media::Devices;
using namespace Windows::Storage::Streams;

#define AUDIO_FILE_NAME "WASAPIAudioCapture.wav"
#define FLUSH_INTERVAL_SEC 3
#define MILLISECONDS_TO_VISUALIZE 20
#define USE_AUDIO_CLIENT_2 // Undefine this if you are using Win 10 or higher. 
#define IS_LOW_LATENCY false // True does not work with IAudioClient2. 

namespace Catrobat_Player
{
    // Primary WASAPI Capture Class
    class WASAPICapture :
        public RuntimeClass< RuntimeClassFlags< ClassicCom >, FtmBase, IActivateAudioInterfaceCompletionHandler >
    {
    public:
        WASAPICapture();
        WASAPICapture(bool writeToFile);

        HRESULT InitializeAudioDeviceAsync();
        HRESULT StartCaptureAsync();
        HRESULT StopCaptureAsync();
        HRESULT FinishCaptureAsync();

        METHODASYNCCALLBACK(WASAPICapture, StartCapture, OnStartCapture);
        METHODASYNCCALLBACK(WASAPICapture, StopCapture, OnStopCapture);
        METHODASYNCCALLBACK(WASAPICapture, SampleReady, OnSampleReady);
        METHODASYNCCALLBACK(WASAPICapture, FinishCapture, OnFinishCapture);
        METHODASYNCCALLBACK(WASAPICapture, SendScopeData, OnSendScopeData);

        // IActivateAudioInterfaceCompletionHandler
        STDMETHOD(ActivateCompleted)(IActivateAudioInterfaceAsyncOperation *operation);

    private:
        ~WASAPICapture();

        HRESULT OnStartCapture(IMFAsyncResult* pResult);
        HRESULT OnStopCapture(IMFAsyncResult* pResult);
        HRESULT OnFinishCapture(IMFAsyncResult* pResult);
        HRESULT OnSampleReady(IMFAsyncResult* pResult);
        HRESULT OnSendScopeData(IMFAsyncResult* pResult);

        HRESULT CreateWAVFile();
        HRESULT FixWAVHeader();
        HRESULT OnAudioSampleRequested(Platform::Boolean IsSilence = false);
        HRESULT InitializeScopeData();
        HRESULT ProcessScopeData(BYTE* pData, DWORD cbBytes);

    private:
        Platform::String^   m_DeviceIdString;
        UINT32              m_BufferFrames;
        HANDLE              m_SampleReadyEvent;
        MFWORKITEM_KEY      m_SampleReadyKey;
        CRITICAL_SECTION    m_CritSec;
        DWORD               m_dwQueueID;
        bool                m_writeToFile;

        DWORD               m_cbHeaderSize;
        DWORD               m_cbDataSize;
        DWORD               m_cbFlushCounter;
        BOOL                m_fWriting;

        IRandomAccessStream^     m_ContentStream;
        IOutputStream^           m_OutputStream;
        DataWriter^              m_WAVDataWriter;
        WAVEFORMATEX            *m_MixFormat;
#ifdef USE_AUDIO_CLIENT_2
        IAudioClient2           *m_AudioClient;
#else
        IAudioClient3           *m_AudioClient;
#endif

        UINT32                  m_DefaultPeriodInFrames;
        UINT32                  m_FundamentalPeriodInFrames;
        UINT32                  m_MaxPeriodInFrames;
        UINT32                  m_MinPeriodInFrames;
        IAudioCaptureClient     *m_AudioCaptureClient;
        IMFAsyncResult          *m_SampleReadyAsyncResult;

        DeviceStateChangedEvent^       m_DeviceStateChanged;
        AudioDataReadyEvent^           m_AudioDataReady;

        Platform::Array<int, 1>^    m_SampleData;
        UINT32                      m_cSampleDataMax;
        UINT32                      m_cSampleDataFilled;

    };
}
