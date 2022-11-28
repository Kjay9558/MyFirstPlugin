using System;
using BepInEx;
using TinyResort;
using UnityEngine;
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
            GameHud = GameObject.Find("MapCanvas/Hud");
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

            var rect = buttonGrid.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 1);
            

            HotkeyButton1 = TRInterface.CreateButton(ButtonTypes.MainMenu, buttonGrid.transform, "Hotkey1", useHotkey1);
            HotkeyButton1.textMesh.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 10);
            HotkeyButton1.textMesh.fontSize = 8;

        }

        public TRButton HotkeyButton1 { get; set; }

        public GameObject GameHud { get; set; }

        private void useHotkey1()
        {
            inSim.Keyboard.KeyPress(VirtualKeyCode.VK_P);
        }
    }
}
