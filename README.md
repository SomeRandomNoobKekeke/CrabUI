This is C# GUI library in a form of a mod for Barotrauma

It's both LuaCsForBarotraumaPlugin "in-memory" mod and compilable chimera of BaseBaroPlugin and 
LuaCsModTemplate  
It's source code for https://steamcommunity.com/sharedfiles/filedetails/?id=3834834427

To use:
- make any c# mod for LuaCsForBarotraumaPlugin, make sure it's placed below CUI ... profit
- vanilla c# mods can't use it rn
- if you need ide hints just add a reference to workshop folder
```xml
<Reference Include="CUI">
  <HintPath>$(AppDataLocalDir)\Daedalic Entertainment GmbH\Barotrauma\WorkshopMods\Installed\3834834427\Windows\Client\CUI.dll</HintPath>
</Reference>
```

## How to compile

rename `UserBuildData.props.Example` into `UserBuildData.props`

get `luacsforbarotrauma_refs.zip` from `https://github.com/evilfactory/LuaCsForBarotrauma/releases`
and unzip into Refs folder

subscribe to [LuaCsForBarotraumaPlugin](https://steamcommunity.com/sharedfiles/filedetails/?id=3693624471)  
the project is referencing workshop folder

run `build.cmd`, it should compile it into `Build` folder  

---------------------------------
for some reason linux/osx don't want to compile from cmd, but you can compile them in VS

only Windows/Client/CUI.dll is used rn


[~~Changelog~~](https://github.com/SomeRandomNoobKekeke/CrabUI/blob/main/ClientProject/ClientSource/Changelog.md)
[~~Docs~~](https://somerandomnoobkekeke.github.io/CrabUI/)

outdated, don't read them