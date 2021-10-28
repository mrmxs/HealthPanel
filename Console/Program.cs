using System;
using Npgsql;

var cs = "Host=localhost;Username=postgres;Password=postgres;Database=test2";

using var con = new NpgsqlConnection(cs);
con.Open();

var sql = "SELECT name From todoitems;";

using var cmd = new NpgsqlCommand(sql, con);

var version = cmd.ExecuteScalar().ToString();
Console.WriteLine($"PostgreSQL version: {version}");

Console.ReadKey();