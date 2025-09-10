using System.Collections;
using UnityEngine;

public class Blue_AI : AI_Controller
{
    private bool _addedExtra;

    public override void Update()
    {
        base.Update();

        if (_isFollowing)
        {
            if (!_addedExtra)
            {
                StartCoroutine(ExtraSpeed());
            }
        }

        ChangeAddedExtraBool();
    }

    void ChangeAddedExtraBool()
    {
        if (!_isFollowing && _addedExtra)
        {
            _addedExtra = false;
        }
    }

    IEnumerator ExtraSpeed()
    {
        _addedExtra = true;
        _botAI.speed += 2;
        yield return new WaitForSeconds(2f);
        _botAI.speed -= 2;
    }
}
