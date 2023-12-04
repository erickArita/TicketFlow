using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketFlow.Common.Utils;
using TicketFlow.Core.Customer;
using TicketFlow.Core.Customer.Dtos;

using FireSharp.Interfaces;
using FireSharp.Config;
using TicketFlow.Entities;
using FireSharp.Response;


namespace TicketFlow.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MantenedorController : ControllerBase{

    IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "0r9YtAc3hn6vcB3wHcOrIOgG9PeqsZ5OCxo4s3M6",
        BasePath = "https://dbtesting-937f4-default-rtdb.firebaseio.com/"
    };

    IFirebaseClient client;

    //get student

    [HttpPost]
    public  ActionResult Create(Student student){
        
        try{
            addStudentToFirebase(student);
            ModelState.AddModelError(string.Empty, "siuuuuu");
        }catch(Exception ex){
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return Ok(new AplicationResponse<string>
        {
            Message = "si se pudooooo"
        });

    }

    private void addStudentToFirebase(Student student){
        client = new FireSharp.FirebaseClient(config);
        var data = student;
        PushResponse response = client.Push("Profesor/", data);
        data.student_id = response.Result.name;
        SetResponse esetRespons = client.Set("Profesor/"+data.student_id, data);
    }
}
