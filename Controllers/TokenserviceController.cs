using Microsoft.AspNetCore.Mvc;
namespace server_comp.Controllers;
using server_comp.models;

using Microsoft.Data.Sqlite;


[ApiController]
[Route("[controller]")]
public class TokenserviceController : ControllerBase
{




    [HttpGet(Name = "GetToken")]
    public Tokenresponse Get(Tokenmessage tm)
    {
        try{
            DB db = new();
            var token = db.getToken(ref tm);   
            if(token.state == status.NOK && token.reason == causes.NO_USER_IN_DB){
                return new Tokenresponse{message="Der User existiert nicht oder das Passwort ist falsch"};
            }
            return new Tokenresponse{token=token.value,message="Anlage des Tokens war erfolgreich,"};
        }
        catch(SqliteException){
            //handle db connection error
            //potentialy logging the error
            return new Tokenresponse{message = "Beim Zugriff auf die DB kam es zu einem Problem. Bitte kontakitieren Sie den Support."};
        }        
    }

}
