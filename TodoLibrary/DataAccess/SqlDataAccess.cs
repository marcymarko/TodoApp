using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TodoLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionStringName)
    {
        connectionStringName = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionStringName);

        var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

        return rows.ToList();
    }

    public async Task SaveData<T>(string storedProcedure,
        T parameters,
        string connectionStringName)
    {
        connectionStringName = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionStringName);

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }


}
