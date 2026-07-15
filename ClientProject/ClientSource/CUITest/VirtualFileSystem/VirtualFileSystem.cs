using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Barotrauma;
using CUILibs;
using CrabUI;
using Microsoft.Xna.Framework;

using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace CrabUIUser
{
  //Unused
  public class VirtualFileSystem : CUICore.CUICoreIOHandle
  {
    public Dictionary<string, string> Files { get; } = new();

    public void SaveXDoc(XDocument xDoc, string path) => Files[path] = xDoc.ToString();
    public XDocument LoadXDoc(string path) => XDocument.Parse(Files[path]);

    public void Write(string data, string path) => Files[path] = data;
    public string Read(string path) => Files[path];
    public bool Exist(string path) => Files.ContainsKey(path);
    public void Delete(string path) => Files.Remove(path);
    public void Clear() => Files.Clear();

    public override string ToString()
    {
      return Logger.Wrap.IDictionary(Files);
    }
  }
}