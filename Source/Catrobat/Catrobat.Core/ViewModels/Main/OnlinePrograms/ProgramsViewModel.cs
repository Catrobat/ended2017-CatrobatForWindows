using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using Catrobat.Core.Models.OnlinePrograms;
using Catrobat.Core.Resources;
using Catrobat.IDE.Core.CatrobatObjects;
using GalaSoft.MvvmLight;
using System.Net.NetworkInformation;
using System.Threading;
using Windows.Networking.Connectivity;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.ViewModels.Main.OnlinePrograms
{
  public class ProgramsViewModel : ObservableObject
  {
    #region private fields

    //always have as many CategoryOnlineNames as CategorySearchKeyWords
    private static readonly string[] CategoryOnlineNames = { "newest", "most downloaded", "most viewed" };
    private static readonly string[] CategorySearchKeyWords = { "API_RECENT_PROJECTS", "API_MOSTDOWNLOADED_PROJECTS", "API_MOSTVIEWED_PROJECTS" };

    private const int InitialProgramOffset = 0;
    private const int InitialNumberOfFeaturedPrograms = 10;
    private const int InitialNumberOfSearchedPrograms = 10;

    private bool inSearchMode_;
    private string searchText_;
    private bool internetAvailable_;

    #endregion

    #region public properties

    public bool InSearchMode
    {
      get { return inSearchMode_; }
      set
      {
        if (inSearchMode_ == value)
        {
          return;
        }

        inSearchMode_ = value;
        RaisePropertyChanged(nameof(InSearchMode));
      }
    }

    public bool InternetAvailable
    {
      get { return internetAvailable_; }
      set
      {
        if (internetAvailable_ == value)
        {
          return;
        }

        internetAvailable_ = value;
        RaisePropertyChanged(nameof(InternetAvailable));
      }
    }

    public string SearchText
    {
      get { return searchText_; }
      set
      {
        if (searchText_ == value)
        {
          return;
        }

        searchText_ = value;
        RaisePropertyChanged(nameof(SearchText));
      }
    }

    public ObservableCollection<SimpleProgramViewModel> FeaturedPrograms { get; private set; }

    public ObservableCollection<CategoryViewModel> Categories { get; set; }

    public ObservableCollection<SimpleProgramViewModel> SearchResults { get; set; }

    #endregion

    #region public helpers

    public async Task<List<OnlineProgramHeader>> GetPrograms(int offset, int count, string category, CancellationToken cancellationToken, string additionalSearchText = null)
    {
      List<OnlineProgramHeader> header;

      try
      {
        header = await ServiceLocator.WebCommunicationService.
          LoadOnlinePrograms(category, offset, count,
          cancellationToken, additionalSearchText);

        InternetAvailable = true;
      }
      catch (Exception)
      {
        //There was probably no working internet connection
        InternetAvailable = false;
        header = new List<OnlineProgramHeader>();
      }

      return header;
    }

    #endregion

    #region commands

    public ICommand SearchCommand => new RelayCommand(Search, CanSearch);

    public ICommand ExitSearchCommand => new RelayCommand(ExitSearch, CanExitSearch);

    public ICommand ReloadCommand => new RelayCommand(ReloadOnlinePrograms);

    #endregion

    #region construction

    public ProgramsViewModel()
    {
      InSearchMode = false;
      SearchText = "";
      InternetAvailable = true;

      FeaturedPrograms = new ObservableCollection<SimpleProgramViewModel>();
      Categories = new ObservableCollection<CategoryViewModel>();      
      SearchResults = new ObservableCollection<SimpleProgramViewModel>();

      PropertyChanged += ProgramsViewModelPropertyChanged;

      

      LoadFeaturedPrograms();
      InitializeCategories();
    }

    private void ProgramsViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(SearchText))
      {
        // TODO: Add another state to show previous results while updating the search text
        InSearchMode = false;
      }
    }

    #endregion

    #region private helpers

    private async void LoadFeaturedPrograms()
    {
      //TODO: Make use of the token?
      var cancellationToken = new CancellationToken();

      var featuredPrograms = await GetPrograms(InitialProgramOffset,
        InitialNumberOfFeaturedPrograms, "API_FEATURED_PROJECTS",
        cancellationToken);

      foreach (var project in featuredPrograms)
      {
        FeaturedPrograms.Add(
          new SimpleProgramViewModel(
            new ProgramInfo(project)));
      }
    }

    private void InitializeCategories()
    {
      for (var i = 0; i < CategoryOnlineNames.Length; ++i)
      {
        Categories.Add(new CategoryViewModel(
          new Category
          {
            DisplayName = CategoryOnlineNames[i].ToUpper() + " PROGRAMS",
            OnlineName = CategoryOnlineNames[i],
            SearchKeyWord = CategorySearchKeyWords[i]
          }, this));
      }
    }

    private void ResetPrograms()
    {
      FeaturedPrograms.Clear();
      LoadFeaturedPrograms();

      foreach (var categoryViewModel in Categories)
      {
        categoryViewModel.ResetPrograms();
      }
    }

    private bool CanSearch()
    {
      return true;
    }

    private bool CanExitSearch()
    {
      return true;
    }

    private async void Search()
    {
      //TODO: Make use of the token
      var cancellationToken = new CancellationToken();

      var retrievedPrograms = await GetPrograms(
        InitialProgramOffset, InitialNumberOfSearchedPrograms, 
        "API_SEARCH_PROJECTS", cancellationToken, SearchText);

      foreach (var programHeader in retrievedPrograms)
      {
        SearchResults.Add(
            new SimpleProgramViewModel(
              new ProgramInfo(programHeader)));
      }

      InSearchMode = true;
    }

    private void ExitSearch()
    {
      SearchResults.Clear();
      SearchText = "";
      InSearchMode = false;

      //TODO: What is that doing? and why here? #ConfusedPatrik

      var reloadPrograms = FeaturedPrograms.Count == 0;

      foreach (var category in Categories)
      {
        if (category.Programs.Count == 0)
          reloadPrograms = true;
      }

      if (reloadPrograms)
        ReloadOnlinePrograms();
      else
        InternetAvailable = true;
    }

    private void ReloadOnlinePrograms()
    {
      if (InSearchMode)
      {
        Search();
      }
      else
      {
        ResetPrograms();
      }
    }

    #endregion
  }
}
