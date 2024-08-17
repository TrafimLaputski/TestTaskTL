public class EnemyPool : Pool<VEnemy>
{
    protected override void ConstructObject(VEnemy obj)
    {
        MEnemy tempModel = new MEnemy();
        obj.MyModel = tempModel;
    }
}
