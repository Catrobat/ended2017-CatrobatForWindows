#include "pch.h"
#include "Catrobat.PlayerMain.h"
#include "Common\DirectXHelper.h"
#include "ProjectDaemon.h"
#include "Constants.h"

using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::System::Threading;
using namespace Concurrency;
using namespace Platform;
using namespace ProjectStructure;
using namespace Catrobat_Player::NativeComponent;

namespace Catrobat_Player
{
	/// Loads and initializes application assets when the application is loaded
	Catrobat_PlayerMain::Catrobat_PlayerMain(
		const std::shared_ptr<DX::DeviceResources>& deviceResources,
		Page^ playerPage, String^ projectName) :
		m_deviceResources(deviceResources),
		m_pointerLocationX(0.0f),
		m_state(PlayerState::Init)
	{
		// Register to be notified if the Device is lost or recreated
		m_deviceResources->RegisterDeviceNotify(this);

		LoadProject(projectName, playerPage);
	/*	m_fpsTextRenderer = std::unique_ptr<SampleFpsTextRenderer>(new SampleFpsTextRenderer(
			m_deviceResources));*/

		// TODO: Change the timer settings if you want something other than the default variable 
		// timestep mode. e.g. for 60 FPS fixed timestep update logic, call:
		/*
		m_timer.SetFixedTimeStep(true);
		m_timer.SetTargetElapsedSeconds(1.0 / 60);
		*/
	}

	Catrobat_PlayerMain::~Catrobat_PlayerMain()
	{
		// Deregister device notification
		m_deviceResources->RegisterDeviceNotify(nullptr);
	}

	/// Initialize Project loading and parsing
	void Catrobat_PlayerMain::LoadProject(String^ projectName, Page^ playerPage)
	{
		if (ProjectDaemon::Instance()->CreateNativeProject(projectName))
		{
			m_basic2dRenderer = std::unique_ptr<Basic2DRenderer>(new Basic2DRenderer(
				m_deviceResources));
			ProcessXamlPageContent(playerPage);
			m_state = PlayerState::Active;
			StartRenderLoop();
		}
	}

	void Catrobat_PlayerMain::ProcessXamlPageContent(Page^ playerPage)
	{
		// Get the CommandBar from the Player's XAML page
		m_appBar = (CommandBar^) playerPage->BottomAppBar;

		// Get the axis button from the CommandBar
		m_btnAxes = (AppBarButton^) m_appBar->PrimaryCommands->GetAt(
			Constants::XAMLPage::BtnAxisPosition);

		// Get the Grid which contains the axes from the Player's XAML page & set the axes' values
		m_gridAxes = FindChildControl<Grid>((DependencyObject^) playerPage->Content,
			Constants::XAMLPage::GridAxesName);

		int projectScreenHeight = ProjectDaemon::Instance()->GetProject()->GetHeader()->GetScreenHeight();
		int projectScreendWidth = ProjectDaemon::Instance()->GetProject()->GetHeader()->GetScreenWidth();

		// horizontal values
		(FindChildControl<TextBlock>((DependencyObject^) m_gridAxes,
			Constants::XAMLPage::GridAxesXLeftName))
			->Text = "-" + (projectScreendWidth / 2).ToString();
		(FindChildControl<TextBlock>((DependencyObject^) m_gridAxes,
			Constants::XAMLPage::GridAxesXRightName))
			->Text = (projectScreendWidth / 2).ToString();

		// vertical values
		(FindChildControl<TextBlock>((DependencyObject^) m_gridAxes,
			Constants::XAMLPage::GridAxesYTopName))
			->Text = (projectScreenHeight / 2).ToString();
		(FindChildControl<TextBlock>((DependencyObject^) m_gridAxes,
			Constants::XAMLPage::GridAxesYBottomName))
			->Text = "-" + (projectScreenHeight / 2).ToString();
	}

	/// Updates application state when the window size changes (e.g. device orientation change)
	void Catrobat_PlayerMain::CreateWindowSizeDependentResources()
	{
		m_basic2dRenderer->CreateWindowSizeDependentResources();
	}

