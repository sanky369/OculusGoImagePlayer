using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace VREasy
{
    public class VREasySDKhelper : EditorWindow
    {
        // SDK - RELATED
        static bool Steam_SDK = false;
        static bool Oculus_SDK = false;
        static bool LeapMotion_SDK = false;
        static bool GoogleVR_SDK = false;
        static bool Playmaker_SDK = false;
        static bool WaveVR_SDK = false;
        static bool Meta2_SDK = false;
        static bool VReePlatform_SDK = false;

        static string Steam_SDK_define = "VREASY_STEAM_SDK";
        static string Oculus_SDK_define = "VREASY_OCULUS_UTILITIES_SDK";
        static string LeapMotion_SDK_define = "VREASY_LEAPMOTION_SDK";
        static string GoogleVR_SDK_define = "VREASY_GOOGLEVR_SDK";
        static string Playmaker_SDK_define = "VREASY_PLAYMAKER_SDK";
        static string WaveVR_SDK_define = "VREASY_WAVEVR_SDK";
        static string Meta2_SDK_define = "VREASY_META2_SDK";
        static string VReePlatform_SDK_define = "VREASY_VREE_PLATFORM_SDK";

        private Vector2 scrollPos;


        static string[] defines = new string[] {
                Steam_SDK_define,
                Oculus_SDK_define,
                LeapMotion_SDK_define,
                GoogleVR_SDK_define,
                Playmaker_SDK_define,
                WaveVR_SDK_define,
                Meta2_SDK_define,
                VReePlatform_SDK_define

    };

        [MenuItem("VREasy/SDK Checker")]
        public static void ShowWindow()
        {
            GetWindow(typeof(VREasySDKhelper), false, "SDK Checker");
        }

        void OnFocus() {
            BuildTargetGroup currentBuildTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            //string cs = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            string cs = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentBuildTarget);

            Steam_SDK = cs.Contains(Steam_SDK_define);
            Oculus_SDK = cs.Contains(Oculus_SDK_define);
            LeapMotion_SDK = cs.Contains(LeapMotion_SDK_define);
            GoogleVR_SDK = cs.Contains(GoogleVR_SDK_define);
            Playmaker_SDK = cs.Contains(Playmaker_SDK_define);
            WaveVR_SDK = cs.Contains(WaveVR_SDK_define);
            Meta2_SDK = cs.Contains(Meta2_SDK_define);
            VReePlatform_SDK = cs.Contains(VReePlatform_SDK_define);
        }

        bool handleRepaintErrors = false;
        void OnGUI()
        {
            // Hack to prevent ArgumentException: GUILayout: Mismatched LayoutGroup.Repaint errors
            // see more: https://forum.unity3d.com/threads/unexplained-guilayout-mismatched-issue-is-it-a-unity-bug-or-a-miss-understanding.158375/
            // and: https://forum.unity3d.com/threads/solved-adding-and-removing-gui-elements-at-runtime.57295/
            if (Event.current.type == EventType.Repaint && !handleRepaintErrors)
            {
                handleRepaintErrors = true;
                return;
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

            EditorStyles.label.wordWrap = true;
            GUILayout.Label("Select imported SDKs for integration", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            GUILayout.Label("Current target build: " + EditorUserBuildSettings.selectedBuildTargetGroup, EditorStyles.wordWrappedLabel);
            EditorGUILayout.HelpBox("This options add information to the current target build Scripting Define Symbols only (not other target builds)", MessageType.Info);

            Steam_SDK = EditorGUILayout.Toggle("SteamVR", Steam_SDK);
            Oculus_SDK = EditorGUILayout.Toggle("Oculus utilities", Oculus_SDK);
            LeapMotion_SDK = EditorGUILayout.Toggle("LeapMotion SDK", LeapMotion_SDK);
            GoogleVR_SDK = EditorGUILayout.Toggle("GoogleVR SDK", GoogleVR_SDK);
            Playmaker_SDK = EditorGUILayout.Toggle("PlayMaker SDK", Playmaker_SDK);
            WaveVR_SDK = EditorGUILayout.Toggle("WaveVR SDK", WaveVR_SDK);
            Meta2_SDK = EditorGUILayout.Toggle("Meta2 SDK", Meta2_SDK);
            VReePlatform_SDK = EditorGUILayout.Toggle("VRee Platform SDK", VReePlatform_SDK);

            EditorGUILayout.HelpBox("Make sure you have imported the approriate SDK before applying its integration here", MessageType.Info);

            if (GUILayout.Button("Apply integration"))
            {
                List<string> customdefines = new List<string>();
                /*customdefines.AddRange(defines);
                if (!Steam_SDK) customdefines.Remove(Steam_SDK_define);
                if (!Oculus_SDK) customdefines.Remove(Oculus_SDK_define);
                if (!LeapMotion_SDK) customdefines.Remove(LeapMotion_SDK_define);
                if (!GoogleVR_SDK) customdefines.Remove(GoogleVR_SDK_define);
                if (!Playmaker_SDK) customdefines.Remove(Playmaker_SDK_define);
                if (!WaveVR_SDK) customdefines.Remove(WaveVR_SDK_define);
                if (!Meta2_SDK) customdefines.Remove(Meta2_SDK_define);
                if (!VReePlatform_SDK) customdefines.Remove(VReePlatform_SDK_define);
                if (!SpectatorView) customdefines.Remove(SpectatorView_define);*/
                List<string> oldDefines = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup).Split(';'));
                
                customdefines.AddRange(oldDefines);
                customdefines.AddRange(defines);

                if (!Steam_SDK) customdefines.RemoveAll(item  => item == Steam_SDK_define);
                if (!Oculus_SDK) customdefines.RemoveAll(item => item == Oculus_SDK_define);
                if (!LeapMotion_SDK) customdefines.RemoveAll(item => item == LeapMotion_SDK_define);
                if (!GoogleVR_SDK) customdefines.RemoveAll(item => item == GoogleVR_SDK_define);
                if (!Playmaker_SDK) customdefines.RemoveAll(item => item == Playmaker_SDK_define);
                if (!WaveVR_SDK) customdefines.RemoveAll(item => item == WaveVR_SDK_define);
                if (!Meta2_SDK) customdefines.RemoveAll(item => item == Meta2_SDK_define);
                if (!VReePlatform_SDK) customdefines.RemoveAll(item => item == VReePlatform_SDK_define);

                SetSymbolsForAll(string.Join(";", customdefines.ToArray()));

            }
            
            EditorGUILayout.LabelField("Remember to set the appropriate VR SDK in PlayerSettings > Other Settings > Virtual Reality SDKs");

            VREasy_utils.DrawHelperInfo();

            EditorGUILayout.EndScrollView();
        }

        void SetSymbolsForAll(string defines)
        {
            BuildTargetGroup target = (EditorUserBuildSettings.selectedBuildTargetGroup);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            /*PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WSA, defines);*/

        }

    }
}