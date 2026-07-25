# QuickStart {#QuickStart}

#### For "in-memory" mods
publish you mod and add workshop dependency to [CUI mod](https://steamcommunity.com/sharedfiles/filedetails/?id=3765325848)
<br><br>
#### For precompiled mods
Go to [CUI mod](https://steamcommunity.com/sharedfiles/filedetails/?id=3765325848) folder, take CUI.dll from bin and add a reference to it in your project

Or download https://github.com/SomeRandomNoobKekeke/CrabUI, add luatrauma Refs and compile it yourself

Or just copy paste sources from [github](https://github.com/SomeRandomNoobKekeke/CrabUI/tree/main/ClientProject/ClientSource)
<br><br>
#### Hybrid mods
You can take best from both worlds

you can setup https://github.com/Luatrauma/LuaCsModTemplate, and then add `ModConfig.xml` loading C# sources as "in-memory" scripts
~~~~~~~~~~~~~{XML}
<ModConfig>
  <Assembly Folder="%ModDir%/ClientProject/ClientSource" Target="Client" IsScript="true" UseInternalAccessName="false" />
</ModConfig>
~~~~~~~~~~~~~

Then you can add project reference to CUI.dll to have ide hints, put it in LocalMods and reload with cl_reloadlua
<br><br>
## Min example:
~~~~~~~~~~~~~{cs}
using CrabUI;

public class Mod : IAssemblyPlugin
{
  public void Initialize()
  {
    CUI.Logger.Log($"Compiled somehow");

    CUI.Start();

    CUIFrame frame = new CUIDefault.Frame("bruh")
    {
      Absolute = new CUINullRect(w: 400, h: 600),
    };

    frame.Open();
  }

  public void OnLoadCompleted() { }
  public void PreInitPatching() { }
  public void Dispose() { }
}
~~~~~~~~~~~~~

Mod with this code should open empty frame in sub editor

\image{inline} html EmptyFrame.png