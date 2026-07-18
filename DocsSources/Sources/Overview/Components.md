# Components {#OverviewComponents}

- CUIComponent - It's just a square with background and borders
- CUIMainComponent -Special component that Draws and calculates layouts of its children, there's 2 CUI.Main and CUI.TopMain, don't create
- CUIButton - A button
- CUICanvas - A canvas, you can draw on its texture
- CUICheckBox - A checkbox
- CUICloseButton - Button with a cross that emits "close" event that forces nearest frame to close
- CUIDropDown - Drop down select (borked)
- CUIFrame - Draggable and resizable container for other components, can be Opened and Closed
- CUIVerticalList - Wrapper with CUIVerticalListLayout
- CUIGrid - Wrapper with Grid layour (half-assed)
- CUIPages - Can have only 1 child - opened page, resizes opened page to its size
- CUIPage - When opened in CUIPages recieves OnOpen / OnClose events
- CUIToggleButton - Button that can be toggled On and Off
- CUIRadioButton - Button that can be added to a group where only 1 button can be On at a time
- CUIScrollBar - Box with a slider that can be dragged, exposes Lambda (half-assed)
- CUITextBlock - Block of text with background, can resist shrinking
- CUITextLine - Line of text
- CUITextInput - Text input (half-assed)

### CUIDefault.
- CUIDefault.Frame - prebuilt frame with handle, caption and close button, add children to frame["layout"]
- InputField - Base class for InputField, wrappers with label and text input, intended to be used in some settings ui (half-assed)
