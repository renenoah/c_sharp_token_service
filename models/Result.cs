namespace server_comp.models;

public enum causes{
    NO_USER_IN_DB,
    NONE
}
public enum status{
    OK,
    NOK
}
public class Result<T>{
    public Exception? e;
    public status state;
    public causes reason;
    public T? value;
}