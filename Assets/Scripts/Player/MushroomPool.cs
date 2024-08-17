public class MushroomPool : Pool<VMushroom>
{
    protected override void ConstructObject(VMushroom obj)
    {
        MMushroom tempModel = new MMushroom();
        obj.MyModel = tempModel;
    }
}
