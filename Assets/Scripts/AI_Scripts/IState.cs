using UnityEngine;

public interface IState
{
    void Enter(AI_Controller _aiData);
    void Exit(AI_Controller _aiData);
    void Update(AI_Controller _aiData);
}
