# KnKModTools - Kai no Kiseki Modification Tool  
**Languages**:  
[简体中文](README.md) |  
[English](README.en.md) |  

![KnKModTools Logo](./KnKModTools/Image/load.png)  

A tool for modifying data tables (.tbl) and script files (.dat) in *Kai no Kiseki*, developed in C# based on the .NET 8 framework.  

## Table of Contents  
• [Features](#features)  
• [Installation & Usage](#installation--usage)  
• [Interface Overview](#interface-overview)  
• [Shortcuts](#shortcuts)  
• [Script Modification](#script-modification)  
• [Known Issues](#known-issues)  
• [Recommended Tools](#recommended-tools)  
• [Community Support](#community-support)  
• [Appendix](#appendix)  

## Features  
• Edit game data tables (.tbl files)  
• Decompile and recompile game scripts (.dat files)  
• Multi-language support (Simplified Chinese, Traditional Chinese, Korean)  
• Data sorting and quick editing  

## Installation & Usage  

### System Requirements  
• Windows 7/10/11  
• .NET 8 Runtime  

### Steps  
1. Run `KnKModTools.exe`  
2. First-time setup requires:  
   • Setting the game installation directory  
   • Setting the output directory for decompiled scripts  
   • Selecting the table language (Simplified Chinese/Traditional Chinese/Korean)  
   • Setting the program language  
![Setup Interface](./KnKModTools/Image/firstrun.png)  

3. Wait for the program to load  
4. Once the operation interface appears at the bottom, editing can begin  

**Important**: Always back up the `script` and `table` directories before use!  

## Interface Overview  

![Main Interface](./KnKModTools/Image/edit.png)  

1. **tbl File List** - Displays all editable data table files  
2. **Node List** - Shows the node structure of the current table  
3. **Detailed Data Table** - Displays and edits specific data  
4. **Table Header** - Click to sort ascending/descending or reset node order  
5. **Save All** - Saves all modifications  
6. **Decompile Scripts** - Decompiles script files to the specified directory  
7. **Recompile Scripts** - Recompiles script files to the specified directory  

## Shortcuts  
• `Left Ctrl + S` - Save the currently viewed table  
• Select a row + `Ins` - Add a new item using the selected row as a template  
• Select a row + `Del` - Delete the selected item  

## Script Modification  

### Decompilation Notes  
Decompilation is complex and may not be 100% accurate. The following tests have been performed:  
• Real-time combat testing  
• Turn-based combat testing  
• Cutscene testing  
• Scene transition testing  
• Treasure chest opening testing  

### JavaScript Syntax Notes  
• Use basic JavaScript syntax  
• Logical NOT (`!`) is not supported; use `if(test){}else{...}` instead  
• Cross-script function calls have two forms:  
  1. Standard form: `sound_ani.SeBattleConfigDefault()`  
  2. With `sc_` prefix: `sc_this.AniFieldChargeAttack(Result[1])`  

• Empty script names (`""`) are replaced with `All`  
• `RUNCMD` instructions are wrapped as `Engine.Run([23, 38], arg)`  

![JS Script](./KnKModTools/Image/js1.png)  
![JS Script](./KnKModTools/Image/js2.png)  

### Irreducible Flow Graph Scripts  
The following scripts contain irreducible flow graphs (Simplified Chinese version):  
```
script_sc\ani\sound_ani.dat  
    SeBattleMon0022  
script_sc\ani\chr5117.dat  
    AniBtlVoiceQuickArts  
script_sc\minigame\basketball.dat  
    SelectCharacter  
script_sc\scena\a4000.dat  
    Dummy_set_150  
    Dummy_set_151  
    Dummy_set_152  
    Dummy_set_42  
    Dummy_set_45  
    Dummy_set_46  
script_sc\scena\a0000.dat  
    TestEmotion  
script_sc\scena\sys_event.dat  
    ReminiscenceCheckMenu  
script_sc\scena\debug.dat  
    FC_NoteBooks  
```  

## Known Issues  
1. Long text with line breaks may not display fully; copy, edit, and paste back  
2. Search functionality is not yet implemented, but sorting can help locate items  
3. Byte alignment in source files is not handled during saving (no impact observed so far)  

## Recommended Tools  
• [KuroTools](https://github.com/nnguyen259/KuroTools) - Data table and script modification tool  
• [kuro_mdl_tool](https://github.com/eArmada8/kuro_mdl_tool) - Model processing tool  
• [kuro_dlc_tool](https://github.com/eArmada8/kuro_dlc_tool) - DLC processing tool  

## Community Support  
• Discord Group: [https://discord.gg/QdEYC6Cd](https://discord.gg/QdEYC6Cd)  
• Trails Series Chinese Community: [Tea Party](https://trails-game.com/)  

## Appendix  

### Instruction Table  
| Decimal | Instruction Name               | Parameter Description       | Stack Change |  
|---------|--------------------------------|----------------------------|--------------|  
| 0       | PUSH                           | (size=4, value)            | +1           |  
| 1       | POP                            | bytes_to_pop (multiple of 4)| -(n)        |  
| 2       | RETRIEVEELEMENTATINDEX         | Byte offset (negative)     | +1           |  
| 3       | RETRIEVEELEMENTATINDEX2        | Byte offset (negative)     | +1           |  
| 4       | PUSHCONVERTINTEGER             | Target byte offset (negative)| +1          |  
| 5       | PUTBACKATINDEX                 | Target byte offset (negative)| -1          |  
| 6       | PUTBACK                        | Target byte offset (negative)| -1          |  
| 7       | LOAD32                         | Global variable index      | +1           |  
| 8       | STORE32                        | Global variable index      | -1           |  
| 9       | LOADRESULT                     | Result index               | +1           |  
| 10      | SAVERESULT                     | Result index               | -1           |  
| 11      | JUMP                           | Target address (absolute)  | 0            |  
| 12      | CALL                           | Target function ID         | -(n+2)       |  
| 13      | EXIT                           | None                       | 0            |  
| 14      | JUMPIFTRUE                     | Target address (absolute)  | -1           |  
| 15      | JUMPIFFALSE                    | Target address (absolute)  | -1           |  
| 16-30   | Operators (+ - * / == != etc.) | None                       | -1           |  
| 31      | NEGATIVE                       | None                       | 0            |  
| 32      | ISTRUE                         | None                       | 0            |  
| 33      | NOT                            | None                       | 0            |  
| 34      | CALLFROMANOTHERSCRIPT          | (script, function, param count)| -(n+5)    |  
| 35      | CALLFROMANOTHERSCRIPT2         | (script, function, param count)| -n         |  
| 36      | RUNCMD                         | (cmd1, cmd2, param count)  | 0            |  
| 37      | PUSHRETURNADDRESSFROMANOTHERSCRIPT | Return address | +5      |  
| 38      | ADDLINEMARKER                  | Debug marker               | 0            |  
| 39      | POP2                           | Element count to pop       | -count       |  

### Operator Mapping  
```  
[16] = ("+")  
[17] = ("-")  
[18] = ("*")  
[19] = ("/")  
[20] = ("%")  
[21] = ("==")  
[22] = ("!=")  
[23] = (">")  
[24] = (">=")  
[25] = ("<")  
[26] = ("<=")  
[27] = ("&")  
[28] = ("|")  
[29] = ("&&")  
[30] = ("||")  
```
