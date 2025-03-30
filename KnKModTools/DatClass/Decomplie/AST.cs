using System.Text;
using System.Text.RegularExpressions;

namespace KnKModTools.DatClass.Decomplie
{
    // 定义基础AST节点类型
    public abstract class AstNode
    {
        public abstract void GenerateCode(StringBuilder sb, int indent);
    }

    // 表达式节点
    public class ExpressionNode : AstNode
    {
        public string Expression { get; set; }
        public string Operator { get; set; }
        public int Precedence { get; set; }

        public override void GenerateCode(StringBuilder sb, int indent) => sb.Append(' ', indent * 4).Append(Expression);

        public override string ToString()
        {
            return Expression;
        }
    }

    // 语句块节点
    public class BlockNode : AstNode
    {
        public List<AstNode> Statements = [];

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            foreach (AstNode stmt in Statements)
            {
                stmt.GenerateCode(sb, indent);
                if (stmt != Statements.Last())
                {
                    sb.AppendLine();
                }
            }
        }
    }

    // 条件语句节点
    public class IfStatementNode : AstNode
    {
        public AstNode Condition { get; set; }
        public BlockNode ThenBlock { get; set; }
        public BlockNode ElseBlock { get; set; }

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            sb.Append(' ', indent * 4).Append("if (");
            Condition.GenerateCode(sb, 0);
            if (ThenBlock == null && ElseBlock == null ||
                ThenBlock?.Statements.Count == 0 &&
                ElseBlock?.Statements.Count == 0)
            {
                sb.AppendLine(") { }");
                return;
            }

            sb.AppendLine(") {");
            if (ThenBlock?.Statements.Count > 0)
            {
                ThenBlock.GenerateCode(sb, indent + 1);
                sb.AppendLine();
            }

            if (ElseBlock?.Statements.Count > 0)
            {
                sb.Append(' ', indent * 4).AppendLine("} else {");
                ElseBlock.GenerateCode(sb, indent + 1);
                sb.AppendLine();
            }

            sb.Append(' ', indent * 4).Append("}");
        }
    }

    public class SwitchNode : AstNode
    {
        public AstNode TestExpression { get; set; }
        public List<CaseNode> Cases { get; } = [];
        public BlockNode DefaultCase { get; set; }

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            // 生成switch头部
            sb.Append(' ', indent * 4).Append("switch (");
            TestExpression.GenerateCode(sb, 0);
            sb.AppendLine(") {");

            // 生成所有case
            foreach (CaseNode caseNode in Cases)
            {
                caseNode.GenerateCode(sb, indent + 1);
            }

            // 生成默认case
            if (DefaultCase.Statements.Count > 0)
            {
                sb.Append(' ', (indent + 1) * 4).AppendLine("default:");
                DefaultCase.GenerateCode(sb, indent + 2);
                sb.AppendLine();
                CaseNode.TryAddBreak(sb, indent + 2);
            }

            sb.Append(' ', indent * 4).AppendLine("}");
        }
    }

    public class CaseNode : AstNode
    {
        public string MatchValue { get; set; }
        public BlockNode Body { get; set; }

        private static readonly string pattern = @"(?m)^.*\b(return|break|continue)\b(?=.*\r?\n[^\r\n]*$)";

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            // 生成case标签
            sb.Append(' ', indent * 4).Append("case ");
            sb.Append(MatchValue);
            sb.AppendLine(":");

            // 生成case体
            Body.GenerateCode(sb, indent + 1);
            sb.AppendLine();

            TryAddBreak(sb, indent + 1);
        }

        public static void TryAddBreak(StringBuilder sb, int indent)
        {
            if (!Regex.IsMatch(sb.ToString(), pattern))
            {
                sb.Append(' ', indent * 4).AppendLine("break");
            }
        }
    }

    public class WhileNode : AstNode
    {
        public AstNode Condition { get; set; }
        public BlockNode Body { get; set; } = new();

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            sb.Append(' ', indent * 4).Append("while (");
            Condition.GenerateCode(sb, 0);
            sb.AppendLine(") {");
            Body.GenerateCode(sb, indent + 1);
            sb.AppendLine();
            sb.Append(' ', indent * 4).AppendLine("}");
        }
    }

    public class IfElseChainNode : AstNode
    {
        public List<ConditionBlock> Conditions = [];
        public BlockNode ElseBlock = new();

        public override void GenerateCode(StringBuilder sb, int indent)
        {
            // 生成首个if
            ConditionBlock first = Conditions[0];
            sb.Append(' ', indent * 4).Append("if (");
            first.Condition.GenerateCode(sb, 0);
            sb.AppendLine(") {");
            first.Body.GenerateCode(sb, indent + 1);
            sb.AppendLine();
            sb.Append(' ', indent * 4).AppendLine("}");

            // 生成后续else if
            foreach (ConditionBlock? cond in Conditions.Skip(1))
            {
                sb.Append(' ', indent * 4).Append("else if (");
                cond.Condition.GenerateCode(sb, 0);
                sb.AppendLine(") {");
                cond.Body.GenerateCode(sb, indent + 1);
                sb.AppendLine();
                sb.Append(' ', indent * 4).AppendLine("}");
            }

            // 生成else
            if (ElseBlock.Statements.Count > 0)
            {
                sb.Append(' ', indent * 4).AppendLine("else {");
                ElseBlock.GenerateCode(sb, indent + 1);
                sb.AppendLine();
                sb.Append(' ', indent * 4).Append("}");
            }
        }
    }

    public class ConditionBlock
    {
        public AstNode Condition { get; set; }
        public BlockNode Body { get; set; }
    }
}