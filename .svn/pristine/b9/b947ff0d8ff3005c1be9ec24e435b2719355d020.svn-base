using System.Data.Common;

namespace System.Data;

[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
public class EmulatedDbTransaction : DbTransaction
{
    private readonly EmulatedDbConnection _connection;
    private bool _completed;

    internal EmulatedDbTransaction(EmulatedDbConnection connection, IsolationLevel isolationLevel)
    {
        _connection = connection;
        IsolationLevel = isolationLevel;
        TransactionId = Guid.NewGuid().ToString("N");
    }

    internal string TransactionId { get; }

    public override IsolationLevel IsolationLevel { get; }

    protected override DbConnection DbConnection => _connection;

    /// <summary>
    /// Commits the database transaction.
    /// </summary>
    public override void Commit()
    {
        // 中文注释：模拟事务提交，记录状态防止重复提交�?
        EnsureNotCompleted();
        _completed = true;
    }

    /// <summary>
    /// Rolls back a transaction from a pending state.
    /// </summary>
    public override void Rollback()
    {
        // 中文注释：模拟事务回滚，记录状态防止重复回滚�?
        EnsureNotCompleted();
        _completed = true;
    }

    private void EnsureNotCompleted()
    {
        if (_completed)
        {
            throw new InvalidOperationException("Transaction is already completed.");
        }
    }
}
