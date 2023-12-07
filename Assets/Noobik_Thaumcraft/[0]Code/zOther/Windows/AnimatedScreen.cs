using UnityEngine.Playables;

namespace Noobik_Thaumcraft
{
    public class AnimatedScreen : Screen
    {
        public PlayableDirector ShowDirector;
        public PlayableDirector HideDirector;

        public override void Show(bool state)
        {
            if (state)
            {
                gameObject.SetActive(true);
                ShowDirector.Play();     
            }
            else
            {
                HideDirector.Play();
            }
        }
    }
}