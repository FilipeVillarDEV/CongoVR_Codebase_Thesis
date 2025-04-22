using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Renderer))]
public class FlickeringEmissive : MonoBehaviour
{
    [SerializeField]
    private Material HighlightMaterial;
    [SerializeField]
    private bool Flicker;
    [SerializeField]
    [Min(0)]
    private float FlickerSpeed = 1f;
    [SerializeField]
    private AnimationCurve BrightnessCurve;
    private void Update()
    {
        if (Flicker)
        {
            float scaledTime = Time.time * FlickerSpeed;

            Color color = HighlightMaterial.GetVector("_Color");

            float brightness = BrightnessCurve.Evaluate(scaledTime);

            // same as done by Unity at: https://github.com/Unity-Technologies/UnityCsReference/blob/c84064be69f20dcf21ebe4a7bbc176d48e2f289c/Editor/Mono/GUI/ColorMutator.cs
            color = new Color(
                color.r * Mathf.Pow(2, brightness),
                color.g * Mathf.Pow(2, brightness),
                color.b * Mathf.Pow(2, brightness),
                color.a
            );
            HighlightMaterial.SetVector("_Color", color);
        }
    }
}