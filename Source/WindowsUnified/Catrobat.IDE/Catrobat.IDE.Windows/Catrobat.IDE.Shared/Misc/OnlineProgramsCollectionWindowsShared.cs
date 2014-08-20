using System;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.WindowsShared.Misc
{
    public class OnlineProgramsCollectionWindowsShared : OnlineProgramsCollection, ISupportIncrementalLoading
    {
        public bool HasMoreItems
        {
            get { return HasMorePrograms; }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run((c) => LoadMoreOnlineProgramsPrivateAsync((int)count, c));
        }

        private async Task<LoadMoreItemsResult> LoadMoreOnlineProgramsPrivateAsync(int count, CancellationToken c)
        {
            var result = await this.LoadMoreOnlineProgramsAsync((int)count, c);
            return new LoadMoreItemsResult {Count = result.ProgramsCount};
        }

        public OnlineProgramsCollectionWindowsShared()
        {
            base.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == PropertyHelper.GetPropertyName(() => HasMorePrograms))
            {
                RaisePropertyChanged(()=>HasMoreItems);
            }
        }
    }
}
