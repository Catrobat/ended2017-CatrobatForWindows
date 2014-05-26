using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Catrobat.IDE.Core.ExtensionMethods;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Url = {Url}")]
    public partial class UploadHeader : Model
    {
        #region Properties

        private string _mediaLicense = string.Empty;
        public string MediaLicense
        {
            get { return _mediaLicense; }
            set { Set(ref _mediaLicense, value); }
        }

        private string _programLicense = string.Empty;
        public string ProgramLicense
        {
            get { return _programLicense; }
            set { Set(ref _programLicense, value); }
        }

        private string _remixOf = string.Empty;
        public string RemixOf
        {
            get { return _remixOf; }
            set { Set(ref _remixOf, value); }
        }

        private ObservableCollection<string> _tags = new ObservableCollection<string>();
        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set { Set(ref _tags, value); }
        }

        private DateTime? _uploaded;
        public DateTime? Uploaded
        {
            get { return _uploaded; }
            set
            {
                if (value.HasValue)
                {
                    // precision loss when saving to xml
                    value = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, value.Value.Hour, value.Value.Minute, value.Value.Second);
                }
                Set(ref _uploaded, value);
            }
        }

        private string _url = string.Empty;
        public string Url
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        private string _userId = string.Empty;
        public string UserId
        {
            get { return _userId; }
            set { Set(ref _userId, value); }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((UploadHeader) other);
        }

        protected bool TestEquals(UploadHeader other)
        {
            // auto-implemented by ReSharper
            return string.Equals(_mediaLicense, other._mediaLicense) && 
                string.Equals(_programLicense, other._programLicense) && 
                string.Equals(_remixOf, other._remixOf) && 
                CollectionExtensions.Equals(_tags, other._tags) && 
                Equals(_uploaded, other._uploaded) && 
                string.Equals(_url, other._url) && 
                string.Equals(_userId, other._userId);
        }

        #endregion
    }
}
