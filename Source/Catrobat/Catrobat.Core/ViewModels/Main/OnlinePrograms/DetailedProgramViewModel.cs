using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class DetailedProgramViewModel : ViewModelBase
  {
    #region attributes

    private bool linkIsVisible_;

    #endregion

    #region properies

    public Program Program { get; private set; }

    public bool LinkIsVisible
    {
      get { return linkIsVisible_; }
      set
      {
        if (linkIsVisible_ == value)
        {
          return;
        }

        linkIsVisible_ = value;
        RaisePropertyChanged(nameof(LinkIsVisible));
      }
    }

    #endregion

    #region commands

    public ICommand DownloadCommand => new RelayCommand(Download);

    public ICommand ReportCommand => new RelayCommand(Report);

    public ICommand ShowLinkCommand => new RelayCommand(ShowLink);

    #endregion

    #region construction

    public DetailedProgramViewModel()
    {
      LinkIsVisible = false;

      Messenger.Default.Register<GenericMessage<Program>>(this,
               ViewModelMessagingToken.ShowDetailedOnlineProgram, ShowDetailedOnlineProgramMessageAction);
    }

    #endregion

    #region helper

    private void ShowDetailedOnlineProgramMessageAction(GenericMessage<Program> message)
    {
      LinkIsVisible = false;

      Program = message.Content;
    }

    private void Download()
    {

    }

    private void Report()
    {
      
    }

    private void ShowLink()
    {
      LinkIsVisible = true;
    }

    #endregion

  }
}
