using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Tests.Misc.Storage
{
  public static class BasePathHelper
  {
    public static string GetTestBasePathWithBranch()
    {
      string basePath = Assembly.GetExecutingAssembly().CodeBase;
      int end = basePath.LastIndexOf("Catrobat/", System.StringComparison.Ordinal) + 9;
      basePath = basePath.Substring(8, end - 8);

      return basePath;
    }

    public static string GetTestBasePath()
    {
      string basePath = Assembly.GetExecutingAssembly().CodeBase;
      int end = basePath.IndexOf("Catrobat/", System.StringComparison.Ordinal) + 9;
      basePath = basePath.Substring(8, end - 8);

      return basePath;
    }

    public static string GetSampleDataPath()
    {
      string path = "";// GetTestBasePathWithBranch();
      path += "SampleData/"; // "TestsCommon/SampleData/";

      return path;
    }

    public static string GetSampleProjectsPath()
    {
      string path = GetSampleDataPath();
      path += "SampleProjects/";

      return path;
    }
  }
}
