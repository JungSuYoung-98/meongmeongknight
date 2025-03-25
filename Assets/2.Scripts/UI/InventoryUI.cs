using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Slot[] slot;
    
    [Header("Stat")]
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI AtkText;
    public TextMeshProUGUI DefText;
    [Header("ItemInfo")]
    public Image Icon;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Contents;
    [Header("ItemButton")]
    public Button DestroyButton;
    [Header("Equipment")]
    public Button EquippedButton;
    public Button UnEquippedButton;
    public Button EnhancementButton;
    [Header("Consumable")]
    public Button UseButton;

    private void Awake()
    {
        TextUpdate();
        slot[1].AddItem(Enemy.Instance.items[6],1);
    }

    private void TextUpdate()
    {
        HpText.text = $"{Player.Instance.maxHp}+({Player.Instance.AddHp})";
        AtkText.text = $"{Player.Instance.Atk}+({Player.Instance.AddAtk})";
        DefText.text = $"{Player.Instance.Def}+({Player.Instance.AddDef})";
    }

    public void equipped()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i == slot[i].item.Index)
            {
                //플레이어 스텟에 더하기
                Player.Instance.maxHp += slot[i].item.Data.Hp;
                Player.Instance.Atk += slot[i].item.Data.Atk;
                Player.Instance.Def += slot[i].item.Data. Def;

                // 추가 공격력에 더하기
                Player.Instance.AddHp += slot[i].item.Data.Hp;
                Player.Instance.AddAtk += slot[i].item.Data.Atk;
                Player.Instance.AddDef += slot[i].item.Data.Def;

                TextUpdate();

                slot[i].item.equipped = true;
                EquippedButton.gameObject.SetActive(false);
                UnEquippedButton.gameObject.SetActive(true);
                break;
            }
        }

    }

    public void Unequipped()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i == slot[i].item.Index)
            {
                //플레이어 스텟에 빼기
                Player.Instance.maxHp -= slot[i].item.Data.Hp;
                Player.Instance.Atk -= slot[i].item.Data.Atk;
                Player.Instance.Def -= slot[i].item.Data.Def;

                // 추가 공격력에 빼기
                Player.Instance.AddHp += slot[i].item.Data.Hp;
                Player.Instance.AddAtk += slot[i].item.Data.Atk;
                Player.Instance.AddDef += slot[i].item.Data.Def;

                TextUpdate();

                slot[i].item.equipped = false;
                EquippedButton.gameObject.SetActive(true);
                UnEquippedButton.gameObject.SetActive(false);
                break;
            }
        }
    }

}
