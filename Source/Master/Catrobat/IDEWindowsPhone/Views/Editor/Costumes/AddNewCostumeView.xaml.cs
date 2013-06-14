using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
  public partial class AddNewCostumeView : PhoneApplicationPage
  {
    private readonly AddNewCostumeViewModel _addNewCostumeViewModel = ServiceLocator.Current.GetInstance<AddNewCostumeViewModel>();

    public AddNewCostumeView()
    {
      InitializeComponent();
    }

    protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
    {
      NavigationService.GoBack();
    }
  }
}