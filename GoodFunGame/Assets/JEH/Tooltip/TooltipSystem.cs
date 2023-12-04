using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    static TooltipSystem current;

    private Tooltip _tooltip;


    void Awake()
    {
        current = this;
        _tooltip = transform.GetChild(0).GetComponent<Tooltip>();
        _tooltip.gameObject.SetActive(true); // 초기화용
        _tooltip.gameObject.SetActive(false);
    }

    public static void Show(string content, string header)
    {
        if (string.IsNullOrEmpty(header))
            current._tooltip.SetText(content);
        else
            current._tooltip.SetText(content, header);

        current._tooltip.gameObject.SetActive(true);

    }

    public static void Hide()
    {
        current._tooltip.gameObject.SetActive(false);
    }
}