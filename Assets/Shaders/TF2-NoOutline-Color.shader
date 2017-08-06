 Shader "Custom/TF2-NoOutline/Color"{
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline Width", Range (.002, 0.03)) = .005
        _RampTex ("Shading Ramp", 2D) = "white" {}
    }
   
    SubShader {
        Tags { "RenderType" = "Opaque" }
       
        CGPROGRAM
            #pragma surface surf TF2
            #pragma target 3.0
 
            struct Input
            {
                float2 uv_MainTex;
                float3 worldNormal;
                INTERNAL_DATA
            };
           
            sampler2D _RampTex;
            float4 _RimColor;
            float  _RimPower;
            fixed4 _Color;
 
            inline fixed4 LightingTF2 (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
            {
                fixed3 h = normalize (lightDir + viewDir);
 
                fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
                fixed3 ramp = tex2D(_RampTex, float2(NdotL * atten, 0)).rgb;
 
                float nh = max (0, dot (s.Normal, h));
				float spec = pow(nh, s.Gloss * 128) * s.Specular * saturate(NdotL);

				fixed4 c;
				c.rgb = ((s.Albedo.rgb * ramp * _LightColor0.rgb + _LightColor0.rgb * spec) * (atten * 2));
                return c;
            }
   
            void surf (Input IN, inout SurfaceOutput o)
            {
				o.Albedo = _Color.rgb;
 
                half3 rim = pow(max(0, dot(float3(0, 1, 0), WorldNormalVector (IN, o.Normal))), _RimPower) * _RimColor.rgb * _RimColor.a;
                o.Emission = rim;
            }
   
            ENDCG
      }
    Fallback "Bumped Specular"
}