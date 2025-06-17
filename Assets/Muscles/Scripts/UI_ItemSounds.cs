using UnityEngine;

public class UI_ItemSounds : MonoBehaviour
{
    public AudioClip _onClickSound;
    public AudioClip _onHoverSound;

    public void OnClick() {
        SoundManager.Instance.PlaySound(_onClickSound);
    }
    public void OnHover() {
        SoundManager.Instance.PlaySound(_onHoverSound);
    }
}
