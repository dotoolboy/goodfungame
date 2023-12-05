public class UI_Scene : UI_Base
{
    public override bool Init()
    {
        if (!base.Init()) return false;

        Main.UI.SetCanvas(gameObject, false);

        return true;
    }
}
