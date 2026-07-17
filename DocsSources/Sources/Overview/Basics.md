# Basics {#OverviewBasics}

All GUI elements: frames, buttons, list, textblock are CUIComponents

They are C# objects, they all have parameterless constructor so you can just create them

They have props that control how they will be drawn and positioned
~~~~~~~~~~~~~{cs}
CUIFrame frame = new CUIFrame()
{
  Absolute = new CUINullRect(w: 400, h: 600),
  Background = { Color = Color.Blue, } // you can set deep props like this
};
~~~~~~~~~~~~~

CUIComponents can be connected to each other forming a tree, they have:
~~~~~~~~~~~~~{cs}
public IList<CUIComponent> Children { get; }
public CUIComponent Parent { get; set; }
~~~~~~~~~~~~~

you can connect them like this
~~~~~~~~~~~~~{cs}
CUI.Main.Children.Add(frame); 
// or
frame.Parent = CUI.Main;
~~~~~~~~~~~~~

Another fancy way to connect components is indexing  
It adds component as a child and memorizes it, so you can later access it by name
~~~~~~~~~~~~~{cs}
frame["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1), };
frame["layout"]["handle"] = new CUIHorizontalList()
{
  FitContent = new CUIBool2(false, true),
};
frame["layout"]["handle"]["caption"] = Caption = new CUITextBlock("bruh")
{
  Flex = 1,
};
~~~~~~~~~~~~~

To make your components visible add them to CUI.Main or CUI.TopMain  
CUI.Main is drawn below vanilla GUI and TopMain is drawn above
~~~~~~~~~~~~~{cs}
frame.Open(); // same as CUI.Main.Children.Add(frame);
~~~~~~~~~~~~~

CUIComponent can handle various events, e.g.
~~~~~~~~~~~~~{cs}
//mmm so consistent
frame.MouseDown += (c, e) => CUI.Logger.Log($"Mouse down at {e.Pos}");
frame.OnFocus += () => frame.Background.Color = Color.Cyan;
frame.OnFocusLost += () => frame.Background.Color = Color.Blue;
frame.KeyPressed += (e) => CUI.Logger.Log(e.Key);
~~~~~~~~~~~~~