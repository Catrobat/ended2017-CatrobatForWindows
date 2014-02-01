using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using System;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaEvaluator
    {
        private readonly Random _random = new Random();

        public object Evaluate(IFormulaTree formula)
        {
            if (formula == null) return null;

            var type = formula.GetType();

            // numbers
            if (type == typeof(FormulaNodeNumber)) return Evaluate((FormulaNodeNumber)formula);
            if (type == typeof(FormulaNodePi)) return Evaluate((FormulaNodePi)formula, Math.PI);

            // arithmetic
            if (type == typeof(FormulaNodeAdd)) return Evaluate((FormulaNodeAdd)formula, (x, y) => x + y);
            if (type == typeof(FormulaNodeSubtract)) return Evaluate((FormulaNodeSubtract)formula, (x, y) => x - y);
            if (type == typeof(FormulaNodeMultiply)) return Evaluate((FormulaNodeMultiply)formula, (x, y) => x * y);
            if (type == typeof(FormulaNodeDivide)) return Evaluate((FormulaNodeDivide)formula, (x, y) => x / y);

            // relational operators
            if (type == typeof(FormulaNodeEquals)) return Evaluate((FormulaNodeEquals)formula,(x, y) => x == y, (x, y) => Math.Abs(x - y) <= double.Epsilon);
            if (type == typeof(FormulaNodeNotEquals)) return Evaluate((FormulaNodeNotEquals)formula, (x, y) => x != y, (x, y) => Math.Abs(x - y) > double.Epsilon);
            if (type == typeof(FormulaNodeLess)) return Evaluate((FormulaNodeLess)formula, (x, y) => x < y);
            if (type == typeof(FormulaNodeLessEqual)) return Evaluate((FormulaNodeLessEqual)formula, (x, y) => x <= y);
            if (type == typeof(FormulaNodeGreater)) return Evaluate((FormulaNodeGreater)formula, (x, y) => x > y);
            if (type == typeof(FormulaNodeGreaterEqual)) return Evaluate((FormulaNodeGreaterEqual)formula, (x, y) => x >= y);

            // logic
            if (type == typeof(FormulaNodeTrue)) return Evaluate((FormulaNodeTrue)formula, true);
            if (type == typeof(FormulaNodeFalse)) return Evaluate((FormulaNodeFalse)formula, false);
            if (type == typeof (FormulaNodeAnd))return Evaluate((FormulaNodeAnd) formula, (x, y) => x && y, (x, y) => ((long) x) & ((long) y));
            if (type == typeof (FormulaNodeOr)) return Evaluate((FormulaNodeOr) formula, (x, y) => x && y, (x, y) => ((long) x) | ((long) y));
            if (type == typeof (FormulaNodeNot)) return Evaluate((FormulaNodeNot) formula, x => !x, x => ~((long) x));

            // min/max
            if (type == typeof(FormulaNodeMin)) return Evaluate((FormulaNodeMin)formula, Math.Min);
            if (type == typeof(FormulaNodeMax)) return Evaluate((FormulaNodeMax)formula, Math.Max);

            // exponential functions and logarithms
            if (type == typeof(FormulaNodeExp)) return Evaluate((FormulaNodeExp)formula, Math.Exp);
            if (type == typeof(FormulaNodeLog)) return Evaluate((FormulaNodeLog)formula, Math.Log);
            if (type == typeof(FormulaNodeLn)) return Evaluate((FormulaNodeLn)formula, x => Math.Log(x, Math.E));

            // trigonometric functions
            if (type == typeof(FormulaNodeSin)) return Evaluate((FormulaNodeSin)formula, Math.Sin);
            if (type == typeof(FormulaNodeCos)) return Evaluate((FormulaNodeCos)formula, Math.Cos);
            if (type == typeof(FormulaNodeTan)) return Evaluate((FormulaNodeTan)formula, Math.Tan);
            if (type == typeof(FormulaNodeArcsin)) return Evaluate((FormulaNodeArcsin)formula, Math.Asin);
            if (type == typeof(FormulaNodeArccos)) return Evaluate((FormulaNodeArccos)formula, Math.Acos);
            if (type == typeof(FormulaNodeArctan)) return Evaluate((FormulaNodeArctan)formula, Math.Atan);

            // miscellaneous functions
            if (type == typeof(FormulaNodeSqrt)) return Evaluate((FormulaNodeSqrt)formula, Math.Sqrt);
            if (type == typeof(FormulaNodeAbs)) return Evaluate((FormulaNodeAbs)formula, Math.Abs);
            if (type == typeof(FormulaNodeMod)) return Evaluate((FormulaNodeMod)formula, (x, y) => x % y);
            if (type == typeof(FormulaNodeRound)) return Evaluate((FormulaNodeRound)formula, Math.Round);
            if (type == typeof(FormulaNodeRandom)) return Evaluate((FormulaNodeRandom)formula, (x, y) => x + _random.NextDouble()*(y - x));

            // TODO: sensors
            // TODO: object variables
            // TODO: user variables

            // brackets
            if (type == typeof(FormulaNodeParentheses)) return Evaluate((FormulaNodeParentheses)formula);

            throw new NotImplementedException();
        }

        private static TOut Evaluate<TOut>(ConstantFormulaTree formula, TOut constant)
        {
            return constant;
        }

        private static TOut Evaluate<TOut>(ConstantFormulaTree<TOut> formula)
        {
            return formula.Value;
        }

        private object Evaluate(UnaryFormulaTree formula)
        {
            return Evaluate(formula.Child);
        }

        private TOut Evaluate<TOut>(UnaryFormulaTree formula, Func<double, TOut> numberEvaluator)
        {
            var x = Evaluate(formula.Child);
            return numberEvaluator.Invoke((double) x);
        }

        private object Evaluate<TOut1, TOut2>(UnaryFormulaTree formula, Func<bool, TOut1> logicEvaluator, Func<double, TOut2> numberEvaluator)
        {
            var x = Evaluate(formula.Child);
            return x is bool ? (object)logicEvaluator((bool)x) : numberEvaluator.Invoke((double)x);
        }

        private TOut Evaluate<TOut>(BinaryFormulaTree formula, Func<double, double, TOut> numberEvaluator)
        {
            var x = Evaluate(formula.FirstChild);
            var y = Evaluate(formula.SecondChild);
            return numberEvaluator.Invoke((double) x, (double) y);
        }

        private object Evaluate<TOut1, TOut2>(BinaryFormulaTree formula, Func<bool, bool, TOut1> logicEvaluator, Func<double, double, TOut2> numberEvaluator)
        {
            var x = Evaluate(formula.FirstChild);
            var y = Evaluate(formula.SecondChild);
            return x is bool && y is bool ? (object) logicEvaluator((bool)x, (bool)y) : numberEvaluator((double)x, (double)y);
        }
    }
}
