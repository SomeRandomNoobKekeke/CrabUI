# Overview {#Overview}

## Lil

Main entity here is CUIComponent, all components derive from CUIComponent

CUIComponents have props that controls their drawing and positioning

~~~~~~~~~~~~~{cs}
CUIFrame frame = new()
{
  Background = { Color = new Color(32, 32, 32) },
  Absolute = new CUINullRect(0, 0, 400, 600),
};
~~~~~~~~~~~~~

CUIComponents can have parent and children and can be attached to one another forming a tree

~~~~~~~~~~~~~{cs}
frame["layout"] = new CUIVerticalList() { Relative = new CUINullRect(0, 0, 1, 1) };
frame["layout"]["handle"] = new CUIHorizontalList()
{
  FitContent = new CUIBool2(false, true),
};
frame["layout"]["handle"]["caption"] = Caption = new CUITextBlock("bruh")
{
  Flex = 1,
};
~~~~~~~~~~~~~

There are 2 root CUIMainComponents CUI.Main and CUI.TopMain, they draw and update layouts oftheir children

To draw your components attach them to one of them

## \subpage Concepts