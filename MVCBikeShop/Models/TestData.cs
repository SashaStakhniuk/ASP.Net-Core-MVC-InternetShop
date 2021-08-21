using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBikeShop.Models
{
    public class TestData
    {
        public static void Initialize(BikeContext db)
        {
            if (!db.BreakTypes.Any())
            {
                db.BreakTypes.AddRange(
                  new BreakType
                  {
                      BreakTypeTitle = "Rim"
                  },
                  new BreakType
                  {
                      BreakTypeTitle = "Mechanical disk"
                  },
                  new BreakType
                  {
                      BreakTypeTitle = "Hydraulic disk"
                  },
                  new BreakType
                  {
                      BreakTypeTitle = "Drum"
                  },
                  new BreakType
                  {
                      BreakTypeTitle = "Combined"
                  }
                );
                db.SaveChanges();
            }
            if (!db.Manufacturies.Any())
            {
                db.Manufacturies.AddRange(
                  new Manufactury
                  {
                      ManufacturyTitle = "Cannondale"
                  },
                  new Manufactury
                  {
                      ManufacturyTitle = "Formula"
                  },
                  new Manufactury
                  {
                      ManufacturyTitle = "Crosser"
                  },
                  new Manufactury
                  {
                      ManufacturyTitle = "Azimut"
                  },
                  new Manufactury
                  {
                      ManufacturyTitle = "Dorozhnik"
                  }, 
                  new Manufactury
                  {
                      ManufacturyTitle = "Discovery"
                  }, 
                  new Manufactury
                  {
                      ManufacturyTitle = "Leon"
                  }
                );
                db.SaveChanges();
            }
            if (!db.Materials.Any())
            {
                db.Materials.AddRange(
                  new Material
                  {
                      MaterialTitle = "Aluminum"
                  },
                  new Material
                  {
                      MaterialTitle = "Steel"
                  },
                  new Material
                  {
                      MaterialTitle = "Carbon"
                  }
                );
                db.SaveChanges();
            }
            
           
            if (!db.Types.Any())
            {
                db.Types.AddRange(
                    new Type
                    {
                        TypeTitle= "Mountain"
                    },
                    new Type
                    {
                        TypeTitle = "City"
                    },
                    new Type
                    {
                        TypeTitle = "Gravel"
                    },
                    new Type
                    {
                        TypeTitle = "Fat bike"
                    },
                    new Type
                    {
                        TypeTitle = "BMX"
                    },
                    new Type
                    {
                        TypeTitle = "Folding"
                    },
                     new Type
                     {
                         TypeTitle = "Road"
                     }
                 );
                db.SaveChanges();
            }
            if (!db.Bikes.Any())
            {
                db.Bikes.AddRange(
                    new Bike
                    {
                        BikeTitle= "Гірний велосипед Formula Motion DD 26",
                        BreakTypeId=2,
                        ManufacturyId=2,
                        MaterialId=1,
                        SpeedCount = 21,
                        TypeId=1,
                        WheelDiameter=26,
                        PhotoPath= "https://velo-land.com.ua/image/cache/catalog/formula_2021/motion-26-grey-orange-1725x1284.jpg",
                        Price=6144
                    },
                    new Bike
                    {
                        BikeTitle = "Підлітковий гірний велосипед Formula Blackwood 2.0 AM DD 24",
                        BreakTypeId = 2,
                        ManufacturyId = 2,
                        MaterialId = 1,
                        Size = 14,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 24,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/formula_2021/blackwood_24_2_0_grey_blue-1725x1284.jpg",
                        Price = 5666
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Formula Thor 1.0 DD 26",
                        BreakTypeId = 2,
                        ManufacturyId = 2,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/FORMULA_2020/26-Formula-THOR-1-0-AM-DD-cherno-seryiy-s-belyim-m-2020-3365-1980x1360-1725x1284.jpg",
                        Price = 5921
                    },
                    new Bike
                    {
                        BikeTitle = "Складний велосипед Formula Smart 24 з ліхтарем",
                        BreakTypeId = 5,
                        ManufacturyId = 2,
                        MaterialId = 2,
                        Size = 14,
                        SpeedCount = 1,
                        TypeId = 6,
                        WheelDiameter = 24,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/FORMULA_2020/24-formula-smart-cherno-seryiy-s-belyim-s-fonarem-2020-7868-1980x1360-1725x1284.jpg",
                        Price = 4200
                    },
                    new Bike
                    {
                        BikeTitle = "Жіночий гірний велосипед Formula Alpina DD 27.5",
                        BreakTypeId = 2,
                        ManufacturyId = 2,
                        MaterialId = 1,
                        Size = 18,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 27.5,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/FORMULA_2020/27-5-Formula-ALPINA-AM-DD-biryuzovo-ciniy-c-korallovyim-2020-3410-1980x1360-1725x1284.jpg",
                        Price = 6367
                    },
                    new Bike
                    {
                        BikeTitle = "Дитячий гірний велосипед Formula Blackwood 1.0 AM Vbr 20",
                        BreakTypeId = 1,
                        ManufacturyId = 2,
                        MaterialId = 1,
                        Size = 11,
                        SpeedCount = 6,
                        TypeId = 1,
                        WheelDiameter = 20,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/formula_2021/blackwood_24_2_0_grey_blue-1725x1284.jpg",
                        Price = 4960
                    },
                    new Bike
                    {
                        BikeTitle = "Підлітковий гірний велосипед Formula Acid 1.0 DD 24",
                        BreakTypeId = 2,
                        ManufacturyId = 2,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 6,
                        TypeId = 1,
                        WheelDiameter = 24,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/FORMULA_2020/24-Formula-ACID-1-0-DD-cherno-krasnyiy-s-seryim-2020-3379-1980x1360-1725x1284.jpg",
                        Price = 5666
                    },
                    new Bike
                    {
                        BikeTitle = "Карбоновий гірний велосипед Crosser Genesis 29",
                        BreakTypeId = 3,
                        ManufacturyId = 3,
                        MaterialId = 3,
                        Size = 19,
                        SpeedCount = 27,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_genesis_29-1725x1284.jpg",
                        Price = 19800
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser Raptor 29",
                        BreakTypeId = 3,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 24,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_raptor_29_black_red-1725x1284.jpg",
                        Price = 17670
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser X880 29 DEORE",
                        BreakTypeId = 3,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 20,
                        SpeedCount = 132,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_new/crosser/crosser_x_880_29_black_red-1725x1284.jpg",
                        Price = 14000
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser Solo 29 DEORE",
                        BreakTypeId = 3,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 14,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/crosser_2021/solo_black-1725x1284.jpg",
                        Price = 16000
                    },
                    new Bike
                    {
                        BikeTitle = "Гібридний велосипед Crosser Hybrid XC 500 28",
                        BreakTypeId = 2,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 20,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_new/crosser/crosser_hybrid_28_white-1725x1284.jpg",
                        Price = 8200
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser Grim 26",
                        BreakTypeId = 2,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_new/crosser/grim_26_black_blue-1725x1284.jpg",
                        Price = 7400
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser Grim 29",
                        BreakTypeId = 2,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/crosser_grim_29_grey-1725x1284.jpg",
                        Price = 7800
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Crosser Jazz 29",
                        BreakTypeId = 3,
                        ManufacturyId = 3,
                        MaterialId = 1,
                        Size = 21,
                        SpeedCount = 24,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/crosser_2021/jazz_29_green-1725x1284.jpg",
                        Price = 8990
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Azimut Spark 29 GD",
                        BreakTypeId = 2,
                        ManufacturyId = 4,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 2,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/AZIMUT/azimut_spark_29_gd_black_red-1725x1284.jpg",
                        Price = 5700
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Azimut 40D 26",
                        BreakTypeId = 2,
                        ManufacturyId = 4,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/azimut_new/azimut_40_d_26_black_red_grey-1725x1284.JPG",
                        Price = 4900
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Azimut Blackmount 26 D",
                        BreakTypeId = 2,
                        ManufacturyId = 4,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/azimut_new/azimut_blackmount_26_d_grey_turquoise-1725x1284.jpg",
                        Price = 4900
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Azimut Blackmount 26 GD",
                        BreakTypeId = 2,
                        ManufacturyId = 4,
                        MaterialId = 2,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/azimut_new/azimut_blackmount_26_d_black_green-1725x1284.jpg",
                        Price = 5600
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Azimut Energy 26 GD",
                        BreakTypeId = 2,
                        ManufacturyId = 4,
                        MaterialId = 2,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/CROSSER/azimut_new/azimut_energy_26_gd_black_green-1725x1284.jpg",
                        Price = 5600
                    },
                    new Bike
                    {
                        BikeTitle = "Міський велосипед Dorozhnik Comfort Female 28",
                        BreakTypeId = 1,
                        ManufacturyId = 5,
                        MaterialId = 2,
                        Size = 19,
                        SpeedCount = 1,
                        TypeId = 2,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/DOROZHNIK_2020/28-Dorozhnik-COMFORT-FEMALE-rubinovyiy-2020-7813-1980x1360-1725x1284.jpg",
                        Price = 5000
                    },
                    new Bike
                    {
                        BikeTitle = "Міський велосипед Dorozhnik Comfort Male 28",
                        BreakTypeId = 1,
                        ManufacturyId = 5,
                        MaterialId = 2,
                        Size = 21,
                        SpeedCount = 1,
                        TypeId = 2,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/DOROZHNIK_2020/28-Dorozhnik-COMFORT-MALE-seryiy-2020-7824-1980x1360-1725x1284.jpg",
                        Price = 5000
                    },
                    new Bike
                    {
                        BikeTitle = "Міський велосипед Dorozhnik Comfort Female 28",
                        BreakTypeId = 1,
                        ManufacturyId = 5,
                        MaterialId = 2,
                        Size = 19,
                        SpeedCount = 1,
                        TypeId = 2,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/DOROZHNIK_2020/28-Dorozhnik-COMFORT-FEMALE-PH-rubinovyiy-2020-7817-1600x1200-1725x1284.jpg",
                        Price = 5000
                    },
                    new Bike
                    {
                        BikeTitle = "Міський велосипед Dorozhnik Comfort Male 28",
                        BreakTypeId = 1,
                        ManufacturyId = 5,
                        MaterialId = 2,
                        Size = 21,
                        SpeedCount = 1,
                        TypeId = 2,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/DOROZHNIK_2020/28-Dorozhnik-COMFORT-MALE-PH-seryiy-2020-7826-1980x1360-1725x1284.jpg",
                        Price = 6200
                    },
                    new Bike
                    {
                        BikeTitle = "Міський велосипед Dorozhnik Coral 28",
                        BreakTypeId = 1,
                        ManufacturyId = 5,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 1,
                        TypeId = 2,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/DOROZHNIK_2020/28-Dorozhnik-CORAL-seryiy-2020-7764-1980x1360-1725x1284.jpg",
                        Price = 6200
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Discovery Bastion DD 26",
                        BreakTypeId = 2,
                        ManufacturyId = 6,
                        MaterialId = 1,
                        Size = 16,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 26,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/discovery_2021/2021/bastion_26_white_13-1725x1284.jpg",
                        Price = 5266
                    },
                    new Bike
                    {
                        BikeTitle = "Підлітковий гірний велосипед Discovery Qube DD 24 AM",
                        BreakTypeId = 2,
                        ManufacturyId = 6,
                        MaterialId = 1,
                        Size = 14,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 24,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/discovery_2021/2021/qube_24_dd_black_green-1725x1284.jpg",
                        Price = 5266
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Discovery Attack 26",
                        BreakTypeId = 2,
                        ManufacturyId = 6,
                        MaterialId = 2,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 28,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/discovery_2021/2021/attack-26-grey-1725x1284.jpg",
                        Price = 4200
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Discovery Bastion DD 27.5",
                        BreakTypeId = 2,
                        ManufacturyId = 6,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 27.5 ,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/discovery_2021/2021/bastion_27_5_white-1725x1284.jpg",
                        Price = 5426
                    },
                    new Bike
                    {
                        BikeTitle = "Гірний велосипед Discovery Bastion DD 29",
                        BreakTypeId = 2,
                        ManufacturyId = 6,
                        MaterialId = 1,
                        Size = 19,
                        SpeedCount = 21,
                        TypeId = 1,
                        WheelDiameter = 29,
                        PhotoPath = "https://velo-land.com.ua/image/cache/catalog/discovery_2021/2021/bastion_29_grey_1-1725x1284.jpg",
                        Price = 5571
                    }
                    );
                db.SaveChanges();
            }
        }
    }
}
