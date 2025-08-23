using PixelBattleText;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TextAnimation normalHitAnimation;
    public TextAnimation critHitAnimation;

    public void ShowDamage(Vector3 worldPos, int damage, bool isCrit = false)
    {
        // Convert world position to normalize canvas position
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 normalizePos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

        // Choose the appropriate animation based on whether it's a critical hit
        TextAnimation anim = isCrit ? critHitAnimation : normalHitAnimation;

        // Display the damage text
        PixelBattleTextController.DisplayText(damage.ToString(), anim, normalizePos);
    }

}
