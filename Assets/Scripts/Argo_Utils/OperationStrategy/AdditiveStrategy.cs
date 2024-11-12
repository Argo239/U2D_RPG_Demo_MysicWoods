using Assets.Scripts.Interface;

public class AdditiveStrategy : IOperationStrategy {
    private readonly float amount;
    public AdditiveStrategy(float amount) => this.amount = amount;
    public float Calculate(float baseValue) => baseValue + amount;
}

