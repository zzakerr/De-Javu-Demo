using UnityEngine;
using UnityEngine.UI;

public class InteractiveBoxUI : SingletonBase<InteractiveBoxUI>
{
    [SerializeField] private float showSpeed = 1f;
    [SerializeField]private Image iconPickUp;

    private bool _isEnable;
    private bool _isCursor;

    private void Awake()
    {
        Init();
        Disable();
    }

    private void Update()
    {
        if (_isCursor) iconPickUp.color = ChangeTransparent(iconPickUp.color,0.2f);
        else iconPickUp.color = ChangeTransparent(iconPickUp.color,0);
        
        if (_isEnable)
        {
            if (Mathf.Approximately(iconPickUp.color.a, 1f))enabled = false;
        }
        else
        {
            if (Mathf.Approximately(iconPickUp.color.a ,0))enabled = false;
        }
    }

    private void HideCursor()
    {
        _isCursor = false;
    }
    
    public void Enable()
    {
        _isCursor = true;
        _isEnable = true;
        enabled = true;
    }
    
    public void Disable()
    {
        HideCursor();
        _isEnable = false;
        enabled = true;
    }
    
    private Color ChangeTransparent(Color currentColor,float change)
    {
        currentColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.MoveTowards(currentColor.a, change, showSpeed * Time.deltaTime));
        return currentColor;
    }
}
