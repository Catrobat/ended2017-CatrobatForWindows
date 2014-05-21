using Catrobat.IDE.Core.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE
{
    [TestClass]
    public class ObservableCollectionTests
    {
        [TestMethod, TestCategory("GatedTests")]
        public void TestSelect()
        {
            var source = new ObservableCollection<int>(Enumerable.Range(1, 3).ToList());
            Func<int, int> selector = value => 100 + value;
            
            var transformed = source.ObservableSelect(selector, c => c - 100);
            TestSelect(source, transformed, selector);
            
            source.Add(6);
            TestSelect(source, transformed, selector);
            
            source.Remove(6);
            TestSelect(source, transformed, selector);
            
            transformed.Add(106);
            TestSelect(source, transformed, selector);
            
            transformed.Remove(106);
            TestSelect(source, transformed, selector);
            
            source.Insert(1, 6);
            TestSelect(source, transformed, selector);
            
            source.RemoveAt(1);
            TestSelect(source, transformed, selector);
            
            transformed.Insert(1, 106);
            TestSelect(source, transformed, selector);
            
            transformed.RemoveAt(1);
            TestSelect(source, transformed, selector);
            
            source.Move(2, 1);
            TestSelect(source, transformed, selector);
            
            source[1] = 9;
            TestSelect(source, transformed, selector);
            
            transformed[1] = 108;
            TestSelect(source, transformed, selector);
            
            source.Clear();
            TestSelect(source, transformed, selector);
            
            source.Add(4);
            TestSelect(source, transformed, selector);

            transformed.Clear();
            TestSelect(source, transformed, selector);
        }

        private void TestSelect<TSource, TTarget>(IList<TSource> expected, IList<TTarget> actual, Func<TSource, TTarget> selector)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (var index = 0; index < expected.Count; index++)
            {
                Assert.AreEqual(selector(expected[index]), actual[index]);
            }
        }
    }
}
