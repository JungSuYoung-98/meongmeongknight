using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour
{
    public Item item = null;
    public Image Image;
    public Button button;

    public void AddItem(Item Additem , int Index)
    {
        button.interactable = true;
        button.onClick.AddListener(Selectitem);
        item = Additem;
        item.Index = Index;
        Image.sprite = item.Data.Icon;
    }

    void Selectitem()
    {
        UIManager.Instance.InventoryuI.EnhancementButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryuI.EquippedButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryuI.UnEquippedButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryuI.UseButton.gameObject.SetActive(false);
        UIManager.Instance.InventoryuI.DestroyButton.gameObject.SetActive(false);

        UIManager.Instance.InventoryuI.Icon.sprite = item.Data.Icon;
        UIManager.Instance.InventoryuI.Name.text = item.Data.Name;
        if (item.Data.Type == ItemType.Equipment)
        {
            UIManager.Instance.InventoryuI.Contents.text = 
                $"H P : +{item.Data.Hp}\n" +
                $"ATK : +{item.Data.Atk}\n" +
                $"DEF : +{item.Data.Def}\n";

            UIManager.Instance.InventoryuI.EnhancementButton.gameObject.SetActive(true);

            if (!item.equipped) UIManager.Instance.InventoryuI.EquippedButton.gameObject.SetActive(true);
            else UIManager.Instance.InventoryuI.UnEquippedButton.gameObject.SetActive(true);
        }
        else
        {
            UIManager.Instance.InventoryuI.Contents.text =
                $"H P : +{item.Data.AddHp}\n" +
                $"ATK : +{item.Data.AddAtk}\n" +
                $"DEF : +{item.Data.AddDef}\n" +
                $"HOLDING TIME : {item.Data.HoldingTime}";

            UIManager.Instance.InventoryuI.UseButton.gameObject.SetActive(true);

        }

        UIManager.Instance.InventoryuI.DestroyButton.gameObject.SetActive(true);

    }


}
