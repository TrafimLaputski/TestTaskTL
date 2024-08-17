using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PMushroom : MonoBehaviour
{
    private MushroomPool _pool = null;
    private PBullet _pBullet = null;
    private PEnemy _pEnemy = null;

    [SerializeField] private List<UpgradeType> _upgradeTypes = new List<UpgradeType>();

    private List<VMushroom> _mushroomsList = new List<VMushroom>();
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private MMushroom _baseModel = new MMushroom();

    private bool _shootNow = true;

    [Inject]
    private void Construct(MushroomPool mushroomPool, PBullet pBullet, PEnemy pEnemy)
    {
        _pool = mushroomPool;
        _pBullet = pBullet;
        _pEnemy = pEnemy;
    }

    private void Start()
    {
        AddMushroom();
    }

    private void AddMushroom()
    {
        VMushroom newMushroom = _pool.GetItem();

        if (newMushroom != null)
        {
            _mushroomsList.Add(newMushroom);

            newMushroom.Presenter = this;
            newMushroom.MyModel.Damage = _baseModel.Damage;
            newMushroom.MyModel.Penetration = _baseModel.Penetration;
            newMushroom.MyModel.Lazer = _baseModel.Lazer;
            newMushroom.MyModel.Shoot = _baseModel.Shoot;
            newMushroom.MyModel.Cooldown = _baseModel.Cooldown;

            float spacing = (float)4 / (_mushroomsList.Count + 1);

            for (int i = 0; i < _mushroomsList.Count; i++)
            {
                float x = -2 + (1 + i) * spacing;

                Vector3 position = new Vector3(x, -3.5f, 0);
                _mushroomsList[i].transform.SetParent(transform);
                _mushroomsList[i].transform.position = position;

                newMushroom.gameObject.SetActive(true);
                Shooting(newMushroom);
            }
        }
    }

    private async void Shooting(VMushroom mushroom)
    {
        bool canShoot = true;
        
        while (canShoot)
        {
            try
            {
                await Reload(mushroom.MyModel.Cooldown, _cancellationTokenSource.Token);


                if (_pEnemy.EnemyList.Count > 0 && _shootNow)
                {
                    float dist = 0;
                    VEnemy tempEnemy = null;

                    foreach (VEnemy enemy in _pEnemy.EnemyList)
                    {
                        float currentDist = Vector2.Distance(mushroom.transform.position, enemy.transform.position);

                        if (currentDist <= mushroom.MyModel.ShootDistance)
                        {
                            if (tempEnemy == null || currentDist < dist)
                            {
                                tempEnemy = enemy;
                            }
                        }
                    }

                    mushroom.MyModel.CurrentEnemy = tempEnemy;

                    Shoot(mushroom);
                }
            }
            catch (TaskCanceledException)
            {
                canShoot = false;
            }
        }
    }

    private async void Shoot(VMushroom mushroom)
    {
        float spacing = (float) 0.5f * mushroom.MyModel.Lazer / 2;

        for (int i = 0; i < mushroom.MyModel.Shoot; i++)
        {
            await Reload(100, _cancellationTokenSource.Token);

            for (int n = 0; n < mushroom.MyModel.Lazer; n++)
            {
                Vector3 enemyPos = mushroom.MyModel.CurrentEnemy.gameObject.transform.position + (Vector3.one * (- spacing + 0.5f * n));
                Vector3 shotDir = mushroom.transform.position + (Vector3.left * (-spacing + 0.1f * n));
                enemyPos.z = 0;

                _pBullet.SpawnBullet(shotDir, enemyPos, mushroom.MyModel.Damage, mushroom.MyModel.Penetration);
            }
        }
    }

    private async Task Reload(int reloarTime, CancellationToken cancellationToken)
    {
        await Task.Delay(reloarTime, cancellationToken);
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }


    public void Upgrade(VMushroom vMushroom)
    {
        vMushroom.MyModel.CurrentLvl++;

        if (vMushroom.MyModel.CurrentLvl < _upgradeTypes.Count)
        {
            switch (_upgradeTypes[vMushroom.MyModel.CurrentLvl])
            {
                case UpgradeType.DMG:
                    vMushroom.MyModel.Damage++;
                    break;

                case UpgradeType.Cooldown:
                    vMushroom.MyModel.Cooldown -= 100;
                    break;

                case UpgradeType.Penetration:
                    vMushroom.MyModel.Penetration++;
                    break;

                default:
                    break;
            }

            if (vMushroom.MyModel.CurrentLvl >= _upgradeTypes.Count)
            {
                vMushroom.MyModel.CanUpgrade = false;
            }
        }
        else
        {
            vMushroom.MyModel.CanUpgrade = false;
        }
    }
    public void CardsUpgrade(CardsType type)
    {
        switch (type)
        {
            case CardsType.New:
                AddMushroom();
                break;

            case CardsType.Joker:

                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Damage += mushroom.MyModel.Damage * 3 / 100;
                    mushroom.MyModel.Penetration++;
                }

                _baseModel.Damage += _baseModel.Damage * 3 / 100;
                _baseModel.Penetration++;
                break;

            case CardsType.Damage:
                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Damage += mushroom.MyModel.Damage * 2 / 100;
                }

                _baseModel.Damage += _baseModel.Damage * 2 / 100;
                break;

            case CardsType.Penetration:
                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Penetration++;
                }

                _baseModel.Penetration++;
                break;

            case CardsType.Double:
                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Damage -= mushroom.MyModel.Damage * 2 / 100;
                    mushroom.MyModel.Shoot++;
                }

                _baseModel.Damage -= _baseModel.Damage * 2 / 100;
                _baseModel.Shoot++;
                break;

            case CardsType.Lazer:
                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Lazer++;
                }

                _baseModel.Lazer++;
                break;

            case CardsType.Cooldown:
                foreach (VMushroom mushroom in _mushroomsList)
                {
                    mushroom.MyModel.Cooldown -= mushroom.MyModel.Cooldown * 2 / 100;
                }

                _baseModel.Cooldown -= _baseModel.Cooldown * 2 / 100;
                break;

            default:
                break;
        }
    }

    public void ShootingStatus(bool vakue)
    {
        _shootNow = vakue;
    }
}

public enum UpgradeType
{
    Nothing,
    DMG,
    Penetration,
    Cooldown
}