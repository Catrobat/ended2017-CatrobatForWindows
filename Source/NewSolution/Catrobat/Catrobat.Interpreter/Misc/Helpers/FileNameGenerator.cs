using System;

namespace Catrobat.Interpreter.Misc.Helpers
{
    public static class FileNameGenerator
    {
        public static string Generate()
        {
            var randomFileName = DateTime.UtcNow.Ticks.ToString("X");
            var random = new Random();
            for (var i = 0; i < 17; i++)
            {
                randomFileName += random.Next(0, 15).ToString("X");
            }

            return randomFileName + "_";
        }
    }
}