	void Catrobat_PlayerMain::StartRenderLoop()
	{
		// If the animation render loop is already running then do not start another thread
		if (m_renderLoopWorker != nullptr && m_renderLoopWorker->Status ==
			Windows::Foundation::AsyncStatus::Started)
		{
			return;
		}

		// Create a task that will be run on a background thread
		auto workItemHandler = ref new WorkItemHandler([this](
			Windows::Foundation::IAsyncAction ^ action)
		{
			// Calculate the updated frame and render once per vertical blanking interval
			while (action->Status == Windows::Foundation::AsyncStatus::Started)
			{
				critical_section::scoped_lock lock(m_criticalSection);
				Update();
				if (Render())
				{
					m_deviceResources->Present();
				}
			}
		});

		// Run task on a dedicated high priority background thread
		m_renderLoopWorker = ThreadPool::RunAsync(workItemHandler, WorkItemPriority::High,
			WorkItemOptions::TimeSliced);
	}

	void Catrobat_PlayerMain::StopRenderLoop()
	{
		m_state = PlayerState::Pause;
		m_renderLoopWorker->Cancel();
	}

	void Catrobat_PlayerMain::PointerPressed(D2D1_POINT_2F point)
	{
		if (m_state == PlayerState::Active)
		{
			m_basic2dRenderer->PointerPressed(point);
		}
	}

	bool Catrobat_PlayerMain::HardwareBackButtonPressed()
	{
		if (m_state == PlayerState::Active)
		{
			// Player is in active play mode, so hitting the hardware back button
			// will cause the game to pause and show the user the command bar --> return false

			critical_section::scoped_lock lock(m_criticalSection);

			StopRenderLoop();
			m_appBar->Visibility = Visibility::Visible;
			return false;
		}
		else
		{
			// Player is in pause or init mode, so hitting the hardware back button
			// will cause the player to terminate and bring the user back to the IDE --> return true

			return true;
		}
	}

	void Catrobat_PlayerMain::RestartButtonClicked()
	{
		critical_section::scoped_lock lock(m_criticalSection);
		if (ProjectDaemon::Instance()->RestartProject())
		{
			m_basic2dRenderer->Initialize();
			CreateWindowSizeDependentResources();
			m_appBar->Visibility = Visibility::Collapsed;
			m_state = PlayerState::Active;
			StartRenderLoop();
		}
	}

	void Catrobat_PlayerMain::ResumeButtonClicked()
	{
		critical_section::scoped_lock lock(m_criticalSection);

		m_appBar->Visibility = Visibility::Collapsed;
		m_state = PlayerState::Active;
		StartRenderLoop();
	}

	void Catrobat_PlayerMain::ThumbnailButtonClicked()
	{
		// TODO implement me: get current screen image 
		//                    --> save the current image as "manual_screenshot.png" in the 
		//                        program's applicationf folder 
		//                    --> notify the user, if it was successful

		/*
		// Create a template for the toast
		ToastTemplateType toastTemplate = ToastTemplateType::ToastText02;
		XmlDocument^ toastXml = ToastNotificationManager::GetTemplateContent(toastTemplate);

		//
		XmlNodeList^ toastTextElements = toastXml->GetElementsByTagName("text");
		// TODO get this string from a multilingual tool/file or switch it completely to the IDE
		toastTextElements->Item(0)->InnerText = "Successfully set a new thumbnail :)";
		ToastNotification^ toast = ref new ToastNotification(toastXml);
		// TODO check if this should maybe set somewhere in a global variable space?? or switch it
		// completely to the IDE
		Platform::String^ toastTag = ref new Platform::String(L"Thumbnail");
		toast->Tag = toastTag;

		// Show the toast and remove it from the action center
		int seconds = 1;
		auto cal = ref new Windows::Globalization::Calendar();
		cal->AddSeconds(seconds);
		toast->ExpirationTime = cal->GetDateTime();
		ToastNotificationManager::CreateToastNotifier()->Show(toast);
		//ToastNotificationManager::History->Remove(toastTag);
		*/
	}

