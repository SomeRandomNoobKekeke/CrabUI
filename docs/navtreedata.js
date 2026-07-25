/*
 @licstart  The following is the entire license notice for the JavaScript code in this file.

 The MIT License (MIT)

 Copyright (C) 1997-2020 by Dimitri van Heesch

 Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 and associated documentation files (the "Software"), to deal in the Software without restriction,
 including without limitation the rights to use, copy, modify, merge, publish, distribute,
 sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all copies or
 substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 @licend  The above is the entire license notice for the JavaScript code in this file
*/
var NAVTREE =
[
  [ "CUI", "index.html", [
    [ "Intro", "index.html", null ],
    [ "QuickStart", "_quick_start.html", [
      [ "Min example:", "_quick_start.html#autotoc_md3", null ]
    ] ],
    [ "Overview", "_overview.html", [
      [ "Basics", "_overview.html#autotoc_md4", null ],
      [ "Components", "_overview.html#autotoc_md5", null ],
      [ "Layouts", "_overview.html#autotoc_md7", null ],
      [ "Types", "_overview.html#autotoc_md8", null ],
      [ "Props", "_overview.html#autotoc_md10", null ],
      [ "Events", "_overview.html#autotoc_md15", null ]
    ] ],
    [ "Other Concepts", "_other_concepts.html", [
      [ "CUIInfrastructure", "_other_concepts.html#autotoc_md17", null ],
      [ "VisualComponents / VisualUnits", "_other_concepts.html#autotoc_md19", null ],
      [ "Serialization (wip, mostly borked)", "_other_concepts.html#autotoc_md20", [
        [ "path resolution", "_other_concepts.html#autotoc_md21", null ],
        [ "Serialization modes", "_other_concepts.html#autotoc_md22", null ]
      ] ],
      [ "Component States", "_other_concepts.html#autotoc_md23", null ],
      [ "Focus (wip, mostly borked)", "_other_concepts.html#autotoc_md24", null ],
      [ "Sprites / Textures", "_other_concepts.html#autotoc_md25", null ],
      [ "Styles and Palettes", "_other_concepts.html#autotoc_md26", null ],
      [ "Animations", "_other_concepts.html#autotoc_md31", null ],
      [ "RoutableCommands", "_other_concepts.html#autotoc_md32", null ],
      [ "Debug (almost non existent)", "_other_concepts.html#autotoc_md33", null ]
    ] ],
    [ "Notes For Code Divers", "_notes_for_code_divers.html", [
      [ "Component Generator", "_notes_for_code_divers.html#autotoc_md34", [
        [ "Components", "_notes_for_code_divers.html#autotoc_md35", null ],
        [ "Parts", "_notes_for_code_divers.html#autotoc_md36", null ],
        [ "Adapter parts", "_notes_for_code_divers.html#autotoc_md37", null ],
        [ "Init methods", "_notes_for_code_divers.html#autotoc_md38", null ],
        [ "Modules", "_notes_for_code_divers.html#autotoc_md39", null ],
        [ "Chimera Parts", "_notes_for_code_divers.html#autotoc_md40", null ],
        [ "IPropContainer IProp", "_notes_for_code_divers.html#autotoc_md41", null ],
        [ "IAware", "_notes_for_code_divers.html#autotoc_md42", null ]
      ] ]
    ] ],
    [ "RoadMap", "_road_map.html", null ],
    [ "Some Examples", "_some_examples.html", [
      [ "E2ETest ClientProject\\InMemory\\CUITest\\E2E\\Test", "_some_examples.html#autotoc_md43", null ],
      [ "Snapshot tests ClientProject\\InMemory\\CUITest\\Snapshots\\Tests", "_some_examples.html#autotoc_md49", null ],
      [ "UTests ClientProject\\InMemory\\CUITest\\UTests", "_some_examples.html#autotoc_md50", null ]
    ] ],
    [ "Changelog", "md__client_project_2_client_source_2_changelog.html", [
      [ "0.3.1.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md59", null ],
      [ "0.3.0.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md60", [
        [ "WIP focus", "md__client_project_2_client_source_2_changelog.html#autotoc_md61", null ],
        [ "Serialization", "md__client_project_2_client_source_2_changelog.html#autotoc_md62", null ],
        [ "not important:", "md__client_project_2_client_source_2_changelog.html#autotoc_md63", null ]
      ] ],
      [ "0.3.0.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md64", [
        [ "Fixes:", "md__client_project_2_client_source_2_changelog.html#autotoc_md65", [
          [ "- Isolated CUICore", "md__client_project_2_client_source_2_changelog.html#autotoc_md66", null ],
          [ "- CUICore is isolated from MonoGame", "md__client_project_2_client_source_2_changelog.html#autotoc_md67", null ],
          [ "- VisualComponents / VisualElements", "md__client_project_2_client_source_2_changelog.html#autotoc_md68", null ],
          [ "- Text measurement / drawing logic was moved to TextBlock VisualElement", "md__client_project_2_client_source_2_changelog.html#autotoc_md69", null ],
          [ "- VisualBounds", "md__client_project_2_client_source_2_changelog.html#autotoc_md70", null ],
          [ "- Most reflection stuff was moved to code generator", "md__client_project_2_client_source_2_changelog.html#autotoc_md71", null ],
          [ "- Everything was public", "md__client_project_2_client_source_2_changelog.html#autotoc_md72", null ],
          [ "- Layout calculations use RectFloat while Drawing uses RectInt", "md__client_project_2_client_source_2_changelog.html#autotoc_md73", null ],
          [ "- Debug system rework", "md__client_project_2_client_source_2_changelog.html#autotoc_md74", null ],
          [ "- other stuff i'm too lazy to mention", "md__client_project_2_client_source_2_changelog.html#autotoc_md75", null ]
        ] ],
        [ "New issues:", "md__client_project_2_client_source_2_changelog.html#autotoc_md76", null ]
      ] ],
      [ "0.2.7.0 -&gt; 0.0.0.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md77", null ],
      [ "↓ Legacy ↓", "md__client_project_2_client_source_2_changelog.html#autotoc_md78", [
        [ "Died From <strike>Cringe</strike> 0 day design flaws", "md__client_project_2_client_source_2_changelog.html#autotoc_md79", null ],
        [ "0.2.7.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md80", null ],
        [ "0.2.6.4", "md__client_project_2_client_source_2_changelog.html#autotoc_md84", null ],
        [ "0.2.6.3", "md__client_project_2_client_source_2_changelog.html#autotoc_md85", null ],
        [ "0.2.6.2", "md__client_project_2_client_source_2_changelog.html#autotoc_md86", null ],
        [ "0.2.6.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md87", null ],
        [ "0.2.6.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md88", null ],
        [ "0.2.5.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md89", null ],
        [ "0.2.5.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md90", null ],
        [ "0.2.4.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md91", null ],
        [ "0.2.3.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md92", null ],
        [ "0.2.2.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md93", null ],
        [ "0.2.2.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md94", null ],
        [ "0.2.1.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md96", null ],
        [ "0.2.0.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md97", null ],
        [ "0.2.0.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md98", null ],
        [ "0.1.0.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md99", null ],
        [ "0.0.5.1", "md__client_project_2_client_source_2_changelog.html#autotoc_md101", null ],
        [ "0.0.5.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md102", null ],
        [ "0.0.4.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md103", null ],
        [ "0.0.3.0", "md__client_project_2_client_source_2_changelog.html#autotoc_md104", null ]
      ] ]
    ] ],
    [ "Packages", "namespaces.html", [
      [ "Package List", "namespaces.html", "namespaces_dup" ],
      [ "Package Members", "namespacemembers.html", [
        [ "All", "namespacemembers.html", null ],
        [ "Functions", "namespacemembers_func.html", null ]
      ] ]
    ] ],
    [ "Classes", "annotated.html", [
      [ "Class List", "annotated.html", "annotated_dup" ],
      [ "Class Index", "classes.html", null ],
      [ "Class Hierarchy", "hierarchy.html", "hierarchy" ],
      [ "Class Members", "functions.html", [
        [ "All", "functions.html", null ],
        [ "Functions", "functions_func.html", null ],
        [ "Variables", "functions_vars.html", null ],
        [ "Properties", "functions_prop.html", null ]
      ] ]
    ] ],
    [ "Files", "files.html", [
      [ "File List", "files.html", "files_dup" ]
    ] ]
  ] ]
];

var NAVTREEINDEX =
[
"1_8cs_source.html",
"_debug_relay_8cs_source.html",
"_text_block_8_resize_strategy_8cs_source.html",
"class_crab_u_i_1_1_____c_u_i_texture_manager.html",
"class_crab_u_i_1_1_c_u_i_serializable_info.html",
"class_crab_u_i_user_1_1_class_mapping_1_1_inconvenient_proxies_1_1_propxy_a.html",
"class_crab_u_i_user_1_1_how_to_access_protected_in_grand_parent_1_1_a.html",
"class_crab_u_i_user_1_1_proxy_performance_1_1_fake_internal_a_proxy3.html",
"interface_crab_u_i_1_1_c_u_i_horizontal_list_layout_1_1_host.html"
];

var SYNCONMSG = 'click to disable panel synchronization';
var SYNCOFFMSG = 'click to enable panel synchronization';
var LISTOFALLMEMBERS = 'List of all members';