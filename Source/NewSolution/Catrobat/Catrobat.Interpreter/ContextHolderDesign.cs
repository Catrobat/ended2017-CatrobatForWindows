using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.Interpreter
{
    public class ContextHolderDesign : IContextHolder
    {
        private static CatrobatContextBase _context;

        public CatrobatContextBase GetContext()
        {
            if (_context == null)
                _context = new CatrobatContextDesign();

            return _context;
        }
    }
}
