using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.IDE.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class SimpleProgramViewModel : ObservableObject
  {
    #region properties

    public ProgramInfo Program { get; }

    #endregion

    #region commands

    public ICommand ShowDetailedCommand => new RelayCommand(ShowDetailed);

    #endregion

    #region construction

    public SimpleProgramViewModel(ProgramInfo program)
    {
      Program = program;
    }

    #endregion

    #region private helpers

    private void ShowDetailed()
    {
      ServiceLocator.NavigationService.NavigateTo(typeof(DetailedProgramViewModel));
      Messenger.Default.Send(new GenericMessage<ProgramInfo>(Program), 
        ViewModelMessagingToken.ShowDetailedOnlineProgram);
    }

    #endregion
  }
}
