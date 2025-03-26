using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Timeline.Actions.MenuPriority;
using System.Collections;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public List<Slot> slot;

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

    public int selectSlot;
    GameObject BasicWeapon;

    private void Awake()
    {
        BasicWeapon = Player.Instance.WeaponPivot.GetComponentInChildren<Item>().gameObject;
        StatUpdate();
    }

    private void StatUpdate() 
    {
        HpText.text = $"{Player.Instance.maxHp}+({Player.Instance.AddHp})";
        AtkText.text = $"{Player.Instance.Atk}+({Player.Instance.AddAtk})";
        DefText.text = $"{Player.Instance.Def}+({Player.Instance.AddDef})";
    }

    public void equippedItem() // 장착버튼
    {
        for (int i = 0; i < slot.Count; i++) // 중복장비 검사
        {
            if (slot[i].item == null) continue;
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Weapon) UnequippedItem(i);
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Armor) UnequippedItem(i);
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Accessories) UnequippedItem(i);
        }

        //플레이어 스텟에 더하기
        Player.Instance.maxHp += slot[selectSlot].item.Data.Hp + slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk += slot[selectSlot].item.Data.Atk + slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def += slot[selectSlot].item.Data.Def + slot[selectSlot].item.ReinforcedDef;

        // 추가 스텟에 더하기
        Player.Instance.AddHp += slot[selectSlot].item.Data.Hp + slot[selectSlot].item.ReinforcedHp;
        Player.Instance.AddAtk += slot[selectSlot].item.Data.Atk + slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.AddDef += slot[selectSlot].item.Data.Def + slot[selectSlot].item.ReinforcedDef;

        StatUpdate();

        slot[selectSlot].item.gameObject.SetActive(true);
        BasicWeapon.SetActive(false); // 기본무기 비활성화

        slot[selectSlot].item.equipped = true;
        EquippedButton.gameObject.SetActive(false);
        DestroyButton.gameObject.SetActive(false);
        EnhancementButton.gameObject.SetActive(true);
        UnEquippedButton.gameObject.SetActive(true);

    }

    public void UnequippedItem() // 장착해제버튼에서 아이템 장착 해제 
    {
        //플레이어 스텟에 빼기
        Player.Instance.maxHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        // 추가 스텟에 더하기
        Player.Instance.AddHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.AddAtk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.AddDef -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        StatUpdate();
        slot[selectSlot].item.gameObject.SetActive(false);
        BasicWeapon.SetActive(true); //기본무기 활성화

        slot[selectSlot].item.equipped = false;
        EquippedButton.gameObject.SetActive(true);
        DestroyButton.gameObject.SetActive(true);
        EnhancementButton.gameObject.SetActive(false);
        UnEquippedButton.gameObject.SetActive(false);
    }
    public void UnequippedItem(int i) // 장착버튼에서 중복장비아이템 장착 해제
    {
        //플레이어 스텟에 빼기
        Player.Instance.maxHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        // 추가 스텟에 더하기
        Player.Instance.AddHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.AddAtk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.AddDef -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        StatUpdate();
        slot[i].item.gameObject.SetActive(false);

        slot[i].item.equipped = false;
        EquippedButton.gameObject.SetActive(true);
        DestroyButton.gameObject.SetActive(true);
        EnhancementButton.gameObject.SetActive(false);
        UnEquippedButton.gameObject.SetActive(false);
    }

    public void DestroyItem() // 삭제 버튼
    {
        slot[selectSlot].RemoveItem();
    }

    public void DestroyItem(int i) // 사용아이템 삭제
    {
        slot[i].RemoveItem();
    }

    public void UseItem() // 소모품 사용 버튼
    {
        Player.Instance.maxHp += slot[selectSlot].item.Data.AddHp;
        Player.Instance.Atk += slot[selectSlot].item.Data.AddAtk;
        Player.Instance.Def += slot[selectSlot].item.Data.AddDef;
        Player.Instance.curHp += slot[selectSlot].item.Data.AddHp;
        UIManager.Instance.baseUI.BaseUIUpdate();
        StatUpdate();
        if (slot[selectSlot].item.Data.HoldingTime == 0) // 유지시간이 0이면 코루틴 타지않고 스텟증가
        {
            DestroyItem();
            return; 
        }
        StartCoroutine(UseItemCoroutine(selectSlot));

    }

    public IEnumerator UseItemCoroutine(int i) // 소모품 사용 코루틴
    {
        DestroyButton.gameObject.SetActive(false);
        UseButton. gameObject.SetActive(false);
        Contents.text = "Useing Item";
        Player.Instance.AddHp += slot[selectSlot].item.Data.AddHp;
        Player.Instance.AddAtk += slot[selectSlot].item.Data.AddAtk;
        Player.Instance.AddDef += slot[selectSlot].item.Data.AddDef;
        StatUpdate();
        slot[selectSlot].UseingItem();

        yield return new WaitForSecondsRealtime(3f);

        Player.Instance.maxHp -= slot[selectSlot].item.Data.AddHp;
        Player.Instance.Atk -= slot[selectSlot].item.Data.AddAtk;
        Player.Instance.Def -= slot[selectSlot].item.Data.AddDef;

        Player.Instance.AddHp -= slot[selectSlot].item.Data.AddHp;
        Player.Instance.AddAtk -= slot[selectSlot].item.Data.AddAtk;
        Player.Instance.AddDef -= slot[selectSlot].item.Data.AddDef;
        UIManager.Instance.baseUI.BaseUIUpdate();
        StatUpdate();
        DestroyItem();
    }

    public void EnhancementItem()
    {
        if(Player.Instance.Gold >= slot[selectSlot].item.Lv * 1000)
        {
            Player.Instance.Gold -= slot[selectSlot].item.Lv * 1000;
            slot[selectSlot].item.Lv++;
            Name.text = $"{slot[selectSlot].item.Data.Name} (+{slot[selectSlot].item.Lv})";

            UnequippedItem();

            if (slot[selectSlot].item.Data.EquipmentType == EquipmentType.Weapon)
            {
                slot[selectSlot].item.ReinforcedAtk += 3;
            }
            else if (slot[selectSlot].item.Data.EquipmentType == EquipmentType.Armor) 
            {
                slot[selectSlot].item.ReinforcedAtk += 3;
            }
            else if (slot[selectSlot].item.Data.EquipmentType == EquipmentType.Accessories)
            {
                slot[selectSlot].item.ReinforcedAtk += 10;
            }

            Contents.text =
                $"H P : +{slot[selectSlot].item.Data.Hp}(+{slot[selectSlot].item.ReinforcedHp})\n" +
                $"ATK : +{slot[selectSlot].item.Data.Atk}(+{slot[selectSlot].item.ReinforcedAtk})\n" +
                $"DEF : +{slot[selectSlot].item.Data.Def}(+{slot[selectSlot].item.ReinforcedDef})\n";

            equippedItem();
        }
        else
        {
            Contents.text = "Enhancement failed.\nYou do not have enough gold.";
            Clear();
        }

    }

    public void Clear()
    {
        StartCoroutine(ClearCoroutine());
    }

    public IEnumerator ClearCoroutine()
    {
        yield return new WaitForSecondsRealtime (3f);

        Contents.text =
                $"H P : +{slot[selectSlot].item.Data.Hp}(+{slot[selectSlot].item.ReinforcedHp})\n" +
                $"ATK : +{slot[selectSlot].item.Data.Atk}(+{slot[selectSlot].item.ReinforcedAtk})\n" +
                $"DEF : +{slot[selectSlot].item.Data.Def}(+{slot[selectSlot].item.ReinforcedDef})\n";
    }
}
