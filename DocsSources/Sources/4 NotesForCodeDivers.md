# Notes For Code Divers {#NotesForCodeDivers}

those //BRUH and //CRINGE marks are actually tech debt markers, i can see them in TODO Tree

## Component Generator
I moved many reflection based tasks to a separate project [here](https://github.com/SomeRandomNoobKekeke/CUICodeGenerator)  

It's in a separate mod so i wouldn't be softlocked if CUI assembly doesn't compile

It allows me to use some funny desing patterns:

### Components
Components implementing IComponent are just containers for stuff  
Their main purpose is to be an identity, a linking point for modules that contain logic and data  
Sometimes they have logic and data on rool level, treat is as root level modules

CUICodeGenerator will generate code for Components with [GeneratedComponent] 

### Parts
~~~~~~~~~~~~~{cs}
public class Component
{
  public class LayoutProps_Part : IPart
  {
    public Component Self { get; set; }
    public float Flex { get; set; }
  }
  public LayoutProps_Part LayoutProps { get; } = new();
}
~~~~~~~~~~~~~
Such nested classes have full access to the privates of containing class  
They can access it with Self, which is injected by CUICodeGenerator  
They basically share identity with host component

Their main purpose is to add structure and move some props deeper  

~~~~~~~~~~~~~{cs}
Component component = new();
component.LayoutProps.Flex = 1;
~~~~~~~~~~~~~

parts can be private and hide internal state, or they can expose limited access to private stuff

### Adapter parts
Parts can implement interfaces and make host appear like something else
~~~~~~~~~~~~~{cs}
public interface IFlexible
{
  public float Flex { get; set; }
}

public class Component
{
  public class IFlexible_Adapter_Part : IPart, IFlexible
  {
    public Component Self { get; set; }
    float IFlexible.Flex
    {
      get => Self.NotFlexAtAll;
      set => Self.NotFlexAtAll = value;
    }
  }

  private IFlexible_Adapter_Part IFlexible_Adapter { get; } = new();

  public float NotFlexAtAll { get; set; }
}
~~~~~~~~~~~~~

### Init methods
Init methods in parts and methods marked with [InitMethod] on component will be called

### Modules
objects implementing IModule are modules  
They can contain logic and data  
They don't know where they are located, but tipically in some component or its part  
They can reques other modules with [In] attribute by type or interface  
Host component is supposed to inject them  
They can be private and hide implementation details

### Chimera Parts
Sometimes i fail to decouple module from component and this happens:
~~~~~~~~~~~~~{cs}
public class Tree_Part : Part, IModule
~~~~~~~~~~~~~
They are parts but they can be injected and request other modules

### IPropContainer IProp
IProps inside IPropContainer will be injected with the container

This allows me to create reactive props

### IAware
Aware objects are injected with host component and their property name  
host component and prop name used only for debug messages  
They shouldn't couple module to outer component