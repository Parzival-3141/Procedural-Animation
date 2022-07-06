using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// A wrapper for <see cref="GUILayout"/> that creates a Box and draws whatever Labels you assign.
    /// <para>
    /// Can be called from anywhere and handles formatting so you don't
    /// have to worry about overlapping GUIs.
    /// </para>
    /// </summary>
    public class DevGUI : Singleton<DevGUI>
    {
        [System.Serializable]
        private struct DevLabel
        {
            public string text;
            public string context;
            public GUILayoutOption[] options;

            public DevLabel(string text, string context, GUILayoutOption[] options) : this()
            {
                this.text = text;
                this.context = context;
                this.options = options;
            }
        }

        private const string NoContext = "NO_CONTEXT";
        private List<DevLabel> devLabels = new List<DevLabel>();

        private void OnEnable() => StartCoroutine(ClearList());
        private void OnDisable() => StopAllCoroutines();

        IEnumerator ClearList()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                devLabels.Clear();
            }
        }

        /// <summary>
        /// Must be called before '<see cref="MonoBehaviour.OnGUI"/>'!
        /// </summary>
        public static void Label(string text, params GUILayoutOption[] options)
        {
            Label(text, NoContext, options);
        }

        /// <summary>
        /// Must be called before '<see cref="MonoBehaviour.OnGUI"/>'!
        /// </summary>
        public static void Label(string text, string context, params GUILayoutOption[] options)
        {
            if (!Instance.enabled) return;
            Instance.devLabels.Add(new DevLabel(text, context, options));
        }

        private int CompareByContext(DevLabel x, DevLabel y)
        {
            if (x.context == NoContext) return 1;
            else if (y.context == NoContext) return -1;
            else return string.Compare(x.context, y.context);
        }

        private void OnGUI()
        {
            if (!Instance.enabled || devLabels.Count == 0) return;

            string currentContext = NoContext;
            devLabels.Sort(CompareByContext);

            GUILayout.BeginVertical("box");

            for (int i = 0; i < devLabels.Count; i++)
            {
                var dl = devLabels[i];

                if (currentContext != dl.context)
                {
                    if(dl.context == NoContext)
                        GUILayout.Label( "------------------------");
                    else
                        GUILayout.Label($"----- {dl.context} -----");
                }

                GUILayout.Label(dl.text, dl.options);
                
                currentContext = dl.context;
            }

            GUILayout.EndVertical();
        }
    }
}
