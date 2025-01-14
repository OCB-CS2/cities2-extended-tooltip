﻿using BepInEx;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using Gooee.Plugins;
using Gooee.Plugins.Attributes;
using ExtendedTooltip.Controllers;

#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace ExtendedTooltip
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID + "_Cities2Harmony");
            var patchedMethods = harmony.GetPatchedMethods().ToArray();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded! Patched methods " + patchedMethods.Length);

            foreach (var patchedMethod in patchedMethods)
            {
                Logger.LogInfo($"Patched method: {patchedMethod.Module.Name}:{patchedMethod.Name}");
            }
        }
    }

    [ControllerTypes(typeof(ExtendedTooltipController))]
    public class ExtendedTooltip : IGooeePluginWithControllers, IGooeeChangeLog, IGooeeStyleSheet
    {
        public string Name => "ExtendedTooltip";
        public string Version => MyPluginInfo.PLUGIN_VERSION;
        public string ScriptResource => "ExtendedTooltip.Resources.ui.js";
        public string StyleResource => null;
        public IController[] Controllers { get; set; }
        public string ChangeLogResource => null;
    }
}