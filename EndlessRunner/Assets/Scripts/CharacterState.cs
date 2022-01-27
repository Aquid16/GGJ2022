using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTransition
{
    ToRunning,
    ToJumping,
    ToFlipping
}

public class CharacterState
{
    public virtual CharacterState HandleInput(PlayerController context, StateTransition transition)
    {
        return this;
    }
}

public class RunningState : CharacterState
{
    public override CharacterState HandleInput(PlayerController context, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.ToJumping:
                context.Jump();
                return new JumpingState();
            case StateTransition.ToFlipping:
                context.FlipAction();
                return new FlippingState();
            default:
                return this;
        }
    }
}

public class JumpingState : CharacterState
{
    public override CharacterState HandleInput(PlayerController context, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.ToRunning:
                return new RunningState();
            default:
                return this;
        }
    }
}

public class FlippingState : CharacterState
{
    public override CharacterState HandleInput(PlayerController context, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.ToRunning:
                return new RunningState();
            default:
                return this;
        }
    }
}
