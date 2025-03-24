using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    public TextMeshProUGUI HpText;
    public Image HpImage;
    public TextMeshProUGUI ExpText;
    public Image ExpImage;
    public TextMeshProUGUI LvText;
    public TextMeshProUGUI StageInfoText;
    public TextMeshProUGUI GoldText;

    private void Update() //UI °»½Å
    {
        HpText.text = $"{Player.Instance.curHp} / {Player.Instance.maxHp}";
        ExpText.text = $"{Player.Instance.curExp} / {Player.Instance.Exp}";
        LvText.text = $"LV : {Player.Instance.Lv}";
        StageInfoText.text = $"STAGE : {StageManager.Instance.StageNum}-{StageManager.Instance.ChapterNum}";
        GoldText.text = $"Gold : {Player.Instance.Gold}";

        HpImage.fillAmount = Player.Instance.curHp / Player.Instance.maxHp;
        ExpImage.fillAmount = Player.Instance.curExp  /  Player.Instance.Exp;
    }
}
