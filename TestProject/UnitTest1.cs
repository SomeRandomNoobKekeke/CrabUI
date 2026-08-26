namespace TestProject;

using CursedUI;
using Microsoft.Xna.Framework;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        CUIParser parser = new CUIParser();

        Assert.Equal(parser.Parse("123", typeof(int)), 123);
        Assert.Equal(parser.Parse("255,0,255", typeof(Color)), Color.Magenta);
        Assert.Equal(parser.Parse("123", typeof(float)), 123.0f);
        Assert.Equal(parser.Parse("123", typeof(float?)), 123.0f);
        Assert.Equal(parser.Parse("{{null}}", typeof(float?)), null);
    }
}
