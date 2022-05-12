using codetest.Controllers;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.Extensions.Logging;

namespace codetest.Services;


public interface ICheeseyDataService
{
    Task<IEnumerable<Cheese>> ListProducts();

    Task<List<CheeseOrder>> ListOrder();

    Task<bool> AddToOrder(CheeseOrder cheese);

    Task<bool> UpdateOrder(CheeseOrder list);

    Task<bool> DeleteOrder(CheeseOrder list);
}

public class CheeseyDataService : ICheeseyDataService
{
    CheeseOrder fakeDatabase = new CheeseOrder();
    private readonly ILogger<CheeseyDataService> _logger;
    private const string connectionString = "Data Source=codetest.db";
    public CheeseyDataService(ILogger<CheeseyDataService> logger)
    {
        _logger = logger;
        Task.Run(EnsureDb);
    }

    async Task EnsureDb()
    {
        // somewhere to store the order
        _logger.LogInformation("Ensuring database exists at connection string '{connectionString}'", connectionString);

        using var con = new SqliteConnection(connectionString);
        var sql = $@"CREATE TABLE IF NOT EXISTS CheeseOrder (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  CheeseId INTEGER NOT NULL,
                  Amount FLOAT NOT NULL,
                  Cost FLOAT NOT NULL
                 );";
        await con.ExecuteAsync(sql);
    }

    public async Task<IEnumerable<Cheese>> ListProducts()
    {
        List<Cheese> list = new List<Cheese>();
        // Lets pretend this is a DB call
        await Task.Run(() =>
        {
            list = new List<Cheese>
        {
            new Cheese{
                Id= 1,
                Name="Cheddar",
                PricePerKilo= 10.0,
                Colour="Yellow",
                Image="images\\cheddar.jpeg"
            },
            new Cheese{
                Id= 2,
                Name="Gorgonzola",
                Colour="Green/Blue",
                PricePerKilo= 1.0,
                Image="images\\Gorgonzola.jpeg"
            },
            new Cheese{
                Id= 3,
                Name="Gouda",
                PricePerKilo= 2.0,
                Colour="Yellow",
                Image="images\\Gouda.jpeg"
            },
            new Cheese{
                Id= 4,
                Name="Mozzarella",
                PricePerKilo= 4.0,
                Colour="White",
                Image="images\\Mozzarella.jpeg"
            },
            new Cheese{
                Id= 5,
                Name="Provolone",
                PricePerKilo= 8.0,
                Colour="Light Yellow",
                Image="images\\Provolone.jpeg"
            },
        };
        });

        return list;
    }

    // REST functions

    public async Task<List<CheeseOrder>> ListOrder()
    {
        using var con = new SqliteConnection(connectionString);
        var sql = $@"SELECT * FROM CheeseOrder;";
        var result = await con.QueryAsync<CheeseOrder>(sql);
        return result.ToList();
    }

    public async Task<bool> AddToOrder(CheeseOrder cheese)
    {
        if (cheese.CheeseId < 1 || cheese.Amount < 1) return false;
        using var con = new SqliteConnection(connectionString);
        var sql = $@"INSERT into CheeseOrder (CheeseId, Amount, Cost) VALUES (@cid, @amount, @cost);";
        var param = new { cid = cheese.CheeseId, amount = cheese.Amount, cost = cheese.Cost };
        return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> UpdateOrder(CheeseOrder cheese)
    {
        using var con = new SqliteConnection(connectionString);
        var sql = $@"UPDATE CheeseOrder set Amount=@amount, Cost= @cost WHERE Id = @oid;";
        var param = new { oid = cheese.Id, amount = cheese.Amount, cost = cheese.Cost };
        return await con.ExecuteAsync(sql, param) > 0;
    }

    public async Task<bool> DeleteOrder(CheeseOrder cheese)
    {
        using var con = new SqliteConnection(connectionString);
        var sql = $@"DELETE FROM CheeseOrder WHERE Id = @oid;";
        var param = new { oid = cheese.Id };
        return await con.ExecuteAsync(sql, param) > 0;
    }
}