# C# Basic

C# မှာ basic data types 5 ခုရှိတယ်။

- int, double, long, float, decimal
- double
- char
- string
- bool
- byte
- object နဲ့
- dynamic ဆိုပြီး data types တွေရှိတယ်။

> Object နဲ့ Dynamic datatype တွေကတော့ မသုံးသင့်ဘူး တော်ရုံ ဘာလို့ဆိုတော့ သူတို့က program ရေးတုန်း error မပြပဲ run မှ error ပြတတ်လို့ဖြစ်တယ်။

ပြီးတော့ C# မှာ variable ကြေညာနည်းက

```C#
DataType variableName = value;
```

နဲ့ကြေညာတယ်။ _multiple variable_ တွေကိုလည်း အဆက်လိုက် ကြေညာလို့ရတယ်။

Object, Array, List တွေက reference type တွေဖြစ်ကြတယ်။

> C# မှာ variable တွေကိုကြေညာတဲ့အခါ typeမသိရင် _var_ သုံးလို့ရပါတယ်။ JS
> _var_ နဲ့မတူတာက သူက static type ပါ။
>
> နောက်တစ်ခုက **readonly** ပါသူက variable ကိုကြေငြာထားပြီး value
> မထည့်လည်းရတယ်။ ဒါပေမယ့်ထည့်ရင်တော တစ်ခါတည်းပဲရပါတယ်။
>
> နောက်တစ်ခုက const ပါသူကတော့ တစ်ခါတည်း value ထည့်ရပြီး လုံး၀ပြန် change
> မရပါဘူး။

C# coding style က variable name တွေကို camelCase နဲ့ရေးတယ်။

ပြီးတော့ data types တွေမှာ default valueလတွေရှိတယ်။
ဥပမာ -

- Int ဆို 0
- String တို့ object တို့ဆို null နဲ့
- Date ဆိုရင် `01-01-0001 12:00AM`

ဆိုပြီးရှိတယိ။

တစ်ခြား default valueတွေက SQL မှာ ပြသနာ သိပ်မရှိနိုင်ပေမယ့် Dateကတော့ ပြသနာ နည်းနည်းရှိနိုင်တယ်။ ဘာလို့ဆို SQL ရဲ့ အနိမ့်ဆုံး date က `1900-01-01 00:00:00` ဖြစ်လို့ဖြစ်တယ်။ အဲ့တာကြောင့် dateကို default ပေးမယ့်အစား null ပေးကြတာများတယ်။

နောက်တစ်ခုက C# မှာ Access Modifier တွေက

- public (public, any where can access it)
- private (code ရဲ့ method အတွင်းကပဲaccess လို့ရတယ်။)
- protected (code ရဲ့ method အတွင်းအပြင် သူ့ကို သူ့ child တွေကပါ access လို့ရတယ်။)
- internal (သူကကျတော့ သူ့ကို တစ်ခြား ကိုယ့် assembly အတွင်းကနေပဲ access လို့ရတယ် နောက် project ကနေတော့မရဘူး။)

