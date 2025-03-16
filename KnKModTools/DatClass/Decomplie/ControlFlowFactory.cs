using KnKModTools.Helper;
using KnKModTools.UI;

namespace KnKModTools.DatClass.Decomplie
{
    public static class ControlFlowFactory
    {
        #region 块节点构建

        public static BlockNode CreateControlFlow(DecompileContext ctx, DecompilerCore core, BasicBlock block)
        {
            if (block == null || block.Visited) return null;
            block.Visited = true;

            var currentBlock = new BlockNode();
            using (ctx.CaptureNodeState(currentBlock))
            {
                core.BuildBaseInstruction(ctx, block, currentBlock);

                // 处理控制流
                InStruction? lastInstr = block.Instructions.LastOrDefault();
                if (lastInstr != null && core.IsBranching(lastInstr.Code))
                {
                    AstNode node = HandleBranchingAst(ctx, core, block, lastInstr);
                    if (node != null) currentBlock.Statements.Add(node);
                }
            }

            return currentBlock;
        }

        private static AstNode HandleBranchingAst(DecompileContext ctx,
            DecompilerCore core, BasicBlock block, InStruction instr)
        {
            AstNode ifNode = null;
            switch (instr.Code)
            {
                case 14: // JUMPIFTRUE
                    ifNode = BuildIFTrueBlock(ctx, core, block);
                    break;

                case 15: //  JUMPIFFALSE
                    ifNode = BuildIFFalseBlock(ctx, core, block);
                    break;
            }
            return ifNode;
        }

        private static AstNode BuildIFTrueBlock(DecompileContext ctx, DecompilerCore core, BasicBlock block)
        {
            BasicBlock trueBlock = core.GetTrueBlock(ctx, block);
            BasicBlock falseBlock = core.GetFalseBlock(ctx, block);

            if (IsSwitchStructure(block, trueBlock, falseBlock, true))
            {
                return BuildTSwitchNode(ctx, core, block, trueBlock, falseBlock);
            }
            else if (IsTIfElseStructure(trueBlock, falseBlock))
            {
                List<BasicBlock> then = block.FalseSuccessors;
                if (block.FalseSuccessors.Count == 1 && falseBlock.Instructions.Count == 1)
                {
                    falseBlock.Visited = true;
                    then = falseBlock.TrueSuccessors;
                }

                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                    ThenBlock = StackMaintenance(ctx, core, then),
                    ElseBlock = StackMaintenance(ctx, core, block.TrueSuccessors)
                };
                return node;
            }
            else if (IsTSingleIfStructure(core, trueBlock, falseBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                    ThenBlock = StackMaintenance(ctx, core, block.TrueSuccessors)
                };

                return node;
            }
            else
            {
                UIData.ShowMessage(Utilities.GetDisplayName("WarningCFG"), 
                    HandyControl.Data.InfoType.Warning);
            }
            return null;
        }

        private static AstNode BuildIFFalseBlock(DecompileContext ctx, DecompilerCore core, BasicBlock block)
        {
            BasicBlock trueBlock = core.GetTrueBlock(ctx, block);
            BasicBlock falseBlock = core.GetFalseBlock(ctx, block);

            if (IsSwitchStructure(block, trueBlock, falseBlock, false))
            {
                return BuildFSwitchNode(ctx, core, block, trueBlock, falseBlock);
            }
            else if (IsFChainStructure(ctx, core, trueBlock, falseBlock))
            {
                return BuildChainNode(ctx, core, block, falseBlock);
            }
            else if (IsFIfElseStructure(ctx, core, trueBlock, falseBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                    ThenBlock = StackMaintenance(ctx, core, block.TrueSuccessors),
                    ElseBlock = StackMaintenance(ctx, core, block.FalseSuccessors)
                };

                return node;
            }
            else if (IsWhileStructure(block, trueBlock))
            {
                var node = new WhileNode
                {
                    Condition = core.GetCondition(ctx, false),
                    Body = StackMaintenance(ctx, core, block.TrueSuccessors)
                };

                return node;
            }
            else if (IsFSingleIfStructure(trueBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                    ThenBlock = StackMaintenance(ctx, core, block.TrueSuccessors)
                };

                return node;
            }
            else
            {
                UIData.ShowMessage(Utilities.GetDisplayName("WarningCFG"),
                    HandyControl.Data.InfoType.Warning);
            }
            return null;
        }

