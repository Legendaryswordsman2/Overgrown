using UnityEngine;

public abstract class ConsumableItemEffect : ScriptableObject
{
  public abstract void ExecuteEffect(BaseUnit unit);
}
