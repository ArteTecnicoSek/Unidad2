void DrawShaderToy_float (float2 screenPosition, out float3 fragColor)
{ 

   // float2 screenPosition=(2*(fragCoord.xy)-_ScreenParams.xy)/_ScreenParams.xy;
    float x=screenPosition.x;
    float y=screenPosition.y;
    float timed=0.5+0.5*sin(_Time.y);
    float a= lerp(1.0,-2.0,timed);
    float b=lerp(-1.0,1.0,timed); 
    float n=lerp(7.0,4.0,timed); 
    float m= lerp(2.0,5.0,timed); 

   float curves= a*sin(PI*n*x)*sin(PI*m*y)+ b*sin(PI*m*x)*sin(PI*n*y);
    
    float valueWithStep=step(abs(curves),0.1);
   fragColor=float3(valueWithStep,valueWithStep,valueWithStep) ; 
 
} 