using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;


using System.IO;
using System.Xml;
using System.Xml.Linq;
using EventInput;
using Microsoft.Xna.Framework.Input;


namespace CrabUI
{
  public class DummyIKeyboardSubscriber : IKeyboardSubscriber
  {
    public bool Selected { get; set; }

    public void ReceiveCommandInput(char command)
    {

    }

    public void ReceiveEditingInput(string text, int start, int length)
    {

    }

    public void ReceiveSpecialInput(Keys key)
    {

    }

    public void ReceiveTextInput(char inputChar)
    {

    }

    public void ReceiveTextInput(string text)
    {

    }
  }

}