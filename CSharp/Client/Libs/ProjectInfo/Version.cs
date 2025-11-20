using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;

namespace BaroJunk
{
  public class Version
  {

    /// <summary>
    /// It checks that present version is compatible or is built based on something compatible
    /// It checks that target version has enough features (minor) and there's no breaking changes (major) since then
    /// </summary>
    public static bool Compatible(Version present, Version required)
    {
      Version target = null;

      foreach (Version version in present.WholeChain)
      {
        if (version.Branch == required.Branch)
        {
          target = version;
          break;
        }
      }

      if (target == null) return false;

      if (target.Major != required.Major) return false;
      if (target.Minor < required.Minor) return false;

      if (
        present.WholeChain.TakeWhile(v => v.Branch != target.Branch)
        .Any(v => v.Major > 0)
      ) return false;

      return true;
    }

    public bool CompatibleWith(Version required) => Compatible(this, required);

    public string Branch { get; set; }
    public Version BasedOn { get; set; }

    public IEnumerable<Version> WholeChain
    {
      get
      {
        yield return this;
        if (BasedOn != null)
        {
          foreach (Version deeper in BasedOn.WholeChain)
          {
            yield return deeper;
          }
        }
      }
    }

    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }

    public Version(int major = 0, int minor = 0, int patch = 0)
    {
      Major = major;
      Minor = minor;
      Patch = patch;
    }

    public override string ToString() => $"{(Branch is null ? "" : $"{Branch} ")}{Major}.{Minor}.{Patch}";
  }
}