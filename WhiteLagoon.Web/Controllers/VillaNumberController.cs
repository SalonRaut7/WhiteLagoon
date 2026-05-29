using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers;

public class VillaNumberController : Controller
{
    private readonly ApplicationDbContext _db;  
    public VillaNumberController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var villaNumbers = _db.VillaNumbers.Include(v => v.Villa).ToList();
        return View(villaNumbers);
    }

    public IActionResult Create()
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = _db.Villas.ToList().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            })
        };
        // IEnumerable<SelectListItem> villaList = _db.Villas.ToList().Select(v => new SelectListItem
        // {
        //     Text = v.Name,
        //     Value = v.Id.ToString()
        // });
        // ViewData["VillaList"] = villaList; //we can also use ViewData to pass the list of villas to the view, but we need to cast it to the specific type in the view, so we will use ViewBag for this purpose.
        // ViewBag.VillaList = villaList; //we are passing the list of villas to the view using ViewBag, we can also use ViewData or TempData for this purpose, but ViewBag is more convenient to use as it is dynamic and we don't need to cast it to the specific type in the view, we can directly use it as a list of SelectListItem in the view, we will use this list to populate the dropdown in the create view for villa number.
        return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Create(VillaNumberVM vm) 
    {
        // ModelState.Remove("Villa"); //removing the validation for the navigation property as it is not being posted from the form, we only need to post the VillaId which is the foreign key, so we remove the validation for the Villa navigation property, this field can also be not validated using data annotations [ValidateNever] which is did in the model itself.
        bool villaNumberExists = _db.VillaNumbers.Any(v => v.Villa_Number == vm.VillaNumber.Villa_Number);
        if(ModelState.IsValid && !villaNumberExists)
        {
            _db.VillaNumbers.Add(vm.VillaNumber);
            _db.SaveChanges();
            TempData["success"] = "Villa number created successfully"; 
            return RedirectToAction(nameof(Index));  
        }
        if(villaNumberExists)
        {
            TempData["error"] = "Villa number already exists";
        }
        vm.VillaList = _db.Villas.ToList().Select(v => new SelectListItem
        {
            Text = v.Name,
            Value = v.Id.ToString()
        });
        return View(vm); 
    }

    [HttpGet]
    public IActionResult Update(int villaNumber)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = _db.Villas.ToList().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }),
            VillaNumber = _db.VillaNumbers.FirstOrDefault(u=>u.Villa_Number==villaNumber)

        };
        if(villaNumberVM.VillaNumber is null)
        {
            return RedirectToAction("Error","Home");
        }
        return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Update(VillaNumberVM vm)
    {
        if(ModelState.IsValid)
        {
            _db.VillaNumbers.Update(vm.VillaNumber);
            _db.SaveChanges();
            TempData["success"] = "Villa number updated successfully"; 
            return RedirectToAction(nameof(Index));  
        }
        vm.VillaList = _db.Villas.ToList().Select(v => new SelectListItem
        {
            Text = v.Name,
            Value = v.Id.ToString()
        });
        return View(vm); 
    }

    [HttpGet]
    public IActionResult Delete(int villaNumber)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = _db.Villas.ToList().Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            }),
            VillaNumber = _db.VillaNumbers.FirstOrDefault(u=>u.Villa_Number==villaNumber)

        };
        if(villaNumberVM.VillaNumber is null)
        {
            return RedirectToAction("Error","Home");
        }
        return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumberVM vm)
    {
        VillaNumber? objFromDb = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == vm.VillaNumber.Villa_Number);
        if(objFromDb is not null)
        {
            _db.VillaNumbers.Remove(objFromDb);
            _db.SaveChanges();
            TempData["success"] = "Villa number deleted successfully"; 
            return RedirectToAction(nameof(Index));
        }
        TempData["error"] = "Villa number could not be deleted"; 
        return View();
    }
}
