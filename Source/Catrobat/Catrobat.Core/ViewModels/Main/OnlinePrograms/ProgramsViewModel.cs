using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    private static readonly string[] CategoryOnlineNames = { "most recent", "most downloaded", "most viewed" };
    private static readonly string[] CategorySearchKeyWords = { "API_RECENT_PROJECTS", "API_MOSTDOWNLOADED_PROJECTS", "API_MOSTVIEWED_PROJECTS" };

    private const int InitialProgramOffset = 0;
    private const int InitialNumberOfFeaturedPrograms = 10;
    private const int InitialNumberOfSearchedPrograms = 10;

    private bool _inSearchMode;
    private string _searchText;
    private bool _internetAvailable;
    private bool _noSearchResults;

    #endregion

    #region public properties

    public bool InSearchMode
    {
      get { return _inSearchMode; }
      set
      {
        if (_inSearchMode == value)
        {
          return;
        }

        _inSearchMode = value;
        RaisePropertyChanged(nameof(InSearchMode));
      }
    }

    public bool NoSearchResults
    {
      get { return _noSearchResults; }
      set
      {
        if (_noSearchResults == value)
        {
          return;
        }

        _noSearchResults = value;
        RaisePropertyChanged(nameof(NoSearchResults));
      }
    }

    public bool InternetAvailable
    {
      get { return _internetAvailable; }
      set
      {
        if (_internetAvailable == value)
        {
          return;
        }

        _internetAvailable = value;
        RaisePropertyChanged(nameof(InternetAvailable));
      }
    }

    public string SearchText
    {
      get { return _searchText; }
      set
      {
        if (_searchText == value)
        {
          return;
        }

        _searchText = value;
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
            SearchResults.Clear();
            InSearchMode = false;
      }
    }

    #endregion

    #region private helpers

    private async void LoadFeaturedPrograms()
    {
      //TODO: Make use of the token?
      var cancellationToken = new CancellationToken();

      var incompleteHeaders = await GetPrograms(InitialProgramOffset,
        InitialNumberOfFeaturedPrograms, "API_FEATURED_PROJECTS",
        cancellationToken);

      foreach (var incompleteHeader in incompleteHeaders)
      {
        var featuredProgramHeader = await GetPrograms(-1, -1, 
          "API_GET_PROJECT_BY_ID", cancellationToken, 
          incompleteHeader.ProjectId);

        var completeHeader = featuredProgramHeader.First();
        completeHeader.FeaturedImage = 
          incompleteHeader.FeaturedImage.Replace(
            ApplicationResourcesHelper.Get("POCEKTCODE_BASE_ADDRESS"), "");
        
        FeaturedPrograms.Add(
          new SimpleProgramViewModel(
            new ProgramInfo(completeHeader)));
      }
    }

    private void InitializeCategories()
    {
      for (var i = 0; i < CategoryOnlineNames.Length; ++i)
      {
        Categories.Add(new CategoryViewModel(
          new Category
          {
            DisplayName = CategoryOnlineNames[i].ToUpper(),
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

      if (retrievedPrograms.Count == 0)
      {
        NoSearchResults = true;
      }
      else
      {
        NoSearchResults = false;
        foreach (var programHeader in retrievedPrograms)
        {
          SearchResults.Add(
              new SimpleProgramViewModel(
                new ProgramInfo(programHeader)));
        }
      }
      
      InSearchMode = true;
    }

    private void ExitSearch()
    {
      SearchResults.Clear();
      SearchText = "";
      InSearchMode = false;
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
