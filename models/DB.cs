namespace server_comp.models;
using Microsoft.Data.Sqlite;

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
        command.Parameters.AddWithValue("$password", tm.username);
        command.Parameters.AddWithValue("$username", tm.password);

        using var reader = command.ExecuteReader();
        if(!reader.HasRows){
            throw new DBException(causes.NO_USER_IN_DB);
        }
        reader.Read();
        string found = reader.GetString(0);
    }
    public void getToken(ref Tokenmessage tm){
        //Check if all needed info are there
        check_if_user_exists(ref tm);


    }

    private void close(){
        conn.Close();
    }

    public DB(){
        open();
    }
}