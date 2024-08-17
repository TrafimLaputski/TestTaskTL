using UnityEngine;
using Zenject;

public class PresentersInstaller : MonoInstaller
{
    [SerializeField] private PMushroom _player = null;
    [SerializeField] private PEnemy _enemy = null;
    [SerializeField] private PBullet _bullet = null;
    [SerializeField] private PExp _exp = null;
    [SerializeField] private PCards _cards = null;

    public override void InstallBindings()
    {
        Container.Bind<PMushroom>().FromInstance(_player).AsSingle();
        Container.QueueForInject(_player);

        Container.Bind<PEnemy>().FromInstance(_enemy).AsSingle();
        Container.QueueForInject(_enemy);

        Container.Bind<PBullet>().FromInstance(_bullet).AsSingle();
        Container.QueueForInject(_bullet);

        Container.Bind<PExp>().FromInstance(_exp).AsSingle();
        Container.QueueForInject(_exp);

        Container.Bind<PCards>().FromInstance(_cards).AsSingle();
        Container.QueueForInject(_cards);
    }
}