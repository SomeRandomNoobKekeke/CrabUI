// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Diagnostics;
// using System.Runtime.CompilerServices;
// using Barotrauma;
// using BaroJunk;
// using CrabUI;
// using Microsoft.Xna.Framework;
// using System.IO;

// namespace CrabUIUser
// {
//   public partial class SnapshotTestManager
//   {
//     public class ConsoleIntefaceClass
//     {
//       public ILogger Logger => CUI.Logger;

//       public ConsoleIntefaceClass(SnapshotTestManager manager)
//       {
//         manager.Events.Add(HandleManagerEvent);
//       }

//       public void HandleManagerEvent(SnapshotTestManager.Event e)
//       {
//         if (e.Name == "passed")
//         {
//           Logger.Log($"{BaroJunk.Logger.WrapInColor($"{e.Test.Name} Passed", "lime")}");
//         }

//         if (e.Name == "failed")
//         {
//           Logger.Log($"{BaroJunk.Logger.WrapInColor($"{e.Test.Name} Failed", "red")}");
//           Logger.Log(e.Message);
//         }
//       }
//     }
//   }
// }