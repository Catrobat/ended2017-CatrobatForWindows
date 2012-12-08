using System;

namespace Catrobat.Core.Helpers
{
    public class FileNameGenerator
    {
        public static string generate()
        {
            string randomFileName;

            randomFileName = DateTime.UtcNow.Ticks.ToString("X");

            var random = new Random();
            for (int i = 0; i < 17; i++)
                randomFileName += random.Next(0, 15).ToString("X");

            return randomFileName + "_";
        }
    }
}