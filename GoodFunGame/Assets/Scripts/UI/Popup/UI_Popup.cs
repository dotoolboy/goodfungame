public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Main.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()  // 팝업이니까 고정 캔버스(Scene)과 다르게 닫는게 필요
    {
        Main.UI.ClosePopupUI(this);
    }
}
