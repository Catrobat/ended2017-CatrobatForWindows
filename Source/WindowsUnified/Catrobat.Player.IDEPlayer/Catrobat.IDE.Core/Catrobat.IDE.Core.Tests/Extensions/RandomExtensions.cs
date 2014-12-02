using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

namespace Catrobat.IDE.Core.Tests.Extensions
{
    public static class RandomExtensions
    {
        private static readonly IList<Func<Random, IFormulaToken>> TokenCreators = typeof(FormulaTokenFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select<MethodInfo, Func<Random, IFormulaToken>>(method => random => (IFormulaToken) method.Invoke(
                    method.GetParameters().Select<ParameterInfo, object>(parameter =>
                    {
                        if (parameter.ParameterType == typeof (int)) return random.Next();
                        if (parameter.ParameterType == typeof (bool)) return random.NextBool();
                        if (parameter.ParameterType == typeof (LocalVariable)) 
                            return new LocalVariable {Name = "LocalVariable" + random.Next()};
                        if (parameter.ParameterType == typeof (GlobalVariable)) 
                            return new GlobalVariable {Name = "GlobalVariable" + random.Next()};
                        throw new NotImplementedException();
                    }).ToArray()))
                .ToArray();

        public static TElement Next<TElement>(this Random random, IList<TElement> elements)
        {
            return elements[random.Next(0, elements.Count)];
        }

        public static IFormulaToken NextFormulaToken(this Random random)
        {
            return random.Next(TokenCreators).Invoke(random);
        }
    }
}
