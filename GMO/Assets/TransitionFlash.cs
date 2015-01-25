using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(AnimatedAlpha))]
public class TransitionFlash : MonoBehaviour {
	public void Flash()
	{
		var animatedAlpha = this.GetComponent<AnimatedAlpha>();
		animatedAlpha.alpha = 1f;
		DOTween.To(() => animatedAlpha.alpha, (x) => {
			animatedAlpha.alpha = x;
		}, 0f, 1f);
	}
}
