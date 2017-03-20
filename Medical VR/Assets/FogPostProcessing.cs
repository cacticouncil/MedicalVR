using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("")]
    public class FogPostProcessing : MonoBehaviour
    {
        /// Provides a shader property that is set in the inspector
        /// and a material instantiated from the shader
        public Shader shader;

        private Material m_Material;

        protected virtual void Start()
        {
            // Disable if we don't support image effects
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }

            // Disable the image effect if the shader can't
            // run on the users graphics card
            if (!shader || !shader.isSupported)
                enabled = false;

            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        }

        void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
        {
            Graphics.Blit(sourceTexture, destTexture, m_Material);//, -1);
        }

        protected virtual void OnDisable()
        {
            if (m_Material)
            {
                DestroyImmediate(m_Material);
            }
        }
        protected virtual void OnEnable()
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
        }
    }
}

