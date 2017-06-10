using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.UI;

namespace Dust.Utils {
	[CustomEditor (typeof (FloorRandomizer))]
	public class FloorRandomizerEditor : Editor
	{
		private SerializedProperty sprites;
		private ReorderableList reorderableList;

		private void OnEnable ()
		{
			sprites = serializedObject.FindProperty ("sprites");
			reorderableList = new ReorderableList (serializedObject, sprites);
			reorderableList.drawElementCallback = DrawElementCallback;
			reorderableList.drawHeaderCallback = DrawHeaderCallback;
		}

		private void DrawHeaderCallback (Rect position)
		{
			EditorGUI.LabelField (position, "Sprites");
		}

		private void DrawElementCallback (Rect position, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = sprites.GetArrayElementAtIndex (index);
			position.y += 2;
			position.height -= 5;
			EditorGUI.PropertyField (position, element, GUIContent.none); 
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update ();
			reorderableList.DoLayoutList ();

			Rect position = EditorGUILayout.GetControlRect ();
			position.width = 80;
			if (GUI.Button (position, "Rebuild")) {
				if (sprites.arraySize == 0)
					throw new System.InvalidOperationException ("Specify at least one sprite");

				FloorRandomizer floorRandomizer = serializedObject.targetObject as FloorRandomizer;
				Image[] images = floorRandomizer.GetComponentsInChildren<Image> ();
				foreach (var image in images) {
					Sprite sprite = sprites.GetArrayElementAtIndex (
						Random.Range (0, sprites.arraySize)).objectReferenceValue as Sprite;

					SerializedObject serializedObject = new SerializedObject (image);
					SerializedProperty serializedProperty = serializedObject.FindProperty ("m_Sprite");
					serializedProperty.objectReferenceValue = sprite;
					serializedObject.ApplyModifiedProperties ();
				}
			}
				

			serializedObject.ApplyModifiedProperties ();
		}
	}
}