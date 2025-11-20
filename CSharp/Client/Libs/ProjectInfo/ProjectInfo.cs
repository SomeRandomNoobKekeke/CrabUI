using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Text;

namespace BaroJunk
{
  /// <summary>
  /// Pathetic attempts to keep track of versions and dependencies of libs
  /// </summary>
  public static class ProjectInfo
  {
    static ProjectInfo() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "ProjectInfo",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk"
      },
      Dependencies = new List<PackageInfo>(){
        new PackageInfo(){
          Name = "Logger",
          Version = new Version(0, 0, 0) { Branch = "BaroJunk" }
        }
      }
    });

    public static Dictionary<string, PackageInfo> UsedLibs = new();

    public static void Add(PackageInfo info) => UsedLibs.Add(info.Name, info);

    public static List<(PackageInfo, PackageInfo)> Incompatible
    {
      get
      {
        List<(PackageInfo, PackageInfo)> incompatible = new();

        foreach (var (key, lib) in UsedLibs)
        {
          if (lib.Dependencies is null) continue;

          foreach (PackageInfo required in lib.Dependencies)
          {
            if (UsedLibs.ContainsKey(required.Name))
            {
              if (!Version.Compatible(UsedLibs[required.Name].Version, required.Version))
              {
                incompatible.Add((lib, required));
              }
            }
          }
        }

        return incompatible;
      }
    }

    public static void CheckIncompatibleLibs()
    {
      List<(PackageInfo, PackageInfo)> incompatible = Incompatible;

      if (incompatible.Count != 0)
      {
        Logger.Default.Warning("-----------------------------------------------------");
        Logger.Default.Warning("Incompatible Libs!\n");
        Logger.Default.Log($"Used Libs: {Logger.Wrap.IEnumerable(UsedLibs.Values, newline: true)}\n");
        Logger.Default.Log($"Incompatible: {(Logger.Wrap.IEnumerable(Incompatible.Select(pair => $"[{pair.Item1}] -- requires -> [{pair.Item2}]"), newline: true))}");
        Logger.Default.Warning("-----------------------------------------------------");
      }
    }

    public static void PrintSummary()
    {
      Logger.Default.Log(Summary());
    }

    public static string Summary()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Used Libs: {Logger.Wrap.IEnumerable(UsedLibs.Values, newline: true)}");

      List<(PackageInfo, PackageInfo)> incompatible = Incompatible;

      if (Incompatible.Count > 0)
      {
        sb.AppendLine($"Incompatible: {(Logger.Wrap.IEnumerable(Incompatible.Select(pair => $"[{pair.Item1}] -- requires -> [{pair.Item2}]"), newline: true))}");
      }

      return sb.ToString();
    }
  }
}