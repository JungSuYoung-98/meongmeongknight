using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI HpText;
    public Image HpImage;

    public void EnemyUpdate()
    {
        if (Enemy.Instance == null) return;
        Name.text = Enemy.Instance.name;
        HpText.text = $"{Enemy.Instance.curHp} / {Enemy.Instance.maxHp}";
        HpImage.fillAmount = Enemy.Instance.curHp / Enemy.Instance.maxHp;
    }
}
