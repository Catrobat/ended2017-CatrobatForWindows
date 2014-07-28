//
// Player.xaml.h
// Declaration of the Player class
//

#pragma once

#include "Player.g.h"

namespace Catrobat_Player
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	[Windows::Foundation::Metadata::WebHostHidden]
	public ref class Player sealed
	{
	public:
		Player();

	protected:
		virtual void OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs^ e) override;
	};
}
