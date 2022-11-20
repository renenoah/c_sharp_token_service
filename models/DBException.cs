namespace server_comp.models;

public class DBException : Exception{
    public causes reason;
    public DBException(causes reason = causes.NONE){
        this.reason = reason;
    }
}

