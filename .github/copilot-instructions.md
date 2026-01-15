# Copilot 指南 — rpg-game（新手上手版）

目标：让 AI 助手在 5–15 分钟内理解项目结构、关键约定与常见改动流程，以便进行状态/动画、简单行为和场景小改动。

概览
- 技术栈：Godot 4 + C#，工程文件 `RpgGame.sln` / `RpgGame.csproj`。
- 运行方式：推荐在 Godot 编辑器中打开项目并运行场景以加载资源；可选用 `dotnet build RpgGame.sln` 验证 C# 代码能编译。

主要位置（首查）
- `Scene/`：游戏场景（`Player.tscn`, `Enemy.tscn`, `Main.tscn`）。
- `Script/`：C# 源码，关注 `Script/StateMachine/` 下的 `StateMachine.cs`、`State.cs` 与 `PlayerStates` / `EnemyStates`。
- `Asset/`：图片与音频，动画 atlas / `SpriteFrames` 在场景文件中引用。

关键架构与设计要点
- 状态机：`StateMachine` 在 `_Ready()` 把子节点（类型为 `State`）注入 `parentStateMachine` 与 `character`，初始状态为第一个子节点（见 `Script/StateMachine/StateMachine.cs`）。
- 状态基类：`State` 定义 `Enter()`, `Exit()`, `Update()`, `UpdatePhysics()`；每个行为写成单独的 `State` 子节点脚本（例如 `PlayerIdle.cs`、`PlayerRun.cs`、`EnemyIdle.cs`）。
- 角色基类：`BaseCharacter.cs` 提供 `inputDirection`、`GetFacingDirection()` 与 `UpdateAnimatataion()`；`UpdateAnimatataion()` 会调用 `animatedSprite2D.Play(currentState.Name + "_" + facing)`。

必须遵守的约定（会影响 AI 修改）
- 动画命名：`<StateName>_<FacingDirection>`（例：`Idle_Down`, `Run_Right`）。在 `Player.tscn` 的 `SpriteFrames` 中可见样例。
- 状态切换：通过 `parentStateMachine.SwitchTo("NodeName")`，`NodeName` 必须是 `StateMachine` 下的子节点名称。
- 场景节点：角色场景必须含 `StateMachine` 节点与 `AnimatedSprite2D` 节点；脚本使用 `GetNode<...>("AnimatedSprite2D")` 与 `GetNode<StateMachine>("StateMachine")`。

输入与控制（可以直接参考的细节）
- 当前代码使用 Godot 动作名：`ui_left`, `ui_right`, `ui_up`, `ui_down`（见 `Script/Player.cs` 中 `Input.GetVector("ui_left","ui_right","ui_up","ui_down")`）。
- 若需要新增输入动作（例如攻击），在 Godot 项目设置的 `InputMap` 中新增动作名并在代码里使用相同字符串。

如何添加一个新状态（最常见任务）
1. 在场景编辑器：选中角色节点下的 `StateMachine`，添加一个新的子节点（类型 `Node`），把它命名为状态名（例如 `Attack`）。
2. 在 `Script/StateMachine/PlayerStates/` 新建 `PlayerAttack.cs`：

```csharp
using Godot;
using System;

public partial class PlayerAttack : State
{
    public override void Enter() { /* 触发一次性攻击 */ }
    public override void Update() { character.UpdateAnimatataion(); }
}
```

3. 将脚本挂到 `StateMachine` 新节点，确保节点名与 `SwitchTo("Attack")` 中使用的一致。

动画与资源注意事项
- 改动画或新增动画时，同时在对应 `*.tscn` 的 `SpriteFrames` 里新增同名动画组（名称必须严格匹配 `<State>_<Facing>`）。
- `BaseCharacter.UpdateAnimatataion()` 会在依赖未就绪时静默返回——在调试初始化问题时，请先确认 `AnimatedSprite2D` 与 `StateMachine` 已正确挂载并在 `_Ready()` 中赋值。

常见陷阱与已知硬编码
- `Enemy.cs` 使用绝对路径查找玩家节点：`GetTree().CurrentScene.GetNode<CharacterBody2D>("/root/Node2D/Level/Player")`。若重构场景树，请修复此路径或改为使用信号/注入方式获取玩家引用。
- 不要随意改节点名（例如 `StateMachine`、`AnimatedSprite2D`），否则脚本的 `GetNode` 调用会失效。

调试建议（快速定位问题）
- 在 `State.Update()` 中，默认会把当前状态名写入 `parentStateMachine.StateLabel`（如果存在），因此在场景中添加 `Label` 并连到 `StateMachine.StateLabel` 可快速看到当前状态。
- 使用 `GD.Print()` 打印位置、角度、状态切换信息来追踪逻辑流。

构建 / 本地运行命令
- 验证 C# 编译：
  ```powershell
  dotnet build RpgGame.sln
  ```
- 运行：推荐用 Godot 编辑器打开 `d:/Godot/rpg-game` 并运行 `Main.tscn`。

CI / 测试
- 仓库当前无 CI 配置；若添加，至少包含 `dotnet build` 步骤并（可选）使用 Godot headless 来做场景级别的 smoke test。

附：快速查阅文件（首看）
- `Script/BaseCharacter.cs`、`Script/Player.cs`、`Script/Enemy.cs`、`Script/StateMachine/StateMachine.cs`、`Script/StateMachine/State.cs`、以及 `Scene/Player.tscn`。

如果你需要，我可以：
- 把示例 `PlayerAttack` 状态创建并挂载到 `Player.tscn`；
- 自动替换 `Enemy` 中的硬编码节点查找为可配置引用或信号注入；
- 添加一个最小 CI workflow（仅运行 `dotnet build`）。

请告诉我你想要我优先做的下一项（示例状态、修复硬编码、或添加 CI），我会继续实现并提交补丁。
