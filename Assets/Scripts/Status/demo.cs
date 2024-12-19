//using System.Collections.Generic;
//using System.Linq;

//public class StatsModifier {
//    private readonly List<StatusModifier> listModifiers = new();
//    private readonly Dictionary<StatusType, IEnumerable<StatusModifier>> modifiersCache = new();

//    public void PerformQuery(object sender, Query query) {
//        if (!modifiersCache.ContainsKey(query.StatusType)) {
//            modifiersCache[query.StatusType] = listModifiers.Where(mod => mod.StatusType == query.StatusType).ToList();
//        }
//        foreach (var mod in modifiersCache[query.StatusType]) {
//            query.BaseValue = mod.Strategy.Calculate(query.BaseValue);
//        }
//    }

//    public void AddModifier(StatusModifier _statusModifier) {
//        listModifiers.Add(_statusModifier);
//        modifiersCache.Remove(_statusModifier.StatusType);
//    }

//    public void RemoveModifier(StatusModifier _statusModifier) {
//        listModifiers.Remove(_statusModifier);
//        modifiersCache.Remove(_statusModifier.StatusType);
//    }
//}

//using System;

//public enum StatusType { AttackStatus, DefenseStatus, HealthStatus }

//public class StatusModifier {
//    public StatusType StatusType { get; }
//    public IOperationStrategy Strategy { get; }

//    public StatusModifier(StatusType type, IOperationStrategy strategy) {
//        StatusType = type;
//        Strategy = strategy;
//    }
//}

//public class Query {
//    public StatusType StatusType { get; }
//    public int BaseValue;

//    public Query(StatusType statusTypes, int value) {
//        StatusType = statusTypes;
//        BaseValue = value;
//    }
//}

//public interface IOperationStrategy {
//    int Calculate(int baseValue);
//}

//public class AddOperation : IOperationStrategy {
//    private readonly int modifierValue;
//    public AddOperation(int modifierValue) {
//        this.modifierValue = modifierValue;
//    }

//    public int Calculate(int baseValue) => baseValue + modifierValue;
//}

//void Start() {
//    StatsModifier mediator = new StatsModifier();

//    // 创建玩家属性
//    PlayerStatus attributes = new PlayerStatus(20, 15, 100, 1, 0);
//    Debug.Log($"Player Initial PlayerAttributesInitData: {attributes}");

//    // 创建敌人属性
//    EnemyBaseStats goblinStats = new EnemyBaseStats(10, 8, 30, "Goblin");
//    Debug.Log($"Goblin Initial PlayerAttributesInitData: {goblinStats}");

//    // 玩家获得临时攻击增益
//    StatusModifier attackBuff = new StatusModifier(StatusType.AttackStatus, new AddOperation(10));
//    mediator.AddModifier(attackBuff);

//    // 调用属性值时，将增益应用到当前状态
//    var playerQuery = new Query(StatusType.AttackStatus, attributes.AttackStatus);
//    mediator.PerformQuery(attributes, playerQuery);
//    Debug.Log($"Player AttackStatus with Buff: {playerQuery.BaseValue}");

//    // 敌人应用难度系数
//    goblinStats.ApplyDifficultyMultiplier(1.2f);
//    Debug.Log($"Goblin PlayerAttributesInitData with Difficulty Multiplier: {goblinStats}");
//}


//using Assets.Scripts.PlayerAttributesInitData.StatsOperation;
//using Mono.Cecil;
//using System;
//using System.Collections.Generic;

//public class StatusValue {
//    public string StatusType { get; }
//    public float BaseValue { get; private set; }
//    public float CurrentValue { get; private set; }

//    public StatusValue(string type, float baseValue) {
//        StatusType = type;
//        BaseValue = baseValue;
//        CurrentValue = baseValue;
//    }

//    // 用于升级后的永久增益
//    public void ApplyPermanentGain(float amount) {
//        BaseValue += amount;
//        CurrentValue = BaseValue;
//    }

