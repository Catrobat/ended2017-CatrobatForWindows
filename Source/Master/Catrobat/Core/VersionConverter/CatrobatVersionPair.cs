using System;
using System.Collections.Generic;

namespace Catrobat.Core.VersionConverter
{
    public class CatrobatVersionPair : IEquatable<CatrobatVersionPair>
    {
        public string InputVersion { get; set; }
        public string OutputVersion { get; set; }

        public bool IsInverse { get; set; }

        public CatrobatVersionPair(string inputVersion, string outputVersion)
        {
            InputVersion = inputVersion;
            OutputVersion = outputVersion;
        }

        public CatrobatVersionPair(string inputVersion, string outputVersion, bool isInverse)
            : this(inputVersion, outputVersion)
        {
            IsInverse = isInverse;
        }

        public void Invert()
        {
            var temp = OutputVersion;
            OutputVersion = InputVersion;
            InputVersion = temp;
        }

        public bool Equals(CatrobatVersionPair other)
        {
            return InputVersion == other.InputVersion && OutputVersion == other.OutputVersion;
        }

        public class EqualityComparer : IEqualityComparer<CatrobatVersionPair>
        {
            public bool Equals(CatrobatVersionPair x, CatrobatVersionPair y)
            {
                return x.InputVersion == y.InputVersion && x.OutputVersion == y.OutputVersion;
            }

            public int GetHashCode(CatrobatVersionPair obj)
            {
                var str = obj.InputVersion + obj.OutputVersion;
                return str.GetHashCode();
            }
        }
    }
}
