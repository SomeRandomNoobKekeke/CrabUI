using System;
using Barotrauma;
using System.Linq;
using System.Collections.Generic;
using CUICodeGenerator;

namespace CrabUI
{
  public partial class CUITextInput
  {
    [Local]
    protected Self_As_CUITextInput As_CUITextInput { get; } = new();
    void IComponent.RunInitMethods()
    {
    }

    void IComponent.InjectModules()
    {
      As_CUITextInput.DragHandle.Host = As_CUIComponent.Adapters.IDraggable;
      As_CUITextInput.RightResizeHandle.Host = As_CUIComponent.Adapters.IResizable;
      As_CUITextInput.SwipeHandle.Host = As_CUIComponent.Adapters.ISwipeable;
      As_CUITextInput.LayoutMarker.Host = As_CUIComponent.Adapters.LayoutMarker;
    }

    void IComponent.InjectParts()
    {
      As_CUIComponent.Self = this;
      As_CUITextInput.Self = this;
      
      As_CUITextInput.SelectionHandle.Self = this;
      As_CUIComponent.Commands.Self = this;
      As_CUIComponent.ProtectedCommands.Self = this;
      As_CUIComponent.InitDebugChannels.Self = this;
      As_CUIComponent.As_Dictionary.Self = this;
      As_CUIComponent.Events.Self = this;
      As_CUIComponent.IFocusableAdapter.Self = this;
      As_CUIComponent.MainComponentTracker.Self = this;
      As_CUIComponent.LayoutUpdateNotifier.Self = this;
      As_CUIComponent.VisualRestructureNotifier.Self = this;
      As_CUIComponent.As_StringDictionary.Self = this;
      As_CUIComponent.Styles.Self = this;
      As_CUIComponent.Adapters.Self = this;
      As_CUIComponent.Adapters.Layout_Child.Self = this;
      As_CUIComponent.Adapters.LayoutMarker.Self = this;
      As_CUIComponent.Adapters.IDraggable.Self = this;
      As_CUIComponent.Adapters.IResizable.Self = this;
      As_CUIComponent.Adapters.ISwipeable.Self = this;
      As_CUIComponent.LayoutProps.Self = this;
      As_CUIComponent.Children.Self = this;
      As_CUIComponent.Children.Operations.Self = this;
      As_CUIComponent.Tree.Self = this;
      As_CUIComponent.TreeOperations.Self = this;
    }

    void IComponent.InitParts()
    {
      As_CUITextInput.SelectionHandle.Init();
      As_CUIComponent.InitDebugChannels.Init();
      As_CUIComponent.Styles.Init();
      As_CUIComponent.LayoutProps.Init();
    }

    void IComponent.InitModules()
    {
      As_CUITextInput.Events.Init();
      As_CUITextInput.Tree.Init();
      As_CUIComponent.Adapters.LayoutMarker.Init();
      As_CUIComponent.Adapters.IDraggable.Init();
      As_CUIComponent.Adapters.IResizable.Init();
      As_CUIComponent.Adapters.ISwipeable.Init();
    }

    void IComponent.InjectProps()
    {
      As_CUIComponent.LayoutProps.Absolute.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.AbsoluteMin.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.AbsoluteMax.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Relative.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.RelativeMin.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.RelativeMax.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.CrossRelative.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Anchor.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.ParentAnchor.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Direction.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.Flex.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.ChildrenOffset.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.ChildrenBounds.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.FitContent.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.GridRow.Container = As_CUIComponent.LayoutProps;
      As_CUIComponent.LayoutProps.GridColumn.Container = As_CUIComponent.LayoutProps;
    }