//    // 获取当前值（包含临时增益）
//    public float GetCurrentValue(StatsModifier mediator) {
//        return mediator.ApplyModifiers(this);  // 向mediator查询临时增益
//    }
//}
//public class StatsModifier {
//    private readonly List<StatusModifier> listModifiers = new();

//    public float ApplyModifiers(StatusValue baseStat) {
//        var statModifiers = listModifiers.Where(m => m.StatusType == baseStat.StatusType);

//        float flatIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Flat)
//            .Sum(mod => mod.Value);

//        float percentageIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Percentage)
//            .Sum(mod => mod.Value);

//        return (baseStat.BaseValue + flatIncrease) * (1 + percentageIncrease);
//    }

//    public void AddModifier(StatusModifier _statusModifier) {
//        listModifiers.Add(_statusModifier);
//    }

//    public void RemoveModifier(StatusModifier _statusModifier) {
//        listModifiers.Remove(_statusModifier);
//    }
//}

//public class AttackStatus {
//    public StatusValue AttackStatus { get; }

//    public AttackStatus(float baseAttack) {
//        AttackStatus = new StatusValue("AttackStatus", baseAttack);
//    }

//    // 升级带来的永久增益
//    public void ApplyLevelUpBonus(float amount) {
//        AttackStatus.ApplyPermanentGain(amount);
//    }

//    public float GetModifiedAttack(StatsModifier mediator) {
//        return AttackStatus.GetCurrentValue(mediator);
//    }
//}


//public class StatExample {
//    public void TestAttackWithModifiersAndPermanentGain() {
//        var mediator = new StatsModifier();

//        var attackStat = new AttackStatus(50);

//        // 升级时增加10点攻击力（永久性增益）
//        attackStat.ApplyLevelUpBonus(10);

//        // 添加临时增益
//        var percentModifier = new StatusModifier(StatTypeRegistry.GetStatType("AttackStatus"), ModifierType.Percentage, 0.2f);
//        mediator.AddModifier(percentModifier);

//        Console.WriteLine($"Modified AttackStatus: {attackStat.GetModifiedAttack(mediator)}");  // 应该显示72
//    }
//}







//using Assets.Scripts.PlayerAttributesInitData.StatsOperation;
//using System;
//using System.Collections.Generic;

//public class StatusValue {
//    public StatusType Type { get; }
//    public float BaseValue { get; private set; }
//    public float CurrentValue { get; private set; }

//    public StatusValue(StatusType type, float baseValue) {
//        Type = type;
//        BaseValue = baseValue;
//        CurrentValue = baseValue;
//    }

//    用于升级后的永久增益
//    public void ApplyPermanentGain(float amount) {
//        var query = new Query(Type, BaseValue);
//        mediator.PerformQuery(query);
//        CurrentValue = query.Value;  // 更新当前值
//        return CurrentValue;
//    }

//    获取当前值（包含临时增益）
//    public float GetCurrentValue(StatusMediator mediator) {
//        return mediator.ApplyModifiers(this);  // 向mediator查询临时增益
//    }
//}

//public interface IOperationStrategy {
//    float Calculate(float baseValue);
//}

//public class AdditiveStrategy : IOperationStrategy {
//    private readonly float amount;
//    public AdditiveStrategy(float amount) => this.amount = amount;
//    public float Calculate(float baseValue) => baseValue + amount;
//}

//public class MultiplicativeStrategy : IOperationStrategy {
//    private readonly float amount;
//    public MultiplicativeStrategy(float amount) => this.amount = amount;
//    public float Calculate(float baseValue) => baseValue * amount;
//}

//public enum ModifierType {
//    Flat,
//    Percentage
//}

//public class StatusModifier : IDisposable {
//    public StatusType Type { get; }
//    public ModifierType ModifierType { get; }
//    public float Value { get; }

//    public event Action<StatusModifier> OnDispose = delegate { };

