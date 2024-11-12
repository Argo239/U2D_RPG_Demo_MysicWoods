//using System.Collections.Generic;
//using System.Linq;

//public class StatsModifier {
//    private readonly List<StatModifier> listModifiers = new();
//    private readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();

//    public void PerformQuery(object sender, Query query) {
//        if (!modifiersCache.ContainsKey(query.StatType)) {
//            modifiersCache[query.StatType] = listModifiers.Where(mod => mod.StatType == query.StatType).ToList();
//        }
//        foreach (var mod in modifiersCache[query.StatType]) {
//            query.BaseValue = mod.Strategy.Calculate(query.BaseValue);
//        }
//    }

//    public void AddModifier(StatModifier modifier) {
//        listModifiers.Add(modifier);
//        modifiersCache.Remove(modifier.StatType);
//    }

//    public void RemoveModifier(StatModifier modifier) {
//        listModifiers.Remove(modifier);
//        modifiersCache.Remove(modifier.StatType);
//    }
//}

//using System;

//public enum StatType { AttackStat, DefenseStat, HealthStat }

//public class StatModifier {
//    public StatType StatType { get; }
//    public IOperationStrategy Strategy { get; }

//    public StatModifier(StatType type, IOperationStrategy strategy) {
//        StatType = type;
//        Strategy = strategy;
//    }
//}

//public class Query {
//    public StatType StatType { get; }
//    public int BaseValue;

//    public Query(StatType statTypes, int value) {
//        StatType = statTypes;
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
//    PlayerStats playerStats = new PlayerStats(20, 15, 100, 1, 0);
//    Debug.Log($"Player Initial Stats: {playerStats}");

//    // 创建敌人属性
//    EnemyBaseStats goblinStats = new EnemyBaseStats(10, 8, 30, "Goblin");
//    Debug.Log($"Goblin Initial Stats: {goblinStats}");

//    // 玩家获得临时攻击增益
//    StatModifier attackBuff = new StatModifier(StatType.AttackStat, new AddOperation(10));
//    mediator.AddModifier(attackBuff);

//    // 调用属性值时，将增益应用到当前状态
//    var playerQuery = new Query(StatType.AttackStat, playerStats.AttackStat);
//    mediator.PerformQuery(playerStats, playerQuery);
//    Debug.Log($"Player AttackStat with Buff: {playerQuery.BaseValue}");

//    // 敌人应用难度系数
//    goblinStats.ApplyDifficultyMultiplier(1.2f);
//    Debug.Log($"Goblin Stats with Difficulty Multiplier: {goblinStats}");
//}


//using Assets.Scripts.Stats.StatsOperation;
//using Mono.Cecil;
//using System;
//using System.Collections.Generic;

//public class StatValue {
//    public string StatType { get; }
//    public float BaseValue { get; private set; }
//    public float CurrentValue { get; private set; }

//    public StatValue(string type, float baseValue) {
//        StatType = type;
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
//    private readonly List<StatModifier> listModifiers = new();

//    public float ApplyModifiers(StatValue baseStat) {
//        var statModifiers = listModifiers.Where(m => m.StatType == baseStat.StatType);

//        float flatIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Flat)
//            .Sum(mod => mod.Value);

//        float percentageIncrease = statModifiers
//            .Where(mod => mod.ModifierType == ModifierType.Percentage)
//            .Sum(mod => mod.Value);

//        return (baseStat.BaseValue + flatIncrease) * (1 + percentageIncrease);
//    }

//    public void AddModifier(StatModifier modifier) {
//        listModifiers.Add(modifier);
//    }

//    public void RemoveModifier(StatModifier modifier) {
//        listModifiers.Remove(modifier);
//    }
//}

//public class AttackStat {
//    public StatValue AttackStat { get; }

//    public AttackStat(float baseAttack) {
//        AttackStat = new StatValue("AttackStat", baseAttack);
//    }

//    // 升级带来的永久增益
//    public void ApplyLevelUpBonus(float amount) {
//        AttackStat.ApplyPermanentGain(amount);
//    }

//    public float GetModifiedAttack(StatsModifier mediator) {
//        return AttackStat.GetCurrentValue(mediator);
//    }
//}


//public class StatExample {
//    public void TestAttackWithModifiersAndPermanentGain() {
//        var mediator = new StatsModifier();

