using Esprima.Ast;
using KnKModTools.DatClass.Decomplie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnKModTools.DatClass.Complie
{
    // 辅助类
    public class Label
    {
        public InStruction Ins { get; set; }

        public Label()
        {
            Ins = new InStruction { Code = 38 };
        }

        public Label(InStruction ins)
        {
            Ins = ins;
        }

        public void SetLine(CompileContext cont, Node node)
        {
            Ins.Operands = [(ushort)cont.GetLine(node)];
        }

        public void SetLine(int line)
        {
            Ins.Operands = [(ushort)line];
        }
    }
}
