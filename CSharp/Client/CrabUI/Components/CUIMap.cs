using System;
using System.Collections.Generic;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Xml.Linq;
namespace CrabUI
{
  /// <summary>
  /// Swipable and zoomable plane  
  /// Allows you to place components in a plane
  /// and connect them with lines like a graph or scheme
  /// </summary>
  public class CUIMap : CUIComponent
  {
    #region CUIMapLink
    #endregion
    public class CUIMapLink
    {
      internal static void InitStatic()
      {
        CUI.OnInit += () => Default = new CUIMapLink(null, null);
        CUI.OnDispose += () => Default = null;
      }
      public static CUIMapLink Default;

      public CUIComponent Start;
      public CUIComponent End;

      //TODO all this crap wasn't designed for nested AKA
      public string StartAKA;
      public string EndAKA;
      public float LineWidth;
      public Color LineColor;

      public XElement ToXML()
      {
        XElement connection = new XElement("Connection");
        if (LineWidth != Default.LineWidth)
        {
          connection.SetAttributeValue("LineWidth", LineWidth);
        }
        connection.SetAttributeValue("Start", StartAKA ?? "");
        connection.SetAttributeValue("End", EndAKA ?? "");

        return connection;
      }

      public CUIMapLink(CUIComponent start, CUIComponent end, Color? lineColor = null, float lineWidth = 2f)
      {
        LineColor = lineColor ?? new Color(128, 128, 128);
        LineWidth = lineWidth;
        Start = start;
        End = end;

        StartAKA = start?.AKA;
        EndAKA = end?.AKA;
      }
    }

    #region LinksContainer
    #endregion
    public class LinksContainer : CUIComponent
    {
      public List<CUIMapLink> Connections = new List<CUIMapLink>();

      public override void Draw(SpriteBatch spriteBatch)
      {
        base.Draw(spriteBatch);

        foreach (CUIMapLink link in Connections)
        {
          Vector2 midPoint = new Vector2(link.End.Real.Center.X, link.Start.Real.Center.Y);

          GUI.DrawLine(spriteBatch,
            link.Start.Real.Center,
            midPoint,
            link.LineColor, width: link.LineWidth
          );

          GUI.DrawLine(spriteBatch,
            midPoint,
            link.End.Real.Center,
            link.LineColor, width: link.LineWidth
          );
        }
      }

      public LinksContainer()
      {
        UnCullable = true;
        BackgroundColor = Color.Transparent;
        Border.Color = Color.Transparent;
      }
    }

    #region CUIMap
    #endregion

    public LinksContainer linksContainer;
    public List<CUIMapLink> Connections => linksContainer.Connections;

    public CUIComponent Add(CUIComponent c) => Append(c, c.AKA);



    public CUIComponent Connect(CUIComponent startComponent, CUIComponent endComponent, Color? color = null)
    {
      if (startComponent != null && endComponent != null)
      {
        if (color == null && (!startComponent.Disabled || !endComponent.Disabled)) color = new Color(0, 0, 255);
        linksContainer.Connections.Add(new CUIMapLink(startComponent, endComponent, color));
      }
      return startComponent;
    }
    public CUIComponent Connect(CUIComponent startComponent, int end = -2, Color? color = null)
    {
      end = MathUtils.PositiveModulo(end, Children.Count);
      CUIComponent endComponent = Children.ElementAtOrDefault(end);
      return Connect(startComponent, endComponent, color);
    }

    //TODO  DRY
    public CUIComponent Connect(string start, string end, Color? color = null)
    {
      CUIComponent startComponent = this[start];
      CUIComponent endComponent = this[end];

      if (startComponent != null && endComponent != null)
      {
        if (color == null && (!startComponent.Disabled || !endComponent.Disabled)) color = new Color(0, 0, 255);
        linksContainer.Connections.Add(new CUIMapLink(startComponent, endComponent, color)
        {
          StartAKA = start,
          EndAKA = end,
        });
      }
      return startComponent;
    }
    public CUIComponent Connect(int start, int end, Color? color = null)
    {
      start = MathUtils.PositiveModulo(start, Children.Count);
      end = MathUtils.PositiveModulo(end, Children.Count);

      CUIComponent startComponent = Children.ElementAtOrDefault(start);
      CUIComponent endComponent = Children.ElementAtOrDefault(end);
      return Connect(startComponent, endComponent, color);
    }

    public CUIComponent ConnectTo(CUIComponent Host, params CUIComponent[] children)
    {
      foreach (CUIComponent child in children) { Connect(Host, child); }
      return Host;
    }


    public override XElement ToXML(CUIAttribute propAttribute = CUIAttribute.CUISerializable)
    {
      Type type = GetType();

      XElement element = new XElement(type.Name);

      PackProps(element, propAttribute);

      XElement connections = new XElement("Connections");
      element.Add(connections);

      foreach (CUIMapLink link in Connections)
      {
        connections.Add(link.ToXML());
      }

      XElement children = new XElement("Children");
      element.Add(children);

      foreach (CUIComponent child in Children)
      {
        if (child == linksContainer) continue;
        children.Add(child.ToXML());
      }

      return element;
    }


    public override void FromXML(XElement element, string baseFolder = null)
    {
      foreach (XElement childElement in element.Element("Children").Elements())
      {
        Type childType = CUIReflection.GetComponentTypeByName(childElement.Name.ToString());
        if (childType == null) continue;

        CUIComponent child = (CUIComponent)Activator.CreateInstance(childType);
        child.FromXML(childElement);

        this.Append(child, child.AKA);
      }

      foreach (XElement link in element.Element("Connections").Elements())
      {
        CUIComponent startComponent = this[link.Attribute("Start").Value];
        CUIComponent endComponent = this[link.Attribute("End").Value];

        if (startComponent == null || endComponent == null)
        {
          CUIDebug.Error("startComponent == null || endComponent == null");
          continue;
        }
        Connect(link.Attribute("Start").Value, link.Attribute("End").Value);
      }

      //TODO: think, this is potentially very bugged,
      // Some props might need to be assigned before children, and some after
      ExtractProps(element);
    }

    public CUIMap() : base()
    {
      Swipeable = true;
      ConsumeMouseClicks = true;
      CullChildren = true;
      BackgroundColor = Color.Transparent;

      //without container links won't be culled
      //TODO linksContainer should be special and not just first child
      this["links"] = linksContainer = new LinksContainer();

      OnScroll += (m) =>
      {
        CUIProps.ChildrenOffset.SetValue(
          ChildrenOffset.Zoom(
            m.MousePosition - Real.Position,
            (-m.Scroll / 500f)
          )
        );
      };
    }
  }

}