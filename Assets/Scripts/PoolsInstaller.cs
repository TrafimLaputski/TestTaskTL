using UnityEngine;
using Zenject;

public class PoolsInstaller : MonoInstaller
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private MushroomPool _mushroomPool;
    [SerializeField] private BulletPool _bulletPool;

    public override void InstallBindings()
    {
        Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle();
        Container.QueueForInject(_enemyPool);

        Container.Bind<MushroomPool>().FromInstance(_mushroomPool).AsSingle();
        Container.QueueForInject(_mushroomPool);

        Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle();
        Container.QueueForInject(_bulletPool);
    }
}