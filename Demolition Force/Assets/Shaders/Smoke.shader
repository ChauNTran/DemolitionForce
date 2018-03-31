// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-3202-OUT,alpha-7424-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31501,y:32503,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bbb367c4e56a3f44d88d43b9759ca3b9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3317,x:30983,y:33019,ptovrint:False,ptlb:node_3317,ptin:_node_3317,varname:node_3317,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-5456-UVOUT;n:type:ShaderForge.SFN_Panner,id:5456,x:30834,y:33019,varname:node_5456,prsc:2,spu:0.0511,spv:0.01237|UVIN-6948-OUT;n:type:ShaderForge.SFN_Tex2d,id:4773,x:30983,y:33240,ptovrint:False,ptlb:node_3317_copy,ptin:_node_3317_copy,varname:_node_3317_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-6292-UVOUT;n:type:ShaderForge.SFN_Panner,id:6292,x:30834,y:33240,varname:node_6292,prsc:2,spu:0.0234,spv:0.0923|UVIN-6095-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:544,x:30219,y:33237,varname:node_544,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_UVTile,id:6095,x:30398,y:33237,varname:node_6095,prsc:2|UVIN-544-UVOUT,WDT-1521-OUT,HGT-1521-OUT,TILE-1521-OUT;n:type:ShaderForge.SFN_Vector1,id:1521,x:30219,y:33396,varname:node_1521,prsc:2,v1:1.12;n:type:ShaderForge.SFN_Multiply,id:865,x:31545,y:33120,varname:node_865,prsc:2|A-2592-OUT,B-7635-OUT,C-9103-OUT,D-4074-OUT;n:type:ShaderForge.SFN_Vector1,id:4074,x:31440,y:33306,varname:node_4074,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:8023,x:30983,y:33462,ptovrint:False,ptlb:node_3317_copy_copy,ptin:_node_3317_copy_copy,varname:_node_3317_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-9271-UVOUT;n:type:ShaderForge.SFN_Panner,id:9271,x:30834,y:33462,varname:node_9271,prsc:2,spu:-0.0234,spv:-0.04562|UVIN-1759-OUT;n:type:ShaderForge.SFN_TexCoord,id:2153,x:30219,y:33459,varname:node_2153,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_UVTile,id:1667,x:30398,y:33459,varname:node_1667,prsc:2|UVIN-2153-UVOUT,WDT-1273-OUT,HGT-1273-OUT,TILE-1273-OUT;n:type:ShaderForge.SFN_Vector1,id:1273,x:30219,y:33618,varname:node_1273,prsc:2,v1:1.89;n:type:ShaderForge.SFN_Clamp01,id:9592,x:31704,y:33120,varname:node_9592,prsc:2|IN-865-OUT;n:type:ShaderForge.SFN_Multiply,id:3808,x:32188,y:32877,varname:node_3808,prsc:2|A-1626-A,B-9614-OUT,C-5312-A;n:type:ShaderForge.SFN_VertexColor,id:1626,x:30285,y:32725,varname:node_1626,prsc:2;n:type:ShaderForge.SFN_Color,id:5312,x:31501,y:32710,ptovrint:False,ptlb:node_5312,ptin:_node_5312,varname:node_5312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:328,x:30219,y:33015,varname:node_328,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_UVTile,id:3539,x:30398,y:33015,varname:node_3539,prsc:2|UVIN-328-UVOUT,WDT-6108-OUT,HGT-6108-OUT,TILE-6108-OUT;n:type:ShaderForge.SFN_Vector1,id:6108,x:30219,y:33174,varname:node_6108,prsc:2,v1:2.3;n:type:ShaderForge.SFN_Multiply,id:2592,x:31315,y:33045,varname:node_2592,prsc:2|A-3317-R,B-3317-R,C-8049-OUT;n:type:ShaderForge.SFN_Vector1,id:8049,x:31096,y:33179,varname:node_8049,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:7635,x:31315,y:33236,varname:node_7635,prsc:2|A-4773-R,B-4773-R,C-8049-OUT;n:type:ShaderForge.SFN_Multiply,id:9103,x:31315,y:33458,varname:node_9103,prsc:2|A-8023-R,B-8023-R,C-8049-OUT;n:type:ShaderForge.SFN_Multiply,id:9614,x:31925,y:33008,varname:node_9614,prsc:2|A-6074-R,B-9592-OUT,C-8080-OUT;n:type:ShaderForge.SFN_Vector1,id:8080,x:31704,y:33060,varname:node_8080,prsc:2,v1:2;n:type:ShaderForge.SFN_Slider,id:1772,x:31908,y:33201,ptovrint:False,ptlb:Soft Particle,ptin:_SoftParticle,varname:node_1772,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2991453,max:3;n:type:ShaderForge.SFN_DepthBlend,id:7146,x:32217,y:33201,varname:node_7146,prsc:2|DIST-1772-OUT;n:type:ShaderForge.SFN_Multiply,id:7424,x:32476,y:32851,varname:node_7424,prsc:2|A-3808-OUT,B-7146-OUT;n:type:ShaderForge.SFN_Add,id:6948,x:30625,y:33015,varname:node_6948,prsc:2|A-1626-R,B-3539-UVOUT;n:type:ShaderForge.SFN_Add,id:7311,x:30625,y:33237,varname:node_7311,prsc:2|A-1626-R,B-6095-UVOUT;n:type:ShaderForge.SFN_Add,id:1759,x:30624,y:33457,varname:node_1759,prsc:2|A-1626-R,B-1667-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3202,x:32500,y:32615,varname:node_3202,prsc:2|A-6074-RGB,B-5312-RGB,C-5312-A,D-1626-RGB;n:type:ShaderForge.SFN_Tex2d,id:2699,x:31501,y:32299,ptovrint:False,ptlb:node_2699,ptin:_node_2699,varname:node_2699,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0e74ad04f1070364983173dac73a1d3a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Sin,id:6221,x:31810,y:32152,varname:node_6221,prsc:2|IN-5625-OUT;n:type:ShaderForge.SFN_Time,id:9983,x:31505,y:32025,varname:node_9983,prsc:2;n:type:ShaderForge.SFN_Vector1,id:8022,x:31505,y:32183,varname:node_8022,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:5625,x:31680,y:32119,varname:node_5625,prsc:2|A-9983-T,B-8022-OUT;n:type:ShaderForge.SFN_Multiply,id:3314,x:31984,y:32246,varname:node_3314,prsc:2|A-6221-OUT,B-2699-RGB;n:type:ShaderForge.SFN_Cos,id:4524,x:31930,y:32011,varname:node_4524,prsc:2|IN-5625-OUT;n:type:ShaderForge.SFN_Blend,id:7072,x:32445,y:32411,varname:node_7072,prsc:2,blmd:10,clmp:True|SRC-3608-OUT,DST-6074-RGB;n:type:ShaderForge.SFN_Clamp01,id:3608,x:32173,y:32287,varname:node_3608,prsc:2|IN-3314-OUT;proporder:6074-3317-4773-8023-5312-1772;pass:END;sub:END;*/