    void IComponent.NotifyAwareObjects()
    {
      TextBlock.HostComponent = this;
      TextBlock.HostPropName = "TextBlock";
      CaretTexture.HostComponent = this;
      CaretTexture.HostPropName = "CaretTexture";
      SelectionOverlay.HostComponent = this;
      SelectionOverlay.HostPropName = "SelectionOverlay";
      DragHandle.HostComponent = this;
      DragHandle.HostPropName = "DragHandle";
      RightResizeHandle.HostComponent = this;
      RightResizeHandle.HostPropName = "RightResizeHandle";
      SwipeHandle.HostComponent = this;
      SwipeHandle.HostPropName = "SwipeHandle";
      Layout.HostComponent = this;
      Layout.HostPropName = "Layout";
      Background.HostComponent = this;
      Background.HostPropName = "Background";
      Borders.HostComponent = this;
      Borders.HostPropName = "Borders";
      VisualBounds.HostComponent = this;
      VisualBounds.HostPropName = "VisualBounds";
      VisualWrapper.HostComponent = this;
      VisualWrapper.HostPropName = "VisualWrapper";
      LayoutProps.Absolute.HostComponent = this;
      LayoutProps.Absolute.HostPropName = "Absolute";
      LayoutProps.AbsoluteMin.HostComponent = this;
      LayoutProps.AbsoluteMin.HostPropName = "AbsoluteMin";
      LayoutProps.AbsoluteMax.HostComponent = this;
      LayoutProps.AbsoluteMax.HostPropName = "AbsoluteMax";
      LayoutProps.Relative.HostComponent = this;
      LayoutProps.Relative.HostPropName = "Relative";
      LayoutProps.RelativeMin.HostComponent = this;
      LayoutProps.RelativeMin.HostPropName = "RelativeMin";
      LayoutProps.RelativeMax.HostComponent = this;
      LayoutProps.RelativeMax.HostPropName = "RelativeMax";
      LayoutProps.CrossRelative.HostComponent = this;
      LayoutProps.CrossRelative.HostPropName = "CrossRelative";
      LayoutProps.Anchor.HostComponent = this;
      LayoutProps.Anchor.HostPropName = "Anchor";
      LayoutProps.ParentAnchor.HostComponent = this;
      LayoutProps.ParentAnchor.HostPropName = "ParentAnchor";
      LayoutProps.Direction.HostComponent = this;
      LayoutProps.Direction.HostPropName = "Direction";
      LayoutProps.Flex.HostComponent = this;
      LayoutProps.Flex.HostPropName = "Flex";
      LayoutProps.ChildrenOffset.HostComponent = this;
      LayoutProps.ChildrenOffset.HostPropName = "ChildrenOffset";
      LayoutProps.ChildrenBounds.HostComponent = this;
      LayoutProps.ChildrenBounds.HostPropName = "ChildrenBounds";
      LayoutProps.FitContent.HostComponent = this;
      LayoutProps.FitContent.HostPropName = "FitContent";
      LayoutProps.GridRow.HostComponent = this;
      LayoutProps.GridRow.HostPropName = "GridRow";
      LayoutProps.GridColumn.HostComponent = this;
      LayoutProps.GridColumn.HostPropName = "GridColumn";
    }

  protected class Self_As_CUITextInput : IAdapterPart
  {
    public CUITextInput.SelectionHandle_Part SelectionHandle => Self.SelectionHandle;
    public CUIComponent.public_Commands_Part Commands => Self.Commands;
    public CUIComponent.Protected_Commands_Part ProtectedCommands => Self.ProtectedCommands;
    public CUIComponent.Events_Part Events => Self.Events;
    public DragHandle DragHandle => Self.DragHandle;
    public ResizeHandle RightResizeHandle => Self.RightResizeHandle;
    public SwipeHandle SwipeHandle => Self.SwipeHandle;
    public LayoutMarker LayoutMarker => Self.LayoutMarker;
    public CUIComponent.MainComponentTracker_Part MainComponentTracker => Self.MainComponentTracker;
    public CUIComponent.TreeEvents_Part Tree => Self.Tree;
    public CUITextInput Self { get; set; }
  }
  }
}