        private static BlockNode StackMaintenance(DecompileContext ctx,
            DecompilerCore core, List<BasicBlock> children)
        {
            var node = new BlockNode();
            using (ctx.CaptureStackState())
            {
                node = BuildBranchNode(ctx, core, children);
            }

            return node;
        }

        private static BlockNode StackMaintenance(DecompileContext ctx,
            DecompilerCore core, BasicBlock start, uint endAddr)
        {
            var node = new BlockNode();

            using (ctx.CaptureStackState())
            {
                var pos = start.EndAddr;
                while (pos < endAddr)
                {
                    BlockNode b = CreateControlFlow(ctx, core, start);
                    if (b == null)
                    {
                        start = core.GetNextBlock(ctx, start);
                        if (start == null) break;
                        break;
                    }

                    foreach (AstNode stam in b.Statements)
                    {
                        node.Statements.Add(stam);
                    }

                    start = core.GetNextBlock(ctx, start);

                    if (start == null) break;
                    pos = start.EndAddr;
                }
            }

            return node;
        }

        private static BlockNode BuildBranchNode(DecompileContext ctx,
            DecompilerCore core, List<BasicBlock> blocks)
        {
            var node = new BlockNode();
            foreach (BasicBlock block in blocks)
            {
                BlockNode b = CreateControlFlow(ctx, core, block);
                if (b == null) continue;

                foreach (AstNode stam in b.Statements)
                {
                    node.Statements.Add(stam);
                }
            }
            return node;
        }

