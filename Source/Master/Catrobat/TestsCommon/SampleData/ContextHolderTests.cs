using Catrobat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.TestsCommon.SampleData
{
  public class ContextHolderTests : IContextHolder
  {
    private CatrobatContext _catrobatContext;

    public ContextHolderTests(CatrobatContext catrobatContext)
    {
      _catrobatContext = catrobatContext;
    }

    public CatrobatContext GetContext()
    {
      return _catrobatContext;
    }
  }
}
