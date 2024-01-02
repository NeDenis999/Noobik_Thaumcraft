namespace Noobik_Thaumcraft
{
    public class MiningSpeedCell : BaseShopCell
    {
        protected override void Buy()
        {
            ref var data = ref Startup.Instantiate.GameData;
            data.MiningBoost += 1;
        }
    }
}