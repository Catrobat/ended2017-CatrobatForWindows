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

    private readonly ProgramsViewModel programsViewModel_;

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
      programsViewModel_ = programsViewModel;

      Programs = new ObservableCollection<SimpleProgramViewModel>();
      ShowMore();
    }

    #endregion

    #region private helpers

    private async void ShowMore()
    {
      //TODO: Make use of the token?
      var cancellationToken = new CancellationToken();

      var retrievedPrograms = await programsViewModel_.GetPrograms(
        Programs.Count, 2, Category.SearchKeyWord, cancellationToken);

      foreach (var project in retrievedPrograms)
      {
        // TODO: Check if programs are already present and reload all to avoid duplicates!
        Programs.Add(
           new SimpleProgramViewModel(
              new Program(project)));
      }
    }

    #endregion
  }
}
