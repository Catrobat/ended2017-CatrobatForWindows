using System;
using System.Windows;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.Utilities.Helpers
{
    public enum Resolutions
    {
        WVGA,
        WXGA,
        HD720P
    };

    public static class ResolutionHelper
    {
        private static bool IsWvga
        {
            get
            {
                try
                {
                    return Application.Current.Host.Content.ScaleFactor == 100;
                }
                catch
                {
                    return true;
                }
            }
        }

        private static bool IsWxga
        {
            get
            {
                try
                {
                    return Application.Current.Host.Content.ScaleFactor == 160;
                }
                catch
                {
                    return true;
                }
            }
        }

        private static bool Is720P
        {
            get
            {
                try
                {
                    return Application.Current.Host.Content.ScaleFactor == 150;
                }
                catch
                {
                    return true;
                }
            }
        }

        public static Resolutions CurrentResolution
        {
            get
            {
                if (IsWvga)
                {
                    return Resolutions.WVGA;
                }
                else if (IsWxga)
                {
                    return Resolutions.WXGA;
                }
                else if (Is720P)
                {
                    return Resolutions.HD720P;
                }
                else
                {
                    throw new InvalidOperationException("Unknown resolution");
                }
            }
        }
    }
}