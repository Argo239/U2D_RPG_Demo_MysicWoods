public enum OperatorType { Add, Multiply }

public interface IOperationStrategy {
    float Calculate(float amount);
    float GetAmount();
}

public class AddOperation : IOperationStrategy {
    private readonly float amount;
    public AddOperation(float amount) => this.amount = amount;
    public float Calculate(float baseValue) => baseValue + amount;
    public float GetAmount() => this.amount;
}

public class MultiplyOperation: IOperationStrategy {
    private readonly float amount;
    public MultiplyOperation(float amount) => this.amount = amount;
    public float Calculate(float baseValue) => baseValue * amount;
    public float GetAmount() => this.amount;
}