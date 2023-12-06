using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_MountSkillBtn : UI_Base
{

    public int num;
    enum GameObjects
    {
    }

    enum Buttons
    {
        Btn
    }

    enum Images
    {
        IconImage,
        Iconmask,
        Btn
    }

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));


        GetButton((int)Buttons.Btn).gameObject.BindEvent(Out);


        Main.Game.OnEquipChanged -= Refresh;
        Main.Game.OnEquipChanged += Refresh;

        Refresh();

        return true;


    }
    SkillData Data;
    private void Refresh()
    {
        if (Data == null) return;

        if (Main.Game.EquipSkills.Contains(Data.skillStringKey))
        {
            GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"{Data.skillStringKey}.sprite");
            GetImage((int)Images.Btn).raycastTarget = true;
        }
        else
        {
            GetImage((int)Images.IconImage).sprite = null;
            // GetImage((int)Images.IconImage).sprite = Main.Resource.Load<Sprite>($"Slider18_Frame.sprite");
            GetImage((int)Images.Btn).raycastTarget = false;
        }

    }

    void Out(PointerEventData data)
    {
             if (Data == null) return;


         Main.Game.EquipSkills.Remove(Data.skillStringKey);

        Debug.Log("스킬 벗기");
        Refresh();

    }


}
