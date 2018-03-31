Shader "Custom/3Textures" {
     Properties {
        // _Color ("Overall Color", Color) = (1,0.5,0.5,1)
         _t1 ("texture in white", 2D) = "white" {}
         _tint1 ("Tint1", Color) = (1.0, 0.6, 0.6, 1.0)
         
         _t2 ("texture in red", 2D) = "white" {}
         _tint2 ("Tint2", Color) = (1.0, 0.6, 0.6, 1.0)
         _rc ("low cutoff", Range(1,80)) = 0.0 
         _t3 ("texture in blue", 2D) = "white" {}
         _tint3 ("Tint3", Color) = (1.0, 0.6, 0.6, 1.0)
         _rb ("low cutoff", Range(1,80)) = 0.0 
         _t4 ("texture map", 2D) = "white" {}
         
         
     }
     SubShader {
         //Tags { "RenderType"="Opaque" }
         LOD 200
         
         CGPROGRAM
         #pragma surface surf Lambert
         float _rc;
         float _rb;
         sampler2D _t1;
         sampler2D _t2;
         sampler2D _t3;
         sampler2D _t4;
         
         fixed4 _tint1;
         fixed4 _tint2;
         fixed4 _tint3;
         
         struct Input {
             
             float2 uv_t1;
             float2 uv_t2;
             float2 uv_t3;
             float2 uv_t4;
             
         };
       
 
         void surf (Input IN, inout SurfaceOutput o) {
             
             float f;
             
             half4 pix = tex2D (_t1, IN.uv_t1)*_tint1;
             half4 map = tex2D (_t4, IN.uv_t4);
             f=map.r;
             
             f=f*_rc;
             if(f>1){f=1;}
             pix=pix*f;
             f=1-f;
             pix=pix+tex2D (_t2, IN.uv_t2)*f*_tint2;
             
             f=map.b;
             f=f*_rb;
             if(f>1){f=1;}
             
             pix=pix*f;
             f=1-f;
             pix=pix+tex2D (_t3, IN.uv_t3)*f*_tint3;
             
             o.Albedo = pix.rgb;
             
         }
         ENDCG
     } 
     FallBack "Diffuse"
 }