using System;
using BepInEx;
using TinyResort;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using WindowsInput;
using WindowsInput.Native;

namespace MyFirstPlugin
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("dev.TinyResort.TRTools")]
    public class Plugin : BaseUnityPlugin
    {
        public static TRPlugin plugin;
        
        // Build an input simulator instance
        InputSimulator inSim = new InputSimulator();
        
        private string hotkey1 = "P";
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            plugin = TRTools.Initialize(this, -1 /* TODO: add yourNexusID*/, "kjay");

            TRTools.sceneSetupEvent += CreateHotkeyButtons;
        }

        private void CreateHotkeyButtons()
        {
            GameHud = GameObject.Find("Canvas/ChatBox");
            var buttonGrid = new GameObject();
            buttonGrid.name = "Hotkey Buttons Grid";
            try
            {
                buttonGrid.transform.SetParent(GameHud.transform);
            }
            catch
            {
                return;
            }
            buttonGrid.transform.SetAsLastSibling();
            var gridLayoutGroup = buttonGrid.AddComponent<GridLayoutGroup>();

            gridLayoutGroup.cellSize = new Vector2(52, 30);
            gridLayoutGroup.spacing = new Vector2(8, 2);
            gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = 3;
            
            
            var rect = buttonGrid.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 1);
            rect.anchorMax = new Vector2(0.5f, 1);
            rect.anchorMin = new Vector2(0.5f, 1);
            rect.localScale = Vector3.one;
            rect.anchoredPosition = new Vector2(-210, -475);
            

            HotkeyButton1 = TRInterface.CreateButton(ButtonTypes.MainMenu, buttonGrid.transform, "Hotkey1", useHotkey1);
            HotkeyButton1.textMesh.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 10);
            HotkeyButton1.textMesh.fontSize = 8;

        }

        public static TRButton HotkeyButton1 { get; set; }

        public static GameObject GameHud { get; set; }


        private void Update()
        {
            useHotkey1();
        }

        private void useHotkey1()
        {
            inSim.Keyboard.KeyPress(VirtualKeyCode.VK_P);
        }
    }
}
