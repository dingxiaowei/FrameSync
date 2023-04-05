// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-1085-OUT,custl-3631-OUT,alpha-4465-A;n:type:ShaderForge.SFN_Tex2d,id:9203,x:31431,y:32640,ptovrint:False,ptlb:node_9203,ptin:_node_9203,varname:node_9203,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:755b608f53dce964ba1ad44c4e1a17a5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:8031,x:31882,y:32852,varname:node_8031,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1085,x:32066,y:33005,varname:node_1085,prsc:2|A-8031-OUT,B-9908-OUT,C-183-RGB,D-8952-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9908,x:31554,y:32916,ptovrint:False,ptlb:power,ptin:_power,varname:node_9908,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:183,x:31670,y:33038,ptovrint:False,ptlb:glow,ptin:_glow,varname:node_183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9432048,c3:0.1764706,c4:1;n:type:ShaderForge.SFN_Time,id:7332,x:31093,y:33110,varname:node_7332,prsc:2;n:type:ShaderForge.SFN_Sin,id:7289,x:31522,y:33220,varname:node_7289,prsc:2|IN-3431-OUT;n:type:ShaderForge.SFN_Abs,id:4463,x:31736,y:33220,varname:node_4463,prsc:2|IN-7289-OUT;n:type:ShaderForge.SFN_Slider,id:373,x:30990,y:33390,ptovrint:False,ptlb:pinglv,ptin:_pinglv,varname:node_373,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:3431,x:31357,y:33220,varname:node_3431,prsc:2|A-7332-T,B-373-OUT;n:type:ShaderForge.SFN_Add,id:8952,x:31914,y:33220,varname:node_8952,prsc:2|A-4463-OUT,B-8826-OUT;n:type:ShaderForge.SFN_Vector1,id:8826,x:31702,y:33479,varname:node_8826,prsc:2,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:217,x:31562,y:32524,ptovrint:False,ptlb:node_217,ptin:_node_217,varname:node_217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6186,x:31900,y:32690,varname:node_6186,prsc:2|A-217-OUT,B-9203-RGB;n:type:ShaderForge.SFN_Color,id:4465,x:32144,y:33237,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4465,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3631,x:32465,y:32979,varname:node_3631,prsc:2|A-4465-RGB,B-6186-OUT;proporder:9203-9908-183-373-217-4465;pass:END;sub:END;*/

Shader "Shader Forge/jinlong" {
    Properties {
        _node_9203 ("node_9203", 2D) = "white" {}
        _power ("power", Float ) = 1
        _glow ("glow", Color) = (1,1,1,1)
        _pinglv ("pinglv", Range(0, 10)) = 0
        _node_217 ("node_217", Float ) = 1
        _Color ("Color", Color) = (1,1,1,1)
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _node_9203; uniform float4 _node_9203_ST;
            uniform float _power;
            uniform float4 _glow;
            uniform float _pinglv;
            uniform float _node_217;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_7332 = _Time;
                float3 emissive = ((1.0-max(0,dot(normalDirection, viewDirection)))*_power*_glow.rgb*(abs(sin((node_7332.g*_pinglv)))+0.2));
                float4 _node_9203_var = tex2D(_node_9203,TRANSFORM_TEX(i.uv0, _node_9203));
                float3 finalColor = emissive + (_Color.rgb*(_node_217*_node_9203_var.rgb));
                return fixed4(finalColor,_Color.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
