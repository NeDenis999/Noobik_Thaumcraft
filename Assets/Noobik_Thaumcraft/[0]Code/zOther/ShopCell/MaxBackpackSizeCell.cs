using UnityEngine;

namespace Noobik_Thaumcraft
{
    public class MaxBackpackSizeCell : BaseShopCell
    {
        [SerializeField]
        private UI _ui;
        
        protected override void Buy()
        {
            ref var data = ref Startup.Instantiate.GameData;
            data.MaxItems += 8;
            _ui.CountItemsLabel.SetSize(data.MaxItems);
        }
    }
}