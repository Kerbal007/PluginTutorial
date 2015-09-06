using UnityEngine;
using PluginTutorial.Extensions; // We add this so we can use our new PluginTutorial.Extensions namespace
using KSP.IO;

namespace PluginTutorial
{
    public class PluginTutorial : PartModule // I am creating a plugin for KSP. PartModule allows me me to override its functions to control it.
    {
        private Rect _windowPosition = new Rect();
        private GUIStyle _windowStyle, _labelStyle; // Setup Gui Style 
        private bool _hasInitStyles = false;

        public override void OnStart(StartState state)
        {
            if (state != StartState.Editor)
            {
                if (!_hasInitStyles) InitStyles(); //if guistyles not initiaited then do it.
            RenderingManager.AddToPostDrawQueue(0, OnDraw);
        }
    }
        public override void OnSave(ConfigNode node)
        {
            PluginConfiguration config = PluginConfiguration.CreateForType<PluginTutorial>();

            config.SetValue("Window Position", _windowPosition);
            config.save();
        }

        public override void OnLoad(ConfigNode node)
        {
            PluginConfiguration config = PluginConfiguration.CreateForType<PluginTutorial>();

            config.load();
            _windowPosition = config.GetValue<Rect>("Window Position");
        }

        private void OnDraw()
        {
            if (this.vessel == FlightGlobals.ActiveVessel && this.part.IsPrimary(this.vessel.parts, this.ClassID)) // If the primary part is destroyed this allows the other part to "take over"
            { 
            _windowPosition = GUILayout.Window(10, _windowPosition, OnWindow, "This is a title", _windowStyle);

                if (_windowPosition.x == 0f && _windowPosition.y == 0f)
                    _windowPosition = _windowPosition.CenterScreen();
            }

        }

        private void OnWindow(int windowId)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("This is a label", _labelStyle);
            GUILayout.EndHorizontal();

            GUI.DragWindow();

        }

        private void InitStyles() // Method for initialising styles
        {
            _windowStyle = new GUIStyle(HighLogic.Skin.window);
            _windowStyle.fixedWidth = 250f;

            _labelStyle = new GUIStyle(HighLogic.Skin.label);
            _labelStyle.stretchWidth = true;

            _hasInitStyles = true;
        }
    }
}
