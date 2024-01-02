namespace Noobik_Thaumcraft
{
    public class SpeedCell : BaseShopCell
    {
        protected override void Buy()
        {
            ref var data = ref Startup.Instantiate.GameData;
            data.Speed += 2;
        }
    }
}