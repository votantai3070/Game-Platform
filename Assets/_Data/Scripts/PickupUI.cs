using TMPro;
using DG.Tweening;
using UnityEngine;

public class PickupUI : MonoBehaviour
{
    public static PickupUI Instance { get; private set; }
    public TextMeshProUGUI pickupText;
    public CanvasGroup pickupGroup;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pickupGroup.alpha = 0f;
    }

    public void ShowPickupMessage(string message)
    {
        pickupText.text = message;
        pickupGroup.DOFade(1f, 0.3f);
    }

    public void HidePickupMessage()
    {
        pickupGroup.DOFade(0f, 0.3f).OnComplete(() => pickupText.text = "");
    }

}
