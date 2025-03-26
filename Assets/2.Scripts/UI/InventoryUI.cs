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

    public void equippedItem() // ������ư
    {
        for (int i = 0; i < slot.Count; i++) // �ߺ���� �˻�
        {
            if (slot[i].item == null) continue;
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Weapon) UnequippedItem(i);
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Armor) UnequippedItem(i);
            else if (selectSlot != slot[i].Index && slot[i].item.equipped && slot[selectSlot].item.Data.EquipmentType == EquipmentType.Accessories) UnequippedItem(i);
        }

        //�÷��̾� ���ݿ� ���ϱ�
        Player.Instance.maxHp += slot[selectSlot].item.Data.Hp + slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk += slot[selectSlot].item.Data.Atk + slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def += slot[selectSlot].item.Data.Def + slot[selectSlot].item.ReinforcedDef;

        // �߰� ���ݿ� ���ϱ�
        Player.Instance.AddHp += slot[selectSlot].item.Data.Hp + slot[selectSlot].item.ReinforcedHp;
        Player.Instance.AddAtk += slot[selectSlot].item.Data.Atk + slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.AddDef += slot[selectSlot].item.Data.Def + slot[selectSlot].item.ReinforcedDef;

        StatUpdate();

        slot[selectSlot].item.gameObject.SetActive(true);
        BasicWeapon.SetActive(false); // �⺻���� ��Ȱ��ȭ

        slot[selectSlot].item.equipped = true;
        EquippedButton.gameObject.SetActive(false);
        DestroyButton.gameObject.SetActive(false);
        EnhancementButton.gameObject.SetActive(true);
        UnEquippedButton.gameObject.SetActive(true);

    }

    public void UnequippedItem() // ����������ư���� ������ ���� ���� 
    {
        //�÷��̾� ���ݿ� ����
        Player.Instance.maxHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        // �߰� ���ݿ� ���ϱ�
        Player.Instance.AddHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.AddAtk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.AddDef -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        StatUpdate();
        slot[selectSlot].item.gameObject.SetActive(false);
        BasicWeapon.SetActive(true); //�⺻���� Ȱ��ȭ

        slot[selectSlot].item.equipped = false;
        EquippedButton.gameObject.SetActive(true);
        DestroyButton.gameObject.SetActive(true);
        EnhancementButton.gameObject.SetActive(false);
        UnEquippedButton.gameObject.SetActive(false);
    }
    public void UnequippedItem(int i) // ������ư���� �ߺ��������� ���� ����
    {
        //�÷��̾� ���ݿ� ����
        Player.Instance.maxHp -= slot[selectSlot].item.Data.Hp - slot[selectSlot].item.ReinforcedHp;
        Player.Instance.Atk -= slot[selectSlot].item.Data.Atk - slot[selectSlot].item.ReinforcedAtk;
        Player.Instance.Def -= slot[selectSlot].item.Data.Def - slot[selectSlot].item.ReinforcedDef;

        // �߰� ���ݿ� ���ϱ�
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

    public void DestroyItem() // ���� ��ư
    {
        slot[selectSlot].RemoveItem();
    }

    public void DestroyItem(int i) // �������� ����
    {
        slot[i].RemoveItem();
    }

    public void UseItem() // �Ҹ�ǰ ��� ��ư
    {
        Player.Instance.maxHp += slot[selectSlot].item.Data.AddHp;
        Player.Instance.Atk += slot[selectSlot].item.Data.AddAtk;
        Player.Instance.Def += slot[selectSlot].item.Data.AddDef;
        Player.Instance.curHp += slot[selectSlot].item.Data.AddHp;
        UIManager.Instance.baseUI.BaseUIUpdate();
        StatUpdate();
        if (slot[selectSlot].item.Data.HoldingTime == 0) // �����ð��� 0�̸� �ڷ�ƾ Ÿ���ʰ� ��������
        {
            DestroyItem();
            return; 
        }
        StartCoroutine(UseItemCoroutine(selectSlot));

    }

    public IEnumerator UseItemCoroutine(int i) // �Ҹ�ǰ ��� �ڷ�ƾ
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
