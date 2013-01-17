Windows Phone 7 Continuous Integration Testing Framework

http://wp7ci.codeplex.com/

Installation Instructons
------------------------

1. Create a Windows Phone Silverlight Application for your tests if you haven't already 
2. Download the manual installation archive and extract it to a folder called "WP7-CI" under your application project folder. 
3. Add a reference to Microsoft.Silverlight.Testing.dll and Microsoft.VisualStudio.QualityTools.UnitTesting.Silverlight.dll in lib/sl3-wp. Remove any references to previous versions of these assemblies.
4. Manually edit your project file (csproj/vbproj) and place the following before the </Project> at the end of the file:

    <Import Project="WP7-CI\tools\WP7CI.targets" />

Also, as is required by the standard testing framework, the following code should be placed in the Loaded event of your startup page:

    var testPage = UnitTestSystem.CreateTestPage() as IMobileTestPage;

    BackKeyPress += (x, xe) => xe.Cancel = testPage.NavigateBack();
    (Application.Current.RootVisual as PhoneApplicationFrame).Content = testPage;  