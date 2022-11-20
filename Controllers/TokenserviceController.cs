using Microsoft.AspNetCore.Mvc;
namespace server_comp.Controllers;
using server_comp.models;

[ApiController]
[Route("[controller]")]
public class TokenserviceController : ControllerBase
{




    [HttpGet(Name = "GetToken")]
    public Tokenresponse Get(Tokenmessage tm)
    {
        try{
            DB db = new();
            string token = db.getToken(ref tm);     
            return new Tokenresponse{token=token,message="Anlage des Tokens warerfolgreich,"}       ;
        }
        catch(DBException){
            //handle db connection error
            //potentialy logging the error
            return new Tokenresponse{message = "Beim Zugriff auf die DB kam es zu einem Problem. Bitte kontakitieren Sie den Support."};
        }
        return new Tokenresponse{token = "hallo"};
    }

}
