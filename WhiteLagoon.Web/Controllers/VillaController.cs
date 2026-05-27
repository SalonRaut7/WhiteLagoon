using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers;

public class VillaController : Controller
{
    private readonly ApplicationDbContext _db;  
    public VillaController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var villas = _db.Villas.ToList();
        return View(villas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Villa obj) //when form is posted,we will get the Villa object with the data filled in the form, as the create.cshtml form is binded to the Villa model
    {
        if(obj.Name == obj.Description) //custom validation to check if name and description are same, we can add more such custom validations as needed
        {
            ModelState.AddModelError("", "Description cannot be same as Name"); // this will add a model error that can be displayed in the view using validation summary, the first parameter is the key for the error, we can use it to display error for specific field, but here we are adding a general error not specific to any field, so we pass empty string as key
        }
        if(ModelState.IsValid)
        {
            _db.Villas.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Villa created successfully"; 
            return RedirectToAction("Index");  
        }
        return View(obj); //if model state is not valid, we return the same view with the object to show validation errors
    }

    [HttpGet]
    public IActionResult Update(int villaId)
    {
        Villa? obj = _db.Villas.FirstOrDefault(u=>u.Id==villaId);
        if(obj is null)
        {
            return RedirectToAction("Error","Home");
        }
        return View(obj);
    }

    [HttpPost]
    public IActionResult Update(Villa obj)
    {
        if(ModelState.IsValid)
        {
            _db.Villas.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Villa updated successfully";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Error while updating villa";
        return View();
    }

    [HttpGet]
    public IActionResult Delete(int villaId)
    {
        Villa? obj = _db.Villas.FirstOrDefault(u=>u.Id==villaId);
        if(obj is null)
        {
            return RedirectToAction("Error","Home");
        }
        return View(obj);
    }

    [HttpPost]
    public IActionResult Delete(Villa obj)
    {
        Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
        if(objFromDb is not null)
        {
            _db.Villas.Remove(objFromDb);
            _db.SaveChanges();
            TempData["success"] = "Villa deleted successfully"; // we can use TempData to pass a success message to the view, which will be displayed after redirection, TempData is a dictionary that is used to store data that needs to be available for the next request, it is stored in session and is available for only one request, after that it is automatically removed, we can use it to show success or error messages after redirection
            return RedirectToAction("Index");
        }
        TempData["error"] = "Error while deleting villa"; // if objFromDb is null, it means there was an error while deleting the villa, we can set an error message in TempData to show in the view
        return View();
    }
}
