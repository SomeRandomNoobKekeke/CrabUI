using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CUILibs;

namespace CursedUI
{
  public interface ICUIRunner
  {
    public CUICore Core { get; set; }
    public ICUIRunnerDataSources DataSources { get; set; }
    public void Connect();
    public void Disconnect();

    public void OnStartAttempt(Assembly callingAssembly);

    public CUITextureManager TextureManager { get; }
  }
}