//        var attackStat = new AttackStat(50);

//        // 升级时增加10点攻击力（永久性增益）
//        attackStat.ApplyLevelUpBonus(10);

//        // 添加临时增益
//        var percentModifier = new StatModifier(StatTypeRegistry.GetStatType("AttackStat"), ModifierType.Percentage, 0.2f);
//        mediator.AddModifier(percentModifier);

//        Console.WriteLine($"Modified AttackStat: {attackStat.GetModifiedAttack(mediator)}");  // 应该显示72
//    }
//}







//using Assets.Scripts.Stats.StatsOperation;
//using System;
//using System.Collections.Generic;

//public class StatValue {
//    public StatType Type { get; }
//    public float BaseValue { get; private set; }
//    public float CurrentValue { get; private set; }

//    public StatValue(StatType type, float baseValue) {
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
//    public float GetCurrentValue(StatsMediator mediator) {
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

//public class StatModifier : IDisposable {
//    public StatType Type { get; }
//    public ModifierType ModifierType { get; }
//    public float Value { get; }

//    public event Action<StatModifier> OnDispose = delegate { };

//    public StatModifier(StatType type, ModifierType modifierType, float value) {
//        Type = type;
//        ModifierType = modifierType;
//        Value = value;
//    }

//    public void Dispose() => OnDispose.Invoke(this);
//}

//public class Query {
//    public readonly StatType StatType;
//    public readonly float BaseValue;
//    public float Value;

//    public Query(StatType statType, float baseValue) {
//        StatType = statType;
//        BaseValue = baseValue;
//        Value = baseValue;  // 初始值为基础值
//    }
//}

//public class StatsMediator {
//    private readonly List<StatModifier> listModifiers = new();
//    private readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();

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

//    public void AddModifier(StatModifier modifier) {
//        listModifiers.Add(modifier);
//    }

//    public void RemoveModifier(StatModifier modifier) {
//        listModifiers.Remove(modifier);
//    }

//    private void InvalidateCache(StatType statType) => modifiersCache.Remove(statType);
//}

//using System.Collections.Generic;
//using System.Linq;

//public class StatsMediator {
//    private readonly List<StatModifier> listModifiers = new();
//    private readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();

//    public void PerformQuery(Query query) {
//        if (!modifiersCache.ContainsKey(query.StatType)) {
//            modifiersCache[query.StatType] = listModifiers.Where(mod => mod.Type == query.StatType).ToList();
//        }
//        foreach (var mod in modifiersCache[query.StatType]) {
//            query.Value = mod.Strategy.Calculate(query.Value);
//        }
//    }

//    private void InvalidateCache(StatType statType) => modifiersCache.Remove(statType);

//    public void AddModifier(StatModifier modifier) {
//        listModifiers.Add(modifier);
//        InvalidateCache(modifier.Type);
//        modifier.OnDispose += _ => InvalidateCache(modifier.Type);
//        modifier.OnDispose += _ => listModifiers.Remove(modifier);
//    }

//    public void Update(float deltaTime) {
//        foreach (var modifier in listModifiers) {
//            modifier.Update(deltaTime);
//        }
//        listModifiers.RemoveAll(mod => mod.MarkedForRemoval);
//    }
//}


//public class AttackStat {
//    public StatValue AttackStat { get; }

//    public AttackStat(float baseAttack) {
//        AttackStat = new StatValue("AttackStat", baseAttack);
//    }

//    升级带来的永久增益
//    public void ApplyLevelUpBonus(float amount) {
//        AttackStat.ApplyPermanentGain(amount);
//    }

//    public float GetModifiedAttack(StatsMediator mediator) {
//        return AttackStat.GetCurrentValue(mediator);
//    }
//}

//public class StatExample {
//    public void TestAttackWithModifiersAndPermanentGain() {
//        var mediator = new StatsMediator();

//        var attackStat = new AttackStat(50);

//        升级时增加10点攻击力（永久性增益）
//        attackStat.ApplyLevelUpBonus(10);

//        添加临时增益
//       var percentModifier = new StatModifier(StatTypeRegistry.GetStatType("AttackStat"), ModifierType.Percentage, 0.2f);
//        mediator.AddModifier(percentModifier);

//        Console.WriteLine($"Modified AttackStat: {attackStat.GetModifiedAttack(mediator)}");  // 应该显示72
//    }
//}