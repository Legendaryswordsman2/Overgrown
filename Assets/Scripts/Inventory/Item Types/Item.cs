using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
	[SerializeField, ReadOnlyInspector] string id;
	public string ID { get { return id; } }
	public string ItemName;

	[TextArea]
	public string ItemDescription = "Some Random Item";

	[Space]

	public Sprite Icon;
	[Range(1, 999)]
	public int MaximumStacks = 1;

	protected static readonly StringBuilder sb = new StringBuilder();

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif
}
