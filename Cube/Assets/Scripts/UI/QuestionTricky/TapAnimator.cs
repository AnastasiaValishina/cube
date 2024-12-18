using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TapAnimator : MonoBehaviour
{
    [SerializeField] Image[] images;

    public void Tap()
    {
        foreach (var image in images)
        {
            image.DOFillAmount(1, 0.1f).OnComplete(() =>
            {
                image.fillOrigin = (int)Image.OriginVertical.Top;

                image.DOFillAmount(0, 0.1f).OnComplete(() =>
                {                
                    image.fillOrigin = (int)Image.OriginVertical.Bottom;
                });
            });
        }
    }
}
