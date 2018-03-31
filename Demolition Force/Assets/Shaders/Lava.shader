// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.64,fgcg:0.56,fgcb:0.366,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-6365-OUT;n:type:ShaderForge.SFN_Tex2d,id:5674,x:31005,y:31925,ptovrint:False,ptlb:Fiery 1,ptin:_Fiery1,varname:_Cloud,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:14d2c9461b7d5af44bdd51e2f990d0fd,ntxv:0,isnm:False|UVIN-3650-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7725,x:31005,y:32138,ptovrint:False,ptlb:Fiery 2,ptin:_Fiery2,varname:_FieryLava,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bad6ffd32567e9b41aa10429a0822951,ntxv:0,isnm:False|UVIN-2881-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6102,x:30994,y:33387,ptovrint:False,ptlb:Burnt Lava 1,ptin:_BurntLava1,varname:_BurntLava1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a2bdcbdad6cfed54b9568bdfc41a89f0,ntxv:0,isnm:False|UVIN-3676-UVOUT;n:type:ShaderForge.SFN_Panner,id:3650,x:30827,y:31924,varname:node_3650,prsc:2,spu:0.09,spv:0.0123|UVIN-7930-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9118,x:30470,y:31925,varname:node_9118,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:2881,x:30815,y:32138,varname:node_2881,prsc:2,spu:0.1,spv:-0.0213|UVIN-7731-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7731,x:30655,y:32138,varname:node_7731,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:3676,x:30800,y:33387,varname:node_3676,prsc:2,spu:0.03,spv:0|UVIN-7823-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7823,x:30637,y:33387,varname:node_7823,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:5123,x:30812,y:32801,ptovrint:False,ptlb:Cloud,ptin:_Cloud,varname:_Gradient,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:850a21ffb5f8fd94b8756cd36d69feb5,ntxv:0,isnm:False|UVIN-1056-UVOUT;n:type:ShaderForge.SFN_Vector1,id:1966,x:31325,y:32858,varname:node_1966,prsc:2,v1:2;n:type:ShaderForge.SFN_Lerp,id:6041,x:32387,y:32800,varname:node_6041,prsc:2|A-8891-OUT,B-5489-OUT,T-2797-OUT;n:type:ShaderForge.SFN_Panner,id:1056,x:30649,y:32801,varname:node_1056,prsc:2,spu:0.1,spv:0|UVIN-2997-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2997,x:30453,y:32801,varname:node_2997,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Blend,id:744,x:31318,y:33308,varname:node_744,prsc:2,blmd:0,clmp:True|SRC-6102-RGB,DST-6102-RGB;n:type:ShaderForge.SFN_UVTile,id:7930,x:30666,y:31925,varname:node_7930,prsc:2|UVIN-9118-UVOUT,WDT-6044-OUT,HGT-6044-OUT,TILE-6044-OUT;n:type:ShaderForge.SFN_Vector1,id:6044,x:30470,y:32084,varname:node_6044,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:5489,x:32026,y:32627,varname:node_5489,prsc:2|A-5674-RGB,B-7725-RGB;n:type:ShaderForge.SFN_Sin,id:8034,x:30824,y:34542,varname:node_8034,prsc:2|IN-2820-OUT;n:type:ShaderForge.SFN_Time,id:1293,x:30494,y:34541,varname:node_1293,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2820,x:30670,y:34541,varname:node_2820,prsc:2|A-1293-T,B-2995-OUT;n:type:ShaderForge.SFN_Vector1,id:2995,x:30494,y:34674,varname:node_2995,prsc:2,v1:1.012;n:type:ShaderForge.SFN_Sin,id:9760,x:30824,y:34748,varname:node_9760,prsc:2|IN-8655-OUT;n:type:ShaderForge.SFN_Time,id:3667,x:30494,y:34747,varname:node_3667,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8655,x:30670,y:34747,varname:node_8655,prsc:2|A-3667-T,B-6457-OUT;n:type:ShaderForge.SFN_Vector1,id:6457,x:30494,y:34880,varname:node_6457,prsc:2,v1:2.33;n:type:ShaderForge.SFN_Multiply,id:7608,x:30981,y:34598,varname:node_7608,prsc:2|A-8034-OUT,B-9760-OUT;n:type:ShaderForge.SFN_Add,id:8891,x:32026,y:32974,varname:node_8891,prsc:2|A-744-OUT,B-3636-OUT;n:type:ShaderForge.SFN_Tex2d,id:4695,x:31376,y:33930,ptovrint:False,ptlb:Burnt Lava 2,ptin:_BurntLava2,varname:node_4695,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f0f932c5b13ffc24796633c438968c48,ntxv:0,isnm:False|UVIN-3497-UVOUT;n:type:ShaderForge.SFN_Panner,id:3497,x:31206,y:33930,varname:node_3497,prsc:2,spu:0.03,spv:0|UVIN-334-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:334,x:31043,y:33930,varname:node_334,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3839,x:30866,y:34116,ptovrint:False,ptlb:Flash Alpha 1,ptin:_FlashAlpha1,varname:node_3839,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0d9fe98f1b4167141a9ff4f386b168d3,ntxv:0,isnm:False|UVIN-7796-UVOUT;n:type:ShaderForge.SFN_Panner,id:7796,x:30696,y:34116,varname:node_7796,prsc:2,spu:0.0475,spv:0|UVIN-2077-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2077,x:30533,y:34116,varname:node_2077,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9843,x:31192,y:34290,varname:node_9843,prsc:2|A-3839-RGB,B-7608-OUT,C-7814-OUT;n:type:ShaderForge.SFN_Vector1,id:7814,x:31162,y:34438,varname:node_7814,prsc:2,v1:2;n:type:ShaderForge.SFN_Sin,id:7624,x:30827,y:34977,varname:node_7624,prsc:2|IN-6093-OUT;n:type:ShaderForge.SFN_Time,id:2121,x:30497,y:34976,varname:node_2121,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6093,x:30673,y:34976,varname:node_6093,prsc:2|A-2121-T,B-1631-OUT;n:type:ShaderForge.SFN_Vector1,id:1631,x:30497,y:35109,varname:node_1631,prsc:2,v1:0.564;n:type:ShaderForge.SFN_Sin,id:1614,x:30827,y:35183,varname:node_1614,prsc:2|IN-9770-OUT;n:type:ShaderForge.SFN_Time,id:1908,x:30497,y:35182,varname:node_1908,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9770,x:30673,y:35182,varname:node_9770,prsc:2|A-1908-T,B-3419-OUT;n:type:ShaderForge.SFN_Vector1,id:3419,x:30497,y:35315,varname:node_3419,prsc:2,v1:1.758;n:type:ShaderForge.SFN_Multiply,id:499,x:31010,y:35032,varname:node_499,prsc:2|A-7624-OUT,B-1614-OUT;n:type:ShaderForge.SFN_Multiply,id:16,x:31209,y:34539,varname:node_16,prsc:2|A-559-RGB,B-499-OUT,C-7814-OUT;n:type:ShaderForge.SFN_Multiply,id:3636,x:31736,y:34003,varname:node_3636,prsc:2|A-4695-RGB,B-6838-OUT,C-7903-OUT;n:type:ShaderForge.SFN_Tex2d,id:559,x:30866,y:34309,ptovrint:False,ptlb:Flash Alpha 2,ptin:_FlashAlpha2,varname:_node_3839_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0d9fe98f1b4167141a9ff4f386b168d3,ntxv:0,isnm:False|UVIN-9671-UVOUT;n:type:ShaderForge.SFN_Panner,id:9671,x:30696,y:34309,varname:node_9671,prsc:2,spu:0.0713,spv:0.1|UVIN-1223-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6351,x:30353,y:34311,varname:node_6351,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3007,x:31416,y:34353,varname:node_3007,prsc:2|A-9843-OUT,B-16-OUT;n:type:ShaderForge.SFN_UVTile,id:1223,x:30533,y:34311,varname:node_1223,prsc:2|UVIN-6351-UVOUT,WDT-5635-OUT,HGT-5635-OUT,TILE-5635-OUT;n:type:ShaderForge.SFN_Vector1,id:5635,x:30353,y:34463,varname:node_5635,prsc:2,v1:0.77;n:type:ShaderForge.SFN_Vector1,id:7903,x:31553,y:34061,varname:node_7903,prsc:2,v1:4;n:type:ShaderForge.SFN_ConstantClamp,id:6838,x:31596,y:34183,varname:node_6838,prsc:2,min:-0.1,max:1|IN-3007-OUT;n:type:ShaderForge.SFN_Tex2d,id:4352,x:30812,y:32602,ptovrint:False,ptlb:Alpha/Lerp,ptin:_AlphaLerp,varname:_Gradient_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6f1c775fb925b204f9c12c8ec59ebed1,ntxv:0,isnm:False|UVIN-357-UVOUT;n:type:ShaderForge.SFN_Panner,id:357,x:30616,y:32602,varname:node_357,prsc:2,spu:0.03,spv:0|UVIN-2409-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:2409,x:30453,y:32602,varname:node_2409,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Clamp01,id:2797,x:32026,y:32798,varname:node_2797,prsc:2|IN-2582-OUT;n:type:ShaderForge.SFN_Multiply,id:2582,x:31213,y:32713,varname:node_2582,prsc:2|A-4352-RGB,B-9589-OUT,C-6727-OUT;n:type:ShaderForge.SFN_Vector1,id:6727,x:31052,y:32877,varname:node_6727,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:9589,x:31033,y:32740,varname:node_9589,prsc:2|A-5123-B,B-5123-B;n:type:ShaderForge.SFN_Color,id:2085,x:32441,y:32463,ptovrint:False,ptlb:node_2085,ptin:_node_2085,varname:node_2085,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6365,x:32555,y:32782,varname:node_6365,prsc:2|A-2085-RGB,B-6041-OUT;proporder:5674-7725-6102-4695-5123-4352-3839-559-2085;pass:END;sub:END;*/

