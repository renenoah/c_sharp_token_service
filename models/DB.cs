namespace server_comp.models;
using Microsoft.Data.Sqlite;
using System.Runtime;

public class DB {
    private const string PATH = "data\\users.db";
    SqliteConnection conn;
    private void open(){        
        var test = string.Format("Data Source={0}",PATH);
        conn = new SqliteConnection(test);
        conn.Open();  
    }
    private string create_Token(){
        Guid g = Guid.NewGuid();
        var command = conn.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO tokens VALUES($token,$created,$id)
        ";
        command.Parameters.AddWithValue("$token", g.ToString());
        command.Parameters.AddWithValue("$created", DateTime.UtcNow.ToString("s"));
        command.Parameters.AddWithValue("$id",10);
        command.ExecuteScalar();
        return g.ToString();

    }
    private bool check_if_user_exists(ref Tokenmessage tm){
        // string username = sanitize_strings(tm.username);
        // string password = sanitize_strings(tm.password);

        //Looking for User inside DB
        var command = conn.CreateCommand();
        command.CommandText =
        @"
            SELECT username
            FROM users
            WHERE password = $password and username= $username
        ";
        command.Parameters.AddWithValue("$username", tm.username);
        command.Parameters.AddWithValue("$password", tm.password);
        
        return command.ExecuteScalar() != null;
    }
    public Result<string> getToken(ref Tokenmessage tm){
        //Check if all needed info are there
        if(!check_if_user_exists(ref tm)){
            conn.Close();
            return new Result<string>{state=status.NOK,reason=causes.NO_USER_IN_DB};
        }
        return new Result<string>{state=status.OK,value=create_Token()};        
    }

    private void close(){
        conn.Close();
    }

    public DB(){
        open();
    }
}