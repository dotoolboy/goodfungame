using UnityEngine.EventSystems;

public class UI_Popup_Shop : UI_Popup
{

    #region Enums

    private enum Texts
    {
        GoldText,
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
        SetSkillCard();

        Main.Game.OnResourcesChanged += Refresh;

        Refresh();

        return true;
    }
    private void Refresh()
    {
        GetText((int)Texts.GoldText).text = $"소지금 : {Main.Game.Gold}";
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
