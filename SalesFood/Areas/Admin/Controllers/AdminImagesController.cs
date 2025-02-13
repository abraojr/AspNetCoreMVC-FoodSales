using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesFood.Models;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminImagesController : Controller
{
    private readonly IWebHostEnvironment _hostingEnv;
    private readonly ConfigurationImages _myConfig;

    public AdminImagesController(IWebHostEnvironment hostEnv, IOptions<ConfigurationImages> myConfig)
    {
        _hostingEnv = hostEnv;
        _myConfig = myConfig.Value;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            ViewData["Error"] = "Error: File(s) not selected";
            return View(ViewData);
        }

        if (files.Count > 10)
        {
            ViewData["Error"] = "Error: Number of files has exceeded the limit";
            return View(ViewData);
        }

        long size = files.Sum(f => f.Length);
        var filePathsName = new List<string>();
        var filePath = Path.Combine(_hostingEnv.WebRootPath, _myConfig.NameImagesFolderProducts);

        foreach (var formFile in files)
        {
            if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") ||
                formFile.FileName.Contains(".png"))
            {
                var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                filePathsName.Add(fileNameWithPath);

                using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                await formFile.CopyToAsync(stream);
            }
        }

        ViewData["Result"] = $"{files.Count} files have been uploaded to the server, with a total size of : {size} bytes";

        ViewBag.Files = filePathsName;

        return View(ViewData);
    }

    public IActionResult GetImages()
    {
        FileManagerModel model = new();

        var userImagesPath = Path.Combine(_hostingEnv.WebRootPath, _myConfig.NameImagesFolderProducts);

        DirectoryInfo directory = new(userImagesPath);
        FileInfo[] files = directory.GetFiles();

        model.PathImagesProduct = _myConfig.NameImagesFolderProducts;

        if (files.Length == 0)
        {
            ViewData["Error"] = $"No files found in {userImagesPath} folder";
        }

        model.Files = files;

        return View(model);
    }

    public IActionResult DeleteFile(string fileName)
    {
        string deletedImage = Path.Combine(_hostingEnv.WebRootPath, _myConfig.NameImagesFolderProducts + "\\", fileName);

        if (System.IO.File.Exists(deletedImage))
        {
            System.IO.File.Delete(deletedImage);
            ViewData["Deleted"] = $"File(s) {deletedImage} successfully deleted";
        }

        return View("Index");
    }
}