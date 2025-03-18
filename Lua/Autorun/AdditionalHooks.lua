-- Hooks necessary for CUI
-- apparently the only solution to https://github.com/evilfactory/LuaCsForBarotrauma/issues/245

if SERVER then return end

-- Global var to track already patched methods
if AdditionalHooks == nil then AdditionalHooks = {} end

-- Harmony.Patch, only if not patched already
local function EnsurePatch(class, method, params, patch, hookType)
  local combinedName = class .. "." .. method
  
  if AdditionalHooks[combinedName] == true then 
    --print(combinedName, " Already patched!")
    return 
  end
  AdditionalHooks[combinedName] = true

  Hook.Patch('AdditionalHooks', class, method,params, patch, hookType)
end

EnsurePatch("Barotrauma.GUI", "Draw", function(instance, ptable)
  Hook.Call("GUI_Draw_Prefix", ptable["spriteBatch"])
end, Hook.HookMethodType.Before)

EnsurePatch("Barotrauma.GUI", "DrawCursor", function(instance, ptable)
  Hook.Call("GUI_DrawCursor_Prefix", ptable["spriteBatch"])
end, Hook.HookMethodType.Before)

-- It works, but i don't need it
-- EnsurePatch("Barotrauma.Camera", "MoveCamera", function(instance, ptable)
--   local result = Hook.Call("Camera_MoveCamera_Prefix", ptable["deltaTime"] , ptable["allowMove"], ptable["allowZoom"],ptable["allowInput"], ptable["followSub"])

--   if result == nil then return end
--   if result["allowMove"] ~= nil then ptable["allowMove"] = result["allowMove"] end
--   if result["allowZoom"] ~= nil then ptable["allowZoom"] = result["allowZoom"] end
-- end, Hook.HookMethodType.Before)

EnsurePatch("EventInput.KeyboardDispatcher", "set_Subscriber", function(instance, ptable)
  Hook.Call("KeyboardDispatcher_set_Subscriber_Prefix", instance, ptable["value"])
end, Hook.HookMethodType.Before)

EnsurePatch("Barotrauma.GUI", "TogglePauseMenu",{},  function(instance, ptable)
  Hook.Call("GUI_TogglePauseMenu_Postfix")
end, Hook.HookMethodType.After)

EnsurePatch("Barotrauma.GUI", "get_InputBlockingMenuOpen", function(instance, ptable)
  local isBlocking = Hook.Call("GUI_InputBlockingMenuOpen_Postfix")
  return ptable.ReturnValue or isBlocking
end, Hook.HookMethodType.After)

-- CUI.CheckPatches("GUI","TogglePauseMenu")