using System.Diagnostics;
using Catrobat.IDE.Core.UI;

namespace Catrobat.IDE.Core.Models
{
    [DebuggerDisplay("Content = {Content}")]
    public partial class BroadcastMessage : Model, ISelectable
    {
        #region Properties

        private string _content;
        public string Content
        {
            get { return _content; }
            set { Set(ref _content, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        #endregion

        #region Implements ITestEquatable

        protected override bool TestEquals(Model other)
        {
            return base.TestEquals(other) && TestEquals((BroadcastMessage) other);
        }

        protected bool TestEquals(BroadcastMessage other)
        {
            return string.Equals(_content, other._content);
        }

        #endregion
    }
}
