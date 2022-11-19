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
        if(tm.date == "heute"){
            return new Tokenresponse{token = "date"};
        }
        return new Tokenresponse{token = "hallo"};
    }
}
