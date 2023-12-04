using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // 마우스올렸을때 글씨 띄울놈들한테 달아야할 스크립트.


    public string header;
    public string content;

    public void OnPointerEnter(PointerEventData eventData) //이것도 콜라이더 없으면 인식안하네
    {
        TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