_(C# code တွေကို compile တဲ့အခါမှာ assembly ထွက်တယ်။)_

ပြီးတော့ method တွေကို name တူပေမယ့် နောက်က လက်ခံတဲ့ parameter မတူရင် ကြိုက်သလောက်ကြေငြာလို့ရတယ်။ အဲ့တာကို method overloading လို့လည်း သိရတယ်။

If else နဲ့ switch မှာဆိုရင် Switch ကပိုမြန်တယ်။ ဘာလို့ဆိုသူက တစ်ခုချင်းမစစ်ပဲ မှန်တာစီတန်းခုံသွာလို့ဖြစ်တယ်။

> But If else is more suitable for complex conditions

If else တွေ switch တွေသုံးတဲ့အခါ elseတို့ default တို့ကို error အပေးများတယ်။
အဲ့မတိုင်ခင် ကိုယ်လိုချင်တာ အားလုံးကို conditions တိုက်ပြီး ရအောင် စစ်တယ်။

Array တို့ list တို့ကို loop ပတ်တဲ့အခါ -
For loop ထက် foreach ကပိုမြန်တယ်။
ဘာလို့ဆို for loop က index ကို နောက်တစ်ခါပြန်ပြန် access နေရပြီး foreach ကတစ်ခါတည်း object တွေကို loop ပတ်နေရတာ ဖြစ်တယ်။

Object တွေရဲ့ default value က null ပဲဖြစ်တယ်။
new ကိုသုံးပြီး ဆောက်မှ သူတို့ရဲ့ initialize လုပ်ထားတဲ့ value တွေ၀င်တယ်။
မဟုတ်ရင် object reference error တက်တယ်။

Abstract class တွေကို inheritance တွေ သုံးတယ်။ တိုက်ရိုက်တော့ ယူသုံးမရဘူး။
Inheritance တွေက class template တွေလိုပဲဖြစ်တယ်။ ဘာလို့ဆိုရင် inheritance တွေကိုယူရင် သူ့အထဲကပါတဲ့ method တွေ data တွေကို ခေါ်သုံးလို့ရလို့ပဲဖြစ်တယ်။

---

# SQL

SQL ဆိုတာက Database တွေနဲ့ interact ဖို့အတွက်သူံးတဲ့ language ပဲဖြစ်တယ်။

သူ့ကိုသုံးပြီး Database user management လုပ်တာကအစ Data တွေကို manipulate လုပ်တာအထိ လုပ်လို့ရတယ်။

SQL က English စကားနဲ့ အရမ်းဆင်ပါတယ် သူ့ရဲ့ Keyword လေးတွေကို သုံးပြီး db ကို manipulate လုပ်ရုံပါပဲ
ဥပမာ -

```sql
SELECT AuthorName, BookName FROM Library WHERE BookId = 'B001';
```

ဆိုရင် `BookId B001` နဲ့ညီတဲ့ data ထဲကမှ author name နဲ့ book name ကိုဆွဲထုတ်ပေးသွားမှာ့ပဲဖြစ်ပါတယ်။

သူ့မှာ တစ်ခြား အသုံးများတဲ့ keyword တွေက
`SELECT, INSERT, UPDATE, DELETE, SET, FROM, WHERE, OR, AND, JOIN` နဲ့စတာတွေပဲဖြစ်ပါတယ်။

---

# DotNet

C# က programming language ဖြစ်ပြီး dotnet က သူ့ framework ပဲဖြစ်ပါတယ်။

.NET framework မှာ Core တပ်ပြီးခေါ်တာရပ်သွားတာက 3.1 ကပဲဖြစ်ပါတယ်။

ပြီးတော့ Ado.Net, Dapper, Efcore စတဲ့ C# dotnet နဲ့ database နဲ့ intract ဖို့ အတွက် package 3ခု အဓိကရှိပါတယ်။

C# project တွေမှာ Console App `Class Library, ASP.NET Core Web API` ဆိုပြီး Project template တွေ အစားစားရှိပါတယ် ။

Node.js က npm လိုမျိုး C# မှာလည်း package တွေကို manage ဖို့အတွက် **nuget** ဆိုတာ ရှိပါတယ်။

---

# ADO.NET

ADO.NET ကတော့ SQL Database တွေနဲ့ interact ဖို့အတွက် အစောဆုံးပေါိလာတဲ့ library ပါ။
သူက old school ဖြစ်တာကြောင့် သူ့ကိုသုံးရင် Database Connections ကြေငြာတာတွေ connection Open, Close စတာတွေပါ လုပ်ပေးရပါမယ်။

အဲ့တာမှ database နဲ့ interact လို့ရပါမယ်။

1. အရင်ဆုံး connection String ကိုကြေငြာဖို့လိုပါတယ်။
   အဲမှာ
   Data Source (server name)
   Initial Catalog (database name)
   User ID (user name)
   Password (user ရဲ့ password)

အဲ့တာတွေကို string တစ်ခုမှာ `;` လေးတွေခြားပြီးရေးရပါမယ်။
ပြီးတာနဲ့ SQL connection object အသစ်ဆောက်ပြီး ခုနက connection ကိုထည့်လိုက်ရင် connection open ပြီး စပြီး interact လို့ရပါပြီ။
ပြီးတော့ connection open တုန်းမှာ `SQL Command` မှာ `query string` ရယ် connection ရယ်ကို parameters ပေးပြီး run လို့ရပါတယ်။

အဲ့တော့ data တွေသိမ်းဖို့ကို `Data Set > Data Table > Data Row > Data Column` အဲ့လို အစဥ်လိုက် ကိုယ်လိုရာကို သုံးလို့ရပါတယ်။

DataAdapter နဲ့ယူရင် Dataတွေကို query အလိုက် တစ်ခါတည်း အကုန်ပေးပါတယ်။
DataReader ကတော့ သူ့ပြောင်းပြန်ပါ သူကကျတော့ data တွေကို တစ်ဖြည်းဖြည်းချင်း stream တစ်ခုလိုပေးပါတယ်။

    Data Adapter ကိုတော့ data နည်းရင်သုံးလို့ရပြီးတော့ အရမ်းများရင်တော့ applications ကို overload ဖြစ်နိုင်ပါတယ်။

ပြီးတော့ **SQL Injection** စတာတွေလိုမျိုးကာကွယ်နိုင်အောင် **SQL parameters** တွေလိုမျိုးတွေကို သုံးသင့်ပါတယ်။
သူတို့တွေကို SQL က Queryက run တဲ့အခါ non executable အဖြစ် သတ်မှတ်ပြီး run တာကြောင့် SQL Injection တွေမဖြစ်စေပါဘူး။

---

# Dapper

Dapper ကတော့ ORM တစ်မျိုးပါ ဒါပေမယ့်သူက ORM အစစ်မဟုတ်သေးပါဘူး သူ့မှာ ADO.NET လိုမျိုး SQL Query တွေရေးရသေးတာမို့ သူ့ကို `Micro ORM` လို့ခေါ်ကြပါတယ်

Dapper မှာလည်း ADO.Net လိုပါပဲ Connection String ထည့်ပြီး DB နဲ့ interact လို့ရပါတယ်။
ADO dotnet နဲ့မတူတာက သူ့မှာ connection open close စတာတွေကို manually ထည့်ဖို့ မလိုတော့ပါဘူး။

> သူ့ပါသာ connection open close ကို code က handle သွားပါတယ်။

Dapper အဟောင်းမှာ using scope ထဲမှာ db interact ရတဲ့ codeကိုရေးရပေမယ့် dapperအသစ်မှာကတာ့ အဲ့လိုမလိုပါဘူး။
DB connection ကြေငြာထားတဲ့ variable ကိုမသုံးတော့ာတာနဲ့ auto close သွားပါတယ်။

Connection တွေကို အပိတ်အဖွင့်လုပ်ဖို့လိုတာက Database မှာ connection တွေက limited ဖြစ်လို့ပါ။
ဥပမာ -

    connection 100 ပဲ handle နိင်တဲ့ database ကို user 100က တစ်ပြိုင်တည်းသုံးရင် အဆင်ပြေနိင်ပေမယ့် 101 ဖြစ်သွားရင်တော့ ျနာက်လာတဲ့တစ်ယောက်က သုံးမရတော့ပါဘူး။
    အဲ့တာကြောင့် connection တွေကို ပိတ်ပြီး ပြန်ဖွင့်ဖို့လိုတာပါ။

Dapper မှာ data တွေကို select ဆွဲထုတ်ရင် dynamic datatype နဲ့ပြန်တတ်တာမလို့ အဲ့တာကို standards ဖြစ်ချင်ရင် DTO ဆိုတဲ့ Data Transfer Object တွေနဲ့သုံးသင့်ပါတယ်။ အဲ့တာမှသာ code က standards ကျပြီး error handle တာတွေဟာ ပိုကောင်းလာမှာပါ။

_Dynamic ရဲ့ကောင်းကျိုးကဘာလဲဆိုတော့ error မတက်တာပါ။ အဲ့တာကလည်းသူရဲ့ ဆိုးကျိုးပါ။
ကိုယ့်မှာ datatype မသေချာသေးချိန် သူ့သုံးသင့်ပေမယ့် သေချာသိရရင်တော့ မသူံးသင့်ပါဘူး။_

DTO မှာဆိုရင် Object ရဲ့ property က Database က column name တွေနဲ့တူရင် အကောင်းဆုံးပါပဲ

---

# EFCore

efcore ကတော့ true ORM ပါသူ့မှာ approach 2 ခုရှိပါတယ်။ `Code First` နံဲ့ `Database First` ပါ။

- Code First ကတော့ ကိုယ့်ရဲ့ code က define ထားတဲ့ code ကို efcore က database မှာသွား create ပေးမယ့်ပုံစံပါ။
- Database First ကတော့ Databaseမှာဆွဲထားတဲ့ tables တွေကို c# class တွေအဖြစ် ဆွဲယူတဲ့ပုံစံပါ။

2 ခုလုံးက သက်ဆိုင်ရာ command line နဲ့သွားပါတယ်။

အရင်ဆုံး efcore ကိုသုံးဖို့ သက်ဆိုင်ရာ package တွေကိုသွင်းဖို့လို့ပါတယ်။

> efcore package တွေက dotnet version အားလုံးနဲ့မကိုက်ပါဘူး။

ကိုယ်သူံးတဲ့ dotnet version ကိုပဲလိုက်ပီး သွင်းရမှာပါ။
ပြီးတော့ နောက်ထပ် command line အတွက် efcore tool နဲ့ ကိုယ်သုံးမယ့် database ပေါ်လိုက်ပြီး package သွင်းဖို့လိုပါတယ်။ ဥပမာ - `sqlserver` ဆိုရင် `efcore sql server` စတဲ့ package သွင်းဖို့ လိုအပ်ပါမယ်။

Database First တွေ Code First တွေမတိုင်ခင်
efcore ကို ကိုယ့်ပါသာ manually ရေးလိူ့ရပါတယ်။ အဲ့တာအတွက် efcore ရဲ့ `DbContext` ကို inheritance ယူပြီး သူရဲ့ `OnConfiguring method` ကို override လုပ်ဖို့လိူပါလိမ့်မယ်။ (_ကိုယ့်ရဲ့ connection string ကိုသုံးပြီးတော့ပေါ့။_)

နောက်တစ်ခုက အဲ့လိုလုပ်ရင် ကိုယ်သုံးတဲ့ class ကိုလည်း Database နဲ့ ကိုက်ဖို့အတွက် mapping လုပ်ဖို့လိုပါတယ်။ name တွေတူရင်တော့ မလိုပေမယ့် name တွေမတူရင်တော့ လုပ်ပေးဖို့လိုပါတယ်။

နောက် သုံးရင် သူက ORM ဖြစ်တဲ့အတွက် SQL Query တွေရေးဖို့မလိုပါဘူး။ အဲ့အစား C# ကနေပဲ တိုက်ရိုက် `Where Select` စတဲ့ keyword တွေနဲ့ လုပ်လို့ရပါတယ်။

အဲ့လို `Where` တို့ဘာတို့ထဲမှာ ရေးတာတွေက `linq ( language integrated query )` လို့သိရပါတယ်။

> အဲ့တာက C# မှာပဲ SQL integrated ထားသလို capability ကိုရအောင်လုပ်ပေးထားတာပါ။

efcore မှာ တစ်ခုခု read ရင် `AsNoTracking` ကိုထည့်သင့်ပါတယ် ဘာလို့ဆိုရင် `AsNoTracking` မထည့်ရင် commit data ရော uncommit dataရော ပါလာမှာပါ။ `AsNoTracking` မထည့်ရင် သူက `Update Insert or Delete` တစ်ခုခုကိုစောင့်နေတတ်ပါတယ်။

`AsNoTracking` ထည့်တာနဲ့ uncommit မပါတဲ့ commitပြီးသား data ကိုပဲဆွဲထုတ်ပါတယ်။

တစ်ခု drawback ကတော့ real-time dataကိုမရတာပါ။ ပြီးတော့ သူ့ထည့်တာကြောင့် data ပြန် Save တာ Delete တာတို့ဆိုရင် entry stateကိုပြန်ထည့်ပြီးမှ save ပေးရပါတယ်။

_(Dapper နဲ့. Efcore မှာ connection string တွေမှာ `TrustServerCertifcate=True` လို့ဆိုပြီး ထည့်ပေးရပါတယ်။)_

အခု C# application တွေကို build တဲ့အခါ library file တွေရော ကိုယ့် pj file တွေပါထွက်ပါတယ်။ နောကိတစ်ခု .dll ဆိုတဲ့ `dynamic linked list` ဆိုတဲ့ file တွေလည်းထွက်ပါသေးတယ်။

နောက် dotnet က သူ့ application information တွေ install လုပ်ထားတဲ့ package တွေအကြောင်းကို project ရဲ့ .`csproj` file မှာ storeလို့ရပါတယ် ။
အဲ့မှာ သွားပြီးလည်း package တွေကို ကြည့်လို့ manage လုပ်လို့ရပါတယ်။

---

# ASP.NET Web API

ASP DotNet Web Api မှာ ၂မျိုးရှိပါတယ်။

- Controller (Traditional API) နဲ့
- Without Controller (Minimal API) ပါ။

ဒီ API project မှာဆိုရင် swagger ပါတစ်ခါတည်းပါပါတယ်။ ပြီးတော့ http ရော https ရော support ပါတယ်။ကိုယ်က project ကို ဘယ် port ဘယ် port မှာ run ချင်တယ်။ စသဖြင့် configure ချင်ရင် Project အောက်က Properties ဆိုတဲ့ folder အောက်မှာ `launchSettings.json` မှာသွားပြင်လို့ရပါတယ်။

အရင်ဆုံး controller မှာဆိုရင်name ပေးတာကို controller နဲ့ဆုံးပေးရင်ပိုကောင်းပါတယ်။
ဥပမာ -
`BlogsController` ဆိုရင် route က auto `/api/Routes` ဆိုပြီးဖြစ်သွားပါတယ်။

API controller တစ်ခု create ရင်သူက ControllerBaseကို inheritance ယူထားတဲ့ class တစ်ခုကိုဖန်တီးပေးပါတယ်။ အဲ့အထဲမှာကျတော်တို့က အဲ့ class ထဲမှာပဲ efcore မှာ class တွေကို mapping မှတ်သလို

    [HttpGet]
    [HttpPost]

စသဖြင့် http method type ကိုကြေငြာပြီးရေးလို့ရပါတယ်။
သူ့တစ်ခုချင်းစီအောက်မှာ `IActionResult` ကိုပြန်မယ့် methodတွေရေးပေးရပါတယ်။

```C#
[HttpGet]
public IActionResult GetBlog() {
    return Ok();
}
```

အဲ့လိုရေးပေးရင် run တဲ့အခါ swagger မှာ GetMethod လေးတစ်ခုပေါ်နေမှာပဲဖြစ်ပါတယ်။

URL parameters တွေ body တွေကြေငြာရင်

```C#
[HttpPost("{id}"]
public IActionResult GetBlog(int id, Blog blog) {
    return Ok();
}
```

အဲ့လို method parameters နဲ့ Http method မှာနောက်က template ထည့်လိူင်ရင် APIမှာ
id က **param** ဖြစ်သွားပြီး blog က **body** ဖြစ်သွားပါတယ်။

## Service Class

Code တွေကို standards နဲ့ရှင်းရှင်းလေးဖြစ်ချင်တဲ့အခါ ထပ်ခါထပ်ခါ မရေးချင်တဲ့အခါမှာ တူညီတာတွေကိုစုပြီး ကျတော်တို့ service တစ်ခုထုတ်ပေးလို့ရပါတယ် ။

ဥပမာ Ado.Net တို့ Dapper တို့အတွက်ပါ သူတို့မှာ ထပ်ခါထပ်ခါရေးနေရတာမျိူး connection ကြေငြာတာတို့ ပိတ်တာတွေ ဖွင့်တာတွေ query ထည့်တာတွေ စတာတွေ တူတဲ့အခါမျိုးမှာ တူတာတွေကို ဘုံထုတ်ပြီး service method တွေရေးလိူ့ရပါတယ်။

နောက်တစ်ခုက `Data Model` နဲ့ `View Model` ပါ။

> ဒါကို front end ကိုပြဖို့ ဒါမှမဟုတ်ကိုယ့် data တွေမှာ protect ရမယ့်အရာတွေပါရင် ကာကွယ်ဖို့အတွက်သုံးပါတယ်။

Data Model ကတော့ Database နဲ့ အနီးဆုံး ဖြစ်တာမို့ response ပြန်ရငိ မသုံးသင့်ပါဘူး။ View Model မှာတော့ အပြင်ကိုမပြချင်တာတွေ ဥပမာ user password တို့ဘာတို့စတာတွေကို model မှာမထည့်လုပ်ထားတာမို့ response ပြန်တဲ့အခါ front-end တွေအတွက်သုံးသင့်ပါတယ်။

Method တွေ return တွေရဲ့နောက်ကွယ်မှာ T စတဲ့ `Generic Type` တွေကိုသုံးလို့ရပါတယ်။ ဘာလို့ဆိုရင် တစ်ချို့ method တွေ type တွေအားလုံးကို developer ကမသိနိုင်လို့ပါ။

# Minimal API

Minimal API က Express နဲ့ဆင်ပါတယ်။
သူက `Controller` အစား `Web​ApplicationBuilder` ဆိုတဲ့ class ကိုပဲသုံးပြီး ရေးတယ်။

သူက minimal မလို့ traditional နဲ့စာရင် ပိုပြီး performance ကောင်းတယ်။ ဒါပေမယ့် method တွေမှာ `withName` တွေထည့်ပေးဖို့လိုပြှးတော့ အဲ့တာတွေကလည်း unique ဖြစ်ဖို့ အရေးကြီးတယ်။

သူ့မှာလည်း. Param တွေ bodyတွေကို traditional နဲ့ ကြေငြာနည်းဆင်တယ်။

```C#
app.MapPut("/blogs/{id}", (int id, Blog blog) => {
	//Some Codes
})
  .WithName("CreateBlog")
  .WithOpenAPI();
```

> Now id is **param** and blog is **body**.

## Extension Method

static class တစ်ခုမှာ static method တစ်ခုရှိမယ် အဲ့ method က parameter တစ်ခုကို this နဲ့တောင်းရင် အဲ့ this ရဲ့ type ပေါ်မူတည်ပြီး အဲ့ type ကို extend လုပ်လို့ရတယ်။ အဲ့တာကြောင့် `Extension method` လို့ခေါ်တယ်။ ဥပမာ -

```C#
public static classEndPoints {
   public static int MinusOne(this int x){
       return x - 1;
   }
}
```

ဆိုရင် `9.MinusOne();` ဆိုတာနဲ့ `8` return ပြန်မှာပါ။

- `NewtonSoft` => JSON tool for C#
  > This handles converting JSON to C# object and C# object to JSON using `SeralizeObject` and `DeseralizeObject`.

Project တစ်ခုခုမှာ package တစ်ခုကို အားလုံးသုံးလိူ့ရအောင် ခဏခဏ import မယ့်အစား global using ကြေငြာလို့ရပါတယ်။

# Response Model & Result Pattern

ဒီ model ၂ခုကို backend server တွေက response ပြန်တဲ့အခါ response ကောင်းဖိုနဲ့ Project မရှုပ်ထွေးစေပဲ ရှင်းရှင်းလေးနဲ့ ရေးချင်တဲ့အခါသုံးပါတယ်။

API project တစ်ခုကို

- Database -> Efcore database or DTO types
- Domain -> project between API and Database processing and validating data
- API -> get input and return via API

အဲ့လိုမျိုး ၃ခုခွဲပြီး လွယ်လွယ်ကူကူ manage လုပ်နိုင်ပါတယ်။

အဲ့တော့ response တစ်ခုကို လွယ်လွယ်ျလးကြည်မယ်ဆိုရင် status အောင်မြင်လား မအောင်မြင်လားဆိုပြီးပဲမြင်ပြီးကြည့်ရအောင်။ သေချာတာတစ်ခုက Message တစ်ခုတော့ပါဦးမယ်။ ဘာဖြစ်သွားတယ် ဆိုတာကို သိရဖို့ နောင်တစ်ခုက အောင်မြင်ရင် data ပြန်တာတို့ဘာတိုွပေါ့။

ဒါပေမယ့် မအောင်မြင်ရင် ဘာဖြစ်တာလဲဆိုတာကျတာ်တို့ သေချာမသိနိုင်တော့ ထပ်ပြီးတော့

- Not Found
- Server Error
- Error
- Validation Error

စသဖြင့် applications လိုအပ်ချက်အလိုက်ထပိခွဲလို့ရနိုင်တယ်။

**Base Response Model** ကတော့. အဲ့ဒိ status တွေကို ဦးစားပေးပြီး

    IsSuccess
    IsError
    ValidationError
    ServerError

စသဖြင့် ကြေငြာပြီး နောက် `response model` တစ်ခုနဲ့ ထပ်အုပ်ရတယ်။ အဲ့တော့ ပြသနာက ဘာရှိသွားလဲဆိုတော့ model နဲ့ applications need အရ class အခွဲတွေ ထပ်ထပ်ပြီး ကြေငြာရတာတွေ ရှိနိုင်တယ်။

အဲ့တော့ သူ့အစား Result Pattern ကိုသုံးကြပါတယ်။ သူ့မှာကျတော့ တစ်ခါတည်း dataကို generic နဲ့ ထည့်ပီး return ပြန်တဲ့အတွက် ထပ်ပြီး wrap ဖို့မလိုတော့ပဲ သူံးရတာတွေ အတော်သက်သာသွားပါတယ်။

API ဘက်ကလည်း Success ဖြစ်လား။
Error ဖြစ်ရင် ဘာဖြစ်တာလဲ အဲ့လိုကြည့်ပြီး

`Ok` ဒါမှမဟုတ် `Not Found` လို့ ပြန်ပေးလိူ့ရသွားပါတယ်

## Async Await

Async Await ကို သုံးတဲ့အခါ C# method တွေမှာ Task ဆိုတာလေးခံသုံးပေးရပါတယ် ။ Async Await ကို ဘယ်လိုနေရာမှာသုံးလဲဆိုရင် ကိုယ့် applications က အလုပ်တစ်ခုမှာ method တွေအများကြီးခေါ်သုံးတာမျို, ဥပမာ - Database ကို read လိုက် calculations လုပ်လိုက် ပြန် saveလိုက် စတဲ့ နေရာတွေမှသုံးပါတယ်။

ဥပမာ -
ကျတော်တို့ ထမင်းစားပြီးမှ. ရုပ်ရှင်ကြည့်တာကျတော့ async await မပါတာဖြစ်ပြီး
ထမင်းစားရင်း ရုပ်ရှင်ကြည့်တာက async await နဲ့လုပ်တာပဲဖြစ်ပါတယ်။

# Http Client | Rest Client

JavaScript တို့ မှာ API နဲ့ communicate လုပ်ဖို့ fetch ဆိုတဲ့ function က native ပါသလိုပဲ။
C# မှာလည်း `HttpClient` ဆိုတဲ့ object က native ရှိပါတယ်။

သူ့ method တွေအားလုံးကို အခုနောက်ပိုင်းမှာ asynchronous လုပ်လိုက်တာပဲဖြစ်တာကြောင့် သူ့သုံးရင် method တွေခေါ်တဲ့အခါ async await ထည့်ပေးရပါတယ်။

`HttpClient` ကလည်း http method တွေဖြစ်တဲ့ `Get, Post, Put, Patch, Delete` စတာတွေအားလုံးကို support ပါတယ်။

Http method မှာ Get နဲ့ delete ကတော့ body ထည့်မရပါဘူး။
URLတွေမှာ maximum length ရှိပါတယ်။ အဲ့တာက `2048` လုံးရှိပါတယ်။ အဲ့လိုတွေကြောင့် တစ်ခါတစ်ရံ data အများကြီးတွေကို get တို့ delete တို့မှာ data တွေသယ်ချင်ရင် သူ့တို့ သုံးမယ့်အစား post တို့ patch တို့ကို သုံးရပါတယ် ။

`HttpClient` မှာ method အားလုံးက request URI (url) တစ်ခုလိုပြီး, POST တို့ PUT/PATCH တို့ကို သုံးတဲ့အခါ `HTTPCONTENT` (for body) လို့ပါတယ်။

သူက JSON တို့ XML တို့လိုမျိုး String Content တစ်ခုပါ။ အဲ့တာကြောင့် body တစ်ခုအတွက် အရင်ဆုံး C# object တစ်ခုကို JSONပြောင်းပီးမှ အဲ့ JSON ကို `StringContent` မှတ်ပေးရပါတယ်။ အဲ့တာမှသာ လိုအပ်တဲ့ `HttpContent` ကိုရမှာပါ။

    C# Object -> JSON -> StringContent -> HttpContent

`StringContent` ကဘာလို့ `HttpContent` ကိုယူလို့ရလဲဆိုရင် သူတို့ inheritance ကြောင့်ပါ။

    HttpContent -> ByteArrayContent -> StringContent

နောက်တစ်ခုကတော့ Rest client ပါ Rest client လို့ခေါ်ပေမယ့် သူ့သုံးချင်ရင် `RestSharp` ဆိုတဲ့ package လိုပါတယ်။

သူကတော့ `HttpClient` ကိုပဲ ငုံထားတဲ့ package တစ်ခုဖြစ်ပြီး `HttpClient` ထက်သုံးရပိုလွယ်အောင်လုပ်ထားပါတယ်။ သူ့မှာ `HttpClient` ထက်ပိုကောင်းတာတွေက အပေါ်ကလို body ထည့်ချင်ရင် converting ခံစရာမလိုပဲ။ C# object ကိုပဲ တန်းထည့်ပေးလို့ရတာပါ။
