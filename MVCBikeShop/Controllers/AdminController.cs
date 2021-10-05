using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCBikeShop.Controllers
{
    [Authorize(Roles = "Admin,MainAdmin")]
    public class AdminController : Controller
    {
        readonly BikeContext context;

        public AdminController(BikeContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(new Base64FilesViewModel { PhotosInBytes = context.Base64Files.ToList() });
        }
        //Goods____________________________________________________________________________________
        //public IActionResult EditGoods(BikeViewModel bike, SortType sortType = SortType.TitleAsc)
        //public IActionResult EditGoods(string priceTo, string priceFrom, string manufactury = "(all)", string type = "(all)", string wheelDiameters = "(all)", string material = "(all)", string speedAmount = "(all)", string size = "(all)", string breakType = "(all)", SortType sortType = SortType.TitleAsc)
        //{
        //    var selectListManufacturies = new List<string> { "(all)" };
        //    selectListManufacturies.AddRange(context.Manufacturies.Select(x => x.ManufacturyTitle));

        //    var selectListTypes = new List<string> { "(all)" };
        //    selectListTypes.AddRange(context.Types.Select(x => x.TypeTitle));

        //    var selectListWheels = new List<string> { "(all)" };
        //    selectListWheels.AddRange(context.Bikes.Select(x => x.WheelDiameter.ToString()).Distinct().ToList());

        //    var selectListMaterials = new List<string> { "(all)" };
        //    selectListMaterials.AddRange(context.Materials.Select(x => x.MaterialTitle));

        //    var selectListSpeedAmount = new List<string> { "(all)" };
        //    selectListSpeedAmount.AddRange(context.Bikes.Select(x => x.SpeedCount.ToString()).Distinct());

        //    var selectListSize = new List<string> { "(all)" };
        //    selectListSize.AddRange(context.Bikes.Select(x => x.Size.ToString()).Distinct());

        //    var selectListBreakType = new List<string> { "(all)" };
        //    selectListBreakType.AddRange(context.BreakTypes.Select(x => x.BreakTypeTitle));


        //    IEnumerable<Bike> bikes = null;
        //    switch (sortType)
        //    {
        //        case SortType.TitleAsc:
        //            bikes = context.Bikes.ToList().OrderBy(x => x.BikeTitle);
        //            break;
        //        case SortType.PriceAsc:
        //            bikes = context.Bikes.ToList().OrderBy(x => x.Price);
        //            break;
        //    }

        //    if (!manufactury.ToLower().Contains("all")) //якщо якийсь конкретний виробник
        //    {
        //        bikes = bikes.Where(b => b.ManufacturyId == context.Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == manufactury.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault());
        //    }
        //    if (!type.ToLower().Contains("all"))//тип
        //    {
        //        //foreach (var bikeType in context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId))
        //        //{
        //        bikes = bikes.Where(b => b.TypeId == context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId).FirstOrDefault());
        //        //}
        //    }

        //    if (!wheelDiameters.ToLower().Contains("all"))//тип
        //    {
        //        bikes = bikes.Where(b => b.WheelDiameter == double.Parse(wheelDiameters));
        //    }
        //    if (!material.ToLower().Contains("all"))//тип
        //    {
        //        bikes = bikes.Where(b => b.MaterialId == context.Materials.Where(x => x.MaterialTitle.ToLower() == material.ToLower()).Select(x => x.MaterialId).FirstOrDefault());
        //    }
        //    if (!speedAmount.ToLower().Contains("all"))//тип
        //    {
        //        bikes = bikes.Where(b => b.SpeedCount == Int32.Parse(speedAmount));
        //    }
        //    if (!size.ToLower().Contains("all"))//тип
        //    {
        //        bikes = bikes.Where(b => b.Size == Int32.Parse(size));
        //    }
        //    if (!breakType.ToLower().Contains("all"))//тип
        //    {
        //        bikes = bikes.Where(b => b.BreakTypeId == context.BreakTypes.Where(x => x.BreakTypeTitle == breakType).Select(x => x.BreakTypeId).FirstOrDefault());
        //    }
        //    if (string.IsNullOrEmpty(priceTo))
        //    {
        //        priceTo = context.Bikes.Select(x => x.Price).Max().ToString();
        //    }
        //    if (string.IsNullOrEmpty(priceFrom))
        //    {
        //        priceFrom = context.Bikes.Select(x => x.Price).Min().ToString();
        //    }
        //    if (!string.IsNullOrEmpty(priceTo) || !string.IsNullOrEmpty(priceFrom))
        //    {
        //        //priceFrom = Convert.ToDecimal(priceFrom);
        //        bikes = bikes.Where(x => x.Price >= Convert.ToDecimal(priceFrom) && x.Price <= Convert.ToDecimal(priceTo)).ToList();
        //    }
        //    return View(new BikeViewModel
        //    {
        //        Bikes = bikes.ToList(),
        //        Manufacturies = new SelectList(selectListManufacturies),
        //        Types = new SelectList(selectListTypes),
        //        WheelDiameters = new SelectList(selectListWheels),
        //        Materials = new SelectList(selectListMaterials),
        //        SpeedCount = new SelectList(selectListSpeedAmount),
        //        Size = new SelectList(selectListSize),
        //        BreakType = new SelectList(selectListBreakType)
        //    });
        //}
        public IActionResult EditGoods()
        {
            return View();
        }
        public IActionResult Create(int? id)
        {
            ViewBag.DB = context;
            if (id != null)
            {
                var bikeToEdit = context.Bikes.Find(id);
                return View(bikeToEdit);
            }
            else
            {
                //Bike bike = new Bike();
                //{
                //    BikeId = 0,
                //    BikeTitle = "",
                //    ManufacturyId = 0,
                //    TypeId = 0,
                //    MaterialId = 0,
                //    SpeedCount=0,
                //    Size=0,
                //    WheelDiameter=0,
                //    BreakTypeId=0,
                //    PhotoPath="",
                //    Price=0
                //};
                return View(new Bike { });
            }
        }
        [HttpPost]
        public IActionResult Remove(int? bikeId)
        {
            context.Bikes.Remove(context.Bikes.Find(bikeId));
            context.SaveChanges();
            return RedirectToAction("EditGoods");
        }
        [HttpPost]
        public IActionResult Create(Bike bike)
        {
            if(ModelState.IsValid)
            {
                
                if (bike.BikeId == 0)
                {
                    //if(!string.IsNullOrEmpty(bike.Photo_Base64))
                    //{
                    //    bike.Photo_Base64 = converter.EncodeTo64(bike.Photo_Base64);
                    //}
                    context.Bikes.Add(bike);
                }
                else
                {
                    var bikeToEdit = context.Bikes.FirstOrDefault(x => x.BikeId == bike.BikeId);
                    bikeToEdit.BikeTitle = bike.BikeTitle;
                    bikeToEdit.ManufacturyId = bike.ManufacturyId;
                    bikeToEdit.TypeId = bike.TypeId;
                    bikeToEdit.MaterialId = bike.MaterialId;
                    bikeToEdit.SpeedCount = bike.SpeedCount;
                    bikeToEdit.Size = bike.Size;
                    bikeToEdit.WheelDiameter = bike.WheelDiameter;
                    bikeToEdit.BreakTypeId = bike.BreakTypeId;
                    bikeToEdit.PhotoPath = bike.PhotoPath;
                    bikeToEdit.Price = bike.Price;
                }
                context.SaveChanges();
                return RedirectToAction("EditGoods", "Admin");
            }
            else
            {
                ModelState.AddModelError("Some datas is wrong", "Check entered datas");
                return View(bike);
            }

        }
        public IActionResult EditGoodsPartial(string priceTo, string priceFrom, string search = "", string manufactury = "(all)", string type = "(all)", string wheelDiameters = "(all)", string material = "(all)", string speedAmount = "(all)", string size = "(all)", string breakType = "(all)", SortType sortType = SortType.TitleAsc)
        {
            var selectListManufacturies = new List<string> { "(all)" };
            selectListManufacturies.AddRange(context.Manufacturies.Select(x => x.ManufacturyTitle));

            var selectListTypes = new List<string> { "(all)" };
            selectListTypes.AddRange(context.Types.Select(x => x.TypeTitle));

            var selectListWheels = new List<string> { "(all)" };
            selectListWheels.AddRange(context.Bikes.Select(x => x.WheelDiameter.ToString()).Distinct().ToList());

            var selectListMaterials = new List<string> { "(all)" };
            selectListMaterials.AddRange(context.Materials.Select(x => x.MaterialTitle));

            var selectListSpeedAmount = new List<string> { "(all)" };
            selectListSpeedAmount.AddRange(context.Bikes.Select(x => x.SpeedCount.ToString()).Distinct());

            var selectListSize = new List<string> { "(all)" };
            selectListSize.AddRange(context.Bikes.Select(x => x.Size.ToString()).Distinct());

            var selectListBreakType = new List<string> { "(all)" };
            selectListBreakType.AddRange(context.BreakTypes.Select(x => x.BreakTypeTitle));


            IEnumerable<Bike> bikes = null;
            switch (sortType)
            {
                case SortType.TitleAsc:
                    bikes = context.Bikes.ToList().OrderBy(x => x.BikeTitle);
                    break;
                case SortType.PriceAsc:
                    bikes = context.Bikes.ToList().OrderBy(x => x.Price);
                    break;
            }

            if(!string.IsNullOrEmpty(search))
            {
                bikes = bikes.Where(x=> x.BikeTitle.ToLower().Contains(search.ToLower())).ToList();
            }

            if (!manufactury.ToLower().Contains("all")) //якщо якийсь конкретний виробник
            {
                bikes = bikes.Where(b => b.ManufacturyId == context.Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == manufactury.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault());
            }
            if (!type.ToLower().Contains("all"))//тип
            {
                //foreach (var bikeType in context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId))
                //{
                bikes = bikes.Where(b => b.TypeId == context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId).FirstOrDefault());
                //}
            }

            if (!wheelDiameters.ToLower().Contains("all"))//тип
            {
                bikes = bikes.Where(b => b.WheelDiameter == double.Parse(wheelDiameters));
            }
            if (!material.ToLower().Contains("all"))//тип
            {
                bikes = bikes.Where(b => b.MaterialId == context.Materials.Where(x => x.MaterialTitle.ToLower() == material.ToLower()).Select(x => x.MaterialId).FirstOrDefault());
            }
            if (!speedAmount.ToLower().Contains("all"))//тип
            {
                bikes = bikes.Where(b => b.SpeedCount == Int32.Parse(speedAmount));
            }
            if (!size.ToLower().Contains("all"))//тип
            {
                bikes = bikes.Where(b => b.Size == Int32.Parse(size));
            }
            if (!breakType.ToLower().Contains("all"))//тип
            {
                bikes = bikes.Where(b => b.BreakTypeId == context.BreakTypes.Where(x => x.BreakTypeTitle == breakType).Select(x => x.BreakTypeId).FirstOrDefault());
            }
            if (string.IsNullOrEmpty(priceTo))
            {
                priceTo = context.Bikes.Select(x => x.Price).Max().ToString();
            }
            if (string.IsNullOrEmpty(priceFrom))
            {
                priceFrom = context.Bikes.Select(x => x.Price).Min().ToString();
            }
            if (!string.IsNullOrEmpty(priceTo) || !string.IsNullOrEmpty(priceFrom))
            {
                //priceFrom = Convert.ToDecimal(priceFrom);
                bikes = bikes.Where(x => x.Price >= Convert.ToDecimal(priceFrom) && x.Price <= Convert.ToDecimal(priceTo)).ToList();
            }
            return PartialView(new BikeViewModel
            {
                Bikes = bikes.ToList(),
                Manufacturies = new SelectList(selectListManufacturies),
                Types = new SelectList(selectListTypes),
                WheelDiameters = new SelectList(selectListWheels),
                Materials = new SelectList(selectListMaterials),
                SpeedCount = new SelectList(selectListSpeedAmount),
                Size = new SelectList(selectListSize),
                BreakType = new SelectList(selectListBreakType)
            });
        }


        [HttpPost]
        public IActionResult GetAllPhotos()
        {
            //List<byte[]> bytes = new List<byte[]>();
            //foreach(var photo in photos.Select(x=> x.Photo_Base64))
            //{
            //    bytes.Add(Convert.FromBase64String(photo.ToString()));
            //}
            return View(new Base64FilesViewModel { PhotosInBytes= context.Base64Files.ToList()});
        }
        [HttpPost]
        public IActionResult CreatePhoto(Base64FilesViewModel bvm)
        {
            if (bvm.Photo_Base64 != null)
            {
                Base64File file = new Base64File { Name = bvm.Name };
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(bvm.Photo_Base64.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)bvm.Photo_Base64.Length);
                }
                // установка массива байтов
                file.Photo_Base64 = imageData;
                context.Base64Files.Add(file);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(bvm);
            }
        }
    }
}
        //Goods____________________________________________________________________________________
        ////Users____________________________________________________________________________________
        //[Authorize(Roles = "mainAdmin")]

        //public IActionResult EditUsers()
        //{
        //    ViewBag.Roles = context.Roles.ToList();

        //    return View(context.Users.ToList());
        //}

        //public IActionResult AddNewUser(int? id)
        //{
        //    ViewBag.DB = context;
        //    if (id != null)
        //    {
        //        var userToEdit = context.Users.Find(id);
        //        return View(userToEdit);
        //    }
        //    else
        //    {
        //        User user = new User();
        //        return View(user);
        //    }
        //}

        //[HttpPost]
        //public IActionResult RemoveUser(int? userId)
        //{
        //    context.Users.Remove(context.Users.Find(userId));
        //    context.SaveChanges();
        //    return RedirectToAction("EditUsers");
        //}

        //[HttpPost]
        //public IActionResult AddNewUser(User user)
        //{
        //    if (user.UserId == 0)
        //    {
        //        context.Users.Add(user);
        //    }
        //    else
        //    {
        //        var userToEdit = context.Users.FirstOrDefault(x => x.UserId == user.UserId);
        //        userToEdit.Name = user.Name;
        //        userToEdit.LastName = user.LastName;
        //        userToEdit.Email = user.Email;
        //        userToEdit.Password = user.Password;
        //        userToEdit.RoleId = user.RoleId;
        //        context.Update(userToEdit);
        //    }
        //    context.SaveChanges();
        //    return RedirectToAction("EditUsers");
        //}
        //Users____________________________________________________________________________________

