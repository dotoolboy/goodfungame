using System.Collections.Generic;

/// <summary>
///  DataLoad Interface
/// </summary>
/// <typeparam name="TKey">Data Types</typeparam>
/// <typeparam name="TValue">EnemyData.cs</typeparam>
public interface ILoadData<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeData();
}