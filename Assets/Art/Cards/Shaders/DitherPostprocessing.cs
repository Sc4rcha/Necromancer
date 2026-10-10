using UnityEngine.Rendering;

[System.Serializable]
[VolumeComponentMenu("Post-processing/Dither")]
public sealed class DitherPostprocessing : VolumeComponent
{
    public ClampedFloatParameter intensity =
        new ClampedFloatParameter(1f, 0f, 1f);

    public ClampedFloatParameter ditherSize =
        new ClampedFloatParameter(1f, 1f, 8f);

    public ClampedIntParameter colorLevels =
        new ClampedIntParameter(4, 2, 32);

    public BoolParameter monochrome =
        new BoolParameter(false);

    public bool IsActive()
    {
        return intensity.value > 0f;
    }
}