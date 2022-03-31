using System;

using UnityEditor;
using UnityEngine;

using UniqueIdentifier;

namespace UniqueIdentifier.Editor
{
    using static EditorGUI;
    using static EditorGUIUtility;

    [CustomPropertyDrawer(typeof(UID))]
    public class UIDPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            LabelField(rect, label.text, ToString(property));

            if (null != Event.current && EventType.ContextClick == Event.current.type)
            {
                if (rect.Contains(Event.current.mousePosition))
                {
                    UID uid = ToUID(property);

                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("New UID"), false, OnNewUID, property);
                    if (UID.Empty != uid)
                        menu.AddItem(new GUIContent("Copy UID"), false, OnCopyUID, uid);
                    else
                        menu.AddDisabledItem(new GUIContent("Copy UID"));

                    if (UID.TryParse(systemCopyBuffer, out UID clipboard))
                    {
                        if (clipboard != uid)
                        {
                            menu.AddItem(new GUIContent("Paste UID"), false, OnPasteUID, property);
                        }
                        else
                        {
                            menu.AddDisabledItem(new GUIContent("Paste UID"));
                        }
                    }
                    else
                    {
                        menu.AddDisabledItem(new GUIContent("Paste UID"));
                    }

                    if (UID.Empty == uid)
                        menu.AddDisabledItem(new GUIContent("Empty UID"));
                    else
                        menu.AddItem(new GUIContent("Empty UID"), false, OnEmptyUID, property);
                    menu.ShowAsContext();
                }
            }
        }

        static void OnEmptyUID(object userData) { Assign(userData as SerializedProperty, UID.Empty); }
        static void OnNewUID(object userData) { Assign(userData as SerializedProperty, UID.NewUID()); }
        static void OnCopyUID(object userData) { systemCopyBuffer = ((UID)userData).ToString(); }
        static void OnPasteUID(object userData)
        {
            if (UID.TryParse(systemCopyBuffer, out UID clipboard))
            {
                Assign(userData as SerializedProperty, clipboard);
            }
        }

        static void Assign(SerializedProperty property, UID uid)
        {
            property.FindPropertyRelative(nameof(UID._a)).intValue = uid._a;
            property.FindPropertyRelative(nameof(UID._b)).intValue = uid._b;
            property.FindPropertyRelative(nameof(UID._c)).intValue = uid._c;
            property.FindPropertyRelative(nameof(UID._d)).intValue = uid._d;
            property.FindPropertyRelative(nameof(UID._e)).intValue = uid._e;
            property.FindPropertyRelative(nameof(UID._f)).intValue = uid._f;
            property.FindPropertyRelative(nameof(UID._g)).intValue = uid._g;
            property.FindPropertyRelative(nameof(UID._h)).intValue = uid._h;
            property.FindPropertyRelative(nameof(UID._i)).intValue = uid._i;
            property.FindPropertyRelative(nameof(UID._j)).intValue = uid._j;
            property.FindPropertyRelative(nameof(UID._k)).intValue = uid._k;

            property.serializedObject.ApplyModifiedProperties();
        }

        static UID ToUID(SerializedProperty property)
        {
            return new UID(property.FindPropertyRelative(nameof(UID._a)).intValue,
                           (short)property.FindPropertyRelative(nameof(UID._b)).intValue,
                           (short)property.FindPropertyRelative(nameof(UID._c)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._d)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._e)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._f)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._g)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._h)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._i)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._j)).intValue,
                           (byte)property.FindPropertyRelative(nameof(UID._k)).intValue);
        }

        static string ToString(SerializedProperty property)
        {
            return UID.ToString(property.FindPropertyRelative(nameof(UID._a)).intValue,
                                (short)property.FindPropertyRelative(nameof(UID._b)).intValue,
                                (short)property.FindPropertyRelative(nameof(UID._c)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._d)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._e)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._f)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._g)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._h)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._i)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._j)).intValue,
                                (byte)property.FindPropertyRelative(nameof(UID._k)).intValue);
        }
    }
}
