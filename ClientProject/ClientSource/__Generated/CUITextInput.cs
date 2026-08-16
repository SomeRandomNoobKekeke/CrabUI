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
      InitLayout();
      InitVisualSlots();
    }

    void IComponent.InjectModules()
    {
      As_CUITextInput.RightResizeHandle.Host = As_CUIVisualComponent.Adapters.IResizable;
      As_CUITextInput.DragHandle.Host = As_CUIVisualComponent.Adapters.IDraggable;
      As_CUITextInput.SwipeHandle.Host = As_CUIVisualComponent.Adapters.ISwipeable;
      As_CUITextInput.LayoutMarker.Host = As_CUIVisualComponent.Adapters.LayoutMarker;
    }

    void IComponent.InjectParts()
    {
      As_CUIVisualComponent.Self = this;
      As_CUIComponent.Self = this;
      As_CUITextInput.Self = this;

      As_CUITextInput.SelectionHandle.Self = this;
      As_CUIVisualComponent.Commands.Self = this;
      As_CUIVisualComponent.ProtectedCommands.Self = this;
      As_CUIVisualComponent.Data.Self = this;
      As_CUIVisualComponent.As_Dictionary.Self = this;
      As_CUIVisualComponent.Events.Self = this;
      As_CUIVisualComponent.MainComponentTracker.Self = this;
      As_CUIVisualComponent.LayoutUpdateNotifier.Self = this;
      As_CUIVisualComponent.VisualRestructureNotifier.Self = this;
      As_CUIVisualComponent.As_StringDictionary.Self = this;
      As_CUIVisualComponent.Styles.Self = this;
      As_CUIVisualComponent.Adapters.Self = this;
      As_CUIVisualComponent.Adapters.Layout_Child.Self = this;
      As_CUIVisualComponent.Adapters.LayoutMarker.Self = this;
      As_CUIVisualComponent.Adapters.IDraggable.Self = this;
      As_CUIVisualComponent.Adapters.IResizable.Self = this;
      As_CUIVisualComponent.Adapters.ISwipeable.Self = this;
      As_CUIVisualComponent.LayoutProps.Self = this;
      As_CUIVisualComponent.Children.Self = this;
      As_CUIVisualComponent.Children.Operations.Self = this;
      As_CUIVisualComponent.Tree.Self = this;
      As_CUIVisualComponent.TreeOperations.Self = this;
    }

    void IComponent.InitParts()
    {
      As_CUITextInput.SelectionHandle.Init();
      As_CUIVisualComponent.Styles.Init();
    }

    void IComponent.InitModules()
    {
      As_CUIVisualComponent.Adapters.LayoutMarker.Init();
      As_CUIVisualComponent.Adapters.IDraggable.Init();
      As_CUIVisualComponent.Adapters.IResizable.Init();
      As_CUIVisualComponent.Adapters.ISwipeable.Init();
    }

    void IComponent.InjectProps()
    {
      As_CUIVisualComponent.LayoutProps.Absolute.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.AbsoluteMin.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.AbsoluteMax.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.Relative.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.RelativeMin.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.RelativeMax.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.CrossRelative.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.Anchor.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.ParentAnchor.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.Direction.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.Flex.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.ChildrenOffset.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.ChildrenBounds.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.FitContent.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.GridRow.Container = As_CUIVisualComponent.LayoutProps;
      As_CUIVisualComponent.LayoutProps.GridColumn.Container = As_CUIVisualComponent.LayoutProps;
    }

    void IComponent.NotifyAwareObjects()
    {
      RightResizeHandle.HostComponent = this;
      RightResizeHandle.HostPropName = "RightResizeHandle";
      Layout.HostComponent = this;
      Layout.HostPropName = "Layout";
      DragHandle.HostComponent = this;
      DragHandle.HostPropName = "DragHandle";
      SwipeHandle.HostComponent = this;
      SwipeHandle.HostPropName = "SwipeHandle";
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
      public ResizeHandle RightResizeHandle => Self.RightResizeHandle;
      public CUIVisualComponent.public_Commands_Part Commands => Self.Commands;
      public CUIVisualComponent.Protected_Commands_Part ProtectedCommands => Self.ProtectedCommands;
      public CUIVisualComponent.Events_Part Events => Self.Events;
      public DragHandle DragHandle => Self.DragHandle;
      public SwipeHandle SwipeHandle => Self.SwipeHandle;
      public LayoutMarker LayoutMarker => Self.LayoutMarker;
      public CUIVisualComponent.MainComponentTracker_Part MainComponentTracker => Self.MainComponentTracker;
      public CUIVisualComponent.TreeEvents_Part Tree => Self.Tree;
      public CUITextInput Self { get; set; }
    }
  }
}