	void Catrobat_PlayerMain::AxesButtonClicked(bool showAxes, Platform::String^ label)
	{
		if (showAxes == true)
		{
			m_gridAxes->Visibility = Visibility::Visible;
			m_btnAxes->Label = label;
		}
		else
		{
			m_gridAxes->Visibility = Visibility::Collapsed;
			m_btnAxes->Label = label;
		}
	}

	void Catrobat_PlayerMain::ScreenshotButtonClicked()
	{
		// TODO implement me: copy the current screen image to the phone's screenshot picture folder
		//                    & notify the user with a toast, if it was succesful
	}

	/// Updates the application state once per frame
	void Catrobat_PlayerMain::Update()
	{
		ProcessInput();

		// Update scene objects
		m_timer.Tick([&]()
		{
			m_basic2dRenderer->Update(m_timer);
			//m_fpsTextRenderer->Update(m_timer);
		});
	}

	/// Process all input from the user before updating game state
	void Catrobat_PlayerMain::ProcessInput()
	{
		// TODO: Add per frame input handling here
		//m_sceneRenderer->TrackingUpdate(m_pointerLocationX);
	}

	/// Renders the current frame according to the current application state.
	/// Returns true if the frame was rendered and is ready to be displayed.
	bool Catrobat_PlayerMain::Render()
	{
		// Don't try to render anything before the first Update
		if (m_timer.GetFrameCount() == 0)
		{
			return false;
		}

		auto context = m_deviceResources->GetD3DDeviceContext();

		// Reset the viewport to target the whole screen
		auto viewport = m_deviceResources->GetScreenViewport();
		context->RSSetViewports(1, &viewport);

		// Reset render targets to the screen
		ID3D11RenderTargetView *const targets[1] =
		{ m_deviceResources->GetBackBufferRenderTargetView() };
		context->OMSetRenderTargets(1, targets, m_deviceResources->GetDepthStencilView());

		// Clear the back buffer and depth stencil view
		context->ClearRenderTargetView(m_deviceResources->GetBackBufferRenderTargetView(),
			DirectX::Colors::White);
		context->ClearDepthStencilView(m_deviceResources->GetDepthStencilView(),
			D3D11_CLEAR_DEPTH | D3D11_CLEAR_STENCIL,
			1.0f,
			0);

		// Render the scene objects
		m_basic2dRenderer->Render();
		return true;
	}

	/// Notifies renderers that device resources need to be released
	void Catrobat_PlayerMain::OnDeviceLost()
	{
		m_basic2dRenderer->ReleaseDeviceDependentResources();
		//m_fpsTextRenderer->ReleaseDeviceDependentResources();
	}

	/// Notifies renderers that device resources may now be recreated
	void Catrobat_PlayerMain::OnDeviceRestored()
	{
		m_basic2dRenderer->CreateDeviceDependentResources();
		//m_fpsTextRenderer->CreateDeviceDependentResources();
		CreateWindowSizeDependentResources();
	}

	template <class T>
	static T^ Catrobat_PlayerMain::FindChildControl(DependencyObject^ control,
		const wchar_t* childName)
	{
		if (control == nullptr || childName == nullptr)
			return nullptr;

		int amountOfChildren = VisualTreeHelper::GetChildrenCount(control);
		for (int i = 0; i < amountOfChildren; i++)
		{
			DependencyObject^ child = nullptr;
			T^ tEle;

			try
			{
				child = VisualTreeHelper::GetChild(control, i);
				tEle = (T^) child;
			}
			catch (Exception ^e)
			{
				tEle = nullptr;
			}

			if (tEle != nullptr && tEle->Name != nullptr
				&& wcscmp(tEle->Name->Data(), childName) == 0)
			{
				// Found control
				return tEle;
			}
			else
			{
				// Did not find control --> search among children
				T^ nextLevel = FindChildControl<T>(child, childName);
				if (nextLevel != nullptr)
					return nextLevel;
			}
		}
		return nullptr;
	}

	/// explicit instantiation for Catrobat.PlayerAdapter
	template SwapChainPanel^ Catrobat_PlayerMain::FindChildControl<SwapChainPanel>(
		DependencyObject^, const wchar_t*);
};