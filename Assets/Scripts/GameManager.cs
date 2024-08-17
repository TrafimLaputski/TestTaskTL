using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private PBullet _pBullet = null;
    private PMushroom _player = null;
    private PEnemy _enemy = null;

    [Inject]
    private void Construct(PMushroom player, PEnemy enemy, PBullet pBullet)
    {
        _player = player;
        _enemy = enemy;
        _pBullet = pBullet;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void GamePause(bool value)
    {
        _player.ShootingStatus(value);
        _enemy.MovingStatus(value);
        _pBullet.FlyStat(value);
    }
}
