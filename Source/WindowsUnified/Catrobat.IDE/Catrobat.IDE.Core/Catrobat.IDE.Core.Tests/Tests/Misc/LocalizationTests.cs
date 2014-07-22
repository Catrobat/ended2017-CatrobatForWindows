using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.Resources.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Misc
{
    [TestClass]
    public class LocalizationTests
    {
        //[TestMethod, TestCategory("GatedTests")]
        //public void TestDe()
        //{
        //    TestCulture(new CultureInfo("de"));
        //}

        //private void TestCulture(CultureInfo culture)
        //{
        //    var actualResources = AppResources.ResourceManager.GetResourceSet(culture, true, false);
        //    Assert.IsNotNull(actualResources);

        //    var expectedResources = AppResources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, false);
        //    var expectedKeys = expectedResources.AsEnumerable().Select(entry => entry.Key).ToList();
        //    var actualKeys = actualResources.AsEnumerable().Select(entry => entry.Key).ToList();

        //    var superfluousTranslations = actualKeys.Except(expectedKeys).ToList();
        //    Assert.AreEqual(0, superfluousTranslations.Count, "Superfluous translation \"" + superfluousTranslations.FirstOrDefault() + "\" in {" + culture.Name + "}");

        //    var missingTranslations = expectedKeys.Except(actualKeys).ToList();
        //    Assert.AreEqual(0, missingTranslations.Count, "Missing translation \"" + missingTranslations.FirstOrDefault() + "\" in {" + culture.Name + "}");
        //}
    }
}
