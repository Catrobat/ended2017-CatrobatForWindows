using System;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public enum Resolutions
    {
        WVGA,
        WXGA,
        HD720p
    };

    public static class ResolutionHelper
    {
        private static bool IsWvga
        {
            get
            {
                try
                {
                    return App.Current.Host.Content.ScaleFactor == 100;
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
                    return App.Current.Host.Content.ScaleFactor == 160;
                }
                catch
                {
                    return true;
                }
            }
        }

        private static bool Is720p
        {
            get
            {
                try
                {
                    return App.Current.Host.Content.ScaleFactor == 150;
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
                else if (Is720p)
                {
                    return Resolutions.HD720p;
                }
                else
                {
                    throw new InvalidOperationException("Unknown resolution");
                }
            }
        }
    }
}