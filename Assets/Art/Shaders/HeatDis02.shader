// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4561,x:32916,y:32532,varname:node_4561,prsc:2|diff-3302-RGB,spec-320-OUT,emission-3302-RGB,alpha-3260-OUT,refract-313-OUT;n:type:ShaderForge.SFN_Add,id:258,x:32271,y:32714,varname:node_258,prsc:2|A-2910-UVOUT,B-4758-OUT;n:type:ShaderForge.SFN_Tex2d,id:3302,x:32484,y:32714,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3302,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-258-OUT;n:type:ShaderForge.SFN_TexCoord,id:2910,x:31999,y:32646,varname:node_2910,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3643,x:31830,y:32988,ptovrint:False,ptlb:Noise01,ptin:_Noise01,varname:node_3643,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8760-OUT;n:type:ShaderForge.SFN_Append,id:3416,x:32013,y:33005,varname:node_3416,prsc:2|A-3643-R,B-3643-G;n:type:ShaderForge.SFN_Add,id:8760,x:31664,y:32850,varname:node_8760,prsc:2|A-7636-UVOUT,B-3765-OUT;n:type:ShaderForge.SFN_TexCoord,id:7636,x:31462,y:32742,varname:node_7636,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:9005,x:31443,y:33024,varname:node_9005,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3765,x:31617,y:33129,varname:node_3765,prsc:2|A-9005-T,B-9744-OUT;n:type:ShaderForge.SFN_Add,id:2497,x:32234,y:33037,varname:node_2497,prsc:2|A-3416-OUT,B-9863-OUT;n:type:ShaderForge.SFN_Multiply,id:4758,x:32075,y:32833,varname:node_4758,prsc:2|A-2497-OUT,B-7796-OUT;n:type:ShaderForge.SFN_Append,id:9863,x:32053,y:33173,varname:node_9863,prsc:2|A-7533-R,B-7533-B;n:type:ShaderForge.SFN_Tex2d,id:7533,x:31816,y:33217,ptovrint:False,ptlb:Noise02,ptin:_Noise02,varname:node_7533,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:7796,x:31844,y:32850,ptovrint:False,ptlb:Mul,ptin:_Mul,varname:node_7796,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:7606,x:32425,y:33068,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_7606,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:9852,x:32694,y:33013,varname:node_9852,prsc:2|A-310-OUT,B-7606-RGB,T-3857-A;n:type:ShaderForge.SFN_Vector3,id:310,x:32443,y:32929,varname:node_310,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_VertexColor,id:3857,x:32351,y:33245,varname:node_3857,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:3260,x:32694,y:32795,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_3260,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:320,x:32605,y:32572,ptovrint:False,ptlb:Specularity,ptin:_Specularity,varname:node_320,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:2486,x:32597,y:33290,ptovrint:False,ptlb:AlphaMask,ptin:_AlphaMask,varname:node_2486,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:9744,x:31417,y:33179,varname:node_9744,prsc:2,v1:0;n:type:ShaderForge.SFN_ComponentMask,id:4089,x:32855,y:33102,varname:node_4089,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9852-OUT;n:type:ShaderForge.SFN_Slider,id:747,x:32496,y:33474,ptovrint:False,ptlb:Strength,ptin:_Strength,varname:node_747,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1522843,max:1;n:type:ShaderForge.SFN_Multiply,id:6801,x:32901,y:33325,varname:node_6801,prsc:2|A-2486-RGB,B-747-OUT;n:type:ShaderForge.SFN_Multiply,id:1716,x:33107,y:33228,varname:node_1716,prsc:2|A-4089-OUT,B-6801-OUT;n:type:ShaderForge.SFN_ComponentMask,id:313,x:33285,y:33081,varname:node_313,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1716-OUT;proporder:3302-3643-7533-7796-7606-3260-320-2486-747;pass:END;sub:END;*/

Shader "Custom/HeatDis02" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Noise01 ("Noise01", 2D) = "white" {}
        _Noise02 ("Noise02", 2D) = "white" {}
        _Mul ("Mul", Float ) = 0
        _Normal ("Normal", 2D) = "bump" {}
        _Opacity ("Opacity", Float ) = 0
        _Specularity ("Specularity", Float ) = 0
        _AlphaMask ("AlphaMask", 2D) = "white" {}
        _Strength ("Strength", Range(0, 1)) = 0.1522843
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
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
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Noise01; uniform float4 _Noise01_ST;
            uniform sampler2D _Noise02; uniform float4 _Noise02_ST;
            uniform float _Mul;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _Opacity;
            uniform float _Specularity;
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform float _Strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 node_9852 = lerp(float3(0,0,1),_Normal_var.rgb,i.vertexColor.a);
                float2 node_4089 = node_9852.rg;
                float4 _AlphaMask_var = tex2D(_AlphaMask,TRANSFORM_TEX(i.uv0, _AlphaMask));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (float3(node_4089,0.0)*(_AlphaMask_var.rgb*_Strength)).rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specularity,_Specularity,_Specularity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_9005 = _Time;
                float2 node_8760 = (i.uv0+(node_9005.g*0.0));
                float4 _Noise01_var = tex2D(_Noise01,TRANSFORM_TEX(node_8760, _Noise01));
                float4 _Noise02_var = tex2D(_Noise02,TRANSFORM_TEX(i.uv0, _Noise02));
                float2 node_258 = (i.uv0+((float2(_Noise01_var.r,_Noise01_var.g)+float2(_Noise02_var.r,_Noise02_var.b))*_Mul));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_258, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Noise01; uniform float4 _Noise01_ST;
            uniform sampler2D _Noise02; uniform float4 _Noise02_ST;
            uniform float _Mul;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _Opacity;
            uniform float _Specularity;
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform float _Strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 node_9852 = lerp(float3(0,0,1),_Normal_var.rgb,i.vertexColor.a);
                float2 node_4089 = node_9852.rg;
                float4 _AlphaMask_var = tex2D(_AlphaMask,TRANSFORM_TEX(i.uv0, _AlphaMask));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (float3(node_4089,0.0)*(_AlphaMask_var.rgb*_Strength)).rg;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specularity,_Specularity,_Specularity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_9005 = _Time;
                float2 node_8760 = (i.uv0+(node_9005.g*0.0));
                float4 _Noise01_var = tex2D(_Noise01,TRANSFORM_TEX(node_8760, _Noise01));
                float4 _Noise02_var = tex2D(_Noise02,TRANSFORM_TEX(i.uv0, _Noise02));
                float2 node_258 = (i.uv0+((float2(_Noise01_var.r,_Noise01_var.g)+float2(_Noise02_var.r,_Noise02_var.b))*_Mul));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_258, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _Opacity,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
