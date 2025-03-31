using KnKModTools.Helper;
using KnKModTools.Localization;
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
                case 11: // JUMP
                    ifNode = BuildJumpNode(ctx, core, instr);
                    break;

                case 14: // JUMPIFTRUE
                    ifNode = BuildIFTrueBlock(ctx, core, block);
                    break;

                case 15: //  JUMPIFFALSE
                    ifNode = BuildIFFalseBlock(ctx, core, block);
                    break;
            }
            return ifNode;
        }

        private static AstNode BuildJumpNode(DecompileContext ctx, DecompilerCore core, InStruction instr)
        {
            if (!ctx.Loop.IsInLoop) return null;

            var targetAddr = (uint)instr.Operands[0];
            if (targetAddr >= ctx.Loop.BreakAddr)
            {
                return new ExpressionNode()
                {
                    Expression = "break"
                };
            }
            else if (instr.Offset < ctx.Loop.EndAddr && targetAddr == ctx.Loop.WhileAddr)
            {
                return new ExpressionNode()
                {
                    Expression = "continue"
                };
            }
            else
            {
                return null;
            }
        }

        private static AstNode BuildIFTrueBlock(DecompileContext ctx, DecompilerCore core, BasicBlock block)
        {
            BasicBlock trueBlock = core.GetTrueBlock(ctx, block);
            BasicBlock falseBlock = core.GetFalseBlock(ctx, block);

            if (IsTSwitchStructure(ctx, core, block, trueBlock, falseBlock))
            {
                return BuildTSwitchNode(ctx, core, block);
            }
            else if (IsTIfElseStructure(ctx, core, trueBlock, falseBlock))
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
            else if (IsEmptyBlock(trueBlock, falseBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                };
                return node;
            }
            else
            {
                UIData.ShowMessage(LanguageManager.GetString("WarningCFG"),
                    HandyControl.Data.InfoType.Warning);
                ctx.IsIrreducibleCFG = true;
            }
            return null;
        }

        private static AstNode BuildIFFalseBlock(DecompileContext ctx, DecompilerCore core, BasicBlock block)
        {
            BasicBlock trueBlock = core.GetTrueBlock(ctx, block);
            BasicBlock falseBlock = core.GetFalseBlock(ctx, block);

            if (IsSwitchStructure(ctx, core, block, trueBlock, falseBlock, false))
            {
                return BuildFSwitchNode(ctx, core, block);
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
            else if (IsWhileStructure(ctx, core, block, trueBlock))
            {
                var node = new WhileNode
                {
                    Condition = core.GetCondition(ctx, false)
                };
                using (ctx.CaptureLoopState())
                {
                    ctx.Loop.EnterLoop(core.GetAddress(block.TrueSuccessors),
                    core.GetAddress(block), core.GetOffset(block.TrueSuccessors));
                    node.Body = StackMaintenance(ctx, core, block.TrueSuccessors);
                    ctx.Loop.ExitLoop();
                }

                return node;
            }
            else if (IsFSingleIfStructure(ctx, core, block, trueBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                    ThenBlock = StackMaintenance(ctx, core, block.TrueSuccessors)
                };

                return node;
            }
            else if (IsEmptyBlock(trueBlock, falseBlock))
            {
                var node = new IfStatementNode
                {
                    Condition = core.GetCondition(ctx, false),
                };
                return node;
            }
            else
            {
                UIData.ShowMessage(LanguageManager.GetString("WarningCFG"),
                    HandyControl.Data.InfoType.Warning);
                ctx.IsIrreducibleCFG = true;
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

        private static BlockNode BuildBranchNode(DecompileContext ctx,
            DecompilerCore core, List<BasicBlock> blocks)
        {
            var node = new BlockNode();
            foreach (BasicBlock block in blocks)
            {
                BlockNode b = CreateControlFlow(ctx, core, block);
                if (ctx.IsIrreducibleCFG) break;
                if (b == null) continue;

                foreach (AstNode stam in b.Statements)
                {
                    node.Statements.Add(stam);
                }
            }
            return node;
        }

        private static SwitchNode BuildTSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header)
        {
            var cond = core.GetCondition(ctx, true).Expression.Split(" == ");
            var switchNode = new SwitchNode
            {
                TestExpression = new ExpressionNode() { Expression = cond[0] }
            };

            AddCaseNode(ctx, core, header.TrueSuccessors, switchNode, cond[1]);
            if (ctx.IsIrreducibleCFG) return null;
            var nextBlock = header.FalseSuccessors.FirstOrDefault();
            if (nextBlock is not null &&
                core.GetAddress(nextBlock) == core.GetAddress(header))
            {
                switchNode.Cases.Last().Body = null;
            }

            foreach (var block in header.FalseSuccessors)
            {
                var match = core.GetCaseMatchValue(ctx, block, cond[0]);
                if (match.Equals("NotCase")) break;

                AddCaseNode(ctx, core, block.TrueSuccessors, switchNode, match);
                if (ctx.IsIrreducibleCFG) return null;

                nextBlock = Utilities.GetNextElement(header.FalseSuccessors, block);
                if (nextBlock is not null &&
                    core.GetAddress(nextBlock) == core.GetAddress(block))
                {
                    switchNode.Cases.Last().Body = null;
                }
            }
            switchNode.DefaultCase = new BlockNode();

            var lastIns = core.GetLastIns(header.TrueSuccessors);
            if (lastIns is null || lastIns.Code == 13)
                return switchNode;

            var endAddr = (uint)lastIns.Operands[0];
            var lastJump = header.FalseSuccessors.LastOrDefault();
            var jumpAddr = core.GetAddress(lastJump);

            if (jumpAddr == endAddr)
                return switchNode;

            switchNode.DefaultCase = StackMaintenance(ctx, core, lastJump.TrueSuccessors);
            return switchNode;
        }

        private static SwitchNode BuildFSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header)
        {
            var cond = core.GetCondition(ctx, true).Expression.Split(" == ");
            var switchNode = new SwitchNode
            {
                TestExpression = new ExpressionNode() { Expression = cond[0] }
            };

            AddCaseNode(ctx, core, header.TrueSuccessors, switchNode, cond[1]);
            if (ctx.IsIrreducibleCFG) return null;

            foreach (var block in header.FalseSuccessors)
            {
                var match = core.GetCaseMatchValue(ctx, block, cond[0]);
                if (match.Equals("NotCase")) break;

                AddCaseNode(ctx, core, block.TrueSuccessors, switchNode, match);
                if (ctx.IsIrreducibleCFG) return null;
            }
            switchNode.DefaultCase = new BlockNode();

            var lastBlock = header.FalseSuccessors.LastOrDefault();
            if (lastBlock?.FalseSuccessors.Count > 0)
            {
                switchNode.DefaultCase = StackMaintenance(ctx, core, lastBlock.FalseSuccessors);
            }

            return switchNode;
        }

        private static (SwitchNode, BasicBlock) BuildSwitchNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock,
            BlockType type, bool isCheck = false)
        {
            var cond = core.GetCondition(ctx, true).Expression.Split(" == ");
            var switchNode = new SwitchNode
            {
                TestExpression = new ExpressionNode() { Expression = cond[0] }
            };

            AddCaseNode(ctx, core, header.TrueSuccessors, switchNode, cond[1]);

            BasicBlock block = BuildCaseNode(ctx, core, falseBlock, switchNode,
                type, cond[0], isCheck);

            switchNode.DefaultCase = new BlockNode();

            return (switchNode, block);
        }

        private static BasicBlock BuildCaseNode(DecompileContext ctx,
            DecompilerCore core, BasicBlock ifFalseBlock,
            SwitchNode switchNode, BlockType type, string test,
            bool isCheck = false)
        {
            while (true)
            {
                List<BasicBlock> trueBlocks = ifFalseBlock.TrueSuccessors;

                var match = core.GetCaseMatchValue(ctx, ifFalseBlock, test);
                if (match.Equals("NotCase")) break;

                AddCaseNode(ctx, core, trueBlocks, switchNode, match);
                if (ctx.IsIrreducibleCFG) break;

                BasicBlock update = core.GetFalseBlock(ctx, ifFalseBlock);
                if (update == null || update.Type != type) break;

                if (isCheck)
                {
                    match = core.GetCaseMatchValue(ctx, update, test);
                    update.Visited = false;
                    if (match.Equals("NotCase")) break;
                }

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
            if (ctx.IsIrreducibleCFG) return null;

            BasicBlock block = BuildChainNode(ctx, core, chainNode, falseBlock);
            if (ctx.IsIrreducibleCFG) return null;

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
                if (ctx.IsIrreducibleCFG) break;

                BasicBlock update = core.GetFalseBlock(ctx, falseBlock);

                if (update == null || update.Type != BlockType.IFFlase) break;

                falseBlock = update;
            }

            chainNode.ElseBlock = new BlockNode();

            return falseBlock;
        }

        #endregion 块节点构建

        #region 逻辑结构判断

        private static bool IsEmptyBlock(BasicBlock trueBlock, BasicBlock falseBlock)
        {
            return trueBlock == null && falseBlock == null;
        }

        private static bool IsWhileStructure(DecompileContext ctx,
            DecompilerCore core, BasicBlock header, BasicBlock trueBlock)
        {
            if (trueBlock == null) return false;

            InStruction? laseHeaderBlock = header.Instructions.LastOrDefault();

            if (trueBlock.Type != BlockType.Jump) return false;

            var trueJumpAddr = (uint)trueBlock.Instructions.Last().Operands[0];

            if (ctx.Loop.IsInLoop && trueJumpAddr <= ctx.Loop.WhileAddr)
            {
                return false;
            }

            return trueJumpAddr < (uint)laseHeaderBlock.Operands[0];
        }

        private static bool IsFSingleIfStructure(
            DecompileContext ctx, DecompilerCore core,
            BasicBlock block, BasicBlock trueBlock)
        {
            if (trueBlock == null) return false;

            if (ctx.Loop.IsInLoop && trueBlock.Type == BlockType.Jump &&
                core.GetAddress(trueBlock) <= ctx.Loop.WhileAddr)
            {
                return true;
            }

            // 特征1：真分支没有终止跳转
            return (trueBlock.Type != BlockType.Jump) ||
                (core.GetAddress(block) == core.GetAddress(trueBlock));
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

        private static bool IsTIfElseStructure(DecompileContext ctx,
            DecompilerCore core, BasicBlock trueBlock, BasicBlock falseBlock)
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
                var hasJumpJump =
                    jumpBlock?.Type == BlockType.Jump; // 存在JUMP指令
                var trueJump = core.GetAddress(trueBlock);

                if (hasJumpJump)
                {
                    var jump = core.GetAddress(jumpBlock);
                    return trueJump == jump;
                }

                if (jumpBlock?.Type == BlockType.Exit ||
                    jumpBlock?.Type == BlockType.Base)
                {
                    var mergeBlock1 = core.GetNextBlock(ctx, trueBlock);
                    var mergeBlock2 = core.GetNextBlock(ctx, jumpBlock);
                    return mergeBlock1 is not null &&
                        mergeBlock2 is not null &&
                        mergeBlock1.Equals(mergeBlock2);
                }

                if (jumpBlock?.Type == BlockType.IFFlase ||
                    jumpBlock?.Type == BlockType.IFTrue)
                {
                    while (true)
                    {
                        var jump = core.GetAddress(jumpBlock);
                        if (jump == trueJump)
                            return true;
                        if (jump > trueJump)
                            return false;
                        jumpBlock = core.GetBlock(ctx, jump);
                        if (jumpBlock == null)
                            return false;
                    }
                }

                return false;
            }
            return true;
        }

        private static bool IsSwitchStructure(DecompileContext ctx, DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock, bool isTF)
        {
            if (falseBlock == null || trueBlock == null) return false;

            if (ctx.Loop.IsInLoop && !isTF) return false;

            if (isTF && falseBlock.Type != BlockType.IFTrue) return false;

            if (!isTF && falseBlock.Type != BlockType.IFFlase) return false;

            if (core.HasCallInstruction(falseBlock)) return false;

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

        private static bool IsTSwitchStructure(DecompileContext ctx, DecompilerCore core, BasicBlock header,
            BasicBlock trueBlock, BasicBlock falseBlock)
        {
            var trueBlocks = header.TrueSuccessors;
            var falseBlocks = header.FalseSuccessors;

            if (falseBlocks is null) return false;
            if (falseBlocks.Count == 0) return false;
            var nextBlock = falseBlock;
            if (trueBlocks is null || trueBlocks.Count == 0)
            {
                trueBlocks = falseBlock.TrueSuccessors;
                while (true)
                {
                    if (trueBlocks is not null && trueBlocks.Count > 0)
                    {
                        trueBlock = trueBlocks.LastOrDefault();
                        break;
                    }
                    nextBlock = Utilities.GetNextElement(falseBlocks, nextBlock);
                    if (nextBlock is null) return false;
                    if (core.GetAddress(nextBlock) != core.GetAddress(falseBlock)) return false;

                    trueBlocks = nextBlock?.TrueSuccessors;
                }

                if (falseBlocks.Count >= 2 && nextBlock.Equals(falseBlocks[^2])) return true;

                var isIfTrue = trueBlock?.Type == BlockType.Jump || trueBlock?.Type == BlockType.Exit;
                if (!isIfTrue) return false;
            }
            else
            {
                var isIfTrue = trueBlock?.Type == BlockType.Jump || trueBlock?.Type == BlockType.Exit;

                BasicBlock? jumpBlock = falseBlock?.TrueSuccessors?.LastOrDefault();
                var isJump = jumpBlock?.Type == BlockType.Jump || jumpBlock?.Type == BlockType.Exit;

                var typeCheck = isIfTrue && isJump;
                if (!typeCheck) return false;

                if (trueBlock?.Type != BlockType.Exit && jumpBlock?.Type != BlockType.Exit)
                {
                    InStruction? lastInTrueBlock = trueBlock?.Instructions.LastOrDefault();

                    InStruction? jump = jumpBlock?.Instructions.LastOrDefault();

                    var targetEquals = (uint)lastInTrueBlock?.Operands[0] == (uint)jump?.Operands[0];

                    if (!targetEquals) return false;
                }
            }

            var equals = false;
            var test = false;
            var list = new List<byte> { 2, 3, 7, 9 };
            List<InStruction>? conBlock1 = falseBlock?.Instructions;
            List<InStruction>? conBlock2 = header?.Instructions;

            if (conBlock1 is null || conBlock2 is null ||
                conBlock1.Count < 4 || conBlock2.Count < 4)
                return false;

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