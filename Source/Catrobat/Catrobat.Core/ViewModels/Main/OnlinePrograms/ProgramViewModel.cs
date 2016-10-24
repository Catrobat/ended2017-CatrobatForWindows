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
    private Program Program { get; set; }

    public string Title
    {
      get { return Program.Title; }
      set
      {
        if (Program.Title == value)
        {
          return;
        }

        Program.Title = value;
        RaisePropertyChanged(nameof(Title));
      }
    }

    public Uri ImageSource
    {
      get { return Program.ImageSource; }
      set
      {
        if (Program.ImageSource == value)
        {
          return;
        }

        Program.ImageSource = value;
        RaisePropertyChanged(nameof(ImageSource));
      }
    }

    public ProgramViewModel(Program program)
    {
      Program = program;
    }

    public ICommand ShowFullCommand => new RelayCommand(ShowFull, CanShowFull);

    private bool CanShowFull()
    {
      return true;
    }

    private void ShowFull()
    {
      //open new windows with clicked program infos in it


      Title = "Clicked";
    }
  }
}
