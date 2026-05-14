using System.Data.Common;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace System.Data;

[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
public abstract class EmulatedDbConnection : DbConnection
{
    private string _connectionString = string.Empty;
    private string _database = "Default";
    private ConnectionState _state = ConnectionState.Closed;

    protected EmulatedDbConnection()
    {
    }

    protected EmulatedDbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal IAdoDataForwarder? LocalForwarder { get; private set; }

    internal IAdoDataForwarder ResolveForwarder() => LocalForwarder ?? AdoEmulatorConfiguration.GetDefaultForwarder();

    internal void UseForwarder(IAdoDataForwarder forwarder)
    {
        LocalForwarder = forwarder;
    }

    public override string ConnectionString
    {
        get => _connectionString;
        set => _connectionString = value ?? string.Empty;
    }

    public override string Database => _database;
    public override string DataSource => "BlazorWasmAdoEmulator";
    public override string ServerVersion => "9.0.0";
    public override ConnectionState State => _state;

    /// <summary>
    /// Changes the current database for an open Connection object.
    /// </summary>
    public override void ChangeDatabase(string databaseName)
    {
        // 中文注释：模拟切库，仅修改本地状态�?
        if (!string.IsNullOrWhiteSpace(databaseName))
        {
            _database = databaseName;
        }
    }

    /// <summary>
    /// Opens a database connection with the property settings specified by the ConnectionString.
    /// </summary>
    public override void Open()
    {
        // 中文注释：WASM 端仅切换连接状态�?
        _state = ConnectionState.Open;
    }

    /// <summary>
    /// Closes the connection to the database.
    /// </summary>
    public override void Close()
    {
        _state = ConnectionState.Closed;
    }

    public override Task OpenAsync(CancellationToken cancellationToken)
    {
        Open();
        return Task.CompletedTask;
    }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        return new EmulatedDbTransaction(this, isolationLevel);
    }

    protected override DbCommand CreateDbCommand() => CreateProviderCommand();

    protected abstract DbCommand CreateProviderCommand();
}
