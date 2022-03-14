using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(menuName = "Items/Item")]
public abstract class Item : ScriptableObject
{
	[Header("Item Settings")]
	[SerializeField, ReadOnlyInspector] string id;
	public string ID { get { return id; } }
	public string ItemName;

	[TextArea]
	public string ItemDescription = "Some Random Item";

	[Space]

	public Sprite Icon;

	bool canStack;
	[Range(1, 99)]
	public int MaximumStacks = 1;

	public abstract void ItemSelected(ItemSlot itemSlot);

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif


//	#region Editor

//#if UNITY_EDITOR
//	[CustomEditor(typeof(Item))]
//	public class ItemEditor : Editor
//	{
//		public override void OnInspectorGUI()
//		{
//			base.OnInspectorGUI();

//			Item item = (Item)target;

//			EditorGUILayout.Space();
//			EditorGUILayout.LabelField("Details");
//		}
//	}
//#endif
//	#endregion
}
