using UnityEngine.EventSystems;

public class UI_Popup_Shop : UI_Popup
{

    #region Enums

    private enum Texts
    {
        GoldText,
        PercentText
    }

    private enum Images
    {
        ShopNpc,
    }

    private enum Buttons
    {
        BackspaceBtn,
    }

    private enum GameObjects
    {
        Content,
    }

    #endregion
    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);
        Refresh();

        return true;
    }
    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = Main.Game.Gold.ToString();
        GetText((int)Texts.PercentText).text = "수집율100퍼";  //$"수집율 : { 해금된스킬갯수 / Main.Data.Skills.Keys * 100f).ToString()}%";
        SetSkillCard();


    }

    private void SetSkillCard()
    {
        foreach (string key in Main.Data.Skills.Keys)
        {
            UI_SkillCard newCard = Main.Resource.InstantiatePrefab("Shop_SkillCard.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_SkillCard>();
            newCard.SetInfo(key);
        }
    }
    public void Close(PointerEventData data)
    {
        Main.UI.ClosePopupUI(this);
    }
}
