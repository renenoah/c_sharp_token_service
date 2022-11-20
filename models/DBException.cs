namespace server_comp.models;
public enum causes{
    NO_USER_IN_DB,
    NONE
}
public class DBException : Exception{
    public causes reason;
    public DBException(causes reason = causes.NONE){
        this.reason = reason;
    }
}

