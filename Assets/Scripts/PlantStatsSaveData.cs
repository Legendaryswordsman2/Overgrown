[System.Serializable]
public class PlantStatsSaveData
{
	// Stats
	public int maxHealth = 100;
	public int currentHealth = 0;
	public int defense = 0;
	public int damage = 10;
	public int critChance = 0;

	public PlantStatsSaveData(PlantStats plantStats)
	{
		maxHealth = plantStats.maxHealth;
		currentHealth = plantStats.currentHealth;
		defense = plantStats.defense;
		damage = plantStats.damage;
		critChance = plantStats.critChance;
	}
}
