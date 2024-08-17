using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PCards : MonoBehaviour
{
    private GameManager _gameManager = null;
    private PMushroom _player = null;

    [SerializeField] private List<VCards> cards = new List<VCards>();

    [Inject]
    private void Construct(PMushroom player, GameManager gameManager)
    {                       
        _player = player;
        _gameManager = gameManager;
    }

    private void Start()
    {
        ConstructCards();
    }

    private void ConstructCards()
    {
        foreach (VCards card in cards)
        {
            card.MyModel = new MCards();
            card.Presenter = this;
            card.gameObject.SetActive(false);
        }
    }

    public void GetCards()
    {
        foreach (VCards card in cards)
        {
            int rund = UnityEngine.Random.Range(0, Enum.GetValues(typeof(CardsType)).Length);
            card.MyModel.MyType = (CardsType)Enum.GetValues(typeof(CardsType)).GetValue(rund);
            card.gameObject.SetActive(true);
            card.UpgradeStat();
        }

        _gameManager.GamePause(false);
    }

    public void Upgrade(VCards chooseCard)
    {
        _player.CardsUpgrade(chooseCard.MyModel.MyType);

        foreach (VCards card in cards)
        {
            card.gameObject.SetActive(false);
        }

        _gameManager.GamePause(true);
    }
}

public enum CardsType
{
    New,
    Joker,
    Damage,
    Penetration,
    Double,
    Lazer,
    Cooldown
}