using UnityEngine;

public class CultAnimationController : MonoBehaviour
{
    public enum AnimType { Idle, Talk, Walk};
    public AnimType animType;
    [SerializeField] Animator mAnimator;
    public string[] animParamNames = { "Talk", "Walk" };

    public void CallAnimation(int typeIndex)
    {
        if (typeIndex == 0)
        {
            PlayAnimation(AnimType.Idle);
        }
        else if (typeIndex == 1)
        {
            PlayAnimation(AnimType.Talk);
        }
        else 
        {
            PlayAnimation(AnimType.Walk);
        }
    }

    public void PlayAnimation(AnimType _type)
    {
        switch (_type)
        {
            case AnimType.Idle: PlayAnimationIdle(); break;
            case AnimType.Talk: PlayAnimationTalk(); break;
            case AnimType.Walk: PlayAnimationWalk(); break;
        }
    }

    private void PlayAnimationIdle()
    {
        mAnimator.SetBool(animParamNames[0], false);
        mAnimator.SetBool(animParamNames[1], false);
    }

    private void PlayAnimationTalk()
    {
        mAnimator.SetBool(animParamNames[1], false);
        mAnimator.SetBool(animParamNames[0], true);
    }

    private void PlayAnimationWalk()
    {
        mAnimator.SetBool(animParamNames[0], false);
        mAnimator.SetBool(animParamNames[1], true);
    }

    public void AnimationInitialized()
    {
        PlayAnimationIdle();
    }
}
