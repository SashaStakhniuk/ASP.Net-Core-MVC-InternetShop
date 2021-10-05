using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBikeShop.Models;
using MVCBikeShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

// при пагінації не передаються параметри фільтрування

namespace MVCBikeShop.Controllers
{
    public class HomeController : Controller
    {
        BikeContext context;
        //public List<Bike> DisplayedArticles { get; set; }

        //private string Text { get; set; }
       //private IEnumerable<Bike> bikes = null;
        private List<string> SelectListManufacturies = new List<string> { "(all)" };
        private List<string> SelectListTypes = new List<string> { "(all)" };
        private List<string> SelectListWheels = new List<string> { "(all)" };
        private List<string> SelectListMaterials = new List<string> { "(all)" };
        private List<string> SelectListSpeedAmount = new List<string> { "(all)" };
        private List<string> SelectListSize = new List<string> { "(all)" };
        private List<string> SelectListBreakType = new List<string> { "(all)" };

        public HomeController(BikeContext context)
        {
            this.context = context;
            var initialize = Task.Run(() => { 
            SelectListManufacturies.AddRange(context.Manufacturies.Select(x => x.ManufacturyTitle));

            SelectListTypes.AddRange(context.Types.Select(x => x.TypeTitle));

            SelectListWheels.AddRange(context.Bikes.Select(x => x.WheelDiameter.ToString()).Distinct().ToList());

            SelectListMaterials.AddRange(context.Materials.Select(x => x.MaterialTitle));

            SelectListSpeedAmount.AddRange(context.Bikes.Select(x => x.SpeedCount.ToString()).Distinct());

            SelectListSize.AddRange(context.Bikes.Select(x => x.Size.ToString()).Distinct());

            SelectListBreakType.AddRange(context.BreakTypes.Select(x => x.BreakTypeTitle));
            });
            initialize.Wait();
        }
        //[HttpPost]
        //public  async Task<IActionResult> Index(/*BikeViewModel model,*/ string findBy, string search, string text, string priceTo, string priceFrom, int id = 1, string manufactury = "(all)", string type = "(all)", string wheelDiameters = "(all)", string material = "(all)", string speedAmount = "(all)", string size = "(all)", string breakType = "(all)", SortType sortType = SortType.TitleAsc)
        //{
        //    IEnumerable<Bike> bikes = null;
        //    //await Task.Run(() =>
        //    //{  });