Shader "Shader Forge/Smoke" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_3317 ("node_3317", 2D) = "white" {}
        _node_3317_copy ("node_3317_copy", 2D) = "white" {}
        _node_3317_copy_copy ("node_3317_copy_copy", 2D) = "white" {}
        _node_5312 ("node_5312", Color) = (1,1,1,1)
        _SoftParticle ("Soft Particle", Range(0, 3)) = 0.2991453
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_3317; uniform float4 _node_3317_ST;
            uniform sampler2D _node_3317_copy; uniform float4 _node_3317_copy_ST;
            uniform sampler2D _node_3317_copy_copy; uniform float4 _node_3317_copy_copy_ST;
            uniform float4 _node_5312;
            uniform float _SoftParticle;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = (_MainTex_var.rgb*_node_5312.rgb*_node_5312.a*i.vertexColor.rgb);
                float3 finalColor = emissive;
                float4 node_1162 = _Time + _TimeEditor;
                float node_6108 = 2.3;
                float2 node_3539_tc_rcp = float2(1.0,1.0)/float2( node_6108, node_6108 );
                float node_3539_ty = floor(node_6108 * node_3539_tc_rcp.x);
                float node_3539_tx = node_6108 - node_6108 * node_3539_ty;
                float2 node_3539 = (i.uv0 + float2(node_3539_tx, node_3539_ty)) * node_3539_tc_rcp;
                float2 node_5456 = ((i.vertexColor.r+node_3539)+node_1162.g*float2(0.0511,0.01237));
                float4 _node_3317_var = tex2D(_node_3317,TRANSFORM_TEX(node_5456, _node_3317));
                float node_8049 = 1.5;
                float node_1521 = 1.12;
                float2 node_6095_tc_rcp = float2(1.0,1.0)/float2( node_1521, node_1521 );
                float node_6095_ty = floor(node_1521 * node_6095_tc_rcp.x);
                float node_6095_tx = node_1521 - node_1521 * node_6095_ty;
                float2 node_6095 = (i.uv0 + float2(node_6095_tx, node_6095_ty)) * node_6095_tc_rcp;
                float2 node_6292 = (node_6095+node_1162.g*float2(0.0234,0.0923));
                float4 _node_3317_copy_var = tex2D(_node_3317_copy,TRANSFORM_TEX(node_6292, _node_3317_copy));
                float node_1273 = 1.89;
                float2 node_1667_tc_rcp = float2(1.0,1.0)/float2( node_1273, node_1273 );
                float node_1667_ty = floor(node_1273 * node_1667_tc_rcp.x);
                float node_1667_tx = node_1273 - node_1273 * node_1667_ty;
                float2 node_1667 = (i.uv0 + float2(node_1667_tx, node_1667_ty)) * node_1667_tc_rcp;
                float2 node_9271 = ((i.vertexColor.r+node_1667)+node_1162.g*float2(-0.0234,-0.04562));
                float4 _node_3317_copy_copy_var = tex2D(_node_3317_copy_copy,TRANSFORM_TEX(node_9271, _node_3317_copy_copy));
                fixed4 finalRGBA = fixed4(finalColor,((i.vertexColor.a*(_MainTex_var.r*saturate(((_node_3317_var.r*_node_3317_var.r*node_8049)*(_node_3317_copy_var.r*_node_3317_copy_var.r*node_8049)*(_node_3317_copy_copy_var.r*_node_3317_copy_copy_var.r*node_8049)*2.0))*2.0)*_node_5312.a)*saturate((sceneZ-partZ)/_SoftParticle)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
