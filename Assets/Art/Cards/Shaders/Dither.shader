Shader "Custom/Dither"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "Dither"

            ZWrite Off
            ZTest Always
            Cull Off

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _Intensity;
            float _DitherSize;
            float _ColorLevels;
            float _Monochrome;

            // 4x4 Bayer matrix
            static const float Bayer4x4[16] =
            {
                 0.0,  8.0,  2.0, 10.0,
                12.0,  4.0, 14.0,  6.0,
                 3.0, 11.0,  1.0,  9.0,
                15.0,  7.0, 13.0,  5.0
            };

            float GetDitherThreshold(float2 pixelPosition)
            {
                float2 p = floor(pixelPosition / _DitherSize);

                int x = ((int)p.x) & 3;
                int y = ((int)p.y) & 3;

                int index = x + y * 4;

                return (Bayer4x4[index] + 0.5) / 16.0;
            }

            float3 ApplyDither(
                float3 color,
                float2 pixelPosition
            )
            {
                float threshold =
                    GetDitherThreshold(pixelPosition);

                float levels = max(_ColorLevels, 2.0);

                return floor(
                    color * levels + threshold
                ) / levels;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                // Blit.hlsl provides the source texture and sampler.
                float4 source =
                    SAMPLE_TEXTURE2D_X(
                        _BlitTexture,
                        sampler_LinearClamp,
                        input.texcoord
                    );

                float2 pixelPosition =
                    input.texcoord * _ScreenParams.xy;

                float3 dithered =
                    ApplyDither(
                        source.rgb,
                        pixelPosition
                    );

                if (_Monochrome > 0.5)
                {
                    float luminance =
                        dot(
                            source.rgb,
                            float3(
                                0.2126,
                                0.7152,
                                0.0722
                            )
                        );

                    dithered =
                        ApplyDither(
                            luminance.xxx,
                            pixelPosition
                        );
                }

                float3 finalColor =
                    lerp(
                        source.rgb,
                        dithered,
                        _Intensity
                    );

                return half4(
                    finalColor,
                    source.a
                );
            }

            ENDHLSL
        }
    }
}