        private static SwitchNode BuildTSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock)
        {
            (SwitchNode switchNode, BasicBlock block) = BuildSwitchNode(ctx, core, header, trueBlock, falseBlock, BlockType.IFTrue);

            var targetAddr = core.GetCode(trueBlock) == 13 ?
                (uint)core.GetLastIns(block.FalseSuccessors).Operands[0] :
                core.GetAddress(trueBlock);
            if (core.GetAddress(block) <= targetAddr)
            {
                BasicBlock? last = block.FalseSuccessors.LastOrDefault();
                if (block.FalseSuccessors.Count == 1 && last?.Instructions.Count == 1)
                {
                    last.Visited = true;
                    last = last.TrueSuccessors[0];
                }
                if (core.GetCode(last) == 13) targetAddr = core.GetInsLength(last);
                switchNode.DefaultCase = StackMaintenance(
                    ctx, core, last, targetAddr);
            }

            return switchNode;
        }

        private static SwitchNode BuildFSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock)
        {
            (SwitchNode switchNode, BasicBlock block) = BuildSwitchNode(ctx, core, header, trueBlock, falseBlock, BlockType.IFFlase);

            if (block.FalseSuccessors.Count > 0)
            {
                switchNode.DefaultCase = StackMaintenance(ctx, core, block.FalseSuccessors);
            }

            return switchNode;
        }

        private static (SwitchNode, BasicBlock) BuildSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock,
            BlockType type)
        {
            var cond = core.GetCondition(ctx, true).Expression.Split(" == ");
            var switchNode = new SwitchNode
            {
                TestExpression = new ExpressionNode() { Expression = cond[0] }
            };

            AddCaseNode(ctx, core, header.TrueSuccessors, switchNode, cond[1]);

            BasicBlock block = BuildCaseNode(ctx, core, falseBlock, switchNode, type, cond[0]);

            switchNode.DefaultCase = new BlockNode();

            return (switchNode, block);
        }

        private static BasicBlock BuildCaseNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock ifFalseBlock,
            SwitchNode switchNode, BlockType type, string test)
        {
            while (true)
            {
                List<BasicBlock> trueBlocks = ifFalseBlock.TrueSuccessors;

                var match = core.GetCaseMatchValue(ctx, ifFalseBlock, test);
                if (match.Equals("NotCase")) break;

                AddCaseNode(ctx, core, trueBlocks, switchNode, match);

                BasicBlock update = core.GetFalseBlock(ctx, ifFalseBlock);
                if (update == null || update.Type != type) break;

                ifFalseBlock = update;
            }

            return ifFalseBlock;
        }

        private static void AddCaseNode(DecompileContext ctx,
            DecompilerCore core, List<BasicBlock> blocks,
            SwitchNode switchNode, string match)
        {
            switchNode.Cases.Add(new CaseNode
            {
                MatchValue = match
            });
            switchNode.Cases.Last().Body = StackMaintenance(ctx, core, blocks);
        }

        private static void AddConditionNode(DecompileContext ctx,
            DecompilerCore core, List<BasicBlock> blocks,
            IfElseChainNode chainNode, AstNode match)
        {
            chainNode.Conditions.Add(new ConditionBlock()
            {
                Condition = match
            });
            chainNode.Conditions.Last().Body = StackMaintenance(ctx, core, blocks);
        }

        private static IfElseChainNode BuildChainNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header, BasicBlock falseBlock)
        {
            var chainNode = new IfElseChainNode();

            AddConditionNode(ctx, core, header.TrueSuccessors,
                chainNode, core.GetCondition(ctx, false));

            BasicBlock block = BuildChainNode(ctx, core, chainNode, falseBlock);

            if (block.FalseSuccessors.Count > 0)
            {
                chainNode.ElseBlock = StackMaintenance(ctx, core, block.FalseSuccessors);
            }

            return chainNode;
        }

        private static BasicBlock BuildChainNode(DecompileContext ctx,
           DecompilerCore core, IfElseChainNode chainNode,
           BasicBlock falseBlock)
        {
            while (true)
            {
                List<BasicBlock> trueBlocks = falseBlock.TrueSuccessors;

                ExpressionNode cond = core.GetChainConditions(ctx, falseBlock);
                if (cond == null) break;

                AddConditionNode(ctx, core, trueBlocks, chainNode, cond);

                BasicBlock update = core.GetFalseBlock(ctx, falseBlock);

                if (update == null || update.Type != BlockType.IFFlase) break;

                falseBlock = update;
            }

            chainNode.ElseBlock = new BlockNode();

            return falseBlock;
        }

        #endregion 块节点构建

        #region 逻辑结构判断

        private static bool IsWhileStructure(BasicBlock header, BasicBlock trueBlock)
        {
            if (trueBlock == null) return false;

            InStruction? laseHeaderBlock = header.Instructions.LastOrDefault();

            if (trueBlock.Type != BlockType.Jump) return false;

            var trueJumpAddr = (uint)trueBlock.Instructions.Last().Operands[0];

            return trueJumpAddr < (uint)laseHeaderBlock.Operands[0];
        }

        private static bool IsFSingleIfStructure(BasicBlock trueBlock)
        {
            if (trueBlock == null) return false;

            // 特征1：真分支没有终止跳转
            return trueBlock.Type != BlockType.Jump;
        }

        private static bool IsTSingleIfStructure(DecompilerCore core, 
            BasicBlock trueBlock, BasicBlock falseBlock)
        {
            if (trueBlock == null) return false;

            // 特征1：真分支没有终止跳转
            return trueBlock.Type == BlockType.Jump && 
                falseBlock.Type == BlockType.Jump && 
                core.GetAddress(trueBlock) == core.GetAddress(falseBlock);
        }

        private static bool IsFIfElseStructure(DecompileContext ctx, 
            DecompilerCore core, BasicBlock trueBlock, BasicBlock falseBlock)
        {
            if (trueBlock == null || falseBlock == null) return false;

            // 特征1：真分支包含跳转到合并点的JUMP
            if (trueBlock.Type != BlockType.Jump &&
                trueBlock.Type != BlockType.Exit) return false;

            if (trueBlock.Type != BlockType.Exit)
            {
                // 特征2：真分支跳转目标地址必定大于假分支开始地址
                var trueJumpAddr = (uint)trueBlock.Instructions.Last().Operands[0];
                var falseStartAddr = falseBlock.StartAddr;
                var validMerge = trueJumpAddr > falseStartAddr &&
                                 core.GetBlock(ctx, trueJumpAddr).StartAddr > falseStartAddr;

                return validMerge;
            }
            return true;
        }

        private static bool IsTIfElseStructure(BasicBlock trueBlock, BasicBlock falseBlock)
        {
            if (trueBlock == null || falseBlock == null) return false;

            // if-else结构特征检测

            // 特征1：真分支和假分支都包含跳转到合并点的JUMP
            var hasTrueJump = trueBlock.Type == BlockType.Jump ||
                trueBlock.Type == BlockType.Exit;// 存在JUMP指令
            var hasFalseJump = falseBlock?.Type == BlockType.Jump ||
                falseBlock?.Type == BlockType.Exit; // 存在JUMP指令

            if (!(hasTrueJump && hasFalseJump)) return false;

            if (falseBlock?.Type != BlockType.Exit)
            {
                if (falseBlock?.TrueSuccessors.Count == 0) return false;
                BasicBlock? jumpBlock = falseBlock?.TrueSuccessors?.LastOrDefault();
                var hasJumpJump = jumpBlock?.Type == BlockType.Jump; // 存在JUMP指令

                if (!hasJumpJump) return false;

                var trueJump = (uint)trueBlock.Instructions.LastOrDefault().Operands[0];
                var jump = (uint)jumpBlock.Instructions.LastOrDefault().Operands[0];

                return trueJump == jump;
            }
            return true;
        }

        private static bool IsSwitchStructure(BasicBlock header, BasicBlock trueBlock, BasicBlock falseBlock, bool isTF)
        {
            if (trueBlock == null || falseBlock == null) return false;

            if (isTF && falseBlock.Type != BlockType.IFTrue) return false;

            if (!isTF && falseBlock.Type != BlockType.IFFlase) return false;

            var isIfTrue = trueBlock.Type == BlockType.Jump || trueBlock.Type == BlockType.Exit;

            if (falseBlock?.TrueSuccessors.Count == 0) return false;
            BasicBlock? jumpBlock = falseBlock?.TrueSuccessors?.LastOrDefault();
            var isJump = jumpBlock?.Type == BlockType.Jump || trueBlock.Type == BlockType.Exit;

            var typeCheck = isIfTrue && isJump;
            if (!typeCheck) return false;

            if (trueBlock.Type != BlockType.Exit)
            {
                InStruction? lastInTrueBlock = trueBlock.Instructions.LastOrDefault();

                InStruction? jump = jumpBlock?.Instructions.LastOrDefault();
                var targetEquals = (uint)lastInTrueBlock?.Operands[0] == (uint)jump?.Operands[0];

                if (!targetEquals) return false;
            }

            var equals = false;
            var test = false;
            var list = new List<byte> { 2, 3, 7, 9 };
            List<InStruction>? conBlock1 = falseBlock?.Instructions;
            List<InStruction>? conBlock2 = header?.Instructions;

            var min = Math.Min(conBlock1.Count, conBlock2.Count);
            if (conBlock1[conBlock1.Count - 2].Code == 21 &&
                    conBlock2[conBlock2.Count - 2].Code == 21)
            {
                equals = true;
            }
            for (var i = 3; i <= min; i++)
            {
                if (list.Contains(conBlock1[conBlock1.Count - i].Code) &&
                    list.Contains(conBlock2[conBlock2.Count - i].Code))
                {
                    test = true;
                }
            }

            return equals && test;
        }

        private static bool IsFChainStructure(DecompileContext ctx, DecompilerCore core, BasicBlock trueBlock, BasicBlock falseBlock)
        {
            if (trueBlock == null || falseBlock == null) return false;

            if (falseBlock.Type != BlockType.IFFlase) return false;

            var isIfTrue = trueBlock.Type == BlockType.Jump || trueBlock.Type == BlockType.Exit;

            if (falseBlock?.TrueSuccessors.Count == 0) return false;

            if (!isIfTrue) return false;

            if (trueBlock.Type != BlockType.Exit)
            {
                InStruction? lastInTrueBlock = trueBlock.Instructions.LastOrDefault();

                InStruction? jump = falseBlock?.Instructions.LastOrDefault();
                InStruction? last = falseBlock?.TrueSuccessors?.LastOrDefault()?.Instructions?.LastOrDefault();
                if (last?.Code == 11)
                {
                    jump = last;
                }
                var targetEquals = (uint)lastInTrueBlock?.Operands[0] == (uint)jump?.Operands[0];

                if (!targetEquals) return false;
            }

            ExpressionNode cond = core.GetChainConditions(ctx, falseBlock);
            falseBlock.Visited = false;

            return cond != null;
        }

        #endregion 逻辑结构判断
    }
}