Shader "Shader Forge/Lava" {
    Properties {
        _Fiery1 ("Fiery 1", 2D) = "white" {}
        _Fiery2 ("Fiery 2", 2D) = "white" {}
        _BurntLava1 ("Burnt Lava 1", 2D) = "white" {}
        _BurntLava2 ("Burnt Lava 2", 2D) = "white" {}
        _Cloud ("Cloud", 2D) = "white" {}
        _AlphaLerp ("Alpha/Lerp", 2D) = "white" {}
        _FlashAlpha1 ("Flash Alpha 1", 2D) = "white" {}
        _FlashAlpha2 ("Flash Alpha 2", 2D) = "white" {}
        [HDR]_node_2085 ("node_2085", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Fiery1; uniform float4 _Fiery1_ST;
            uniform sampler2D _Fiery2; uniform float4 _Fiery2_ST;
            uniform sampler2D _BurntLava1; uniform float4 _BurntLava1_ST;
            uniform sampler2D _Cloud; uniform float4 _Cloud_ST;
            uniform sampler2D _BurntLava2; uniform float4 _BurntLava2_ST;
            uniform sampler2D _FlashAlpha1; uniform float4 _FlashAlpha1_ST;
            uniform sampler2D _FlashAlpha2; uniform float4 _FlashAlpha2_ST;
            uniform sampler2D _AlphaLerp; uniform float4 _AlphaLerp_ST;
            uniform float4 _node_2085;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_1776 = _Time + _TimeEditor;
                float2 node_3676 = (i.uv0+node_1776.g*float2(0.03,0));
                float4 _BurntLava1_var = tex2D(_BurntLava1,TRANSFORM_TEX(node_3676, _BurntLava1));
                float2 node_3497 = (i.uv0+node_1776.g*float2(0.03,0));
                float4 _BurntLava2_var = tex2D(_BurntLava2,TRANSFORM_TEX(node_3497, _BurntLava2));
                float2 node_7796 = (i.uv0+node_1776.g*float2(0.0475,0));
                float4 _FlashAlpha1_var = tex2D(_FlashAlpha1,TRANSFORM_TEX(node_7796, _FlashAlpha1));
                float4 node_1293 = _Time + _TimeEditor;
                float4 node_3667 = _Time + _TimeEditor;
                float node_7814 = 2.0;
                float node_5635 = 0.77;
                float2 node_1223_tc_rcp = float2(1.0,1.0)/float2( node_5635, node_5635 );
                float node_1223_ty = floor(node_5635 * node_1223_tc_rcp.x);
                float node_1223_tx = node_5635 - node_5635 * node_1223_ty;
                float2 node_1223 = (i.uv0 + float2(node_1223_tx, node_1223_ty)) * node_1223_tc_rcp;
                float2 node_9671 = (node_1223+node_1776.g*float2(0.0713,0.1));
                float4 _FlashAlpha2_var = tex2D(_FlashAlpha2,TRANSFORM_TEX(node_9671, _FlashAlpha2));
                float4 node_2121 = _Time + _TimeEditor;
                float4 node_1908 = _Time + _TimeEditor;
                float node_6044 = 0.5;
                float2 node_7930_tc_rcp = float2(1.0,1.0)/float2( node_6044, node_6044 );
                float node_7930_ty = floor(node_6044 * node_7930_tc_rcp.x);
                float node_7930_tx = node_6044 - node_6044 * node_7930_ty;
                float2 node_7930 = (i.uv0 + float2(node_7930_tx, node_7930_ty)) * node_7930_tc_rcp;
                float2 node_3650 = (node_7930+node_1776.g*float2(0.09,0.0123));
                float4 _Fiery1_var = tex2D(_Fiery1,TRANSFORM_TEX(node_3650, _Fiery1));
                float2 node_2881 = (i.uv0+node_1776.g*float2(0.1,-0.0213));
                float4 _Fiery2_var = tex2D(_Fiery2,TRANSFORM_TEX(node_2881, _Fiery2));
                float2 node_357 = (i.uv0+node_1776.g*float2(0.03,0));
                float4 _AlphaLerp_var = tex2D(_AlphaLerp,TRANSFORM_TEX(node_357, _AlphaLerp));
                float2 node_1056 = (i.uv0+node_1776.g*float2(0.1,0));
                float4 _Cloud_var = tex2D(_Cloud,TRANSFORM_TEX(node_1056, _Cloud));
                float3 emissive = (_node_2085.rgb*lerp((saturate(min(_BurntLava1_var.rgb,_BurntLava1_var.rgb))+(_BurntLava2_var.rgb*clamp(((_FlashAlpha1_var.rgb*(sin((node_1293.g*1.012))*sin((node_3667.g*2.33)))*node_7814)+(_FlashAlpha2_var.rgb*(sin((node_2121.g*0.564))*sin((node_1908.g*1.758)))*node_7814)),-0.1,1)*4.0)),(_Fiery1_var.rgb+_Fiery2_var.rgb),saturate((_AlphaLerp_var.rgb*(_Cloud_var.b*_Cloud_var.b)*2.0))));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
