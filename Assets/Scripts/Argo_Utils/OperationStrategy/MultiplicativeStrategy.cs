using Assets.Scripts.Interface;

public class MultiplicativeStrategy : IOperationStrategy {
    private readonly float amount;
    public MultiplicativeStrategy(float amount) => this.amount = amount;
    public float Calculate(float baseValue) => baseValue * amount;
}