        //    if (!string.IsNullOrEmpty(search))
        //    {          
        //        if(findBy=="Title")
        //        {
        //            bikes = context.Bikes.Where(x => x.BikeTitle.ToLower().Contains(search.ToLower())).ToList();
        //            return View(new BikeViewModel { Bikes=bikes.ToList()});
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    //else
        //    //{
        //    //    return RedirectToAction("Index");
        //    //}
        //    switch (sortType)
        //    {
        //        case SortType.TitleAsc:
        //            bikes = context.Bikes.ToList().OrderBy(x => x.BikeTitle);
        //            break;
        //        case SortType.PriceAsc:
        //            bikes = context.Bikes.ToList().OrderBy(x => x.Price);
        //            break;
        //    }
        //    //Task.Run(() => { 
        //    if (!manufactury.ToLower().Contains("all")) //якщо якийсь конкретний виробник
        //    {
        //        bikes =  bikes.Where(b => b.ManufacturyId == context.Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == manufactury.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault());
        //    }
        //    //});
        //    if (!type.ToLower().Contains("all"))//тип
        //    {
        //        //foreach (var bikeType in context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId))
        //        //{
        //        bikes = bikes.Where(b => b.TypeId == context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId).FirstOrDefault());
        //        //}
        //    }
        //    if (!wheelDiameters.ToLower().Contains("all"))//діаметр коліс
        //    {
        //        bikes = bikes.Where(b => b.WheelDiameter == double.Parse(wheelDiameters));
        //    }
        //    if (!material.ToLower().Contains("all"))//матеіал
        //    {
        //        bikes = bikes.Where(b => b.MaterialId == context.Materials.Where(x => x.MaterialTitle.ToLower() == material.ToLower()).Select(x => x.MaterialId).FirstOrDefault());
        //    }
        //    if (!speedAmount.ToLower().Contains("all"))//к-сть швидкостей
        //    {
        //        bikes = bikes.Where(b => b.SpeedCount == Int32.Parse(speedAmount));
        //    }
        //    if (!size.ToLower().Contains("all"))//розмір (в дюймах)
        //    {
        //        bikes = bikes.Where(b => b.Size == Int32.Parse(size));
        //    }
        //    if (!breakType.ToLower().Contains("all"))//гальма
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
        //    //bikes = Pagination(id,bikes);
        //    if(bikes.Count()==0)
        //    {
        //        ViewBag.Text = "There are no bikes with such parameters";
        //        bikes = context.Bikes.ToList();
        //    }
        //    await Task.Factory.StartNew(() =>
        //    {
        //        if(!manufactury.Contains("all"))
        //        {
        //            SelectListManufacturies.Remove(manufactury);
        //            SelectListManufacturies.Insert(0, manufactury);
        //        }
        //        if (!type.Contains("all"))
        //        {
        //            SelectListTypes.Remove(type);
        //            SelectListTypes.Insert(0, type);
        //        }
        //        if (!wheelDiameters.Contains("all"))
        //        {
        //            SelectListWheels.Remove(wheelDiameters);
        //            SelectListWheels.Insert(0, wheelDiameters);
        //        }
        //        if (!material.Contains("all"))
        //        {
        //            SelectListMaterials.Remove(material);
        //            SelectListMaterials.Insert(0, material);
        //        }
        //        if (!speedAmount.Contains("all"))
        //        {
        //            SelectListSpeedAmount.Remove(speedAmount);
        //            SelectListSpeedAmount.Insert(0, speedAmount);
        //        }
        //        if (!size.Contains("all"))
        //        {
        //            SelectListSize.Remove(size);
        //            SelectListSize.Insert(0, size);
        //        }
        //        if (!breakType.Contains("all"))
        //        {
        //            SelectListBreakType.Remove(breakType);
        //            SelectListBreakType.Insert(0, breakType);
        //        }
        //    });
        //    //model.Bikes = bikes.ToList();
        //    return View(
        //        //model
        //        new BikeViewModel
        //        {
        //            Bikes = bikes.ToList(),
        //            Manufacturies = new SelectList(SelectListManufacturies),
        //            Types = new SelectList(SelectListTypes),
        //            WheelDiameters = new SelectList(SelectListWheels),
        //            Materials = new SelectList(SelectListMaterials),
        //            SpeedCount = new SelectList(SelectListSpeedAmount),
        //            Size = new SelectList(SelectListSize),
        //            BreakType = new SelectList(SelectListBreakType)
        //        }
        //        );
        //    //if (findBy == "Type")
        //    //{
        //    //    List<Type> Types = context.Types.ToList().Where(y => y.TypeTitle.ToLower().Contains(search.ToLower())).ToList();
        //    //    foreach(var type in Types)
        //    //    {
        //    //        //DisplayedArticles.Add(context.Bikes.ToList().Where(x => x.TypeId == type.TypeId).ToList());
        //    //        DisplayedArticles = context.Bikes.ToList().Where(x => x.TypeId == type.TypeId).ToList();
        //    //    }
        //    //    //DisplayedArticles = context.Bikes.Where(x => x.TypeId == context.Types.ToList().Where(y => y.TypeTitle.ToLower().Contains(search.ToLower())).Select(y=> y.TypeId));
        //    //}
        //}
        private IEnumerable<Bike> Pagination(int id, IEnumerable<Bike> bikes)
        {
            int postsAmount = 10;
            if (bikes == null)
            {
                bikes = context.Bikes.ToList();
            }          
                if (bikes.Count() % postsAmount == 0)
                {
                    ViewBag.PagesCount = (bikes.Count() / postsAmount);
                }
                else
                {
                    ViewBag.PagesCount = (bikes.Count() / postsAmount) + 1;
                }
                if (id == 0 || id > ViewBag.PagesCount)
                {
                    id = 1;
                }
                ViewBag.CurrentPage = id;
                return bikes.Skip((id - 1) * postsAmount).Take(postsAmount).ToList();
        }
        public IActionResult Index()
        {
            return View(
                new BikeViewModel
                {
                    Bikes = Pagination(1,context.Bikes.ToList()).ToList(),
                    Manufacturies = new SelectList(SelectListManufacturies),
                    Types = new SelectList(SelectListTypes),
                    WheelDiameters = new SelectList(SelectListWheels),
                    Materials = new SelectList(SelectListMaterials),
                    SpeedCount = new SelectList(SelectListSpeedAmount),
                    Size = new SelectList(SelectListSize),
                    BreakType = new SelectList(SelectListBreakType)
                }
            );
        }
        public async Task<IActionResult> IndexPartial(/*BikeViewModel model,*/string text, string priceFrom, string priceTo, int id = 1, string manufactury = "(all)", string type = "(all)", string wheelDiameters = "(all)", string material = "(all)", string speedAmount = "(all)", string size = "(all)", string breakType = "(all)", SortType sortType = SortType.TitleAsc)
        {
            IEnumerable<Bike> bikes = null;
            ViewBag.Text = text;
            await Task.Run(() =>
            { 
                switch (sortType)
                {
                    case SortType.TitleAsc:
                        bikes = context.Bikes.ToList().OrderBy(x => x.BikeTitle);
                        break;
                    case SortType.PriceAsc:
                        bikes = context.Bikes.ToList().OrderBy(x => x.Price);
                        break;
                }
            });
            //await Task.Run(() => { 
            if (!manufactury.ToLower().Contains("all")) //якщо якийсь конкретний виробник
            {
                bikes = bikes.Where(b => b.ManufacturyId == context.Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == manufactury.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault());
            }
            //});
            if (!type.ToLower().Contains("all"))//тип
            {
                //foreach (var bikeType in context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId))
                //{
                bikes = bikes.Where(b => b.TypeId == context.Types.Where(x => x.TypeTitle.ToLower() == type.ToLower()).Select(x => x.TypeId).FirstOrDefault());
                //}
            }
            if (!wheelDiameters.ToLower().Contains("all"))//діаметр коліс
            {
                bikes = bikes.Where(b => b.WheelDiameter == double.Parse(wheelDiameters));
            }
            if (!material.ToLower().Contains("all"))//матеіал
            {
                bikes = bikes.Where(b => b.MaterialId == context.Materials.Where(x => x.MaterialTitle.ToLower() == material.ToLower()).Select(x => x.MaterialId).FirstOrDefault());
            }
            if (!speedAmount.ToLower().Contains("all"))//к-сть швидкостей
            {
                bikes = bikes.Where(b => b.SpeedCount == Int32.Parse(speedAmount));
            }
            if (!size.ToLower().Contains("all"))//розмір (в дюймах)
            {
                bikes = bikes.Where(b => b.Size == Int32.Parse(size));
            }
            var breakTypeThread = Task.Run(() =>
            {
                if (!breakType.ToLower().Contains("all"))//гальма
                {
                    bikes = bikes.Where(b => b.BreakTypeId == context.BreakTypes.Where(x => x.BreakTypeTitle == breakType).Select(x => x.BreakTypeId).FirstOrDefault());
                }
            }
           );
            breakTypeThread.Wait();
            var priceSettingThread = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(priceTo))
                {
                    priceTo = context.Bikes.Select(x => x.Price).Max().ToString();
                }
                if (string.IsNullOrEmpty(priceFrom))
                {
                    priceFrom = context.Bikes.Select(x => x.Price).Min().ToString();
                }
            });
            priceSettingThread.Wait();
            if (!string.IsNullOrEmpty(priceTo) || !string.IsNullOrEmpty(priceFrom))
            {
                //priceFrom = Convert.ToDecimal(priceFrom);
                bikes = bikes.Where(x => x.Price >= Convert.ToDecimal(priceFrom) && x.Price <= Convert.ToDecimal(priceTo)).ToList();
            }
            bikes = Pagination(id,bikes);
            if (bikes.Count() == 0)
            {
                ViewBag.Text = "There are no bikes with such parameters";
                bikes = context.Bikes.ToList();
                bikes = Pagination(id, bikes);
                //await Task.Run(() =>
                //{ 
                //    //if (!manufactury.ToLower().Contains("all")) //якщо якийсь конкретний виробник
                //    //{
                //    //    bikes = bikes.Where(b => b.ManufacturyId == context.Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == manufactury.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault());
                //    //}
                //    //else
                //    //{
                //        bikes = context.Bikes.ToList();
                //        bikes = Pagination(id, bikes);
                //    //}
                //});
            }
            await Task.Factory.StartNew(() =>
            {
                if (!manufactury.Contains("all"))
                {
                    SelectListManufacturies.Remove(manufactury);
                    SelectListManufacturies.Insert(0, manufactury);
                }
                if (!type.Contains("all"))
                {
                    SelectListTypes.Remove(type);
                    SelectListTypes.Insert(0, type);
                }
                if (!wheelDiameters.Contains("all"))
                {
                    SelectListWheels.Remove(wheelDiameters);
                    SelectListWheels.Insert(0, wheelDiameters);
                }
                if (!material.Contains("all"))
                {
                    SelectListMaterials.Remove(material);
                    SelectListMaterials.Insert(0, material);
                }
                if (!speedAmount.Contains("all"))
                {
                    SelectListSpeedAmount.Remove(speedAmount);
                    SelectListSpeedAmount.Insert(0, speedAmount);
                }
                if (!size.Contains("all"))
                {
                    SelectListSize.Remove(size);
                    SelectListSize.Insert(0, size);
                }
                if (!breakType.Contains("all"))
                {
                    SelectListBreakType.Remove(breakType);
                    SelectListBreakType.Insert(0, breakType);
                }
            });
            //model.Bikes = bikes.ToList();
            return PartialView(
                new BikeViewModel
                {
                    Bikes = bikes.ToList(),
                    Manufacturies = new SelectList(SelectListManufacturies),
                    Types = new SelectList(SelectListTypes),
                    WheelDiameters = new SelectList(SelectListWheels),
                    Materials = new SelectList(SelectListMaterials),
                    SpeedCount = new SelectList(SelectListSpeedAmount),
                    Size = new SelectList(SelectListSize),
                    BreakType = new SelectList(SelectListBreakType),
                    //____________________________________________________________//Delete It
                    ManufacturiesList = await context.Manufacturies.ToListAsync(),
                    TypesList = await context.Types.ToListAsync(),
                    MaterialsList = await context.Materials.ToListAsync(),
                    BreakTypeList = await context.BreakTypes.ToListAsync()
                    //____________________________________________________________//Delete It
                }
                );
        }
        public IActionResult Buy(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            //ViewBag.BikeId = id;
            //ViewBag.Bike = context.Bikes.Find(id);
            return View(context.Bikes.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> Buy(int? id,Order order)
        {
            if (ModelState.IsValid)
            {
                context.Orders.Add(order);
                context.SaveChanges();
                if (!string.IsNullOrEmpty(order.Email))
                {
                    var bike = await context.Bikes.FirstOrDefaultAsync(x => x.BikeId == order.BikeId);
                    string htmlString = @$"
                        <h2>{order.Name}, thanks for purchase!</h2>
                        <html>
                            <head>
                            <meta charset=""UTF-8"" >
                                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" >
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" >
                            </head>
                          <body>
                                <div style=""display:flex; flex-direction:column; text-align:center; border:1px solid lightgray;border-radius:5px;"" >
                                    <div>{bike.BikeTitle}</div>
                                    <img style=""width:50vw;"" src=""{bike.PhotoPath}""/>
                                    <div><h6>{bike.Price} UAH</h6></div>
                                </div>
                          </body>
                      </html>
                     ";
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("messagess40@gmail.com");
                        mail.To.Add(order.Email);
                        mail.Subject = "Order";
                        mail.Body = htmlString;
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential("messagess40@gmail.com", "MessageForYou");
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }
                return RedirectToAction("Index", new { text = $"{order.Name}, thanks for order" });
            }
            else
            {
                return View(context.Bikes.Find(id));
            }
        }
    }
}
