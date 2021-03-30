using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
using project_back_end_foureach.Models;


public class ChatRepository : BaseRepository, IRepository<ChatMessage>
{

    public ChatRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<ChatMessage>> GetAll()
    {
        using var connection = CreateConnection();
        IEnumerable<ChatMessage> chats = await connection.QueryAsync<ChatMessage>("SELECT * FROM Chats;");
        return chats;
    }


    public async Task Delete(long id)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Chats WHERE Id = @Id;", new { Id = id });
    }

    public async Task<ChatMessage> Get(long id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ChatMessage>("SELECT * FROM Chats WHERE Id = @Id;", new { Id = id });
    }


    public async Task<ChatMessage> Insert(ChatMessage chatMessage)
    {
        using var connection = CreateConnection();
        System.Console.WriteLine(chatMessage.User_info);
        return await connection.QuerySingleAsync<ChatMessage>("INSERT INTO Chats (User_info, Message) VALUES (@User_info, @Message) RETURNING *;", chatMessage);
    }
}