namespace server_comp.models;
using Microsoft.Data.Sqlite;
using System.Runtime;

public class DB {
    private const string PATH = "data\\users.db";
    SqliteConnection conn;
    private void open(){
        try {
            var test = string.Format("Data Source={0}",PATH);
            conn = new SqliteConnection(test);
            conn.Open();            
        }
        catch(SqliteException){
            throw new DBException();
        }
    }
    //Sanitzing the string for the db
    private string sanitize_strings(string input){
        return input.Trim().Remove(';');
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
    private void check_if_user_exists(ref Tokenmessage tm){
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
        
        if(command.ExecuteScalar() == null){
            conn.Close();
            throw new DBException(causes.NO_USER_IN_DB);
        }
        return;
    }
    public string getToken(ref Tokenmessage tm){
        //Check if all needed info are there
        check_if_user_exists(ref tm);
        return create_Token();
    }

    private void close(){
        conn.Close();
    }

    public DB(){
        open();
    }
}