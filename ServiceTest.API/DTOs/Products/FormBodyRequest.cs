using System;
using System.Collections.Generic;

namespace ServiceTest.API.DTOs.Products
{
    public class FormBodyRequest
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public List<FormBodyList> list { get; set; }
    }
    public class FormBodyList
    {
        public string key { get; set; }
        public string value { get; set; }
        public int? number1 { get; set; }
        public int number2 { get; set; }
        public decimal? decimal1 { get; set; }
        public decimal decimal2 { get; set; }
        public DateTime? datetime1 { get; set; }
        public DateTime datetime2 { get; set; }
        public bool bool1 { get; set; }
        public bool? bool2 { get; set; }
        public List<FormBodyListInList> list { get; set; }
    }

    public class FormBodyListInList
    {
        public string test1 { get; set; }
        public string test2 { get; set; }

    }
}
