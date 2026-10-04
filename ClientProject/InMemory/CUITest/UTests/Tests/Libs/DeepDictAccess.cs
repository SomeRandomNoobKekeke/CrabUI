using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using CUILibs;
using System.Text.Json;

namespace CursedUI
{
  public class DeepDictAccessTest : UTestPack
  {



    public override void CreateTests()
    {
      Dictionary<string, object> repo = new();

      DeepDictAccess.Set("qwerqwer.12313243.wverwvwer", "bruh", repo);
      DeepDictAccess.Set("wergwerg.oojiwer", "kek", repo);

      Tests.Add(new UTest(DeepDictAccess.Get<string>("qwerqwer.12313243.wverwvwer", repo), "bruh"));
      Tests.Add(new UTest(DeepDictAccess.Has("wergwerg.oojiwer", repo)));
      Tests.Add(new UTest(DeepDictAccess.Get("wergwerg.oojiwer", repo), "kek"));

      Tests.Add(new UTest(repo["qwerqwer"] is Dictionary<string, object>));
      Tests.Add(new UTest(DeepDictAccess.Get("qwerqwer.12313243", repo) is Dictionary<string, object>));

      Tests.Add(new UTest(!DeepDictAccess.Has("", repo)));
      Tests.Add(new UTest(!DeepDictAccess.Has(null, repo)));
      Tests.Add(new UTest(DeepDictAccess.Get<bool>("bebebe", repo), false));

      DeepDictAccess.Set("   ...  ", "cringe", repo);
      Tests.Add(new UTest(DeepDictAccess.Get("   ...  ", repo), "cringe"));
      DeepDictAccess.Remove("   ...  ", repo);
      Tests.Add(new UTest(!DeepDictAccess.Has("   ...  ", repo)));



      DeepDictAccess.Set("some settings.sussy tuple", ("123", 123), repo);
      Tests.Add(new UTest(DeepDictAccess.Get<(string, int)>("some settings.sussy tuple", repo), ("123", 123)));

      Logger.Default.Log(JsonSerializer.Serialize(repo, options: new JsonSerializerOptions()
      {
        WriteIndented = true,
      }));
    }

  }
}