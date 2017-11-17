﻿using ArchPM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchPM.Core.Tests
{
    enum EnumWithDescription
    {
        [EnumDescription("Test Environment")]
        Test,
        [EnumDescription("Development Environment")]
        Dev,
        [EnumDescription("Production Environment")]
        Prod
    }

    enum EnumWithPartialDescription
    {
        [EnumDescription("Test Environment")]
        Test,
        Dev,
        [EnumDescription("Production Environment")]
        Prod
    }

    enum EnumWithNoDescription
    {
        Test,
        Dev,
        Prod
    }

    class AllLowerWithUnderscoreClass
    {
        public int? hanede_calisan_kisi_sayisi { get; set; }
    }

    class AllUpperWithUnderscoreClass
    {
        public int? HANEDE_CALISAN_KISI_SAYISI { get; set; }
    }

    class PascalCaseClass
    {
        public int? HanedeCalisanKisiSayisi { get; set; }
    }

    class CamelCaseClass
    {
        public int? hanedeCalisanKisiSayisi { get; set; }
    }

    class SmallTable
    {
        public Int32 Id42 { get; set; }
        public Int32 Id2 { get; set; }
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Decimal Salary { get; set; }
    }

    class SmallTableInherited : SmallTable
    {
        public Int32 Age { get; set; }
    }


    class Person
    {
        public Int32 Id { get; set; }
        public Int32? Id2 { get; set; }
        public Int64 Height { get; set; }
        public Int64? Height2 { get; set; }
        public Int16 Weight { get; set; }
        public Int16? Weight2 { get; set; }
        public String Name { get; set; }
        public String Name2 { get; set; }
        public Decimal Salary { get; set; }
        public Decimal? Salary2 { get; set; }
        public Genders Gender { get; set; }
        public Genders? Gender2 { get; set; }
        public Fears Fear { get; set; }
        public Fears? Fear2 { get; set; }
        public Boolean IsFriendly { get; set; }
        public Boolean? IsFriendly2 { get; set; }
        public IMyInterface MyInterface { get; set; }
        public IMyInterface MyInterface2 { get; set; }
        public MyClass Myclass { get; set; }
        public MyClass Myclass2 { get; set; }
        public DateTime Birth { get; set; }
        public DateTime? Birth2 { get; set; }
        public String OnlyRead { get { return "test"; } }

    }

    class Address
    {
        public Int32 Id { get; set; }
        public Int32? Id2 { get; set; }
        public Int32 PersonId { get; set; }
        public Int32? PersonId2 { get; set; }
        public Int64 Size { get; set; }
        public Int64? Size2 { get; set; }
        public String Name { get; set; }
        public String Name2 { get; set; }
        public String Description { get; set; }
        public String Description2 { get; set; }
        public DateTime MovingDate { get; set; }
        public DateTime? MovingDate2 { get; set; }
    }

    interface IMyInterface
    {
        Int32 Id { get; set; }
    }

    class MyInterfaceClass : IMyInterface
    {
        public Int32 Id { get; set; }
    }

    class MyClass
    { }

    enum Genders
    {
        Male,
        Female,
        Other
    }

    enum Fears : byte
    {
        Dark = 0,
        Alone = 1,
        Cat = 2
    }
}
