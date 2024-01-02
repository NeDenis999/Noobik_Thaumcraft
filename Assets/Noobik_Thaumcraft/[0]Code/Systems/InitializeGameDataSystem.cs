using Leopotam.Ecs;

namespace Noobik_Thaumcraft
{
    public class InitializeGameDataSystem : IEcsInitSystem
    {
        private GameData _data;
        private SceneData _sceneData;
        private Configuration _config;
        
        public void Init()
        {
            _data.TargetPoint = _sceneData.TargetPoints[0].Value[0];
            _data.IndexConfigTarget = 0;
            
            if (_data.Diamonds >= _config.TargetData[_data.IndexConfigTarget].Amount)
            {
                _data.IndexConfigTarget += 1;
                _data.Diamonds = 0;
            }

            _sceneData.UI.TargetScreen.ViewUpgrade(_data.Diamonds, _config.TargetData[_data.IndexConfigTarget]);
            _sceneData.UI.CountItemsLabel.ViewUpdate(0, _data.MaxItems);
        }
    }
}