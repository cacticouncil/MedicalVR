Shader "GUI/3D Text Shader - Overlay" {
	Properties{
		_MainTex("Font Texture", 2D) = "white" {}
		_Color("Text Color", Color) = (1,1,1,1)
	}

		SubShader{
		Tags{ "Queue" = "Overlay+1000" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Lighting Off Cull Back ZWrite On Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass{
		Color[_Color]
		SetTexture[_MainTex]{
		combine primary, texture * primary
	}
	}
	}
}