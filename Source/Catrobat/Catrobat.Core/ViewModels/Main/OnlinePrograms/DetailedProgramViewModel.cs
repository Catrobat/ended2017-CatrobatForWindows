using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class DetailedProgramViewModel : ViewModelBase
  {
    #region attributes

    #endregion

    #region properies

    public Program Program { get; private set; }

    #endregion

    #region commands

    #endregion

    #region construction

    public DetailedProgramViewModel()
    {
      Messenger.Default.Register<GenericMessage<SimpleProgramViewModel>>(this,
               ViewModelMessagingToken.ShowDetailedOnlineProgram, ShowDetailedOnlineProgramMessageAction);
    }

    #endregion

    #region helper

    private void ShowDetailedOnlineProgramMessageAction(GenericMessage<SimpleProgramViewModel> message)
    {
      Program = message.Content.Program;
    }

    #endregion

  }
}
