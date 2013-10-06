using System;

namespace Catrobat.Core.Services.Common
{
    public static class FileNameGenerationHelper
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString();

            //var randomFileName = DateTime.UtcNow.Ticks.ToString("X");
            //var random = new Random();
            //for (var i = 0; i < 17; i++)
            //{
            //    randomFileName += random.Next(0, 15).ToString("X");
            //}

            //return randomFileName + "_";
        }
    }
}