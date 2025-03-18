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
![Screenshot_7](https://github.com/user-attachments/assets/816f304d-5295-4551-a66d-dc3548829c93)

Some documentation is [here](https://somerandomnoobkekeke.github.io/CrabUI/index.html)

Changelog is [here](https://github.com/SomeRandomNoobKekeke/CrabUI/blob/main/CSharp/Client/CrabUI/Changelog.md) 

## Instalation

Currently luatrauma workshop dependency system is broken  
When IFactory fixes it, using CUI will be as simple as subscribing to a mod  
But here's hacky install steps if you want to try it now:
### For c# mods
- Download main branch of this repository
- Copy `CSharp\Client\CrabUI` folder into `CSharp/Client` if it's a source code mod, and into `ClientSource` if it's an assembly mod
- Copy `Assets` folder into your mod folder / Make MSBuild copy it into your mod folder (idk how, lol) , you can put it anywhere, rename it, whatever
#### In public void Initialize(), 
- find mod folder
- Set `CUI.ModDir = "found Mod folder";`
- Set `CUI.AssetsPath = "Path to assets folder with cui stuff which i told you to copy";`
- Run `CUI.Initialize();`
#### In public void Dispose()
- Run `CUI.Dispose();` 

Check `CSharp\Client\Mod.cs` for example

#### Also in assembly mods
I highly recommend you to set CUI.UseCursedPatches to false and then set CUI.HookIdentifier to some name and add AdditionalHooks.lua to Lua/Autorun to avoid [curse](https://github.com/evilfactory/LuaCsForBarotrauma/issues/245)


### For lua mods
- Copy entire `CSharp` folder into your mod folder
- Copy `Assets` folder into your mod folder
- Go to `CSharp\Client\Mod.cs` and change public static string PackageName = "CrabUI"; to the name of your mod as in `filelist.xml`
- Modify `public static string AssetsPath => Path.Combine(ModDir, "Assets");` if you changed the name or location of `Assets` folder
