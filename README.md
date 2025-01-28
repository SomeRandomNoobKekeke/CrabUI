# CrabUI pre-release

GUI framework for barotrauma C# and Lua modders  
Should be much easier to use than vanilla barotrauma GUI

Looks as shrimple as that
```C#
CUIFrame frame = new CUIFrame()
{
  Relative = new CUINullRect(w: 0.2f, h: 0.2f),
  Anchor = CUIAnchor.Center,
};

frame["guh button"] = new CUIButton("Press me")
{
  Anchor = CUIAnchor.Center,
  AddOnMouseDown = (e) => CUI.Log("Guh"),
};

CUI.Main.Append(frame);
```
![Screenshot_1](https://github.com/user-attachments/assets/91791edd-5b3d-48b0-a6f4-46f60d86f95f)

Some documentation is [here](https://somerandomnoobkekeke.github.io/CrabUI/index.html)

Changelog is [here](https://github.com/SomeRandomNoobKekeke/CrabUI/blob/main/CSharp/Client/CrabUI/Changelog.md) 

## Instalation

Currently luatrauma workshop dependency system is broken  
When IFactory fixes it, using CUI will be as simple as subscribing to a mod  
But here's hacky install steps if you want to try it now:
- Download main branch of this repository
- Copy CSharp folder into your mod
- If you already have C# mod just copy CrabUI folder and add `CUI.Initialize();` to mod Initialize and `CUI.Dispose();` to mod Dispose
