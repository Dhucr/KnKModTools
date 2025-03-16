using KnKModTools.Helper;
using System.Text.RegularExpressions;

namespace KnKModTools.DatClass.Decomplie
{
    public class CodeFormatting
    {
        public static string TryInitializeLocalVar(DecompileContext ctx,
            int index, string value)
        {
            if (ctx.CurrentNode.Statements.LastOrDefault()
            is ExpressionNode ex && value == "Result[0]")
            {
                value = ex.Expression;
                ctx.CurrentNode.Statements.Remove(ex);
            }

            var ele = ctx.EvalStack.GetElementAtIndex(index).Expression;

            ex = ctx.CurrentNode.Statements.LastOrDefault() as ExpressionNode;
            if (ex != null && ex.Expression.Equals("let " + ele + " = null"))
            {
                ele = "let " + ele;
                ctx.CurrentNode.Statements.Remove(ex);
            }

            return $"{ele} = {value}";
        }

        public static void SimplifyConditions(DecompileContext ctx,
            ExpressionNode value, MatchCollection matches = null,
            List<AstNode> stam = null)
        {
            if (matches == null)
            {
                var pattern = @"Result\[(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\]";
                matches = Regex.Matches(value.Expression, pattern);
            }

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                stam ??= ctx.CurrentNode.Statements;
                if (stam.Count < i) break;

                for (var j = stam.Count - 1; j >= 0; j--)
                {
                    if (stam[j] != null && stam[j] is ExpressionNode node)
                    {
                        if (node.Expression.Contains("Result[0]") || node.Expression.Contains('='))
                        {
                            break;
                        }

                        value.Expression = Utilities.ReplaceNthOccurrence(value.Expression,
                            matches[i].Value, node.Expression, i + 1);
                        stam.Remove(stam[j]);
                        break;
                    }
                }
            }
        }

        public static string SimplifyConditions(DecompileContext ctx,
            string value, MatchCollection matches = null,
            List<AstNode> stam = null)
        {
            if (matches == null)
            {
                var pattern = @"Result\[(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\]";
                matches = Regex.Matches(value, pattern);
            }

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                stam ??= ctx.CurrentNode.Statements;
                if (stam.Count < i) break;

                for (var j = stam.Count - 1; j >= 0; j--)
                {
                    if (stam[j] != null && stam[j] is ExpressionNode node)
                    {
                        if (node.Expression.Contains("Result[0]") || node.Expression.Contains('='))
                        {
                            break;
                        }

                        value = Utilities.ReplaceNthOccurrence(value,
                        matches[i].Value, node.Expression, i + 1);
                        stam.Remove(stam[j]);
                        break;
                    }
                }
            }

            return value;
        }

        public static void TempVar(DecompileContext ctx, ExpressionNode id)
        {
            if (id.Expression.Contains("_temp"))
            {
                ctx.CurrentNode.Statements.RemoveAll(node =>
                node is ExpressionNode expr && expr.Expression.Contains(id.Expression));
                ctx.TempVarCounter--;
            }
        }
    }
}