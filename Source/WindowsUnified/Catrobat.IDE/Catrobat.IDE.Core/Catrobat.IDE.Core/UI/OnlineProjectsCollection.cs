using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModels;

namespace Catrobat.IDE.Core.UI
{
    public class OnlineProgramsLoadingResult
    {
            public uint ProgramsCount { get; set; }
    }

    public class OnlineProgramsCollection : ObservableCollection<OnlineProjectHeader>
    {
        private CancellationTokenSource _downloadTaskCancellation = new CancellationTokenSource();
        private readonly object _manipulationLock = new object();

        private string _filterText = "";
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText == value) return;

                _filterText = value;
                RaisePropertyChanged(() => FilterText);
            }
        }

        private bool _hasMorePrograms = true;
        protected bool HasMorePrograms
        {
            get { return _hasMorePrograms; }
            set
            {
                _hasMorePrograms = value;
                RaisePropertyChanged(() => HasMorePrograms);
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            private set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private bool _isLoadingEnabled = true;
        public bool IsLoadingEnabled
        {
            get { return _isLoadingEnabled; }
            set
            {
                _isLoadingEnabled = value; 
                RaisePropertyChanged(()=>IsLoadingEnabled);
            }
        }

        private bool _isAutoUpdate = false;


        protected bool IsAutoUpdate
        {
            get { return _isAutoUpdate; }
            set
            {
                _isAutoUpdate = value;
                RaisePropertyChanged(() => IsAutoUpdate);
            }
        }

        public void ClearOnlineProjects()
        {
            // cancel previous uncompleted AsyncLoadOnlineProjects-Tasks
            if (_downloadTaskCancellation != null)
            {
                _downloadTaskCancellation.Cancel();
                _downloadTaskCancellation = new CancellationTokenSource();
            }
            this.Clear();
            HasMorePrograms = true;
            IsLoading = false;
        }

        public async Task<OnlineProgramsLoadingResult> LoadMoreOnlineProjectsAsync(int count, CancellationToken c)
        {
            if (!IsLoadingEnabled)
                return new OnlineProgramsLoadingResult {ProgramsCount = 0};

            if(IsLoading)
                _downloadTaskCancellation.Cancel();

            IsLoading = true;
            HasMorePrograms = true;

            int offset = this.Count;
            if (IsAutoUpdate == true)
                offset = 0;

            var projects = await 
                CatrobatWebCommunicationService.AsyncLoadOnlineProjects(
                _filterText, offset, count, c);

            uint newProgramsCount = 0;

            lock (_manipulationLock)
            {
                if (projects != null)
                {
                    foreach (var header in projects)
                    {
                        this.Add(header);
                        newProgramsCount ++;
                    }
                }

                IsLoading = false;
            }

            HasMorePrograms = count == newProgramsCount;

            return new OnlineProgramsLoadingResult {ProgramsCount = newProgramsCount};
        }

        public async Task ResetAndLoadFirstPrograms()
        {
            ClearOnlineProjects();
            //await LoadMoreOnlineProjectsAsync(int.Parse(ApplicationResources.API_REQUEST_LIMIT), _downloadTaskCancellation.Token);
        }

        #region PropertyChanged

        [NotifyPropertyChangedInvocator]
        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
        }

        #endregion
    }
}
