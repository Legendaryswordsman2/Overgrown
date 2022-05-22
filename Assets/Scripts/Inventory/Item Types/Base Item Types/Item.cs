using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(menuName = "Items/Item")]
public abstract class Item : ScriptableObject
{
	[SerializeField, ReadOnlyInspector] string id;
	public string ID { get { return id; } }
	[Header("Item Settings")]
	public string ItemName;

	[TextArea]
	public string ItemDescription = "Some Random Item";

	[Space]

	public Sprite Icon;

	[Space]

 	public bool Sellable = true;
	public int price = 10;
	public int sellPrice = 5;

	[Space]

	[ReadOnlyInspector] public ItemSlot itemSlotReference;

	bool canStack;

	public abstract void ItemSelected(ItemSlot itemSlot);

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
#endif
}
