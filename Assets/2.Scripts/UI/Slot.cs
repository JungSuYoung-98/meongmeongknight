using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public Item item;
    public GameObject Object;
    public Image Image;
    public Button button;
    public int Index;

    public Slot(Item _item, GameObject _Object, int index)
    {
        item = _item;
        Object = _Object;
        Index =index;
    }

    public void AddItem(GameObject Additem)
    {
        button.interactable = true;
        button.onClick.AddListener(Selectitem);
        Object = Additem;
        Object.SetActive(false);
        item = Additem.GetComponent<Item>();
        Image.sprite = item.Data.Icon;
    }

    public void RemoveItem()
    {
        clear();
        button.interactable = false;
        button.onClick.RemoveListener(Selectitem);
        Destroy(Object);
        item = null;
        Image.sprite = null;
    }

    public void UseingItem()
    {
        clear();
        button.interactable = false;
        button.onClick.RemoveListener(Selectitem);
    }

    public void clear()
    {
        UIManager.Instance.InventoryUI.Icon.sprite = null;
        UIManager.Instance.InventoryUI.Name.text = null;
        UIManager.Instance.InventoryUI.Contents.text = null;
        UIManager.Instance.InventoryUI.EnhancementButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryUI.EquippedButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryUI.UnEquippedButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryUI.UseButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryUI.DestroyButton.gameObject.SetActive(false);
    }

    void Selectitem()
    {
        clear();

        UIManager.Instance.InventoryUI.Icon.sprite = item.Data.Icon;
        UIManager.Instance.InventoryUI.Name.text = item.Data.Name + $" (+{item.Lv})";

        if (item.Data.Type == ItemType.Equipment)
        {
            UIManager.Instance.InventoryUI.Contents.text = 
                $"H P : +{item.Data.Hp}(+{item.ReinforcedHp})\n" +
                $"ATK : +{item.Data.Atk}(+{item.ReinforcedAtk})\n" +
                $"DEF : +{item.Data.Def}(+{item.ReinforcedDef})\n";

            if (!item.equipped)
            {
                UIManager.Instance.InventoryUI.EquippedButton.gameObject.SetActive(true);
                UIManager.Instance.InventoryUI.DestroyButton.gameObject.SetActive(true);
            }
            else
            {
                UIManager.Instance.InventoryUI.UnEquippedButton.gameObject.SetActive(true);
                UIManager.Instance.InventoryUI.EnhancementButton.gameObject.SetActive(true);
            }
        }
        else
        {
            UIManager.Instance.InventoryUI.Contents.text =
                $"H P : +{item.Data.AddHp}\n" +
                $"ATK : +{item.Data.AddAtk}\n" +
                $"DEF : +{item.Data.AddDef}\n" +
                $"HOLDING TIME : {item.Data.HoldingTime}";

            UIManager.Instance.InventoryUI.UseButton.gameObject.SetActive(true);
            UIManager.Instance.InventoryUI.DestroyButton.gameObject.SetActive(true);
        }
        UIManager.Instance.InventoryUI.selectSlot = Index;
    }


}
