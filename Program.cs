global using System.Collections.Generic;
global using System.Linq;
global using RMS.Data;
global using RMS.Interfaces;
global using RMS.Models;
global using RMS.Repositories;
global using Microsoft.EntityFrameworkCore;
global using CodesByPallab.Tools;
global using System.ComponentModel.DataAnnotations;

global using Microsoft.AspNetCore.Mvc;
global using System;
global using Microsoft.AspNetCore.Authorization;

global using Microsoft.Extensions.Logging;
global using System.Diagnostics;

global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.AspNetCore.Hosting;
global using System.IO;

global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Http;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IRole, RoleRepo>();

builder.Services.AddScoped<IHRDepartment, HRDepartmentRepo>();
builder.Services.AddScoped<IHRDesignation, HRDesignationRepo>();
builder.Services.AddScoped<IHREmpAtt, HREmpAttRepo>();
builder.Services.AddScoped<IHREmpDetails, HREmpDetailsRepo>();
builder.Services.AddScoped<IHREmpRoaster, HREmpRoasterRepo>();
builder.Services.AddScoped<IHRHolidays, HRHolidaysRepo>();
builder.Services.AddScoped<IHRLeaveDetails, HRLeaveDetailsRepo>();
builder.Services.AddScoped<IHRLeavePolicy, HRLeavePolicyRepo>();
builder.Services.AddScoped<IHRSalaryPolicy, HRSalaryPolicyRepo>();
builder.Services.AddScoped<IHREmpSalary, HREmpSalaryRepo>();
builder.Services.AddScoped<IHRWeekend, HRWeekendRepo>();
builder.Services.AddScoped<IHRWStatus, HRWStatusRepo>();

builder.Services.AddScoped<IStoreCategory, StoreCategoryRepo>();
builder.Services.AddScoped<IStoreDClose, StoreDCloseRepo>();
builder.Services.AddScoped<IStoreGIssueMaster, StoreGIssueMasterRepo>();
builder.Services.AddScoped<IStoreGoodsStock, StoreGoodsStockRepo>();
builder.Services.AddScoped<IStoreGReceive, StoreGReceiveRepo>();
builder.Services.AddScoped<IStoreIGen, StoreIGenRepo>();
builder.Services.AddScoped<IStoreSCategory, StoreSCategoryRepo>();
builder.Services.AddScoped<IStoreUnit, StoreUnitRepo>();



//services.AddScoped<IProductAttribute, ProductAttributeRepo>();

builder.Services.AddDbContext<RmsDbContext>(options =>  options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:dbconn").Value));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
}).AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<RmsDbContext>();


var app = builder.Build();



if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();


