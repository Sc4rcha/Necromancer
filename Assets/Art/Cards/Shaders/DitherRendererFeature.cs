using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

public class DitherRendererFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material;

        public RenderPassEvent injectionPoint =
            RenderPassEvent.AfterRenderingPostProcessing;
    }

    public Settings settings = new Settings();

    private DitherRenderPass renderPass;

    public override void Create()
    {
        renderPass = new DitherRenderPass(settings);
        renderPass.renderPassEvent = settings.injectionPoint;
    }

    public override void AddRenderPasses(
        ScriptableRenderer renderer,
        ref RenderingData renderingData)
    {
        if (settings.material == null)
            return;

        renderer.EnqueuePass(renderPass);
    }

    protected override void Dispose(bool disposing)
    {
        renderPass?.Dispose();
    }

    private class DitherRenderPass : ScriptableRenderPass
    {
        private Settings settings;

        private static readonly int IntensityID =
            Shader.PropertyToID("_Intensity");

        private static readonly int DitherSizeID =
            Shader.PropertyToID("_DitherSize");

        private static readonly int ColorLevelsID =
            Shader.PropertyToID("_ColorLevels");

        private static readonly int MonochromeID =
            Shader.PropertyToID("_Monochrome");

        public DitherRenderPass(Settings settings)
        {
            this.settings = settings;

            profilingSampler =
                new ProfilingSampler("Dither");
        }

        public override void RecordRenderGraph(
            RenderGraph renderGraph,
            ContextContainer frameData)
        {
            if (settings.material == null)
                return;

            // Get the current Volume stack.
            DitherPostprocessing volume =
                VolumeManager.instance.stack.GetComponent<DitherPostprocessing>();

            if (volume == null || !volume.IsActive())
                return;

            // Send Volume values to the shader.
            settings.material.SetFloat(
                IntensityID,
                volume.intensity.value
            );

            settings.material.SetFloat(
                DitherSizeID,
                volume.ditherSize.value
            );

            settings.material.SetFloat(
                ColorLevelsID,
                volume.colorLevels.value
            );

            settings.material.SetFloat(
                MonochromeID,
                volume.monochrome.value ? 1f : 0f
            );

            // Camera data.
            UniversalCameraData cameraData =
                frameData.Get<UniversalCameraData>();

            // Camera color.
            UniversalResourceData resourceData =
                frameData.Get<UniversalResourceData>();

            TextureHandle source =
                resourceData.activeColorTexture;

            if (!source.IsValid())
                return;

            // Create destination texture.
            TextureDesc destinationDesc =
                renderGraph.GetTextureDesc(source);

            destinationDesc.name = "Dither Destination";

            TextureHandle destination =
                renderGraph.CreateTexture(destinationDesc);

            // Fullscreen blit.
            RenderGraphUtils.BlitMaterialParameters blitParameters =
                new RenderGraphUtils.BlitMaterialParameters(
                    source,
                    destination,
                    settings.material,
                    0
                );

            renderGraph.AddBlitPass(
                blitParameters,
                "Dither"
            );

            // Make the destination the active camera texture.
            resourceData.cameraColor =
                destination;
        }

        public void Dispose()
        {
        }
    }
}
