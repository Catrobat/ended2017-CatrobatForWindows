using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Phone.Converters
{
    /// <summary>
    /// <p>Workaround <see cref="TypeConverter"/> to reference classes in other namespaces in XAML. </p>
    /// <p>Example: <example><c>Type = "nodes:FormulaNodeBla"</c></example>. </p>
    /// <p>See <see cref="http://stackoverflow.com/a/3810871/1220972"/></p>
    /// </summary>
    public class NamespaceTypeConverter : TypeConverter
    {
        #region Static members

        private static Assembly _core;
        private static Assembly Core
        {
            get { return _core ?? (_core = typeof(IFormulaTree).Assembly); }
        }

        private static string _nodesNamespace;
        private static string NodesNamespace
        {
            get { return _nodesNamespace ?? (_nodesNamespace = typeof(FormulaNodeNumber).Namespace); }
        }

        private static string _tokensNamespace;
        private static string TokensNamespace
        {
            get { return _tokensNamespace ?? (_tokensNamespace = typeof (FormulaTokenParenthesis).Namespace); }
        }

        #endregion

        private static Type GetType(string prefix, string name)
        {
            if (prefix == "formulaTree")
            {
                return Core.GetType(NodesNamespace + "." + name);
            }
            if (prefix == "formulaToken")
            {
                return Core.GetType(TokensNamespace + "." + name);
            }
            throw new NotImplementedException("Please implement namespace \"" + prefix + "\". ");
        }

        #region TypeConverter

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var value2 = (string) value;
            var prefixLength = value2.IndexOf(":");
            if (prefixLength == -1) return base.ConvertFrom(context, culture, value);
            return GetType(value2.Substring(0, prefixLength), value2.Substring(prefixLength + 1));
        }


        #endregion
    }
}