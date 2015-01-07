using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Tests.Extensions;

namespace Catrobat.IDE.Core.Tests.Tests.Services.Common
{
    [TestClass]
    public class TraceServiceTests
    {
        [TestMethod, TestCategory("Services")]
        public void TraceServiceTest1()
        {
            var service = new TraceService();

            service.Add(TraceType.Info, "T1");
            service.Add(TraceType.Warining, "T2", "D2");
            service.Add(TraceType.Error, "T3", "D3", "C3");

            Assert.AreEqual(TraceType.Info, service.Entries[0].Type);
            Assert.AreEqual(TraceType.Warining, service.Entries[1].Type);
            Assert.AreEqual(TraceType.Error, service.Entries[2].Type);

            Assert.AreEqual("T1", service.Entries[0].Title);
            Assert.AreEqual("T2", service.Entries[1].Title);
            Assert.AreEqual("T3", service.Entries[2].Title);

            Assert.AreEqual(null, service.Entries[0].Description);
            Assert.AreEqual("D2", service.Entries[1].Description);
            Assert.AreEqual("D3", service.Entries[2].Description);

            Assert.AreEqual(null, service.Entries[0].Content);
            Assert.AreEqual(null, service.Entries[1].Content);
            Assert.AreEqual("C3", service.Entries[2].Content);
        }

        [TestMethod, TestCategory("Services")]
        public void TraceServiceTest2()
        {
            var service = new TraceService();

            service.Add(TraceType.Info, "T1");

            var traceString = service.CreateTraceString();

            Assert.AreEqual("Info: T1" +Environment.NewLine , traceString);
        }

        [TestMethod, TestCategory("Services")]
        public void TraceServiceTest3()
        {
            var service = new TraceService();
            
            for(int i = 0; i <210; i++)
                service.Add(TraceType.Info, "T" + i);

            Assert.AreEqual(200, service.Entries.Count);
        }
    }
}
