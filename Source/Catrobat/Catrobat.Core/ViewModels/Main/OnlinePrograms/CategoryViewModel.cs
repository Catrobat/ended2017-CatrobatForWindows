using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Models.OnlinePrograms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.Core.Resources;
using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class CategoryViewModel : ObservableObject
  {
    #region private 

    private readonly ProgramsViewModel _programsViewModel;
    private int _programOffset;

    #endregion

    #region public properties

    public Category Category { get; }

    public ObservableCollection<SimpleProgramViewModel> Programs { get; set; }

    #endregion

    #region public interfaces

    public void ResetPrograms()
    {
      Programs.Clear();
      ShowMore();
    }

    #endregion

    #region commands

    public ICommand ShowMoreCommand => new RelayCommand(ShowMore);

    #endregion

    #region construction

    public CategoryViewModel(Category category, ProgramsViewModel programsViewModel)
    {
      Category = category;
      _programsViewModel = programsViewModel;

      Programs = new ObservableCollection<SimpleProgramViewModel>();
      ShowMore();
    }

    #endregion

    #region private helpers

    private async void ShowMore()
    {     
      while(true)
      { 
        var retrievedPrograms = await _programsViewModel.GetPrograms(
        Programs.Count + _programOffset, 2, Category.SearchKeyWord, new CancellationToken());

        if (CheckForDuplicates(retrievedPrograms))
        {
          _programOffset++;
          continue;
        }

        foreach (var project in retrievedPrograms)
        {
          Programs.Add(
            new SimpleProgramViewModel(
              new ProgramInfo(project)));
        }
        break;
      }
    }

    private bool CheckForDuplicates(List<OnlineProgramHeader> retrievedPrograms)
    {
      switch (Category.SearchKeyWord)
      {
        case "API_RECENT_PROJECTS":
          if (Programs.Any(p => p.Program.Uploaded <= ProgramInfo.FromUnixTime(retrievedPrograms.First().Uploaded)))
            return true;
          break;

        case "API_MOSTDOWNLOADED_PROJECTS":
          if (Programs.Any(p => p.Program.Downloads <= Convert.ToUInt32(retrievedPrograms.First().Downloads)))
            return true;
          break;

        case "API_MOSTVIEWED_PROJECTS":
          if (Programs.Any(p => p.Program.Views <= Convert.ToUInt32(retrievedPrograms.First().Views)))
            return true;
          break;

        default:
          throw new Exception("Unknown Category.SearchKeyWord: " + Category.SearchKeyWord);
      }

      return false;
    }

    #endregion
  }
}
