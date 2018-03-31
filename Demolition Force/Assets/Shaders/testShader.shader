Shader "Custom/testShader" {
	Properties {
		_Color ("Color Tint (A = Opacity)", Color) = (1,1,1,1)
		_Blend1 ("Blend", Range (0, 1)) = 0.0
		_Blend2 ("Blend", Range (0, 1)) = 0.0
		_MainTex1 ("Base (RGB)", 2D) = "red"
		_MainTex2 ("Base (RGB)", 2D) = "blue"
		_MainTex3 ("Base (RGB)", 2D) = "green"
		_Mask ("Culling Mask", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
	}

	SubShader {
		Tags {"Queue"="Transparent"}
		Lighting On
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest GEqual [_Cutoff]
		Pass {

			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
                 
			SetTexture [_Mask] {combine texture}

			SetTexture [_MainTex1]{combine texture, previous}
				     
			SetTexture [_MainTex2] {
				constantColor (0, 0, 0, [_Blend1])
				combine texture lerp (constant) previous 
			}
             
			SetTexture [_MainTex3] {
				constantColor (0, 0, 0, [_Blend2])
				combine texture lerp (constant) previous 
			}
		}
	}
}
