using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Core.Models.OnlinePrograms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;

namespace Catrobat.Core.ViewModels.Main.OnlinePrograms
{
  public class ProgramViewModel : ObservableObject
  {
    #region private fields



    #endregion

    #region public properties

    public Program Program { get; }

    #endregion

    #region commands

    public ICommand ShowFullCommand => new RelayCommand(ShowFull);

    #endregion

    #region construction

    public ProgramViewModel(Program program)
    {
      Program = program;
    }

    #endregion

    #region private helpers

    private void ShowFull()
    {
      // TODO: open new window with clicked program infos in it
    }

    #endregion
  }
}
