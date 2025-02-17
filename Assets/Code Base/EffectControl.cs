using UnityEngine;

public  class EffectControl: SingletonBase<EffectControl>
{
   [SerializeField] private ScreenShaderControl screenShaderControl;

   private void Awake()
   {
      Init();
   }

   public void StartEffect(EffectType effectType)
   {
      switch (effectType)
      {
         case EffectType.Normal:
            screenShaderControl.EnableGlitch(0,0,0);
            break;
         case EffectType.GlitchSmall:
            screenShaderControl.EnableGlitch(10,2,5);
            break;
         case EffectType.GlitchMedium:
            screenShaderControl.EnableGlitch(20,5,20);
            break;
         case EffectType.GlitchHard:
            screenShaderControl.EnableGlitch(40,10,40);
            break;
      }
   }
}
