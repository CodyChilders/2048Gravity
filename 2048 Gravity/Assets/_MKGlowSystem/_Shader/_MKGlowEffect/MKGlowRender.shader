﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/MKGlowRender"
{
	SubShader 
	{
		Tags { "RenderType"="MKGlow" "Queue"="Transparent"}		
		Pass 
		{
			ZTest LEqual 
			Fog { Mode Off }
			//ColorMask RGB
			Cull Off
			Lighting Off
			ZWrite On
			
			CGPROGRAM
			#pragma target 2.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
					
			sampler2D _MKGlowTex;
			fixed4 _MKGlowColor;
			half _MKGlowPower;
			half _MKGlowTexPower;
			float _MKGlowOffSet;
			
			struct Input
			{
				float2 texcoord : TEXCOORD0;
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct Output 
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			Output vert (Input i)
			{
				Output o;
				i.vertex.xyz += i.normal * _MKGlowOffSet;
				o.pos = UnityObjectToClipPos (i.vertex);
				o.uv = i.texcoord;
				return o;
			}

			fixed4 frag (Output i) : Color
			{
				fixed4 glow = tex2D(_MKGlowTex, i.uv);	
				glow *= (_MKGlowColor * _MKGlowPower);
				//return (glow.a * _MKGlowColor.a) * (_MKGlowPower * _MKGlowColor  * glow);
				return (_MKGlowPower * _MKGlowColor  * glow);
			}
			ENDCG
		}
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		Pass 
		{
			Fog { Mode Off }
			Color (0,0,0,0)
		}
	} 
	SubShader 
	{
		Tags { "RenderType"="Transparent" }
		Pass 
		{
			Fog { Mode Off }
			Color (0,0,0,0)
		}
	} 
} 

