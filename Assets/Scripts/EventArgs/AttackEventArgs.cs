using System;

public class AttackEventArgs : EventArgs{
    public ComboStepData StepData;

    public AttackEventArgs(ComboStepData stepData) {
        StepData = stepData;
    }
}