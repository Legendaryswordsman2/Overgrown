
[System.Serializable]
public class PlantItemSaveData
{
	public string itemID;
	public int defaultHealth = 100;
	public int level = 1;
	public int currentHealth = 0;
	public int damage = 10;
	public int defense = 0;
	public int critChance = 0;

	public int xp = 0;
	public int xpToLevelUp = 100, xpIncreaseOnLevelUp = 100, xpIncreaseIncreaseOnLevelUp = 20;

	public bool isEquipped;

	public PlantItemSaveData(EquipablePlantItem plantItem)
	{
		itemID = plantItem.ID;
		isEquipped = plantItem.isEquipped;

		defaultHealth = plantItem.plantSO.defaultHealth;
		level = plantItem.plantSO.level;
		currentHealth = plantItem.plantSO.currentHealth;
		damage = plantItem.plantSO.damage;
		defense = plantItem.plantSO.defense;
		critChance = plantItem.plantSO.critChance;

		xp = plantItem.plantSO.xp;
		xpToLevelUp = plantItem.plantSO.xpToLevelUp;
		xpIncreaseOnLevelUp = plantItem.plantSO.xpIncreaseOnLevelUp;
		xpIncreaseIncreaseOnLevelUp = plantItem.plantSO.xpIncreaseIncreaseOnLevelUp;
	}
}
