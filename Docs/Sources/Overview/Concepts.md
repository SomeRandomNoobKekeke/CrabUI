# Concepts {#Concepts}


All functionality is incapsulated in CUICore
CUICore is isolated from main game, it's passive state machine, it needs CUIRunner to run it, and CUIRunner needs DataSources to read game data and folow game life cycle

It's all wired up by CUISetup, static CUI class already has one and it will clean it up on plugin unload

Most stuff can be accessed via CUI static class or static members of CUICore, CUI is intended to be used by user and CUICore should be accessed by stuff from inside CUICore but whatever

Main entity in CUICore is CUIComponent, all CUIFrames, CUIButtons are CUIComponents

CUIComponents have parents and children and can be organized in trees

CUIComponents have various props like size, position, anchors, color e.t.c.

CUIComponents have layouts, they are separate objects that incapsulates all logic for calculating children rects and resizing parent

There's a special master component: CUIMainComponent, instead of drawing and updating itself it's orchestrating drawing and layout calculations of it's children

CUICore has 2 CUIMainComponents: CUI.Main and CUI.TopMain, CUI.Main is drawn below vanilla GUI and CUI.TopMain is drawn above

Actually unit of drawing here is not CUIComponent, it's VisualElement
CUIComponents may have multiple primitive VisualElements like background, text, borders

CUIMainComponent extracts and flattens them in one big list of draw instructions and plugs it into the chain drawer

Important thing here is that same list is used for event handling, so order of event call is always the opposite of draw order