//    public StatusModifier(StatusType type, ModifierType modifierType, float value) {
//        Type = type;
//        ModifierType = modifierType;
//        Value = value;
//    }

//    public void Dispose() => OnDispose.Invoke(this);
//}

//public class Query {
//    public readonly StatusType StatusType;
//    public readonly float BaseValue;
//    public float Value;

//    public Query(StatusType statType, float baseValue) {
//        StatusType = statType;
//        BaseValue = baseValue;
//        Value = baseValue;  // 初始值为基础值
//    }
//}

//public class StatusMediator {
//    private readonly List<StatusModifier> listModifiers = new();
//    private readonly Dictionary<StatusType, IEnumerable<StatusModifier>> modifiersCache = new();

//    public float PerformQuery(Query query) {
//        var statModifiers = listModifiers.Where(m => m.Type == baseStat.Type);

//        float flatIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Flat)
//            .Sum(mod => mod.Value);

//        float percentageIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Percentage)
//            .Sum(mod => mod.Value);

//        return (baseStat.BaseValue + flatIncrease) * (1 + percentageIncrease);
//    }

//    public void AddModifier(StatusModifier _statusModifier) {
//        listModifiers.Add(_statusModifier);
//    }

//    public void RemoveModifier(StatusModifier _statusModifier) {
//        listModifiers.Remove(_statusModifier);
//    }

//    private void InvalidateCache(StatusType statType) => modifiersCache.Remove(statType);
//}

//using System.Collections.Generic;
//using System.Linq;

//public class StatusMediator {
//    private readonly List<StatusModifier> listModifiers = new();
//    private readonly Dictionary<StatusType, IEnumerable<StatusModifier>> modifiersCache = new();

//    public void PerformQuery(Query query) {
//        if (!modifiersCache.ContainsKey(query.StatusType)) {
//            modifiersCache[query.StatusType] = listModifiers.Where(mod => mod.Type == query.StatusType).ToList();
//        }
//        foreach (var mod in modifiersCache[query.StatusType]) {
//            query.Value = mod.Strategy.Calculate(query.Value);
//        }
//    }

//    private void InvalidateCache(StatusType statType) => modifiersCache.Remove(statType);

//    public void AddModifier(StatusModifier _statusModifier) {
//        listModifiers.Add(_statusModifier);
//        InvalidateCache(_statusModifier.Type);
//        _statusModifier.OnDispose += _ => InvalidateCache(_statusModifier.Type);
//        _statusModifier.OnDispose += _ => listModifiers.Remove(_statusModifier);
//    }

//    public void Update(float deltaTime) {
//        foreach (var _statusModifier in listModifiers) {
//            _statusModifier.Update(deltaTime);
//        }
//        listModifiers.RemoveAll(mod => mod.MarkedForRemoval);
//    }
//}


//public class AttackStatus {
//    public StatusValue AttackStatus { get; }

//    public AttackStatus(float baseAttack) {
//        AttackStatus = new StatusValue("AttackStatus", baseAttack);
//    }

//    升级带来的永久增益
//    public void ApplyLevelUpBonus(float amount) {
//        AttackStatus.ApplyPermanentGain(amount);
//    }

//    public float GetModifiedAttack(StatusMediator mediator) {
//        return AttackStatus.GetCurrentValue(mediator);
//    }
//}

//public class StatExample {
//    public void TestAttackWithModifiersAndPermanentGain() {
//        var mediator = new StatusMediator();

//        var attackStat = new AttackStatus(50);

//        升级时增加10点攻击力（永久性增益）
//        attackStat.ApplyLevelUpBonus(10);

//        添加临时增益
//       var percentModifier = new StatusModifier(StatTypeRegistry.GetStatType("AttackStatus"), ModifierType.Percentage, 0.2f);
//        mediator.AddModifier(percentModifier);

//        Console.WriteLine($"Modified AttackStatus: {attackStat.GetModifiedAttack(mediator)}");  // 应该显示72
//    }
//}