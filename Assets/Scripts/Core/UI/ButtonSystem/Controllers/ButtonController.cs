using UnityEngine;

public abstract class ButtonController : MonoBehaviour
{
    [SerializeField] protected ButtonModel model;
    [SerializeField] protected ButtonView view;

    protected virtual void Start()
    {
        view.Initialize(model);
        view.BindClick(OnButtonClicked);
    }

    protected abstract void OnButtonClicked();
}