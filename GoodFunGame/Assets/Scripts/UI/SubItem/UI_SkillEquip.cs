using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillEquip : UI_Base
{

    public SkillData Data { get; private set; }
    enum GameObjects
    {
    }

    enum Buttons
    {
    }

    enum Images
    {
        IconImage
    }


    public override bool Init()
    {
        if (!base.Init()) return false;

        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));


        //    GetButton((int)Buttons.BackspaceBtn).gameObject.BindEvent(Close);

        return true;
    }
    public void SetInfo(string key)
    {
        Data = Main.Data.Skills[key];
        Refresh();
    }

    public void Refresh()
    {
        if (Data == null) return;
        Init();

        //  GetText((int)Texts.Name).text = Data.skillStringKey;
        //   GetText((int)Texts.Introduce).text = Data.skillDesc;
        //   GetText((int)Texts.Price).text = $"{Data.skillPrice} Gold";
          GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"{Data.skillStringKey}.sprite");



        // 팝업창에뜰 설명, 스킬이름, 아이콘, 장착여부
    }

}
