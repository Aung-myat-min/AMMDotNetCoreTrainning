﻿public class Ben10ResponseModel
{
    public Tbl_Ben10[] Tbl_Ben10 { get; set; }
}

public class Tbl_Ben10
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string color { get; set; }
    public string power { get; set; }
    public int rating { get; set; }
}
