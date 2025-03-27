using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUI
{

  public class AttachedItems
  {
    public static void InitStatic()
    {
      CUI.OnDispose += () =>
      {
        All.Clear();
        PrefabMapping.Clear();
        PrevSelected.Clear();
        Selected.Clear();
      };
    }

    public static Dictionary<ushort, AttachedItemHandle> All = new();
    public static Dictionary<int, Type> PrefabMapping = new();

    public static HashSet<AttachedItemHandle> PrevSelected = new();
    public static HashSet<AttachedItemHandle> Selected = new();

    public static void UpdateAll()
    {
      Selected.Clear();

      if (Character.Controlled is { } controlled)
      {
        if (controlled.SelectedItem != null && controlled.CanInteractWith(controlled.SelectedItem))
        {
          if (All.ContainsKey(controlled.SelectedItem.ID))
          {
            Selected.Add(All[controlled.SelectedItem.ID]);
          }

          UpdateItem(controlled.SelectedItem);
        }
        if (controlled.Inventory != null)
        {
          foreach (Item item in controlled.Inventory.AllItems)
          {
            if (controlled.HasEquippedItem(item))
            {
              if (All.ContainsKey(item.ID)) Selected.Add(All[item.ID]);
              UpdateItem(item);
            }
          }
        }
      }

      foreach (var handle in Selected.Except(PrevSelected)) handle.Select();
      foreach (var handle in PrevSelected.Except(Selected)) handle.Deselect();
      (PrevSelected, Selected) = (Selected, PrevSelected);
    }

    public static void UpdateItem(Item item)
    {
      if (All.ContainsKey(item.ID))
      {
        All[item.ID].Update();
      }
    }

    public static void ShakeTheRefs()
    {
      List<ushort> keys = new List<ushort>(All.Keys);
      foreach (ushort key in keys)
      {
        if (All[key].IsDead) All.Remove(key);
      }
    }

    public static void Connect(Item item, CUIComponent component, Action<Item, CUIComponent> callback = null)
    {
      if (item == null || component == null) return;

      AttachedItemHandle handle = component.AttachedItemHandle;
      handle ??= new AttachedItemHandle(item, component);
      if (callback != null) handle.OnUpdate += callback;
      component.AttachedItemHandle = handle;

      //ShakeTheRefs();

      All[item.ID] = handle;
    }

    public static void ConnectPrefabs(ItemPrefab prefab, Type CUIType) => ConnectPrefabs(prefab.Identifier, CUIType);
    public static void ConnectPrefabs(string prefabName, Type CUIType) => ConnectPrefabs(new Identifier(prefabName), CUIType);
    public static void ConnectPrefabs(Identifier prefabId, Type CUIType)
    {
      if (CUIType == null) return;

      PrefabMapping[prefabId.HashCode] = CUIType;

      foreach (Item item in Item.ItemList)
      {
        ConnectIfMapped(item);
      }
    }

    internal static void ConnectIfMapped(Item item)
    {
      Type CUIType = PrefabMapping.GetValueOrDefault(item.Prefab.Identifier.HashCode);
      if (item == null || CUIType == null) return;

      CUIComponent component = (CUIComponent)Activator.CreateInstance(CUIType);

      AttachedItemHandle handle = component.AttachedItemHandle;
      handle ??= new AttachedItemHandle(item, component);
      component.AttachedItemHandle = handle;

      All[item.ID] = handle;
    }
  }

  /// <summary>
  /// Link between Item and CUIComponent
  /// </summary>
  public class AttachedItemHandle
  {
    private WeakReference<Item> Reference;
    public CUIComponent Component { get; set; }
    public Item Item
    {
      get
      {
        if (Reference == null) return null;
        if (Reference.TryGetTarget(out Item target)) return target;
        else return null;
      }
      set => Reference = new WeakReference<Item>(value);
    }
    public bool IsDead => Component == null;
    /// <summary>
    /// Called on Item.UpdateHud
    /// </summary>
    public event Action<Item, CUIComponent> OnUpdate;
    /// <summary>
    /// Called when item hud is opened
    /// </summary>
    public event Action<Item, CUIComponent> OnSelect;
    /// <summary>
    /// Called when item hud is closed
    /// </summary>    
    public event Action<Item, CUIComponent> OnDeselect;


    public Action<Item, CUIComponent> AddOnUpdate { set => OnUpdate += value; }
    public Action<Item, CUIComponent> AddOnSelect { set => OnSelect += value; }
    public Action<Item, CUIComponent> AddOnDeselect { set => OnDeselect += value; }

    internal void Update() => OnUpdate?.Invoke(Item, Component);
    internal void Select() => OnSelect?.Invoke(Item, Component);
    internal void Deselect() => OnDeselect?.Invoke(Item, Component);
    public AttachedItemHandle() { }
    public AttachedItemHandle(Item item, CUIComponent component)
    {
      this.Item = item;
      Component = component;
    }
  }
}
