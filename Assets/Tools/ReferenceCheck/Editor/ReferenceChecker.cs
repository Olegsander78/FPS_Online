#if UNITY_EDITOR
using System;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameObjectDebug.UnityEditor
{
    internal static class ReferenceChecker
    {
        [MenuItem("GameObject/Check References", false, 0)]
        private static void CheckReferences(MenuCommand menuCommand)
        {
            var targetObject = Selection.activeGameObject;
            if (targetObject != null)
            {
                CheckReferences(targetObject);
            }
        }

        internal static void CheckReferences(GameObject target)
        {
            var monoBehaviours = target.GetComponentsInChildren<MonoBehaviour>(true);
            var wasError = false;

            foreach (var monoBehaviour in monoBehaviours)
            {
                CheckReferences(monoBehaviour, ref wasError);
            }

            if (!wasError)
            {
                PrintSuccess(target);
            }
        }

        private static void CheckReferences(MonoBehaviour monoBehaviour, ref bool wasError)
        {
            if (monoBehaviour == null)
            {
                return;
            }
            
            var type = monoBehaviour.GetType();
            if (type.Assembly.GetName().Name != "Assembly-CSharp")
            {
                return;
            }

            while (type != typeof(MonoBehaviour))
            {
                CheckReferences(type, monoBehaviour, ref wasError);
                type = type.BaseType;
            }
        }

        private static void CheckReferences(Type type, MonoBehaviour target, ref bool wasError)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fieldInfo in fields)
            {
                if (!fieldInfo.IsDefined(typeof(SerializeField), inherit: true))
                {
                    continue;
                }

                if (fieldInfo.FieldType == typeof(string))
                {
                    continue;
                }

                if (fieldInfo.IsDefined(typeof(OptionalFieldAttribute)))
                {
                    continue;
                }

                var value = fieldInfo.GetValue(target);
                if (value is Object unityObject && unityObject == null)
                {
                    wasError = true;
                    PrintError(target, fieldInfo);
                }
                else if (value == null)
                {
                    wasError = true;
                    PrintError(target, fieldInfo);
                }
            }
        }

        private static void PrintError(MonoBehaviour monoBehaviour, FieldInfo fieldInfo)
        {
            var objectName = monoBehaviour.name;
            var componentName = monoBehaviour.GetType().Name;
            var propertyName = fieldInfo.Name;
            var message = $"<color=#F62B2F>Failed game object: {objectName}! Component: {componentName} Property: {propertyName}</color>";
            Debug.LogError(message, monoBehaviour.gameObject);
        }

        private static void PrintSuccess(GameObject target)
        {
            Debug.Log($"<color=#40A1C7>Success game object: {target.name}! All references installed!</color>");
        }
    }
}
#endif