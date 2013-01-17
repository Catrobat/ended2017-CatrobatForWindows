using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestsWindowsStore.IDE
{
  [TestClass]
  public class ConverterTest
  {
    // ####### IntStringConverter #############################################
    [TestMethod]
    public void TestStringToIntConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //IntStringConverter conv = new IntStringConverter();
      //object output = conv.ConvertBack((object)"42", null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is int);
      //Assert.Equals((int)output, 12);
    }

    [TestMethod]
    public void TestIntToStringConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //IntStringConverter conv = new IntStringConverter();
      //object output = conv.Convert((object)42, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is string);
      //Assert.Equals((string)output, "42");
    }

    [TestMethod]
    public void TestFaultyStringToIntConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //IntStringConverter conv = new IntStringConverter();
      //object output = conv.ConvertBack((object)"4d2", null, 42, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is int);
      //Assert.Equals((int)output, 42);
    }
    
    // ####### FloatStringConverter ###########################################

    [TestMethod]
    public void TestStringToFloatConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //FloatStringConverter conv = new FloatStringConverter();
      //object output = conv.ConvertBack((object)"4.2", null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is float);
      //Assert.Equals((float)output, 4.2f);
    }

    [TestMethod]
    public void TestFloatToStringConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //FloatStringConverter conv = new FloatStringConverter();
      //object output = conv.Convert((object)4.2f, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is string);
      //Assert.Equals((string)output, "4.2");
    }

    [TestMethod]
    public void TestFaultyStringToFloatConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //FloatStringConverter conv = new FloatStringConverter();
      //object output = conv.ConvertBack((object)"4d2", null, 42f, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is float);
      //Assert.Equals((float)output, 42);
    }

    // ####### SecondStringMillisecondConverter ###############################

    [TestMethod]
    public void TestSecondStringToMillisecondConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //SecondStringMillisecondConverter conv = new SecondStringMillisecondConverter();
      //object output = conv.ConvertBack((object)"4.2", null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is int);
      //Assert.Equals((int)output, 4200);
    }

    [TestMethod]
    public void TestMillisecondToSecondStringConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //SecondStringMillisecondConverter conv = new SecondStringMillisecondConverter();
      //object output = conv.Convert((object)4200, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is string);
      //Assert.Equals((string)output, "4.2");
    }

    [TestMethod]
    public void TestFaultySecondStringToMillisecondConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //SecondStringMillisecondConverter conv = new SecondStringMillisecondConverter();
      //object output = conv.ConvertBack((object)"4d2", null, 4200, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is int);
      //Assert.Equals((int)output, 4200);
    }

    // ####### DoubleStringConverter ##########################################

    [TestMethod]
    public void TestStringToDoubleConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //DoubleStringConverter conv = new DoubleStringConverter();
      //object output = conv.ConvertBack((object)"4.2", null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is double);
      //Assert.Equals((double)output, 4.2d);
    }

    [TestMethod]
    public void TestDoubleToStringConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //DoubleStringConverter conv = new DoubleStringConverter();
      //object output = conv.Convert((object)4.2d, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is string);
      //Assert.Equals((string)output, "4.2");
    }

    [TestMethod]
    public void TestFaultyStringToDoubleConversion()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //DoubleStringConverter conv = new DoubleStringConverter();
      //object output = conv.ConvertBack((object)"4d2", null, 42d, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is double);
      //Assert.Equals((double)output, 42d);
    }

    // ####### NullColorConverter #############################################

    [TestMethod]
    public void TestNullColorConverter1()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //NullColorConverter conv = new NullColorConverter();
      //object output = conv.ConvertBack(null, null, null, null);
      //Assert.IsNull(output);
    }

    [TestMethod]
    public void TestNullColorConverter2()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //NullColorConverter conv = new NullColorConverter();
      //object output = conv.Convert(null, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is Brush);
      //Assert.Equals(output, Application.Current.Resources["PhoneAccentBrush"]);
    }

    [TestMethod]
    public void TestNullColorConverter3()
    {
      throw new NotImplementedException("Implement for WindowsStore");
      //NullColorConverter conv = new NullColorConverter();
      //object output = conv.Convert(this, null, null, null);
      //Assert.IsNotNull(output);
      //Assert.IsTrue(output is Brush);
      //Assert.Equals(output, Application.Current.Resources["PhoneTextBoxForegroundColor"]);
    }
  }
}
