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
    private readonly CatrobatContext _catrobatContextField;

    public ContextHolderTests(CatrobatContext catrobatContext)
    {
      _catrobatContextField = catrobatContext;
    }

    public CatrobatContextBase GetContext()
    {
      return _catrobatContextField;
    }
  }
}
