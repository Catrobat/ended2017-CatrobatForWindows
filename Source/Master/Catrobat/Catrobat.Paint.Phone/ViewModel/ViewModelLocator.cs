/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Catrobat.Paint"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

 * For Catrobat.Paint:
 * There is no App.xml in library so we have to define the Locator in each .xaml,
 * but that's no problem because ViewModel references are static and we always
 * get the same ones. 
 * Mr. MvvmLight recommends this solution:
 * See http://blog.galasoft.ch/archive/2010/03/16/whatrsquos-new-in-mvvm-light-v3.aspx#515715

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.Paint.Annotations;
using Catrobat.Paint.Resources;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.Paint.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator: INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            _localizedStrings = new LocalizedStrings();
            _localizedStrings.PropertyChanged += LocalizedStringsOnPropertyChanged;

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ColorPickerViewModel>();
            SimpleIoc.Default.Register<PaintingAreaViewModel>();
        }

        private void LocalizedStringsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged("Resources");
        }

        private readonly LocalizedStrings _localizedStrings;
        public AppResources Resources
        {
            get
            {
                return _localizedStrings.Resources;
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ColorPickerViewModel ColorPicker
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ColorPickerViewModel>();
            }
        }

        public PaintingAreaViewModel PaintingArea
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PaintingAreaViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        #region PropertyChenged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}