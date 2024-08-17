public class BulletPool : Pool<VBullet>
{
    protected override void ConstructObject(VBullet obj)
    {
        MBullet tempModel = new MBullet();
        obj.MyModel = tempModel;
    }

}
