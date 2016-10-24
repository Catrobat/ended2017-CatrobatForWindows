using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.Networking.Connectivity;

namespace Catrobat.Core.ViewModels.Main.OnlinePrograms
{
  public class ProgramsViewModel : ObservableObject
  {
    //always have as many CategoryOnlineNames as CategorySearchKeyWords
    private static readonly string[] CategoryOnlineNames = { "newest", "most downloaded", "most viewed" };
    private static readonly string[] CategorySearchKeyWords = { "API_RECENT_PROJECTS", "API_MOSTDOWNLOADED_PROJECTS", "API_MOSTVIEWED_PROJECTS" };

    private bool _inSearchMode;
    private bool _isInternetAvailable;

    private string _searchText;

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

    public bool IsInternetAvailable
    {
      get { return _isInternetAvailable; }
      set
      {
        if (_isInternetAvailable == value)
        {
          return;
        }

        _isInternetAvailable = value;
        RaisePropertyChanged(nameof(IsInternetAvailable));
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

    public ObservableCollection<ProgramViewModel> FeaturedPrograms { get; private set; }

    public ObservableCollection<CategoryViewModel> Categories { get; set; }

    public ObservableCollection<ProgramViewModel> SearchResults { get; set; }

    public ICommand SearchCommand => new RelayCommand(Search, CanSearch);

    public ICommand ExitSearchCommand => new RelayCommand(ExitSearch, CanExitSearch);

    public ICommand ReloadCommand => new RelayCommand(ReloadOnlinePrograms);

    public ProgramsViewModel()
    {
      InSearchMode = false;
      SearchText = "";
      Categories = new ObservableCollection<CategoryViewModel>();
      FeaturedPrograms = new ObservableCollection<ProgramViewModel>();

      for (int i = 0; i < CategoryOnlineNames.Length; ++i)
      {
        Categories.Add(new CategoryViewModel(
          new Category { DisplayName = CategoryOnlineNames[i], OnlineName = CategoryOnlineNames[i], SearchKeyWork = CategorySearchKeyWords[i] }, this));
      }

      LoadOnlinePrograms();

      SearchResults = new ObservableCollection<ProgramViewModel>();
    }

    public static async Task<List<OnlineProgramHeader>> GetPrograms(int Offset, int Count, string CategorySearchKeyWords, string SearchText = null)
    {
      try
      {
        System.Threading.CancellationToken cToken = new System.Threading.CancellationToken();
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ApplicationResourcesHelper.Get("API_BASE_ADDRESS"));
        HttpResponseMessage httpResponse = null;

        if (SearchText != null)
        {
          var encodedSearchText = WebUtility.UrlEncode(SearchText);
          httpResponse = await httpClient.GetAsync(
                                  string.Format(ApplicationResourcesHelper.Get(CategorySearchKeyWords), encodedSearchText,
                                  Count, Offset), cToken);
        }
        else
        {
          httpResponse = await httpClient.GetAsync(
          string.Format(ApplicationResourcesHelper.Get(CategorySearchKeyWords),
          Count, Offset), cToken);
        }

        httpResponse.EnsureSuccessStatusCode();

        string jsonResult = await httpResponse.Content.ReadAsStringAsync();
        var featuredPrograms = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<OnlineProgramOverview>(jsonResult));

        return featuredPrograms.CatrobatProjects;
      }
      catch (Exception)
      {
        //There was probably no working internet connection
        return new List<OnlineProgramHeader>();
      }
      
    }

    private async void LoadOnlinePrograms()
    {
      if (FeaturedPrograms.Count == 0)
      {
        var featuredPrograms = await GetPrograms(0, 10, "API_FEATURED_PROJECTS");
        IsInternetAvailable = featuredPrograms.Count != 0;

        foreach (var project in featuredPrograms)
        {
          FeaturedPrograms.Add(new ProgramViewModel(
            new Program
            {
              Title = project.ProjectName,
              ImageSource = new Uri(project.FeaturedImage)
            }));
        }
      }     

      //Set 2 Progams for each Category
      foreach (var category in Categories)
      {
        if (category.Programs.Count == 0)
        {
          var resultPrograms = await GetPrograms(0, 2, category.SearchKeyWord);
          IsInternetAvailable = resultPrograms.Count != 0;

          foreach (var project in resultPrograms)
          {
            category.Programs.Add(
              new ProgramViewModel(
                new Program
                {
                  Title = project.ProjectName,
                  ImageSource = new Uri(project.ScreenshotBig)
                }));
          }
        }        
      }
    }

    private void ReloadOnlinePrograms()
    {
      if (InSearchMode)
      {
        Search();
      }
      else
      {
        LoadOnlinePrograms();
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
      var retrievedPrograms = await GetPrograms(0, 10, "API_SEARCH_PROJECTS", SearchText);
      IsInternetAvailable = retrievedPrograms.Count != 0;

      foreach (var project in retrievedPrograms)
      {
        SearchResults.Add(
          new ProgramViewModel(
            new Program
            {
              Title = project.ProjectName,
              ImageSource = new Uri(project.ScreenshotBig)
            }));
      }

      InSearchMode = true;
    }

    private void ExitSearch()
    {
      SearchResults.Clear();
      SearchText = "";
      InSearchMode = false;

      bool ReloadPrograms = FeaturedPrograms.Count == 0;

      foreach (var category in Categories)
      {
        if (category.Programs.Count == 0)
          ReloadPrograms = true;
      }

      if (ReloadPrograms)
        ReloadOnlinePrograms();
      else
        IsInternetAvailable = true;
    }
  }
}
