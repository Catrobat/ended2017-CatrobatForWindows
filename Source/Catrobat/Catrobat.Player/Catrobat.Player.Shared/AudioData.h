//*********************************************************
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//*********************************************************
#pragma once
#include <Windows.h>

namespace Catrobat_Player
{
	public ref class AudioDataReadyEventArgs sealed
	{
	internal:
		AudioDataReadyEventArgs(Platform::Array<int, 1>^ data, UINT32 size) :
            m_DataArray(data),
			m_Size(size)
		{};

		property Platform::Array<int, 1>^ PcmData
		{
			Platform::Array<int, 1>^ get() { return m_DataArray; }
		};

		property UINT32 Size
		{
			UINT32 get() { return m_Size; }
		};

	private:
		Platform::Array<int, 1>^     m_DataArray;
		UINT32                      m_Size;
	};

	// AudioDataReady delegate
	public delegate void AudioDataReadyHandler(Platform::Object^ sender, AudioDataReadyEventArgs^ e);

	// AudioDataReady Event
	public ref class AudioDataReadyEvent sealed
	{
	public:
		AudioDataReadyEvent() {};

	internal:
		void SendEvent(Object^ obj, Platform::Array<int, 1>^ points, UINT32 size)
		{
			AudioDataReadyEventArgs^ e = ref new AudioDataReadyEventArgs(points, size);
			AudioDataReady(obj, e);
		}

	public:
		static event AudioDataReadyHandler^    AudioDataReady;
	};
}