This is a parser, it's a thing that can serialize any object to/from string

```C#
public class CUIParser 
{
  public string Serialize(object o);
  public string SerializeList(IEnumerable items);
  public string SerializeDict(IDictionary dict);
  public T Parse<T>(string raw);
  public object Parse(string raw, Type T);
  public List<string> SplitList(string raw);
  public IEnumerable<T> ParseList<T>(string raw);
  public Dictionary<string, string> SplitDict(string raw);
}
```
### How is it better than just ToString()?

- by default it uses ToString() yes
- null values serialized to NullTerm e.g. `_null_`and won't be confused with empty strings
- types that implement IParsable can customize their serialization
```C#
public interface IParsable
{
  public static abstract object Parse(string raw);
  public string ToText();
}
```
- For types that you don't control you can provide custom serialization / parsing methods, e.g. you can serialize Vector2 as `[1,2]` instead of `{X:1 Y:2}`
- Enums are parsed with `Enum.Parse(T, raw)`
- primitives are parsed with `Convert.ChangeType(raw, T)`
- Nullable types are unwrapped and parsed by underlying type
- you can serialize and parse Enumerables as [a,b,c,d]
- you can serialize and parse Dictionaries as {a: 123, b: 321, c: 213}
- nested enumerables / dictionaries work fine
- if value contains special chars you can wrap it in any quotes to parse it as one value
- unlike nested json it should work fine in xml
- you can use default extra parsing methods for commonly used game types

### settings
Settings are in Parser.Settings

You can specify special chars and null term there

Note that WhenSerializing uses strings instead of chars just so you could specify indent
parsing with multichar separators isn't supported

if you want to redirect warnings you can pass your ILogger