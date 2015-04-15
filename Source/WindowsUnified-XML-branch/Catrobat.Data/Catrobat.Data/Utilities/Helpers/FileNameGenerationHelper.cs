using System;

namespace Catrobat.Data.Utilities.Helpers
{
    public static class FileNameGenerationHelper
    {
        public static string Generate()
        {
            var uuid = Guid.NewGuid().ToString().Replace("-", "1");

            return uuid;

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