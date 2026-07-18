# Types {#OverviewTypes}

### Commonly used "primitive" types:

~~~~~~~~~~~~~{cs}
public enum CUIDirection { Straight, Reverse, }
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIRect {
  public float Left;
  public float Top;
  public float Width;
  public float Height;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUINullRect {
  public float? Left;
  public float? Top;
  public float? Width;
  public float? Height;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIBool2 {
  public bool X;
  public bool Y;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUISizes {
  public float Left;
  public float Top;
  public float Right;
  public float Bottom;
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public static class CUIAnchor {
  public static Vector2 LeftTop = new Vector2(0.0f, 0.0f);
  public static Vector2 CenterTop = new Vector2(0.5f, 0.0f);
  public static Vector2 RightTop = new Vector2(1.0f, 0.0f);
  ...
}
~~~~~~~~~~~~~

~~~~~~~~~~~~~{cs}
public struct CUIBoundaries {
  public float? MinX;
  public float? MaxX;
  public float? MinY;
  public float? MaxY;
}
~~~~~~~~~~~~~


