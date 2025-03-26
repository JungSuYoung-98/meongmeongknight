using UnityEngine;

public enum ItemType 
{
    Equipment,
    Consumable
}

public enum EquipmentType
{
    None,
    Weapon,
    Armor,
    Accessories
}


[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public Sprite Icon;
    public GameObject GameObjet;
    public string Name;

    [Header("Equipment")]
    public EquipmentType EquipmentType;
    public float Hp;
    public float Atk;
    public float Def;  

    [Header("Consumable")]
    public float AddHp;
    public float AddAtk;
    public float AddDef;
    public float HoldingTime;
}
