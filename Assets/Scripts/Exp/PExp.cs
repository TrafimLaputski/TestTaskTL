using UnityEngine;
using Zenject;

public class PExp : MonoBehaviour
{
    private PCards _card = null;
    [SerializeField] private VExp _view = null;

    private MExp _model = new MExp();

    [Inject]
    private void Construct(PCards card)
    {
        _card = card;
    }

    private void Start()
    {
        _view.Presenter = this;
        _view.MyModel = _model;
    }

    public void AddExp(int expValue)
    {
        _model.CurrentExp += expValue;
        _view.Upgrade();
    }

    public void Upgrade()
    {
        _model.CurrentExp = 0;
        _model.CurrentLevel++;
        _model.NeedExp *= 2;

        _card.GetCards();
    }
}
